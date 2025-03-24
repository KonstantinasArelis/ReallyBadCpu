using System.Globalization;
using reallyBadCpu;

public static class RegisterFile {
    public static Dictionary<string, string> registers = new Dictionary<string, string>() {
        { "R0", "0" },
        { "R1", "0" },
        { "R2", "0" },
        { "R3", "0" },
        { "R4", "0" },
        { "R5", "0" },
        { "R6", "0" },
        { "R7", "0" },
        { "PC", "0" },         // Program Counter Register
        { "MAR", "0" },        // Memory Address Register
        { "MDR", "0" },        // Memory Data Register
        { "IR", "0" },         // Instruction Register 
        { "IVTP", "1010000" }, // Interrupt vector table pointer
        { "FLG", "000" },      // flags (Equal, Greater, Overflow)
        { "PP", "0" },         // Page pointer, points to virtual memory page table
    };

    public static void PrintAllRegisters() {
        Console.WriteLine("Register Values:");
        foreach (KeyValuePair<string, string> register in registers) {
            Console.WriteLine($"{register.Key}: {register.Value}");
        }
    }

    //Accessors for more convenient usage
    //TODO figure out which bit of the Flags register is actually Virtual Mode Flag and set it up correctly
    //bit 3 when getting virtual mode flag is temporary
    public static bool inVirtualMode => uint.Parse(registers["FLG"], NumberStyles.BinaryNumber).getBit(3);
    public static uint pagePointer => uint.Parse(registers["PP"], NumberStyles.BinaryNumber);
}