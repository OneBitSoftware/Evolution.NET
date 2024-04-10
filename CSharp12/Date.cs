namespace CSharp12;

// Before .NET 8
/*public readonly struct Date
{
    public Date(int year, int month, int day)
    {
        this.Year = year;
        this.Month = month;
        this.Day = day;
    }

    public int Year { get; }
    public int Month { get; }
    public int Day { get; }
    
    public override string ToString() => $"{this.Year:0000}-{this.Month:00}-{this.Day:00}";
}*/

// After .NET 8
public readonly struct Date(int year, int month, int day)
{
    public int Year { get; } = year;
    public int Month { get; } = month;
    public int Day { get; } = day;
    
    public override string ToString() => $"{this.Year:0000}-{this.Month:00}-{this.Day:00}";
}