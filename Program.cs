Cpu.printRegisters();

string assemblerInput = "Add R0 R1 R2 R3 Add R4 R1 R2 R2 Ldr R6 R7 Halt";
string memory = "111111111111111111111111111111111111111111111111";

Console.WriteLine("Assembler input: " + assemblerInput);
string assemblerOutput = Assembler.assemble(assemblerInput);

Console.WriteLine("Cpu input: " + assemblerOutput);
Cpu.executeBinary(assemblerOutput, memory);

Cpu.printRegisters();