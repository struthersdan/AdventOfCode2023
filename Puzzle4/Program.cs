// See https://aka.ms/new-console-template for more information

using System.Xml.Serialization;

Console.WriteLine("Hello, World!");



var scratchCards = File.ReadAllLines("input.txt");

var sum = 0;

foreach (var scratchCard in scratchCards)
{
    var gameData = scratchCard.Split(":")[1].Split("|");
    var expectedNumbers = gameData[0].Trim().Split(" ")
        .Select(x => int.TryParse(x.Trim(), out var number) ? number : default(int?)).OfType<int>().ToList();
    var gameNumbers = gameData[1].Trim().Split(" ")
        .Select(x => int.TryParse(x.Trim(), out var number) ? number : default(int?)).OfType<int>().ToList();

    var winningNumbers = expectedNumbers.Intersect(gameNumbers).ToList();

    if (!winningNumbers.Any()) continue;

    var amount = winningNumbers.Aggregate(0.5, (current, winningNumber) => current * 2);

    Console.WriteLine(amount);

    sum += (int)amount;
}

Console.WriteLine(sum);
