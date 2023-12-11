// See https://aka.ms/new-console-template for more information

using System.ComponentModel.Design;
using System.Numerics;

Console.WriteLine("Hello, World!");


var partRows = File.ReadAllLines("input.txt");

var sum = 0;

for (var row = 0; row < partRows.Length; row++)
{

    for (var column = 0; column < partRows[row].Length; column++)
    {
        var character = partRows[row][column];
        if (character == '*')
        {
            var ratio = FindGears(row, column);

            //Console.WriteLine(ratio.Count);

            if (ratio.Count == 2)
            {
                Console.WriteLine($"{ratio.First()}* {ratio.Last()}");
                sum += ratio.First() * ratio.Last();
            }

            List<int> FindGears(int ratioRow, int ratioColumn)
            {
                var places = new List<Tuple<int, int>>
                {
                    new(ratioRow, ratioColumn-1),
                    new(ratioRow, ratioColumn + 1),
                    new(ratioRow - 1, ratioColumn - 1),
                    new(ratioRow - 1, ratioColumn),
                    new(ratioRow - 1, ratioColumn + 1),
                    new(ratioRow + 1, ratioColumn - 1),
                    new(ratioRow + 1, ratioColumn),
                    new(ratioRow + 1, ratioColumn + 1),
                };

                if (ratioColumn < 0 || 
                    ratioRow < 0 ||
                    ratioRow >= partRows.Length ||
                    ratioColumn >= partRows[ratioRow].Length) return new List<int>();

                return places.Select(y => FindNumber(y.Item1, y.Item2)).OfType<int>().Distinct().ToList();


                int? FindNumber(int numberRow, int numberColumn)
                {
                    //Console.WriteLine($"xy {numberRow}-{numberColumn}");
                    //Console.WriteLine($"{numberRow}-{numberColumn}");
                    if (ratioColumn < 0 || 
                        numberRow < 0 ||
                        numberRow >= partRows.Length ||
                        numberColumn >= partRows[numberColumn].Length) return null;
                    var numberCharacter = partRows[numberRow][numberColumn];

                    if (!char.IsNumber(numberCharacter)) return null;


                    var number = $"{numberCharacter}";
                    var localCol = numberColumn;
                    var nextChar = partRows[numberRow][--localCol];
                    while (char.IsNumber(nextChar) && localCol >= 0)
                    {
                        number = $"{nextChar}{number}";
                        //Console.WriteLine($"{number}");
                        if(localCol == 0) break;
                        nextChar = partRows[numberRow][--localCol];

                    }
                    localCol = numberColumn;
                    nextChar = partRows[numberRow][++localCol];
                    while (char.IsNumber(nextChar) && localCol < partRows[numberRow].Length)
                    {
                        number += nextChar; 
                        if(localCol == partRows[numberRow].Length -1)break;
                        nextChar = partRows[numberRow][++localCol];
                    }

                    return int.Parse(number);
                }
            }


        }
    }
}

Console.WriteLine(sum);