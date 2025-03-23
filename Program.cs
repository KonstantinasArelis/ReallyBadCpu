string assemblerInput = "Add R0 R1 R2 R0 Add R4 R1 R2 R2 Ldr R6 R7 Movi R5 0000000000000000000111 Halt";

Console.WriteLine("Assembler input: " + assemblerInput);
string assemblerOutput = Assembler.assemble(assemblerInput);
//assemblerOutput+= "1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
Console.WriteLine("Assembler output: " + assemblerOutput);

MemoryAccessUnit.setMemory(assemblerOutput, 0);


Cpu.start();