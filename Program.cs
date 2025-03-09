// Cia labai rough pirmas bandymas. Kolkas veikia tik su ADD ir be Cond lauku.
// Taip pat cpu vykdo tik pacia pirma instrukcija ir tada pasiduoda.
// R0 yra destination, kuriame bus sudeti R1 ir R3

Cpu.printRegisters();

string assemblerInput = "Add R0 R1 R2 R3";

Console.WriteLine("Assembler input: " + assemblerInput);

string assemblerOutput = Assembler.assemble(assemblerInput);

Console.WriteLine("Cpu input: " + assemblerOutput);

Cpu.executeBinary(assemblerOutput);

Cpu.printRegisters();
