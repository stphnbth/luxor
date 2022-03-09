using System.Reflection;
using System.Text;

namespace Extensions
{
    public static class Extensions
    {
        public static char[] ExposeCharBuffer(this StreamReader sr, int size)
        {
            Type stream = sr.GetType();

            FieldInfo? buffer = stream.GetField("_charBuffer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo? index = stream.GetField("_charPos", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (buffer is not null && index is not null)
            {
                char[] charBuffer = (char[]) buffer.GetValue(sr)!;   
                int low = (int) index.GetValue(sr)! - 1;

                return charBuffer.Slice(low, low + size);
            }

            return new char[0] {};
        }

        // slices char array (include, exclude)
        public static char[] Slice(this char[] buffer, int low, int high)
        {
            int length = high - low;

            if (length < 0)
                return new char[0] {};
            
            char[] result = new char[length];

            for (int i = 0; i < length ; i++)
                result[i] =  buffer[i + low];

            return result;
        }

        
    }
}