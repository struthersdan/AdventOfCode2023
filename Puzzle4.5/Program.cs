var scratchCards = File.ReadAllLines("input.txt");

var sum = 0;

var cardCounts = new Dictionary<int, int>();
for (var i = 1; i <= scratchCards.Length; i++)
{
    cardCounts[i] = 1;
}

foreach (var scratchCard in scratchCards)
{
    var cardNumber =  int.Parse( scratchCard.Split(":")[0].Split("Card")[1].Trim());

    var runs = cardCounts[cardNumber];

    var gameData = scratchCard.Split(":")[1].Split("|");
    var expectedNumbers = gameData[0].Trim().Split(" ")
        .Select(x => int.TryParse(x.Trim(), out var number) ? number : default(int?)).OfType<int>().ToList();
    var gameNumbers = gameData[1].Trim().Split(" ")
        .Select(x => int.TryParse(x.Trim(), out var number) ? number : default(int?)).OfType<int>().ToList();

    var winningNumbers = expectedNumbers.Intersect(gameNumbers).ToList();

    if (!winningNumbers.Any()) continue;

    foreach (var winningNumber in winningNumbers)
    {
        cardCounts[++cardNumber] += runs;
    }

    
}

foreach (var cardCount in cardCounts)
{
    Console.WriteLine($"{cardCount.Key} - {cardCount.Value}");
}

Console.WriteLine(cardCounts.Sum(x=>x.Value));