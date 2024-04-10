namespace CSharp12;

using System.Collections.Immutable;
using System.Reflection;
using System.Text;
using CSharp12.Attributes;

delegate void PrintNumber(Money num = 100);

public static class Program
{
    public static void Main()
    {
        // Primary constructors
        var constructors = typeof(Guitar).GetConstructors().Where(x => x.IsDefined(typeof(ReferenceableConstructorAttribute))).ToArray();
        Console.WriteLine($"Found {constructors.Length} referenceable constructors.");

        foreach (var constructorInfo in constructors) Console.WriteLine(constructorInfo);

        var emptyDate = new Date();
        Console.WriteLine(emptyDate);
        
        var todayDate = new Date(2024, 4, 11);
        Console.WriteLine(todayDate);
        
        // What improvements are discussed for the latter versions of C#?
        // - Using the `readonly` keyword for the parameters of the primary constructor
        // - Having the ability to define body for the primary constructor

        // Collection expressions
        
        // NOTE: The ImmutableList<T> type does not declare a public constructor, nor supports a collection initializer.
        // var projects = ImmutableList<string>.Empty;
        // ImmutableList<string> projects = [];
        // var projects = ImmutableList.Create<string>("hello", "world");
        ImmutableList<string> projects = ["hello", "world"];

        // The `spread` operator is not new.
        // We have been using it for defining range literals and pattern matching (see the `Separate` method).
        var message = "Hello, .NET developers!";
        var greet = message[..5];
        Console.WriteLine(greet); // Hello

        int[] primes = [2, 3, 5, 7, 11, 13, 17, 19];
        var primesSlice = Slice(primes.AsSpan());
        Console.WriteLine($"First: {primesSlice.First}; Middle: {JoinSpan(", ", primesSlice.Middle)}; Last: {primesSlice.Last}");

        int[] factorions = [2, 145, 40585];
        var factorionsSlice = Slice(factorions.AsSpan());
        Console.WriteLine($"First: {factorionsSlice.First}; Middle: {JoinSpan(", ", factorionsSlice.Middle)}; Last: {factorionsSlice.Last}");

        int[] specialNumbers = [..primes, -1, ..factorions];
        Console.WriteLine($"Special numbers: {string.Join(", ", specialNumbers)}");
        
        int[][] matrix = [[1, 0, 0], [0, 1, 0], [0, 0, 1]]; // This is the so-called Identity matrix.
        int[] flatMatrix = [..matrix[0], ..matrix[1], ..matrix[2]];
        
        // The `UnderdevelopedCollection{T}` supports a collection initializer
        UnderdevelopedCollection<int> customCollection = [1, 2, 3];
        Console.WriteLine(string.Join(", ", customCollection));

        UnderdevelopedBuffer<int> customBuffer = [4, 5, 6];
        Console.WriteLine(string.Join(", ", customBuffer));

        int[] allElementsFromCustomCollections = [..customCollection, ..customBuffer];
        
        PrintNumber func1 = (Money amount) => Console.WriteLine(amount);
        PrintNumber func2 = (Money amount = 100) => Console.WriteLine(amount);
        PrintNumber func3 = (Money amount = 500) => Console.WriteLine(amount); // Parameter has different default value in the target delegate type
        Action<decimal> func4 = (Money amount = 100) => Console.WriteLine(amount); // Parameter has default value but `<missing>` in the target delegate type

        func1();
        func2();
        func3();
        // func4(); // Compile-time error... The delegate type requires 1 parameter.
        func4(100);

        // SearchValues.Create("helo")
        // Showcase the Ascii class that helps with optimization of Kestrel
    }
    
    private static ArraySlice<T> Slice<T>(this Span<T> array)
    {
        return array switch
        {
            { Length: 0 } or { Length: 1 } or { Length: 2 } => throw new InvalidOperationException("In order to separate the array, it must consist of at least three elements"),
            [var first, .. var middle, var last] => new ArraySlice<T>(first, middle, last)
        };
    }

    private static string JoinSpan<T>(string separator, Span<T> span)
    {
        var result = new StringBuilder();
        for (var i = 0; i < span.Length; i++)
        {
            if (i > 0) result.Append(separator);
            result.Append(span[i]);
        }

        return result.ToString();
    }

    private readonly ref struct ArraySlice<T>(T first, Span<T> middle, T last)
    {
        public T First { get; } = first;
        public Span<T> Middle { get; } = middle;
        public T Last { get; } = last;
    }
}