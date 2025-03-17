using System;
using System.Collections.Generic;

static class Cpu
{
    private static Dictionary<string, string> registers = new Dictionary<string, string>()
    {
        {"R0", "0"},
        {"R1", "10"},
        {"R2", "100000"},
        {"R3", "11"},
        {"R4", "0"},
        {"R5", "0"},
        {"R6", "0"},
        {"R7", "10"},
        {"IP", "0"}
    };

    public static void executeBinary(string binaryInput, string memory)
    {
        string currentInstructionBinary;
        string instructionName;
        string instructionType;

        while(true)
        {
            currentInstructionBinary = binaryInput.Substring(Convert.ToInt32(registers["IP"], 2) * 31, 32); // take first 32 bits
            Console.WriteLine("currently executing: " + currentInstructionBinary);
            instructionName = InstructionMappings.BinaryCodeToInstruction[currentInstructionBinary.Substring(0, 5)]; // translate first 5 bits to instruction name
            instructionType = InstructionMappings.InstructionNameToInstructionType[instructionName];
            switch(instructionType)
            {
                case "Arithmetic":
                    int[] segmentSizes = {5,4,4,4,4};  // opcode, rd, rn, ra, rs (Ignoring Cond field for now)
                    List<string> segmentedBinary = StringSplitter.SplitStringByLengths(currentInstructionBinary, segmentSizes);
                    
                    string result = InstructionMappings.BinaryCodeToRegister[segmentedBinary[1]];
                    string operand1 = InstructionMappings.BinaryCodeToRegister[segmentedBinary[2]];
                    string operand2 = InstructionMappings.BinaryCodeToRegister[segmentedBinary[4]];
                    Console.WriteLine("op1 " + operand1 + "op2 " + operand2);
                    switch(instructionName)
                    {
                        case "Add":
                            registers[result] = Convert.ToString(Convert.ToInt32(registers[operand1], 2) + Convert.ToInt32(registers[operand2], 2), 2);
                            break;
                        case "Sub":
                            registers[result] = Convert.ToString(Convert.ToInt32(registers[operand1], 2) - Convert.ToInt32(registers[operand2], 2), 2);
                            break;
                        case "Mul":
                            registers[result] = Convert.ToString(Convert.ToInt32(registers[operand1], 2) * Convert.ToInt32(registers[operand2], 2), 2);
                            break;
                        case "Div":
                            registers[result] = Convert.ToString(Convert.ToInt32(registers[operand1], 2) / Convert.ToInt32(registers[operand2], 2), 2);
                            break;
                    }
                break;
                case "Shutdown":
                    return;
                break;
                case "DataProcessing":
                    int[] segmentSizes2 = {5,4,4,19};  // opcode, rd, rn, unused (Ignoring Cond field for now)
                    List<string> segmentedBinary2 = StringSplitter.SplitStringByLengths(currentInstructionBinary, segmentSizes2);
                    string memoryAddress = registers[InstructionMappings.BinaryCodeToRegister[segmentedBinary2[2]]];
                    switch(instructionName){
                        case "Ldr":
                            string retrievedMemory = memory.Substring( Convert.ToInt32(memoryAddress, 2) , 32);
                            Console.WriteLine("retrieved memory: " + retrievedMemory + " leght: " + retrievedMemory.Length);
                            registers[InstructionMappings.BinaryCodeToRegister[segmentedBinary2[1]]] = retrievedMemory;
                        break;
                    }
                break;
            }
            registers["IP"] =  Convert.ToString(Convert.ToInt32(registers["IP"], 2) + 1, 2);
        }
        
    }

    public static void printRegisters()
    {
        Console.WriteLine("Register Contents: (Left bit most significant)");
        foreach (var registerEntry in registers)
        {
            Console.WriteLine($"{registerEntry.Key}: {registerEntry.Value}");
        }
    }
}


public class StringSplitter
{
    public static List<string> SplitStringByLengths(string inputString, int[] segmentLengths)
    {
        if (inputString == null)
        {
            throw new ArgumentNullException(nameof(inputString), "Input string cannot be null.");
        }
        if (segmentLengths == null || segmentLengths.Length == 0)
        {
            throw new ArgumentException("Segment lengths array cannot be null or empty.", nameof(segmentLengths));
        }

        List<string> substrings = new List<string>();
        int currentPosition = 0;

        foreach (int length in segmentLengths)
        {
            if (currentPosition >= inputString.Length)
            {
                // No more characters left in the input string
                break; // Or you could handle this differently, e.g., throw an exception, add an empty string, etc.
            }

            int segmentLength = length;
            if (currentPosition + segmentLength > inputString.Length)
            {
                // If the requested length goes beyond the end of the string,
                // take only the remaining characters.
                segmentLength = inputString.Length - currentPosition;
            }

            string substring = inputString.Substring(currentPosition, segmentLength);
            substrings.Add(substring);
            currentPosition += segmentLength;
        }

        return substrings;
    }

    public static void Main(string[] args)
    {
        string originalString = "ThisIsMyStringToSplit";
        int[] lengths = { 4, 5, 6 }; // First 4 chars, then next 5, then next 6

        List<string> splitStrings = SplitStringByLengths(originalString, lengths);

        Console.WriteLine($"Original String: \"{originalString}\"");
        Console.WriteLine("Split Strings:");
        for (int i = 0; i < splitStrings.Count; i++)
        {
            Console.WriteLine($"String {i + 1}: \"{splitStrings[i]}\"");
        }
    }
}