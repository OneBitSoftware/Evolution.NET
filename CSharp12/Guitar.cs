namespace CSharp12;

using CSharp12.Attributes;

// Before .NET 8
/*public class Guitar : MusicalInstrument
{
    [ReferenceableConstructor]
    public Guitar(string manufacturer, int countOfStrings) : base("stringed", manufacturer)
    {
        this.CountOfStrings = countOfStrings;
    }

    [ReferenceableConstructor]
    public Guitar(string manufacturer) : this(manufacturer, countOfStrings: 6)
    {
    }
    
    public int CountOfStrings { get; }
}*/

// After .NET 8
[method: ReferenceableConstructor]
public class Guitar(string manufacturer, int countOfStrings) : MusicalInstrument("stringed", manufacturer)
{
    [ReferenceableConstructor]
    public Guitar(string manufacturer) : this(manufacturer, countOfStrings: 6)
    {
    }
    
    // NOTE: This is not allowed - every constructor must chain (directly or transitively) the primary one.
    // [ReferenceableConstructor]
    // public Guitar(string manufacturer) : base("stringed", manufacturer)
    // {
    // }
    
    public int CountOfStrings { get; } = countOfStrings;
}