public static class MemoryAccessUnit
{
    private static string memory = "";
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
                return memory.Substring(Convert.ToInt32(RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rn]], 2), GlobalConstants.instructionSize);
            case "Str":
                // TODO
                return "";
            
        }
        throw new Exception("MAU tried to perform non MAU operation");
    }

    public static string fetchInstruction(string Rn)
    {
        return memory.Substring(Convert.ToInt32(RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rn]], 2), GlobalConstants.instructionSize);
    }

    public static void setMemory(string _memory)
    {
        memory = _memory;
    }
}