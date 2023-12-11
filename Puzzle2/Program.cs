
// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
var games = File.ReadAllLines("input.txt");

var sum = 0;



foreach (var game in games)
{
    var gameParts = game.Split(":");

 
    var gameId = int.Parse(Regex.Replace(gameParts[0], "[^0-9]", ""));

    var blockCounts = new Dictionary<string, int>
    {
        {"red", 0},
        {"green", 0},
        {"blue", 0},
    };
    var sets = gameParts[1].Split(";");
    foreach (var set in sets)
    {
        var blocks = set.Split(",");
        foreach (var block in blocks)
        {
            var x = block.Trim().Split(" ");
            var color = x[1];
            var amount = int.Parse(x[0]);

           blockCounts.TryGetValue(color, out int existingAmount);

           if (amount > existingAmount)
           {
               blockCounts[color] = amount;
           }
        }

       
        
    }

    //if (blockCounts["red"] <= 12 && blockCounts["green"] <= 13 && blockCounts["blue"] <= 14)
    //{
    //    Console.WriteLine(gameId);
    //    foreach (var blockCount in blockCounts)
    //    {
    //        Console.Write(blockCount);
    //    }

    //    Console.WriteLine();
    //    Console.WriteLine("-----");
        
    //    sum += gameId;
    //}

    var power = 1;
    foreach (var blockCount in blockCounts)
    {
        Console.WriteLine(blockCount.Value);
        power *= blockCount.Value;
    }

    Console.WriteLine(power);

    sum += power;
}

Console.WriteLine(sum);
