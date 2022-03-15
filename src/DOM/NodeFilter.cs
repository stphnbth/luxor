
namespace Luxor.DOM
{
    public static class NodeFilter
    {
        private const ushort FILTER_ACCEPT = 1;
        private const ushort FILTER_REJECT = 2;
        private const ushort FILTER_SKIP = 3;

        private const ulong SHOW_ALL = 0xFFFFFFFF;
        private const ulong SHOW_ELEMENT = 0x1;
        private const ulong SHOW_ATTRIBUTE = 0x2;
        private const ulong SHOW_TEXT = 0x4;
        private const ulong SHOW_CDATA_SECTION = 0x8;
        private const ulong SHOW_ENTITY_REFERENCE = 0x10;
        private const ulong SHOW_ENTITY = 0x20;
        private const ulong SHOW_PROCESSING_INSTRUCTION = 0x40;
        private const ulong SHOW_COMMNET = 0x80;
        private const ulong SHOW_DOCUMENT = 0x100;
        private const ulong SHOW_DOCUMENT_TYPE = 0x200;
        private const ulong SHOW_DOCUMENT_FRAGMENT = 0x400;
        private const ulong SHOW_NOTATION = 0x800;

        public static ushort AcceptNode(Node node) 
        {
            throw new NotImplementedException();
        }

    }
}