using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Puzzle7
    {
        internal static class PartB
        {
            public static void Run()
            {
                long total = 0;
                var bets = File.ReadAllLines("input.txt");

                var cardBets = new List<CardBet>();

                foreach (var bet in bets)
                {
                    var cardBet = new CardBet(bet);
                    cardBets.Add(cardBet);
                }


                var sortedBets = cardBets
                    .OrderBy(x => x.IsFiveOfAKind)
                    .ThenBy(x => x.IsFourOfAKind)
                    .ThenBy(x => x.IsFullHouse)
                    .ThenBy(x => x.IsThreeOfAKind)
                    .ThenBy(x => x.IsTwoPair)
                    .ThenBy(x => x.IsOnePair)
                    .ThenBy(x => x.CardArray[0])
                    .ThenBy(x => x.CardArray[1])
                    .ThenBy(x => x.CardArray[2])
                    .ThenBy(x => x.CardArray[3])
                    .ThenBy(x => x.CardArray[4]);
a

                var betCount = 1;
                foreach (var sortedBet in sortedBets)
                {
                    sortedBet.Cards.ForEach(x => Console.Write($"{x.Text}"));

                    if (sortedBet.IsFiveOfAKind) Console.Write($" -5 ");
                    else if (sortedBet.IsFourOfAKind) Console.Write($" -4 ");
                    else if (sortedBet.IsFullHouse) Console.Write($" -FH");
                    else if (sortedBet.IsThreeOfAKind) Console.Write($" -3 ");
                    else if (sortedBet.IsTwoPair) Console.Write($" -2p");
                    else if (sortedBet.IsOnePair) Console.Write($" -1p");

                    else Console.Write(" -H ");

                    Console.Write(sortedBet.CardArray[0]);


                    var winnings = sortedBet.Bet * betCount;
                    Console.Write($" {sortedBet.Bet.ToString(),3} * {betCount.ToString(),4} {winnings.ToString(),10}");
                    Console.WriteLine();
                    total += winnings;

                    betCount++;
                }

                Console.WriteLine(total);
            }
        }

        public class CardBet
        {
            public CardBet(string text)
            {
                var inputs = text.Split(" ");
                Cards = inputs[0].ToCharArray().Select(x => new Card(x)).ToList();
                Bet = int.Parse(inputs[1]);
            }

            public int Bet { get; set; }

            public List<Card> Cards { get; set; }

            public int UniqueCardCount => Cards.Where(x => x.Value != 1).DistinctBy(x => x.Value).Count();

            public IEnumerable<IGrouping<int, Card>> GroupedCards =>
                Cards.Where(x => x.Value != 1).GroupBy(x => x.Value);

            public int JokerCount => Cards.Count(x => x.Value == 1);

            public bool IsFiveOfAKind => UniqueCardCount is 0 or 1;

            public bool IsFourOfAKind => !IsFiveOfAKind && UniqueCardCount == 2 &&
                                         GroupedCards.Any(x => x.Count() + JokerCount == 4);

            public bool IsFullHouse => !IsFiveOfAKind && !IsFourOfAKind && UniqueCardCount == 2 &&
                                       GroupedCards.Any(x => x.Count() + JokerCount == 3);

            public bool IsThreeOfAKind => !IsFiveOfAKind && !IsFourOfAKind && !IsFullHouse && UniqueCardCount == 3 &&
                                          GroupedCards.Any(x => x.Count() + JokerCount == 3);

            public bool IsTwoPair => !IsFiveOfAKind && !IsFourOfAKind && !IsFullHouse && !IsThreeOfAKind &&
                                     UniqueCardCount == 3 && GroupedCards.Any(x => x.Count() + JokerCount == 2);

            public bool IsOnePair => !IsFiveOfAKind && !IsFourOfAKind && !IsFullHouse && !IsThreeOfAKind &&
                                     !IsTwoPair && UniqueCardCount == 4;

            public int[] CardArray => Cards.Select(x => x.Value).ToArray();
        }

        public class Card(char c)
        {
            public char Text { get; set; } = c;

            public int Value { get; set; } = c switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'T' => 10,
                'J' => 1,
                _ => int.Parse($"{c}")
            };
        }
    }
}