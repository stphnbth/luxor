using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Structures
{
    public sealed partial class Buffer
    {
        internal Int32[] m_Chunk;
        internal Buffer? m_Previous;

        internal int m_Length;
        internal int m_Offset;
        internal int m_MaxCapacity;

        internal const int DefaultCapacity = 16;
        internal const int MaxChunkSize = 8000;

        // public StringBuilder() {}

        // public StringBuilder(Int32 value) {}

        // public StringBuilder(Int32[] values) {}

        /* Getters and Setters */

        public int Capacity
        {
            get => m_Chunk.Length + m_Offset;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                if (value > MaxCapacity)
                    throw new ArgumentOutOfRangeException(nameof(value));
                if (value < Length)
                    throw new ArgumentOutOfRangeException(nameof(value));

                if (Capacity != value)
                {
                    int newLen = value - m_Offset;
                    Int32[] newArr = GC.AllocateUninitializedArray<Int32>(newLen);
                    Array.Copy(m_Chunk, newArr, m_Length);
                    m_Chunk = newArr;
                }
            }
        }

        public int Length
        {
            get => m_Offset + m_Length;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                if (value > MaxCapacity)
                    throw new ArgumentOutOfRangeException(nameof(value));
                
                if (value == 0 && m_Previous == null)
                {
                    m_Length = 0;
                    m_Offset = 0;
                    return;
                }

                int delta = value - Length;
                if (delta > 0)
                    Append(0x0000, delta);
                else
                {
                    Buffer chunk = FindChunkForIndex(value);
                    if (chunk != this)
                    {
                        int capacityToPreserve = Math.Min(Capacity, Math.Max(Length * 6 / 5, m_Chunk.Length));
                        int newLen = capacityToPreserve - chunk.m_Offset;

                        if (newLen > chunk.m_Chunk.Length)
                        {
                            Int32[] newArr = GC.AllocateUninitializedArray<Int32>(newLen);
                            Array.Copy(chunk.m_Chunk, newArr, chunk.m_Length);
                            m_Chunk = newArr;
                        }
                        else
                        {
                            Debug.Assert(newLen == chunk.m_Chunk.Length, "The new chunk should be larger or equal to the one it is replacing.");
                            m_Chunk = chunk.m_Chunk;
                        }

                        m_Previous = chunk.m_Previous;
                        m_Offset = chunk.m_Offset;
                    }

                    m_Length = value - chunk.m_Offset;
                    AssertInvariants();
                }

                Debug.Assert(Length == value, "Something went wrong setting Length");
            }
        }

        public int MaxCapacity => m_MaxCapacity;

        /* Public Methods */

        public Buffer Append(Int32 value)
        {
            int nextIndex = m_Length;
            Int32[] currentChunk = m_Chunk;

            if (currentChunk.Length > nextIndex)
            {
                currentChunk[nextIndex] = value;
                m_Length++;
            }
            else
                Append(value, 1);

            return this;
        }

        public Buffer Append(Int32 value, int repeatCount)
        {
            if (repeatCount < 0)
                throw new ArgumentOutOfRangeException(nameof(repeatCount));
            
            if (repeatCount == 0)
                return this;

            // ENSURE repeatCount + Length LT m_MaxCapacity
            int newLen = Length + repeatCount;
            if (newLen > m_MaxCapacity || newLen < repeatCount)
                throw new ArgumentOutOfRangeException(nameof(repeatCount));

            int index = m_Length;
            while (repeatCount > 0)
            {
                if (index < m_Chunk.Length)
                {
                    m_Chunk[index++] = value;
                    --repeatCount;
                }
                else
                {
                    m_Length = index;
                    ExpandByAChunk(repeatCount);
                    Debug.Assert(m_Length == 0);
                    index = 0;
                }
            }

            m_Length = index;
            AssertInvariants();

            return this;
        }

        /*
        public Buffer AppendJoin(params Int32?[] values)
        {
        
        }
        */

        public Buffer Clear()
        {
            this.Length = 0;
            return this;
        }

        public Buffer Insert(int index, Int32 value)
        {
            if (index > Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            Insert(index, ref value, 1);
            return this;
        }

        public Buffer Insert(int index, Int32[] value)
        {
            if (index > Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (value != null)
                Insert(index, ref MemoryMarshal.GetArrayDataReference(value), value.Length);

            return this;
        }

        public Buffer Insert(int index, ref Int32 value, int count) => Insert(index, MemoryMarshal.CreateReadOnlySpan<Int32>(ref value, count), count);

        private Buffer Insert(int index, ReadOnlySpan<Int32> value, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (index > Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (value.IsEmpty || count == 0)
                return this;

            long toInsert = (long) value.Length * count;
            if (toInsert > MaxCapacity - this.Length)
                throw new OutOfMemoryException();

            Debug.Assert(toInsert + this.Length < int.MaxValue);

            MakeRoom(index, (int) toInsert, out Buffer chunk, out int indexInChunk, false);

            while (count > 0)
            {
                ReplaceInPlaceAtChunk(ref chunk!, ref indexInChunk, ref MemoryMarshal.GetReference(value), value.Length);
                --count;
            }

            return this;
        }


        public Buffer Remove(int start, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));
            if (length > Length - start)
                throw new ArgumentOutOfRangeException(nameof(length));

            if (Length == length && start == 0)
            {
                Length = 0;
                return this;
            }

            if (length > 0)
                Remove(start, length, out _, out _);

            return this;

        }

        /*
        public string ToString(int startIndex, int length)
        {

        }
        */

        /* Helper Functions */
        private Buffer(Buffer pb)
        {
            m_Length = pb.m_Length;
            m_Offset = pb.m_Offset;
            m_Chunk = pb.m_Chunk;
            m_Previous = pb.m_Previous;
            m_MaxCapacity = pb.m_MaxCapacity;

            AssertInvariants();
        }

        private Buffer(int size, int max, Buffer? previous)
        {   
            Debug.Assert(size > 0);
            Debug.Assert(max > 0);

            m_Chunk = GC.AllocateUninitializedArray<Int32>(size);
            m_MaxCapacity = max;
            m_Previous = previous;

            if (previous != null)
                m_Offset = previous.m_Offset + previous.m_Length;

            AssertInvariants();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void AssertInvariants()
        {
            Debug.Assert(m_Offset + m_Chunk.Length >= m_Offset, "The length of the string is greater than int.MaxValue");

            Buffer currentChunk = this;
            int maxCapacity = this.m_MaxCapacity;

            while (true)
            {
                Debug.Assert(currentChunk.m_MaxCapacity == maxCapacity);
                Debug.Assert(currentChunk.m_Chunk != null);

                Debug.Assert(currentChunk.m_Length <= currentChunk.m_Chunk.Length);
                Debug.Assert(currentChunk.m_Length >= 0);
                Debug.Assert(currentChunk.m_Offset >= 0);

                Buffer? prevChunk = currentChunk.m_Previous;
                if (prevChunk == null)
                {
                    Debug.Assert(currentChunk.m_Offset == 0);
                    break;
                }

                Debug.Assert(currentChunk.m_Offset == prevChunk.m_Offset + prevChunk.m_Length);
                currentChunk = prevChunk;
            }
        }

        private void ExpandByAChunk(int minChunkCharCount)
        {
            Debug.Assert(Capacity == Length, nameof(ExpandByAChunk) + " should only be called when there is no space left");
            Debug.Assert(minChunkCharCount > 0);

            AssertInvariants();

            if ((minChunkCharCount + Length) > m_MaxCapacity || minChunkCharCount + Length < minChunkCharCount)
                throw new ArgumentOutOfRangeException("required length");

            int newChunkLength = Math.Max(minChunkCharCount, Math.Min(Length, MaxChunkSize));

            if (m_Offset + m_Length + newChunkLength < newChunkLength)
                throw new OutOfMemoryException();

            Int32[] chunk = GC.AllocateUninitializedArray<Int32>(newChunkLength);

            m_Previous = new Buffer(this);
            m_Offset += m_Length;
            m_Length = 0;

            m_Chunk = chunk;

            AssertInvariants();
        }

        private Buffer FindChunkForIndex(int index)
        {
            Debug.Assert(0 <= index && index <= Length);

            Buffer result = this;
            while (result.m_Offset > index)
            {
                Debug.Assert(result.m_Previous != null);
                result = result.m_Previous;
            }

            Debug.Assert(result != null);
            return result;
        }

        private void MakeRoom(int index, int count, out Buffer chunk, out int indexInChunk, bool doNotMove)
        {
            AssertInvariants();
            Debug.Assert(count > 0);
            Debug.Assert(index >= 0);

            if (count + Length > m_MaxCapacity || count + Length < count)
                throw new ArgumentOutOfRangeException("required length");

            chunk = this;
            while (chunk.m_Offset > index)
            {
                chunk.m_Offset += count;
                Debug.Assert(chunk.m_Previous != null);
                chunk = chunk.m_Previous;
            }

            indexInChunk = index - chunk.m_Offset;

            if (!doNotMove && chunk.m_Length <= DefaultCapacity * 2 && chunk.m_Chunk.Length - chunk.m_Length >= count)
            {
                for (int i = chunk.m_Length; i > indexInChunk;)
                {
                    i--;
                    chunk.m_Chunk[i + count] = chunk.m_Chunk[i];
                }

                chunk.m_Length += count;
                return;
            }

            Buffer newChunk = new Buffer(Math.Max(count, DefaultCapacity), chunk.m_MaxCapacity, chunk.m_Previous);
            newChunk.m_Length = count;

            int copyCount1 = Math.Min(count, indexInChunk);
            if (copyCount1 > 0)
            {
                new ReadOnlySpan<Int32>(chunk.m_Chunk, 0, copyCount1).CopyTo(newChunk.m_Chunk);

                int copyCount2 = indexInChunk - copyCount1;
                if (copyCount2 >= 0)
                {
                    new ReadOnlySpan<Int32>(chunk.m_Chunk, copyCount1, copyCount2).CopyTo(chunk.m_Chunk);
                    indexInChunk = copyCount2;
                }
            }

            chunk.m_Previous = newChunk;
            chunk.m_Offset += count;
            if (copyCount1 < count)
            {
                chunk = newChunk;
                indexInChunk = copyCount1;
            }

            AssertInvariants();
        }

        private Buffer? Next(Buffer chunk) => chunk == this ? null : FindChunkForIndex(chunk.m_Offset + chunk.m_Length);

        private void Remove(int start, int count, out Buffer chunk, out int indexInChunk)
        {
            AssertInvariants();
            Debug.Assert(start >= 0 && start < Length);

            int end = start + count;
            int endInChunk = 0;

            chunk = this;
            Buffer? endChunk = null;
            while (true)
            {
                if (end - chunk.m_Offset >= 0)
                {
                    if (endChunk == null)
                    {
                        endChunk = chunk;
                        endInChunk = end - endChunk.m_Offset;
                    }

                    if (start - chunk.m_Offset >= 0)
                    {
                        indexInChunk = start - chunk.m_Offset;
                        break;
                    }
                }
                else
                    chunk.m_Offset -= count;

                Debug.Assert(chunk.m_Previous != null);
                chunk = chunk.m_Previous;
            }

            Debug.Assert(chunk != null, "We fell off the beginning of the string!");

            int copyTarget = indexInChunk;
            int copyCount = endChunk.m_Length - endInChunk;
            if (endChunk != chunk)
            {
                copyTarget = 0;
                chunk.m_Length = indexInChunk;

                endChunk.m_Previous = chunk;
                endChunk.m_Offset = chunk.m_Offset + chunk.m_Length;

                if (indexInChunk == 0)
                {
                    endChunk.m_Previous = chunk.m_Previous;
                    chunk = endChunk;
                }
            }

            endChunk.m_Length -= (endInChunk - copyTarget);
            if (copyTarget != endInChunk)
            {
                var slice = endChunk.m_Chunk.Skip(endInChunk).Take(copyCount).ToArray();                  
                MemoryMarshal.CreateReadOnlySpan(ref slice[0], copyCount).CopyTo(endChunk.m_Chunk.AsSpan(copyTarget));
            }

            Debug.Assert(chunk != null, "We fell off the beginning of the string!");
            AssertInvariants();
                

        }

        private void ReplaceInPlaceAtChunk(ref Buffer? chunk, ref int indexInChunk, ref Int32 value, int count)
        {
            if (count != 0)
            {
                while (true)
                {
                    Debug.Assert(chunk != null, "chunk should not be null at this point");
                    int lengthInChunk = chunk.m_Length - indexInChunk;
                    Debug.Assert(lengthInChunk >= 0, "Index isn't in the chunk.");

                    int lengthToCopy = Math.Min(lengthInChunk, count);
                    MemoryMarshal.CreateReadOnlySpan(ref value, lengthToCopy).CopyTo(chunk.m_Chunk.AsSpan(indexInChunk));

                    indexInChunk += lengthToCopy;
                    if (indexInChunk >= chunk.m_Length)
                    {
                        chunk = Next(chunk);
                        indexInChunk = 0;
                    }
                    
                    count -= lengthToCopy;
                    if (count == 0)
                        break;

                    value = ref Unsafe.Add(ref value, lengthToCopy);
                }
            }
        }

    }
}