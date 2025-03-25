namespace reallyBadCpu; 

public static class ConvertExtensions {
    public static int toIntBinary(this string s) => Convert.ToInt32(s, 2);
    public static uint toUIntBinary(this string s) => Convert.ToUInt32(s, 2);
    public static string toStringBinary(this int i) => Convert.ToString(i, 2);
    public static string toStringBinary(this uint u) => Convert.ToString(u, 2);
}