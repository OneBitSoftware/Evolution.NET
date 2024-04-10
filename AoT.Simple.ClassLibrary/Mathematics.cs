namespace AoT.Simple.ClassLibrary;

using System.Runtime.InteropServices;

public static class Mathematics
{
    [UnmanagedCallersOnly(EntryPoint = "csharp_maths_gcd")]
    
    public static ulong Gcd(ulong a, ulong b) => InternalGcd(a, b);

    [UnmanagedCallersOnly(EntryPoint = "csharp_maths_lcm")]
    public static ulong Lcm(ulong a, ulong b) => a / InternalGcd(a, b) * b;

    private static ulong InternalGcd(ulong a, ulong b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b) a %= b;
            else b %= a;
        }

        return a | b;
    }
}