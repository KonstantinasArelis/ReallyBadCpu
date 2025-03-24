public static class Utility {
    public static string PadLeftToLength(this string input, int targetLength)
    {
        if (input == null)
        {
            return new string('0', targetLength);
        }

        if (input.Length >= targetLength)
        {
            return input; // No padding needed or already longer
        }

        int paddingNeeded = targetLength - input.Length;
        return new string('0', paddingNeeded) + input;
    }
}