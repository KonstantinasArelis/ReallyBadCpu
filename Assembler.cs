static class Assembler
{
    public static string assemble(string instructionsString)
    {
        string binaryOutput = "";
        IList<string> instructionWords = instructionsString.Split();

        string opcode = "defaultValue";
        string Rd = "defaultValue";
        string Rn = "defaultValue";
        string Ra = "defaultValue";
        string Rs = "defaultValue";

        string nextExpectedThing = "instructionName";
        string currentInstructionType = "start";

        foreach(string instructionWord in instructionWords)
        {
            if(currentInstructionType == "start"){
                currentInstructionType = InstructionMappings.InstructionNameToInstructionType[instructionWord];
            }
            switch(currentInstructionType)
            {
                case "Arithmetic":
                    switch(nextExpectedThing)
                    {
                        case "instructionName":
                        opcode = InstructionMappings.InstructionToBinaryCode[instructionWord];
                        nextExpectedThing = "Rd";
                        continue;

                        case "Rd":
                        Rd = InstructionMappings.RegisterToBinaryCode[instructionWord];
                        nextExpectedThing = "Rn";
                        continue;

                        case "Rn":
                        Rn = InstructionMappings.RegisterToBinaryCode[instructionWord];
                        nextExpectedThing = "Ra";
                        continue;

                        case "Ra":
                        Ra = InstructionMappings.RegisterToBinaryCode[instructionWord];
                        nextExpectedThing = "Rs";
                        continue;

                        case "Rs":
                        nextExpectedThing = "instructionName";
                        Rs = InstructionMappings.RegisterToBinaryCode[instructionWord];
                        binaryOutput += opcode + Rd + Rn + Ra + Rs + "0000000000"; // padding to bloat to 32 bits
                        currentInstructionType = "start";
                        continue;
                    }
                break;
                case "Shutdown":
                    switch(instructionWord)
                    {
                        case "Halt":
                            binaryOutput += InstructionMappings.InstructionToBinaryCode[instructionWord] + "0000000000000000000000000000";
                        break;
                    }
                break;
            }
        }
        
        return binaryOutput;
    }
}