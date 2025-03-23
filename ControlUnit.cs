public static class ControlUnit
{
    private static Dictionary<string, string> registers4 = new Dictionary<string, string>()
    {
        {"Rd", "0"}, // destination register pointer
        {"Rn", "0"}, // operand 1 register pointer
        {"Ra", "0"}, // ??
        {"Rs", "0"}, // operand 2 register pointer
        {"Imm", "0"},
    };

    private static string currentInstruction = "";
    private static string currentInstructionType = "";

    public static void fetch()
    {
        // signal memory access unit (MAU) -> update PC
        RegisterFile.registers["MAR"] =  RegisterFile.registers["PC"]; // update MAR
        RegisterFile.registers["PC"] =  Convert.ToString(Convert.ToInt32(RegisterFile.registers["PC"], 2) + Convert.ToInt32(GlobalConstants.instructionSize, 2), 2); // increment PC
        
        // receive instruction from MAU, place it in IR
        RegisterFile.registers["MDR"] = MemoryAccessUnit.fetchInstruction(InstructionMappings.RegisterToBinaryCode["MAR"]);
        RegisterFile.registers["IR"] = RegisterFile.registers["MDR"];

        Console.WriteLine("Executing: " + RegisterFile.registers["IR"]);
    }

    public static void decode()
    {
        // decode instruction into opcode, operands
        string opcode = RegisterFile.registers["MDR"].Substring(0, 5); // translate first 5 bits to instruction name

        /*
        if(opcode is invalid){
            place interrupt code in Rn
            change currentInstruction and currentInstructionType to invalid instruction interupt
            'return', since 'execute' will make sure next instruction is fetched from interrupt vector table
        }
        */

        try {
            currentInstruction = InstructionMappings.BinaryCodeToInstruction[opcode];
        } catch (Exception e){
            currentInstruction = "InvalidInstruction";
            currentInstructionType = "Interrupt";
            RegisterFile.registers["R5"] = "0";
            registers4["Rn"] = InstructionMappings.RegisterToBinaryCode["R5"];
            Console.WriteLine("INVALID INSTRUCTION INTERRUPT");
            return;
        }

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
            case "Branching":
                int[] segmentSizes3 = {5,5, 22}; // opcode, Rd
                List<string> segmentedBinary3 = StringSplitter.SplitStringByLengths(RegisterFile.registers["IR"], segmentSizes3);
                registers4["Rd"] = segmentedBinary3[1];
            break;
            case "Moving":
                switch (currentInstruction)
                {
                    case "Movi":
                        int[] segmentSizes4 = {5,5,16, 6}; // opcode, Rd, imm16
                        List<string> segmentedBinary4 = StringSplitter.SplitStringByLengths(RegisterFile.registers["IR"], segmentSizes4);
                        registers4["Rd"] = segmentedBinary4[1];
                        registers4["Imm"] = segmentedBinary4[2];
                    break;
                    case "Movr":
                        int[] segmentSizes5 = {5,5,5, 17}; // opcode, Rd, Rn
                        List<string> segmentedBinary5 = StringSplitter.SplitStringByLengths(RegisterFile.registers["IR"], segmentSizes5);
                        registers4["Rd"] = segmentedBinary5[1];
                        registers4["Rn"] = segmentedBinary5[2];
                    break;
                }
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
            case "Interrupt":
                // Rn will hold the index of the interrupt vector in the table.
                RegisterFile.registers["PC"] = Convert.ToString(Convert.ToInt32(RegisterFile.registers["IVTP"],2) * 8 + Convert.ToInt32(GlobalConstants.instructionSize, 2) * Convert.ToInt32(RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[registers4["Rn"]]],2),2);
            break;
            case "Branching":
                switch (currentInstruction)
                {
                    case "Jmp":
                        RegisterFile.registers["PC"] = RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[registers4["Rd"]]];
                    break;
                    case "Jmpe":
                        if(RegisterFile.registers["FLG"][0] == 1)
                        {
                            RegisterFile.registers["PC"] = RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[registers4["Rd"]]];
                        }
                    break;
                    case "Jmpg":
                        if(RegisterFile.registers["FLG"][1] == 1)
                        {
                            RegisterFile.registers["PC"] = RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[registers4["Rd"]]];
                        }
                    break;
                }
            break;
            case "Moving":
                switch(currentInstruction)
                {
                    case "Movi":
                        
                        RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[registers4["Rd"]]] = registers4["Imm"];
                    break;
                    case "Movr":
                        RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[registers4["Rd"]]] = RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[registers4["Rn"]]]; 
                    break;
                }
            break;
        }
    }

    public static void memoryAccessAndReadBack()
    {
        if(currentInstructionType == "DataProcessing")
        {
            switch(currentInstruction)
            {
                //Rn - memory address
                case "Ldr":
                    RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[registers4["Rd"]]] = MemoryAccessUnit.execute(registers4["Rd"], registers4["Rn"]);
                break;
                //Rd - data
                //Rn - memory address
                case "Str":
                    MemoryAccessUnit.execute(registers4["Rd"], registers4["Rn"]);
                break;
            }
            
        }
    }
}