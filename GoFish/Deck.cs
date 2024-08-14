using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CardComparer;

namespace GoFish
{
    public class Deck : ObservableCollection<Card>
    {
        private static Random random = Player.Random;
        public Deck()
        {
            Reset();
        }
        public void Reset()
        {
            /* Call Clear() to remove all cards from the deck, then use two for loops to add
            * all combinations of suit and value, calling Add(new Card(...)) to add each card */
            Clear();

            for(int suit = 0; suit <= 3; suit++)
            {
                for(int value = 1; value <= 13; value++)
                    Add(new Card((Values)value, (Suits)suit));
            }
        }
        public Card Deal(int index)
        {
            Card cardToDeal = base[index];
            RemoveAt(index);
            return cardToDeal;
        }
        public void Shuffle()
        {
            List<Card> copy = new List<Card>(this);
            Clear();
            while (copy.Count > 0)
            {
                int index = random.Next(copy.Count);
                Card card = copy[index];
                copy.RemoveAt(index);
                Add(card);
            }
        }
        public void Sort()
        {
            List<Card> sortedCards = new List<Card>(this);
            sortedCards.Sort(new CardComparerByValue());
            Clear();
            foreach (Card card in sortedCards)
            {
                Add(card);
            }
        }
    }
}
