using System.Text;

public static class MemoryAccessUnit
{
    private static string memory = new string('0', 1024);
    private static string instruction = "";
    
    public static void prepare(string opcode)
    {
        instruction = InstructionMappings.BinaryCodeToInstruction[opcode];
    }

    public static string execute(string Rn)
    {
        switch (instruction)
        {
            case "Ldr":
                return memory.Substring(Convert.ToInt32(RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rn]], 2), Convert.ToInt32(GlobalConstants.instructionSize, 2));
            case "Str":
                // TODO
                return "";
            
        }
        throw new Exception("MAU tried to perform non MAU operation");
    }

    public static string fetchInstruction(string Rn)
    {
        return memory.Substring(Convert.ToInt32(RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rn]], 2), Convert.ToInt32(GlobalConstants.instructionSize, 2));
    }

    public static void setMemory(string _memory, int startingBitIndex)
    {
        memory = ReplaceStringPart(memory, _memory, startingBitIndex);
    }

    public static string ReplaceStringPart(string originalString, string replacementString, int startIndex)
    {
        if (startIndex < 0 || startIndex > originalString.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex), "Start index is out of range.");
        }

        StringBuilder sb = new StringBuilder(originalString);

        for (int i = 0; i < replacementString.Length; i++)
        {
            if (startIndex + i < sb.Length)
            {
                sb[startIndex + i] = replacementString[i];
            }
            else
            {
                // If the replacement extends beyond the original length, we stop.
                break;
            }
        }

        return sb.ToString();
    }
}