static class Assembler
{
    public static string assemble(String instructionsString)
    {
        IList<string> instructionWords = instructionsString.Split();

        string opcode = "defaultValue";
        string Rd = "defaultValue";
        string Rn = "defaultValue";
        string Ra = "defaultValue";
        string Rs = "defaultValue";

        string nextExpectedThing = "instructionName";

        foreach(string instructionWord in instructionWords)
        {
            if(nextExpectedThing == "instructionName"){
                opcode = InstructionMappings.InstructionToBinaryCode[instructionWord];
                nextExpectedThing = "Rd";
                continue;
            }

            if(nextExpectedThing == "Rd")
            {
                Rd = InstructionMappings.RegisterToBinaryCode[instructionWord];
                nextExpectedThing = "Rn";
                continue;
            }

            if(nextExpectedThing == "Rn")
            {
                Rn = InstructionMappings.RegisterToBinaryCode[instructionWord];
                nextExpectedThing = "Ra";
                continue;
            }

            if(nextExpectedThing == "Ra")
            {
                Ra = InstructionMappings.RegisterToBinaryCode[instructionWord];
                nextExpectedThing = "Rs";
                continue;
            }

            if(nextExpectedThing == "Rs")
            {
                nextExpectedThing = "instructionName";
                Rs = InstructionMappings.RegisterToBinaryCode[instructionWord];
                continue;
            }
        }

        return opcode + Rd + Rn + Ra + Rs;
    }
}