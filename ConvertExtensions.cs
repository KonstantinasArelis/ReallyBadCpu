namespace reallyBadCpu; 

public static class ConvertExtensions {
    public const int numberBase = 2;
    
    public static int toIntBinary(this string s) => Convert.ToInt32(s, numberBase);
    public static uint toUIntBinary(this string s) => Convert.ToUInt32(s, numberBase);
    public static string toStringBinary(this int i) => Convert.ToString(i, numberBase);
    public static string toStringBinary(this uint u) => Convert.ToString(u, numberBase);
}