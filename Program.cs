// when using movi, the full 22 bit value must be provided (Padded if too short)
//string assemblerInput = "Add R0 R1 R2 R0 Add R4 R1 R2 R2 Movi R5 100100 Str R6 R5 Halt";
//string assemblerInput = "Str R6 R7 Add R0 R1 R2 R0 Add R4 R1 R2 R2 Movi R5 100100 Str R6 R5";
string assemblerInput = 
"Movi R0 1111100000000000 Movi R1 1010000 Str R0 R1"; // Setup invalid instruction interrupt

Console.WriteLine("Assembler input: " + assemblerInput);
string assemblerOutput = Assembler.assemble(assemblerInput);
assemblerOutput+= "1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
Console.WriteLine("Assembler output: " + assemblerOutput);

MemoryAccessUnit.setMemory(assemblerOutput, 0);

MemoryAccessUnit.PrintBinaryMemory(32);
Cpu.start();
MemoryAccessUnit.PrintBinaryMemory(32);