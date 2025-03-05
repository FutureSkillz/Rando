using System.Security.Cryptography;

namespace Rando;

public static class Rando
{
    private const int BYTES_PER_ULONG = 8;
    private static readonly RandomNumberGenerator randomNumGen = RandomNumberGenerator.Create(); //instantiated as soon as it's called first time
    public static ulong GetUlong(ulong min, ulong max)
    {
        byte[] bytes = new byte[BYTES_PER_ULONG];
        randomNumGen.GetBytes(bytes);
        ulong range = max - min + 1;
        if (range == 0) //provided min and max were type's literal min and max values
            return BitConverter.ToUInt64(bytes);
        var maxAcceptable = ulong.MaxValue - (ulong.MaxValue % range);
        var quan = BitConverter.ToUInt64(bytes);
        while (quan >= maxAcceptable)
        {
            randomNumGen.GetBytes(bytes);
            quan = BitConverter.ToUInt64(bytes);
        }
        var result = min + (quan % range);
        return result;
    }
}