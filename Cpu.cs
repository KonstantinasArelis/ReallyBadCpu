public static class Cpu
{
    private static bool shutdownFlag = false;
    
    public static void start()
    {
        while(!shutdownFlag)
        {
            RegisterFile.PrintAllRegisters();
            ControlUnit.fetch();
            ControlUnit.decode();
            ControlUnit.execute();
            ControlUnit.memoryAccessAndReadBack();
        }
    }

    public static void shutdown()
    {
        shutdownFlag = true;
    }
}