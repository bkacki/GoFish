using GoFish;

namespace GoFishTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void TestGetNextHand()
        {
            var player = new Player("Owen", new List<Card>());
            player.GetNextHand(new Deck());
            CollectionAssert.AreEqual(
                new Deck().Take(5).Select(card => card.ToString()).ToList(),
                player.Hand.Select(card => card.ToString()).ToList());
        }
        [TestMethod]
        public void TestDoYouHaveAny()
        {
            IEnumerable<Card> cards = new List<Card>()
             {
                 new Card(Values.Walet, Suits.pik),
                 new Card(Values.Tr�jka, Suits.trefl),
                 new Card(Values.Tr�jka, Suits.kier),
                 new Card(Values.Czw�rka, Suits.karo),
                 new Card(Values.Tr�jka, Suits.karo),
                 new Card(Values.Walet, Suits.trefl),
             };
            var player = new Player("Owen", cards);
            var threes = player.DoYouHaveAny(Values.Tr�jka, new Deck())
                .Select(Card => Card.ToString())
                .ToList();
            CollectionAssert.AreEqual(new List<string>()
            {
                 "Tr�jka karo",
                 "Tr�jka trefl",
                 "Tr�jka kier",
            }, threes);
            Assert.AreEqual(3, player.Hand.Count());
            var jacks = player.DoYouHaveAny(Values.Walet, new Deck())
            .Select(Card => Card.ToString())
            .ToList();
            CollectionAssert.AreEqual(new List<string>()
             {
                 "Walet trefl",
                 "Walet pik",
             }, jacks);
            var hand = player.Hand.Select(Card => Card.ToString()).ToList();
            CollectionAssert.AreEqual(new List<string>() { "Czw�rka karo" }, hand);
            Assert.AreEqual("Owen has 1 card and 0 books", player.Status);
        }
        [TestMethod]
        public void TestAddCardsAndPullOutBooks()
        {
            IEnumerable<Card> cards = new List<Card>()
             {
                 new Card(Values.Walet, Suits.pik),
                 new Card(Values.Tr�jka, Suits.trefl),
                 new Card(Values.Walet, Suits.kier),
                 new Card(Values.Tr�jka, Suits.kier),
                 new Card(Values.Czw�rka, Suits.karo),
                 new Card(Values.Walet, Suits.karo),
                 new Card(Values.Walet, Suits.trefl),
             };
            var player = new Player("Owen", cards);
            Assert.AreEqual(0, player.Books.Count());
            var cardsToAdd = new List<Card>()
             {
                 new Card(Values.Tr�jka, Suits.karo),
                 new Card(Values.Tr�jka, Suits.pik),
             };
            player.AddCardsAndPullOutBooks(cardsToAdd);
            var books = player.Books.ToList();
            CollectionAssert.AreEqual(new List<Values>() { Values.Tr�jka, Values.Walet }, books);
            var hand = player.Hand.Select(Card => Card.ToString()).ToList();
            CollectionAssert.AreEqual(new List<string>() { "Czw�rka karo" }, hand);
            Assert.AreEqual("Owen has 1 card and 2 books", player.Status);
        }
        [TestMethod]
        public void TestDrawCard()
        {
            var player = new Player("Owen", new List<Card>());
            player.DrawCard(new Deck());
            Assert.AreEqual(1, player.Hand.Count());
            Assert.AreEqual("As karo", player.Hand.First().ToString());
        }
        [TestMethod]
        public void TestRandomValueFromHand()
        {
            var player = new Player("Owen", new Deck());
            Player.Random = new MockRandom() { ValueToReturn = 0 };
            Assert.AreEqual("As", player.RandomValueFromHand().ToString());
            Player.Random = new MockRandom() { ValueToReturn = 4 };
            Assert.AreEqual("Dw�jka", player.RandomValueFromHand().ToString());
            Player.Random = new MockRandom() { ValueToReturn = 8 };
            Assert.AreEqual("Tr�jka", player.RandomValueFromHand().ToString());
        }
    }
    /// <summary>
    /// Mock Random for testing that always returns a specific value
    /// </summary>
    public class MockRandom : System.Random
    {
        public int ValueToReturn { get; set; } = 0;
        public override int Next() => ValueToReturn;
        public override int Next(int maxValue) => ValueToReturn;
        public override int Next(int minValue, int maxValue) => ValueToReturn;
    }
}
