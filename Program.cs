// when using movi, the full 22 bit value must be provided (Padded if too short)
//string assemblerInput = "Add R0 R1 R2 R0 Add R4 R1 R2 R2 Movi R5 100100 Str R6 R5 Halt";
//string assemblerInput = "Str R6 R7 Add R0 R1 R2 R0 Add R4 R1 R2 R2 Movi R5 100100 Str R6 R5";
string assemblerInput1 = "Movi R0 1111100000000000 Movi R1 1010000 Str R0 R1 Halt"; // Setup invalid instruction interrupt
Assembler assembler1 = new Assembler();
MemoryAccessUnit.setMemory(assembler1.assemble(assemblerInput1), 0);

// string assemblerInput2 = "Movi R4 10000 Movi R2 1 Add R3 R2 R0 R3 Jmp R4"; // Setup invalid instruction interrupt
// Assembler assembler2 = new Assembler();
// MemoryAccessUnit.setMemory(assembler2.assemble(assemblerInput2), 12);

MemoryAccessUnit.PrintBinaryMemory(32);
Cpu.start();
MemoryAccessUnit.PrintBinaryMemory(32);