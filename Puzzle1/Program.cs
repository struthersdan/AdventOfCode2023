// See https://aka.ms/new-console-template for more information

using static System.Text.RegularExpressions.Regex;

Console.WriteLine("Hello, World!");


var sum = 0;
try
{
    //Pass the file path and file name to the StreamReader constructor
    var sr = new StreamReader(@"C:\\code\\AdventOfCode2023\\AdventOfCode2023\input.txt");
    //Read the first line of text
    var line = sr.ReadLine();
    //Continue to read until you reach end of file
  

    while (line != null)
    {
        //write the line to console window
        Console.WriteLine($"Input: {line}");

        var numberText = line;

        var numbers = new Dictionary<string, int>
        {
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight",8},
            {"nine", 9},
        };

        foreach (var number in numbers)
        {
            numberText = Replace(numberText, $"{number.Key}", $"{number.Key}{number.Value}{number.Key}"); 
        }

        Console.WriteLine($"AllNumbers: {numberText}");

        numberText = Replace(numberText, "[^1-9]", "");
        
        Console.WriteLine($"ParsedNumbers: {numberText}");

        var twoDigitNumber = $"{numberText.First(char.IsDigit)}{numberText.Last(char.IsDigit)}";
        Console.WriteLine($"Number:  {twoDigitNumber}\n");
        sum+= int.Parse(twoDigitNumber);

        //Read the next line
        line = sr.ReadLine();
    }

    Console.WriteLine(sum);
    //close the file
    sr.Close();
    Console.ReadLine();
}
catch(Exception e)
{
    Console.WriteLine("Exception: " + e.Message);
}
finally
{
    Console.WriteLine("Executing finally block.");
}