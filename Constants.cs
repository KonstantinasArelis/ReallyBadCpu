public static class GlobalConstants {
    public static string instructionSize = Convert.ToString(32, 2); // bits

    public const int WORD_SIZE_BYTES = 4;
    public const int BYTE_SIZE_BITS = 8;
    public const int WORD_SIZE_BITS = WORD_SIZE_BYTES * BYTE_SIZE_BITS;
}