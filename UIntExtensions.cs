namespace reallyBadCpu;

public static class UIntExtensions {
    //Gets any bits of a uint
    //Gets 'count' bits, starting from the bit at 'start', indexed from zero
    //Returns them as the least significant bits of a uint
    public static uint getBits(this uint number, int start, int count) =>
        number << (GlobalConstants.WORD_SIZE_BITS - (start + count)) >> (GlobalConstants.WORD_SIZE_BITS - count);

    //Returns whether the bit at 'index', indexed from zero, is set or not
    public static bool getBit(this uint number, int index) =>
        ((number >> index) & 1) == 1;
}