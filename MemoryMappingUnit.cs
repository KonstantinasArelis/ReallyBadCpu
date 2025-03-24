using System.Globalization;

namespace reallyBadCpu;

//Helper structs
public struct VirtualAddress(uint address) {
    //Determine by how much the base address retrieved from the page table will be offset
    public uint offset = address.getBits(0, 12);

    //Used for looking up the page address to which the offset is added
    public uint pageTableIndex = address.getBits(12, 20);
}

public struct PageTableEntry(uint entry) {
    //Is the page valid (available)
    //TODO throw exception when accessing invalid page
    public bool isValid = entry.getBit(0);

    //Determines the address which will be offset by the virtual address offset to get the physical address
    public uint baseAddress = entry.getBits(12, 20);

    //Other bits are reserved for OS stuff
}

public static class MemoryMappingUnit {
    //Encapsulating functions so i can work with numbers instead of strings
    //UNRELATED TO SIMILARLY NAMED FUNCTIONS IN MEMORY ACCESS UNIT, DONT GET CONFUSED
    public static uint fetchWordReal(uint address) {
        string wordString = MemoryAccessUnit.fetchWordStringReal(address);
        return Convert.ToUInt32(wordString, 2);
    }

    public static uint getPhysicalAddress(uint address) {
        VirtualAddress virtualAddress = new(address);

        //Fetch page table entry at specified index
        uint pagePointer = RegisterFile.pagePointer;
        uint pageTableEntryValue = fetchWordReal(pagePointer + virtualAddress.pageTableIndex);
        PageTableEntry pageTableEntry = new(pageTableEntryValue);

        if (!pageTableEntry.isValid) {
            //TODO call appropriate interrupt if attempting to access invalid page
        }

        //Calculate physical address
        uint physicalAddress = (pageTableEntry.baseAddress << 12) & virtualAddress.offset;

        return physicalAddress;
    }
}