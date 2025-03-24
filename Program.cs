// Set Illegal instruction interrupt vector [80] to 160. set [160] to halt instruction (11111)
string assemblerInput1 = "Movi R0 10100000 Movi R1 1010010 Str R0 R1 Movi R0 1111100000000000 Movi R1 10100000 Str R0 R1"; // Setup invalid instruction interrupt.
Assembler assembler1 = new Assembler();
MemoryAccessUnit.setMemory(assembler1.assemble(assemblerInput1), 0);

// Set Illegal instruction interrupt vector [84] to 192.
string assemblerInput2 = "Movi R0 10111000 Movi R1 1010110 Str R0 R1"; // Setup Int 1 software interrupt
Assembler assembler2 = new Assembler();
MemoryAccessUnit.setMemory(assembler2.assemble(assemblerInput2), 32);

string assemblerInput3 = "Movi R0 1 Int R0"; // Call int 1 software interrupt
Assembler assembler3 = new Assembler();
MemoryAccessUnit.setMemory(assembler3.assemble(assemblerInput3), 48);

string assemblerInput4 = "Movi R4 11000000 Movi R2 1 Add R3 R2 R0 R3 Jmp R4"; // Setup infinite counter. R3 will be incremented by 1 indefinitely.
Assembler assembler4 = new Assembler();
MemoryAccessUnit.setMemory(assembler4.assemble(assemblerInput4), 192);

// MemoryAccessUnit.setMemory("100000", 24); // append illegal instruction

MemoryAccessUnit.PrintBinaryMemory(32);
Cpu.start();
MemoryAccessUnit.PrintBinaryMemory(32);