namespace Unicode
{
    public static class UnicodeInt32
    {
        public static string Text(this Int32 val)
        {
            var bytes = BitConverter.GetBytes(val);
            var encoding = System.Text.Encoding.Unicode.GetChars(bytes, 0, 1);
            return new String(encoding);
        }
    }
}