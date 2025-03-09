public static class InstructionMappings
{
    public static readonly Dictionary<string, string> InstructionToBinaryCode = new Dictionary<string, string>()
    {
        {"Add", "00000"},
        {"Sub", "00001"},
        {"Mul", "00010"},
        {"Div", "00011"}
    };

    public static readonly Dictionary<string, string> RegisterToBinaryCode = new Dictionary<string, string>()
    {
        {"R0", "0000"},
        {"R1", "0001"},
        {"R2", "0010"},
        {"R3", "0011"},
        {"R4", "0100"},
        {"R5", "0101"},
        {"R6", "0110"},
        {"R7", "0111"}
    };

    public static readonly Dictionary<string, string> BinaryCodeToInstruction; // Declare reverse dictionary
    public static readonly Dictionary<string, string> BinaryCodeToRegister;    // Declare reverse dictionary

    static InstructionMappings()
    {
        // Initialize reverse instruction dictionary by reversing key-value pairs
        BinaryCodeToInstruction = InstructionToBinaryCode.ToDictionary(pair => pair.Value, pair => pair.Key);

        // Initialize reverse register dictionary by reversing key-value pairs
        BinaryCodeToRegister = RegisterToBinaryCode.ToDictionary(pair => pair.Value, pair => pair.Key);
    }
}