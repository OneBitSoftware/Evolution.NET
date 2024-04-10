namespace CSharp12;

// Before .NET 8
/*public abstract class MusicalInstrument
{
    protected MusicalInstrument(string classification, string manufacturer)
    {
        if (string.IsNullOrWhiteSpace(classification)) throw new ArgumentNullException(nameof(classification));
        if (string.IsNullOrWhiteSpace(manufacturer)) throw new ArgumentNullException(nameof(manufacturer));
        
        this.Classification = classification;
        this.Manufacturer = manufacturer;
    }
    
    public string Classification { get; }
    public string Manufacturer { get; }
}*/

// After .NET 8
public abstract class MusicalInstrument(string classification, string manufacturer)
{
    public string Classification { get; } = string.IsNullOrWhiteSpace(classification) ? throw new ArgumentNullException(nameof(classification)) : classification;
    public string Manufacturer { get; } = string.IsNullOrWhiteSpace(manufacturer) ? throw new ArgumentNullException(nameof(manufacturer)) : manufacturer;
}