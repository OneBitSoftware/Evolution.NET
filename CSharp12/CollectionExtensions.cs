namespace CSharp12;

public static class CollectionExtensions
{
    public static (T First, IEnumerable<T> Middle, T Last) Separate<T>(this T[] array)
    {
        return array switch
        {
            { Length: 0 } or { Length: 1 } or { Length: 2 } => throw new InvalidOperationException("In order to separate the array, it must consist of at least three elements"),
            [var first, .. var middle, var last] => (first, middle, last)
        };
    }
}