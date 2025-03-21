public static class InstructionMappings
{
    public static readonly Dictionary<string, string> InstructionToBinaryCode = new Dictionary<string, string>()
    {
        {"Add", "00000"},
        {"Sub", "00001"},
        {"Mul", "00010"},
        {"Div", "00011"},
        {"Ldr", "00100"},
        {"Halt", "11111"}
    };

    public static readonly Dictionary<string, string> RegisterToBinaryCode = new Dictionary<string, string>()
    {
        {"R0",  "00000"},
        {"R1",  "00001"},
        {"R2",  "00010"},
        {"R3",  "00011"},
        {"R4",  "00100"},
        {"R5",  "00101"},
        {"R6",  "00110"},
        {"R7",  "00111"},
        {"PC",  "01000"}, // Program Counter Register
        {"MAR", "01001"}, // Memory Address Register
        {"MDR", "01010"}, // Memory Data Register
        {"IR",  "01011"}, // Instruction Register
    };

    public static readonly Dictionary<string, string> InstructionNameToInstructionType = new Dictionary<string, string>()
    {
        {"Add", "Arithmetic"},
        {"Sub", "Arithmetic"},
        {"Div", "Arithmetic"},
        {"Mul", "Arithmetic"},
        {"Halt", "Shutdown"},
        {"Ldr", "DataProcessing"},
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