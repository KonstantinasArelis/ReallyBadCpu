using System.Text;
using reallyBadCpu;

public static class MemoryAccessUnit
{
    private static string memory = new string('0', 2048);
    private static string instruction = "";
    
    public static void prepare(string opcode)
    {
        instruction = InstructionMappings.BinaryCodeToInstruction[opcode];
    }

    public static string execute(string Rd, string Rn)
    {
        switch (instruction)
        {
            case "Ldr": {
                uint address = RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rn]].toUIntBinary();
                return fetchWord(address);
            }
            case "Str": {
                int address = RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rn]].toIntBinary();
                memory = ReplaceStringPart(memory, RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rd]], address * 8);
                return "";
            }
        }
        throw new Exception("MAU tried to perform non MAU operation");
    }

    public static string fetch4Bytes(string Rn) {
        uint address = RegisterFile.registers[InstructionMappings.BinaryCodeToRegister[Rn]].toUIntBinary();
        return fetchWord(address * 8);
    }
    
    public static string fetchWord(uint address) {
        return memory.Substring((int)address, GlobalConstants.wordSize * 8);
    }
    
    public static void setMemory(string _memory, int startingByteIndex)
    {
        memory = ReplaceStringPart(memory, _memory, startingByteIndex * 8);
    }

    public static string ReplaceStringPart(string originalString, string replacementString, int startIndex)
    {
        if (startIndex < 0 || startIndex > originalString.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex), "Start index is out of range.");
        }

        StringBuilder sb = new StringBuilder(originalString);

        for (int i = 0; i < replacementString.Length; i++)
        {
            if (startIndex + i < sb.Length)
            {
                sb[startIndex + i] = replacementString[i];
            }
            else
            {
                // If the replacement extends beyond the original length, we stop.
                break;
            }
        }

        return sb.ToString();
    }

    public static void PrintBinaryMemory(int bitsPerGroup = 8)
    {

        string binaryString = memory;
        if (string.IsNullOrEmpty(binaryString))
        {
            Console.WriteLine("Binary string is empty.");
            return;
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(binaryString, "^[01]+$"))
        {
            Console.WriteLine("Invalid binary string. It should only contain '0' and '1'.");
            return;
        }

        if (bitsPerGroup <= 0)
        {
            Console.WriteLine("Bits per group must be a positive integer.");
            return;
        }

        StringBuilder formattedOutput = new StringBuilder();
        int length = binaryString.Length;

        for (int i = 0; i < length; i += bitsPerGroup)
        {
            int remainingBits = Math.Min(bitsPerGroup, length - i);
            string chunk = binaryString.Substring(i, remainingBits);

            formattedOutput.Append(chunk);

            // If we've processed a full group, add a separator
            if (remainingBits == bitsPerGroup && i + bitsPerGroup < length)
            {
                formattedOutput.Append(" ");
            }
        }
        Console.WriteLine("Current Memory:");
        Console.WriteLine(formattedOutput.ToString());
    }
}