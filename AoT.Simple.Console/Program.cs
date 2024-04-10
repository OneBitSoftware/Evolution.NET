namespace AoT.Simple.Console;

using System.Reflection;
using AoT.Simple.Console.Commands;

public static class Program
{
    public static void Main()
    {
        if (NativeMethods.GetDiskFreeSpaceEx("C:/", out var freeBytes, out var totalBytes, out _))
            System.Console.WriteLine($"Free bytes: {freeBytes}; Total bytes: {totalBytes}");
        else System.Console.WriteLine("Could not retrieve successfully information about the disk free space.");

        var allStartupTaskTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t is { IsClass: true, IsAbstract: false } && t.IsAssignableTo(typeof(IStartupTask))).ToArray();
        System.Console.WriteLine($"Found {allStartupTaskTypes.Length} startup tasks to execute.");
        
        foreach (var startupTaskType in allStartupTaskTypes)
        {
            var instance = Activator.CreateInstance(startupTaskType);
            if (instance is IStartupTask startupTask) startupTask.Execute();
        }
        
        System.Console.Write("Enter a number: ");

        if (!int.TryParse(System.Console.ReadLine(), out var n)) System.Console.WriteLine("An invalid integer was entered.");
        if (n < 2) System.Console.WriteLine("The minimum allowed input is 2");
        else
        {
            var allPrimes = GeneratePrimes(n).ToArray();
            System.Console.WriteLine($"All primes in the range [2, {n}] are: {string.Join(", ", allPrimes)}");
        }

        System.Console.Write("Press any key to exit...");
        System.Console.ReadLine();
    }

    private static IEnumerable<int> GeneratePrimes(int n)
    {
        var primes = new List<int>();

        for (var i = 2; i <= n; i++)
        {
            var isPrime = true;
            foreach (var p in primes)
            {
                if (i % p == 0)
                {
                    isPrime = false;
                    break;
                }

                if (p * p > i) break;
            }

            if (isPrime) primes.Add(i);
        }

        return primes;
    }
}