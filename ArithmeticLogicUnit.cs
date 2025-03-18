using System.Collections;

public static class ArithmeticLogicUnit
{
    private static string instruction = "";

    public static void prepare(string opcode)
    {
        instruction = InstructionMappings.BinaryCodeToInstruction[opcode];
    }

    public static string execute(string Rn, string Ra, string Rs)
    {
        int _Rn = Convert.ToInt32(RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rn]], 2);
        int _Ra = Convert.ToInt32(RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Ra]], 2);
        int _Rs = Convert.ToInt32(RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rs]], 2);
        
        switch (instruction)
        {
            case "Add":
                return Convert.ToString(_Rn + _Rs, 2);
            case "Sub":
                return Convert.ToString(_Rn - _Rs, 2);
            case "Mul":
                return Convert.ToString(_Rn * _Rs, 2);
            case "Div":
                return Convert.ToString(_Rn / _Rs, 2);
        }

        throw new Exception("ALU tried to perform non ALU operation");
    }
}