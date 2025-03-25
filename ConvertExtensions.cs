using System.Text;

namespace reallyBadCpu; 

public static class ConvertExtensions {
    public const int numberBase = 2;
    
    public static int toIntBinary(this string s) => Convert.ToInt32(s, numberBase);
    public static uint toUIntBinary(this string s) => Convert.ToUInt32(s, numberBase);
    public static byte toByteBinary(this string s) => Convert.ToByte(s, numberBase);
    public static string toStringBinary(this int i) => Convert.ToString(i, numberBase);
    public static string toStringBinary(this uint u) => Convert.ToString(u, numberBase);
    public static string toStringBinary(this byte b) => Convert.ToString(b, numberBase);
    
    public static byte[] toBytes(this string s) {
        byte[] bytes = new byte[(s.Length + 7) / 8];
        
        for (int i = 0; i < bytes.Length; i++) {
            bytes[i] = s.Substring(i * 8, 8).toByteBinary();
        }

        return bytes;
    }

    public static string toString(this byte[] bs) {
        StringBuilder s = new();
        
        foreach (byte b in bs) {
            s.Append(b.toStringBinary().zeroExtend(8));
        }

        return s.ToString();
    }
    
    //Add zeroes to the front of the string if the string is too short
    //Useful for binary numbers: '1101' becomes '00000000000000000000000000001101'
    public static string zeroExtend(this string s, int size) {
        int missing = size - s.Length;

        if (missing <= 0) {
            return s;
        }

        string zeroes = new('0', missing);
        return zeroes + s;
    }
}