public static class ControlUnit
{
    private static Dictionary<string, string> registers4 = new Dictionary<string, string>()
    {
        {"Rd", "0"}, // destination register pointer
        {"Rn", "0"}, // operand 1 register pointer
        {"Ra", "0"}, // ??
        {"Rs", "0"}, // operand 2 register pointer
    };

    private static string currentInstruction = "";
    private static string currentInstructionType = "";

    public static void fetch()
    {
        // signal memory access unit (MAU) -> update PC
        RegisterFile.registers["MAR"] =  RegisterFile.registers["PC"]; // update MAR
        RegisterFile.registers["PC"] =  Convert.ToString(Convert.ToInt32(RegisterFile.registers["PC"], 2) + GlobalConstants.instructionSize, 2); // increment PC

        // receive instruction from MAU, place it in IR
        RegisterFile.registers["MDR"] = MemoryAccessUnit.fetchInstruction(InstructionMappings.RegisterToBinaryCode["MAR"]);
        RegisterFile.registers["IR"] = RegisterFile.registers["MDR"];

        Console.WriteLine("Executing: " + RegisterFile.registers["IR"]);
    }

    public static void decode()
    {
        // decode instruction into opcode, operands
        string opcode = RegisterFile.registers["MDR"].Substring(0, 5); // translate first 5 bits to instruction name
        currentInstruction = InstructionMappings.BinaryCodeToInstruction[opcode];
        currentInstructionType = InstructionMappings.InstructionNameToInstructionType[currentInstruction];
        switch(currentInstructionType)
        {
            case "Arithmetic":
                int[] segmentSizes = {5,5,5,5,5, 7};  // opcode, rd, rn, ra, rs, unused
                List<string> segmentedBinary = StringSplitter.SplitStringByLengths(RegisterFile.registers["IR"], segmentSizes);
                
                registers4["Rd"] = segmentedBinary[1];
                registers4["Rn"] = segmentedBinary[2];
                registers4["Ra"] = segmentedBinary[3];
                registers4["Rs"] = segmentedBinary[4];

                ArithmeticLogicUnit.prepare(opcode);
            break;
            case "DataProcessing":
                // if direct values or offsets are to be used, this needs to be expanded
                int[] segmentSizes2 = {5,5,5, 17};  // opcode, rd, rn, unused (Ignoring Cond field for now)
                List<string> segmentedBinary2 = StringSplitter.SplitStringByLengths(RegisterFile.registers["IR"], segmentSizes2);
                registers4["Rd"] = segmentedBinary2[1];
                registers4["Rn"] = segmentedBinary2[2];

                MemoryAccessUnit.prepare(opcode);
            break;
        }
    }

    public static void execute()
    {
        switch(currentInstructionType)
        {
            case "Arithmetic":
                RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[registers4["Rd"]]] = ArithmeticLogicUnit.execute(registers4["Rn"], registers4["Ra"], registers4["Rs"]);
            break;
            case "Shutdown":
                Cpu.shutdown();
                return;
        }
    }

    public static void memoryAccessAndReadBack()
    {
        if(currentInstructionType == "DataProcessing")
        {
            RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[registers4["Rd"]]] = MemoryAccessUnit.execute(registers4["Rn"]);
        }
    }
}