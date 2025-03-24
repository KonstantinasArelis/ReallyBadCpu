using System.Text;
using reallyBadCpu;

public static class MemoryAccessUnit {
    public static string memory = new string('0', 2048);
    private static string instruction = "";

    public static void prepare(string opcode) {
        instruction = InstructionMappings.BinaryCodeToInstruction[opcode];
    }

    public static string execute(string Rd, string Rn) {
        switch (instruction) {
            case "Ldr":
                return memory.Substring(
                    Convert.ToInt32(RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rn]], 2),
                    Convert.ToInt32(GlobalConstants.instructionSize, 2)
                );
            case "Str":
                memory = ReplaceStringPart(
                    memory, RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rd]],
                    Convert.ToInt32(RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rn]], 2) * 8
                );
                return "";
        }

        throw new Exception("MAU tried to perform non MAU operation");
    }

    public static string fetchInstruction(string Rn) {
        return memory.Substring(
            Convert.ToInt32(RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rn]], 2),
            Convert.ToInt32(GlobalConstants.instructionSize, 2)
        );
    }

    public static void setMemory(string _memory, int startingBitIndex) {
        memory = ReplaceStringPart(memory, _memory, startingBitIndex);
    }

    public static string ReplaceStringPart(string originalString, string replacementString, int startIndex) {
        if (startIndex < 0 ||
            startIndex > originalString.Length) {
            throw new ArgumentOutOfRangeException(nameof(startIndex), "Start index is out of range.");
        }

        StringBuilder sb = new StringBuilder(originalString);

        for (int i = 0; i < replacementString.Length; i++) {
            if (startIndex + i < sb.Length) {
                sb[startIndex + i] = replacementString[i];
            }
            else {
                // If the replacement extends beyond the original length, we stop.
                break;
            }
        }

        return sb.ToString();
    }

    public static void PrintBinaryMemory(int bitsPerGroup = 8) {
        string binaryString = memory;
        if (string.IsNullOrEmpty(binaryString)) {
            Console.WriteLine("Binary string is empty.");
            return;
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(binaryString, "^[01]+$")) {
            Console.WriteLine("Invalid binary string. It should only contain '0' and '1'.");
            return;
        }

        if (bitsPerGroup <= 0) {
            Console.WriteLine("Bits per group must be a positive integer.");
            return;
        }

        StringBuilder formattedOutput = new StringBuilder();
        int length = binaryString.Length;

        for (int i = 0; i < length; i += bitsPerGroup) {
            int remainingBits = Math.Min(bitsPerGroup, length - i);
            string chunk = binaryString.Substring(i, remainingBits);

            formattedOutput.Append(chunk);

            // If we've processed a full group, add a separator
            if (remainingBits == bitsPerGroup &&
                i + bitsPerGroup < length) {
                formattedOutput.Append(" ");
            }
        }

        Console.WriteLine("Current Memory:");
        Console.WriteLine(formattedOutput.ToString());
    }

    public static string PadLeftToLength(this string input, int targetLength) {
        if (input == null) {
            return new string('0', targetLength);
        }

        if (input.Length >= targetLength) {
            return input; // No padding needed or already longer
        }

        int paddingNeeded = targetLength - input.Length;
        return new string('0', paddingNeeded) + input;
    }

    //Fetch string with binary of a word (32 bit value) at specified address
    //Checks virtual mode and converts virtual address to physical address if necessary
    public static string fetchWordString(uint address) {
        if (RegisterFile.inVirtualMode) {
            address = MemoryMappingUnit.getPhysicalAddress(address);
        }

        return fetchWordStringReal(address);
    }

    //Fetch binary string for word (32 bit value) at specified physical address
    public static string fetchWordStringReal(uint address) {
        int bitPosition = (int)address * 8;
        string word = memory.Substring((int)address * 8, GlobalConstants.WORD_SIZE_BITS);

        return word;
    }
}