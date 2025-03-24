string assemblerInput1 = "Movi R0 1111100000000000 Movi R1 1010000 Str R0 R1"; // Setup invalid instruction interrupt.
Assembler assembler1 = new Assembler();
MemoryAccessUnit.setMemory(assembler1.assemble(assemblerInput1), 0);

string assemblerInput2 = "Movi R0 1111100000000000 Movi R1 1010100 Str R0 R1"; // Setup Int 1 software interrupt
Assembler assembler2 = new Assembler();
MemoryAccessUnit.setMemory(assembler2.assemble(assemblerInput2), 12);

// string assemblerInput3 = "Movi R4 10000 Movi R2 1 Add R3 R2 R0 R3 Jmp R4"; // Setup infinite counter. R3 will be incremented by 1 indefinitely.
// Assembler assembler3 = new Assembler();
// MemoryAccessUnit.setMemory(assembler3.assemble(assemblerInput3), 24);

string assemblerInput3 = "Movi R0 1 Int R0"; // Call int 1 software interrupt
Assembler assembler3 = new Assembler();
MemoryAccessUnit.setMemory(assembler3.assemble(assemblerInput3), 24);

// MemoryAccessUnit.setMemory("100000", 24); // append illegal instruction

MemoryAccessUnit.PrintBinaryMemory(32);
Cpu.start();
MemoryAccessUnit.PrintBinaryMemory(32);