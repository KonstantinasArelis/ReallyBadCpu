public class StringSplitter
{
    public static List<string> SplitStringByLengths(string inputString, int[] segmentLengths)
    {
        if (inputString == null)
        {
            throw new ArgumentNullException(nameof(inputString), "Input string cannot be null.");
        }
        if (segmentLengths == null || segmentLengths.Length == 0)
        {
            throw new ArgumentException("Segment lengths array cannot be null or empty.", nameof(segmentLengths));
        }

        List<string> substrings = new List<string>();
        int currentPosition = 0;

        foreach (int length in segmentLengths)
        {
            if (currentPosition >= inputString.Length)
            {
                // No more characters left in the input string
                break; // Or you could handle this differently, e.g., throw an exception, add an empty string, etc.
            }

            int segmentLength = length;
            if (currentPosition + segmentLength > inputString.Length)
            {
                // If the requested length goes beyond the end of the string,
                // take only the remaining characters.
                segmentLength = inputString.Length - currentPosition;
            }

            string substring = inputString.Substring(currentPosition, segmentLength);
            substrings.Add(substring);
            currentPosition += segmentLength;
        }

        return substrings;
    }

    public static void Main(string[] args)
    {
        string originalString = "ThisIsMyStringToSplit";
        int[] lengths = { 4, 5, 6 }; // First 4 chars, then next 5, then next 6

        List<string> splitStrings = SplitStringByLengths(originalString, lengths);

        Console.WriteLine($"Original String: \"{originalString}\"");
        Console.WriteLine("Split Strings:");
        for (int i = 0; i < splitStrings.Count; i++)
        {
            Console.WriteLine($"String {i + 1}: \"{splitStrings[i]}\"");
        }
    }
}