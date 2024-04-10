namespace AoT.Simple.Console.Commands;

public class PrintHelloWorldStartupTask : IStartupTask
{
    public void Execute() => System.Console.WriteLine("Hello, world!");
}