using System;
using MyLibrary;

class Program
{
    static void Main()
    {
        var calc = new Calculator();
        Console.WriteLine($"2 + 3 = {calc.Add(2, 3)}");
    }
}
