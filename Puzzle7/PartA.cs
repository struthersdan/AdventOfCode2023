using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle7
{
    internal static class PartA
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

           

            var sortedBets = cardBets.OrderBy(x => x.IsFiveOfAKind)
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
             

            var betCount = 1;
            foreach (var sortedBet in sortedBets)
            {
                
                sortedBet.Cards.ForEach(x=> Console.Write($"{x.Value}".PadLeft(3)));

                if(sortedBet.IsFiveOfAKind)  Console.Write($"-5 ");
               else if (sortedBet.IsFourOfAKind) Console.Write($"-4 ");
               else if(sortedBet.IsFullHouse)   Console.Write($"-FH");
                else if (sortedBet.IsThreeOfAKind) Console.Write($"-3 ");
                else if (sortedBet.IsTwoPair) Console.Write($"-2p");
                else if(sortedBet.IsOnePair)   Console.Write($"-1p");
                else Console.Write("-H ");

               
              
                   
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

        public int UniqueCardCount => Cards.DistinctBy(x=>x.Value).Count();
        public IEnumerable<IGrouping<int, Card>> GroupedCards => Cards.GroupBy(x => x.Value);


        public bool IsFiveOfAKind => UniqueCardCount == 1;
        public bool IsFourOfAKind => UniqueCardCount == 2 && GroupedCards.Any(x=>x.Count() == 4);
        public bool IsFullHouse => UniqueCardCount == 2 && GroupedCards.Any(x=>x.Count() == 3);
        public bool IsThreeOfAKind => UniqueCardCount== 3 && GroupedCards.Any(x=>x.Count() == 3);
        public bool IsTwoPair => UniqueCardCount== 3 && GroupedCards.Any(x=>x.Count() == 2);
        public bool IsOnePair => UniqueCardCount == 4;
        public int[] CardArray => Cards.Select(x => x.Value).ToArray();
    }

    public class Card(char c)
    {
        public int Value { get; set; } = c switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 11,
            'T' => 10,
            _ => int.Parse($"{c}")
        };
    }
}
