string assemblerInput = "Add R0 R1 R2 R3 Add R4 R1 R2 R2 Ldr R6 R7 Halt";

Console.WriteLine("Assembler input: " + assemblerInput);
string assemblerOutput = Assembler.assemble(assemblerInput);

Console.WriteLine("Assembler output: " + assemblerOutput);

MemoryAccessUnit.setMemory(assemblerOutput);
Cpu.start();