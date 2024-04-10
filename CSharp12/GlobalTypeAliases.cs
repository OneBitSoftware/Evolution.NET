// Before .NET 8
// global using Money = System.Decimal;
// It is not possible to use aliases for tuples or arrays

// After .NET 8
global using Money = decimal;
global using Stats = (int Sum, int Min, int Max);
global using Text = string[];
global using unsafe RawPtr = void*; // This line requires the `AllowUnsafeBlocks` parameter