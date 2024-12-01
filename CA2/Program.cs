using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System;
using System.Threading;

namespace CA2
{
    class Program
    {
        //global vars needed
        static int PlayerScore = 0;
        static int cardNum = 0;
        static int DealerScore = 0;
        static bool playAgain = true;
        static int jokerSwitch = 0;
        static int jokerDeal = 0;

        //Main Method
        static void Main(string[] args)
        {
            //while loop allows for player to play again
            while (playAgain  == true)
            {
                //Asking player what version of the game they want to play, normal BlackJack or BadJack <--- Additional Feature
                Console.WriteLine("What version of the game do you want to play?");
                Console.Write("BlackJack or BadJacks - 1/2: ");
                string gamein = Console.ReadLine();
                int game = int.Parse(gamein);

                //If player inputs 1, they have selected normal BlackJack
                if (game == 1)
                {
                    //Rules and Explaination
                    Console.WriteLine();
                    Console.WriteLine("Goal: Get the closest to 21 in card value without going over.");
                    Console.WriteLine("Kings, Queens, and Jacks are worth 10, Aces are worth 11 or 1");
                    Console.WriteLine("Player Vs. PC Dealer");
                    Console.WriteLine();

                    //Call Game
                    Deal();
                    Deal();
                    Console.WriteLine($"Your score is {PlayerScore}");
                    Console.WriteLine();

                    //asks player if they want to stick or twist
                    Console.Write("Do you want to stick or twist, t/s: ");
                    string sORt = Console.ReadLine();

                    //calls twist or stick function depending on response
                    if (sORt == "t")
                    {
                        Twist();
                    }
                    else if (sORt == "s")
                    {
                        Stick();
                    }

                }

                //If player inputs 2, they have selected BadJacks <--- Additional Feature
                else if (game == 2)
                {
                    //Rules and Explaination
                    Console.WriteLine();
                    Console.WriteLine("Goal: Get the closest to 21 in card value without going over.");
                    Console.WriteLine("Kings and Queens are worth 10, Aces are worth 11 or 1");
                    Console.WriteLine("Be careful: JACKS are an instant loss card. JOKERS switch you and the dealers cards.");
                    Console.WriteLine("Player Vs. PC Dealer");
                    Console.WriteLine();

                    //Call Game
                    BadDeal();
                    BadDeal();
                    Console.WriteLine($"Your score is {PlayerScore}");
                    Console.WriteLine();
                    Console.Write("Do you want to stick or twist, t/s: ");
                    string sORt = Console.ReadLine();

                    //calls twist or stick function for BadJack depending on response
                    if (sORt == "t")
                    {
                        BadTwist();
                    }
                    else if (sORt == "s")
                    {
                        BadStick();
                    }
                }

                //If anything else is inputed, player gets this message and the application closes
                else
                {
                    Console.WriteLine("Not an option");
                    Console.WriteLine("Closing game in 5 seconds...");
                    Thread.Sleep(5000);
                }
            }
            //Closes game
            Console.WriteLine("Closing game in 5 seconds...");
            Thread.Sleep(5000);

        }


        //Initial dealt cards generator
        static void Deal() 
        {
            //generates random card and suit
            var rand = new Random();
            string[] cards = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            int newCard = rand.Next(cards.Length);
            string cardVal = cards[newCard];

            string[] suits = { "Hearts", "Clubs", "Spades", "Diamonds" };
            int newSuit = rand.Next(suits.Length);
            string cardSuit = suits[newSuit];

            //adds to score depending on random card pulled
            switch (cardVal)
            {
                case "Ace":
                    if (PlayerScore + cardNum > 21)
                    {
                        cardNum = 1;
                        PlayerScore = PlayerScore + cardNum;

                    }
                    else
                    {
                        cardNum = 11;
                        PlayerScore = PlayerScore + cardNum;
                    }
                    break;

                case "2":
                    cardNum = 2;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "3":
                    cardNum = 3;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "4":
                    cardNum = 4;
                    PlayerScore = PlayerScore + cardNum;
                    break;
                
                case "5":
                    cardNum = 5;
                    PlayerScore = PlayerScore + cardNum;
                    break;
                
                case "6":
                    cardNum = 6;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "7":
                    cardNum = 7;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "8":
                    cardNum = 8;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "9":
                    cardNum = 9;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "10":
                case "Jack":
                case "Queen":
                case "King":
                    cardNum = 10;
                    PlayerScore = PlayerScore + cardNum;
                    break;

            }

            //Creates new card
            Card card = new Card()
            {
                CardVal = cardVal, CardSuit = cardSuit, CardNum = cardNum
            };

            //Prints new card to the format in override method in Card Class
            Console.WriteLine(card);
        }
        
        //Stick method, calls dealer method and compares scores
        static void Stick()
        {
            //dealer now pull cards for itself, must be 17 or over in value
            Console.WriteLine();
            Console.WriteLine("Dealer Plays");
            Dealer();
            Dealer();

            while (DealerScore < 17)
            {
                Dealer();
            }

            Console.WriteLine($"Dealer score is {DealerScore}");

            Console.WriteLine();

            //Compare score values, if won, lost, or bust
            if (PlayerScore > 21)
            {
                Console.WriteLine("Player Busts");
                Console.WriteLine("Dealer Wins");
            }
            else if (DealerScore > 21)
            {
                Console.WriteLine("Dealer Busts");
                Console.WriteLine("Player Wins");
            }
            else
            {
                //compaare dealer and player values
                if(PlayerScore <= 21 && PlayerScore > DealerScore)
                {
                    Console.WriteLine("Player Wins");
                }
                else if (DealerScore <= 21 && PlayerScore < DealerScore)
                {
                    Console.WriteLine("Dealer Wins");
                }
                else
                {
                    Console.WriteLine("It's a draw");
                }
            }

            //Asks if player wants to playagain, resets everything
            Console.WriteLine();
            Console.Write("Would you like to play again? - y/n: ");
            string ans = Console.ReadLine();

            if (ans == "y")
            {
                PlayerScore = 0;
                DealerScore = 0;
                cardNum = 0;
                Console.WriteLine();
                playAgain = true;
            }
            else if (ans == "n")
            {
                playAgain = false;
            }

        }

        //Twist method deals one more card every time it is called
        static void Twist()
        {
            //generates random card and suit
            var rand = new Random();
            string[] cards = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            int newCard = rand.Next(cards.Length);
            string cardVal = cards[newCard];

            string[] suits = { "Hearts", "Clubs", "Spades", "Diamonds" };
            int newSuit = rand.Next(suits.Length);
            string cardSuit = suits[newSuit];

            //adds to score depending on random card pulled
            switch (cardVal)
            {
                case "Ace":
                    if (PlayerScore + cardNum > 21)
                    {
                        cardNum = 1;
                        PlayerScore = PlayerScore + cardNum;

                    }
                    else
                    {
                        cardNum = 11;
                        PlayerScore = PlayerScore + cardNum;
                    }
                    break;

                case "2":
                    cardNum = 2;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "3":
                    cardNum = 3;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "4":
                    cardNum = 4;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "5":
                    cardNum = 5;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "6":
                    cardNum = 6;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "7":
                    cardNum = 7;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "8":
                    cardNum = 8;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "9":
                    cardNum = 9;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "10":
                case "Jack":
                case "Queen":
                case "King":
                    cardNum = 10;
                    PlayerScore = PlayerScore + cardNum;
                    break;

            }

            //Creates new card
            Card card = new Card()
            {
                CardVal = cardVal,
                CardSuit = cardSuit,
                CardNum = cardNum
            };

            //Prints new card to the format in override method in Card Class
            Console.WriteLine(card);
            Console.WriteLine($"Your score is {PlayerScore}");
            Console.WriteLine();

            //asks player if they want to stick or twist
            Console.Write("Do you want to stick or twist, t/s: ");
            string sORt = Console.ReadLine();

            //calls twist or stick function depending on response
            if (sORt == "t")
            {
                Twist();
            }
            else if (sORt == "s")
            {
                Stick();
            }
        }

        //generate dealers cards
        static void Dealer()
        {
            //generates random card and suit
            var rand = new Random();
            string[] cards = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            int newCard = rand.Next(cards.Length);
            string cardVal = cards[newCard];

            string[] suits = { "Hearts", "Clubs", "Spades", "Diamonds" };
            int newSuit = rand.Next(suits.Length);
            string cardSuit = suits[newSuit];


            //adds to score depending on random card pulled
            switch (cardVal)
            {
                case "Ace":
                    if (PlayerScore + cardNum > 21)
                    {
                        cardNum = 1;
                        DealerScore = DealerScore + cardNum;

                    }
                    else
                    {
                        cardNum = 11;
                        DealerScore = DealerScore + cardNum;
                    }
                    break;

                case "2":
                    cardNum = 2;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "3":
                    cardNum = 3;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "4":
                    cardNum = 4;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "5":
                    cardNum = 5;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "6":
                    cardNum = 6;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "7":
                    cardNum = 7;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "8":
                    cardNum = 8;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "9":
                    cardNum = 9;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "10":
                case "Jack":
                case "Queen":
                case "King":
                    cardNum = 10;
                    DealerScore = DealerScore + cardNum;
                    break;

            }

            //Creates new card
            Card card = new Card()
            {
                CardVal = cardVal,
                CardSuit = cardSuit,
                CardNum = cardNum
            };

            //Prints new card to the format in override method in Card Class
            Console.WriteLine(card);
        }

        //----------------------------------BADJACK CODE BELOW (ADDITONAL FEATURE)---------------------------------------------
        
        //Initial dealt cards generator
        static void BadDeal()
        {
            //generates random card and suit
            var rand = new Random();
            string[] cards = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "JOKER" };
            int newCard = rand.Next(cards.Length);
            string cardVal = cards[newCard];

            string[] suits = { "Hearts", "Clubs", "Spades", "Diamonds" };
            int newSuit = rand.Next(suits.Length);
            string cardSuit = suits[newSuit];

            //adds to score depending on random card pulled; if card is joker, jokerSwitch increases by 1
            switch (cardVal)
            {
                case "Ace":
                    if (PlayerScore + cardNum > 21)
                    {
                        cardNum = 1;
                        PlayerScore = PlayerScore + cardNum;

                    }
                    else
                    {
                        cardNum = 11;
                        PlayerScore = PlayerScore + cardNum;
                    }
                    break;

                case "2":
                    cardNum = 2;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "3":
                    cardNum = 3;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "4":
                    cardNum = 4;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "5":
                    cardNum = 5;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "6":
                    cardNum = 6;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "7":
                    cardNum = 7;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "8":
                    cardNum = 8;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "9":
                    cardNum = 9;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "10":
                case "Queen":
                case "King":
                    cardNum = 10;
                    PlayerScore = PlayerScore + cardNum;
                    break;
                
                case "Jack":
                    cardNum = 100;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "JOKER":
                    cardNum = 0;
                    jokerSwitch++;
                    break;

            }

            //Creates new card
            Card card = new Card()
            {
                CardVal = cardVal,
                CardSuit = cardSuit,
                CardNum = cardNum
            };

            //Prints new card to the format in override method in Card Class
            Console.WriteLine(card);
        }

        //Stick method, calls dealer method and compares scores
        static void BadStick()
        {
            //dealer now pull cards for itself, must be 17 or over in value
            Console.WriteLine();
            Console.WriteLine("Dealer Plays");
            Dealer();
            Dealer();

            while (DealerScore < 17)
            {
                Dealer();
            }

            Console.WriteLine($"Dealer score is {DealerScore}");

            Console.WriteLine();

            //Joker card checker
            if(jokerSwitch > 0)
            {
                Console.WriteLine($"You have drawn {jokerSwitch} Joker Cards, you will switch scores {jokerSwitch} times");
                for (int i = 0; i < jokerSwitch; i++)
                {
                    int switchCase;
                    switchCase = PlayerScore;
                    PlayerScore = DealerScore;
                    DealerScore = switchCase;
                    Console.WriteLine("You and the dealer have switched scores");
                    Console.WriteLine();
                }
            }
            
            if(jokerDeal > 0) 
            {
                Console.WriteLine($"Dealer has drawn {jokerDeal} Joker Cards, you will switch scores {jokerDeal} times");
                for (int i = 0; i < jokerDeal; i++)
                {
                    int switchCase;
                    switchCase = PlayerScore;
                    PlayerScore = DealerScore;
                    DealerScore = switchCase;
                    Console.WriteLine("You and the dealer have switched scores");
                    Console.WriteLine();
                }
            }

            //Compare score values, if won, lost, or bust
            if (PlayerScore > 21)
            {
                Console.WriteLine("Player Busts");
                Console.WriteLine("Dealer Wins");
            }
            else if (DealerScore > 21)
            {
                Console.WriteLine("Dealer Busts");
                Console.WriteLine("Player Wins");
            }
            else
            {
                //compaare dealer and player values
                if (PlayerScore <= 21 && PlayerScore > DealerScore)
                {
                    Console.WriteLine("Player Wins");
                }
                else if (DealerScore <= 21 && PlayerScore < DealerScore)
                {
                    Console.WriteLine("Dealer Wins");
                }
                else
                {
                    Console.WriteLine("It's a draw");
                }
            }

            //Asks if player wants to playagain, resets everything
            Console.WriteLine();
            Console.Write("Would you like to play again? - y/n: ");
            string ans = Console.ReadLine();

            if (ans == "y")
            {
                PlayerScore = 0;
                DealerScore = 0;
                cardNum = 0;
                Console.WriteLine();
                playAgain = true;
            }
            else if (ans == "n")
            {
                playAgain = false;
            }

        }

        //Twist method deals one more card every time it is called
        static void BadTwist()
        {
            //generates random card and suit
            var rand = new Random();
            string[] cards = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "JOKER" };
            int newCard = rand.Next(cards.Length);
            string cardVal = cards[newCard];

            string[] suits = { "Hearts", "Clubs", "Spades", "Diamonds" };
            int newSuit = rand.Next(suits.Length);
            string cardSuit = suits[newSuit];

            //adds to score depending on random card pulled; if card is joker, jokerSwitch increases by 1
            switch (cardVal)
            {
                case "Ace":
                    if (PlayerScore + cardNum > 21)
                    {
                        cardNum = 1;
                        PlayerScore = PlayerScore + cardNum;

                    }
                    else
                    {
                        cardNum = 11;
                        PlayerScore = PlayerScore + cardNum;
                    }
                    break;

                case "2":
                    cardNum = 2;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "3":
                    cardNum = 3;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "4":
                    cardNum = 4;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "5":
                    cardNum = 5;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "6":
                    cardNum = 6;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "7":
                    cardNum = 7;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "8":
                    cardNum = 8;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "9":
                    cardNum = 9;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "10":
                case "Queen":
                case "King":
                    cardNum = 10;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "Jack":
                    cardNum = 100;
                    PlayerScore = PlayerScore + cardNum;
                    break;

                case "JOKER":
                    cardNum = 0;
                    jokerSwitch++;
                    break;

            }

            //Creates new card
            Card card = new Card()
            {
                CardVal = cardVal,
                CardSuit = cardSuit,
                CardNum = cardNum
            };

            //Prints new card to the format in override method in Card Class
            Console.WriteLine(card);
            Console.WriteLine($"Your score is {PlayerScore}");
            Console.WriteLine();

            //asks player if they want to stick or twist
            Console.Write("Do you want to stick or twist, t/s: ");
            string sORt = Console.ReadLine();

            //calls twist or stick function depending on response
            if (sORt == "t")
            {
                BadTwist();
            }
            else if (sORt == "s")
            {
                BadStick();
            }
        }

        //generate dealers cards
        static void BadDealer()
        {
            //generates random card and suit
            var rand = new Random();
            string[] cards = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "JOKER" };
            int newCard = rand.Next(cards.Length);
            string cardVal = cards[newCard];

            string[] suits = { "Hearts", "Clubs", "Spades", "Diamonds" };
            int newSuit = rand.Next(suits.Length);
            string cardSuit = suits[newSuit];


            //adds to score depending on random card pulled; if card is joker, jokerSwitch increases by 1
            switch (cardVal)
            {
                case "Ace":
                    if (PlayerScore + cardNum > 21)
                    {
                        cardNum = 1;
                        DealerScore = DealerScore + cardNum;

                    }
                    else
                    {
                        cardNum = 11;
                        DealerScore = DealerScore + cardNum;
                    }
                    break;

                case "2":
                    cardNum = 2;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "3":
                    cardNum = 3;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "4":
                    cardNum = 4;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "5":
                    cardNum = 5;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "6":
                    cardNum = 6;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "7":
                    cardNum = 7;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "8":
                    cardNum = 8;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "9":
                    cardNum = 9;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "10":
                case "Queen":
                case "King":
                    cardNum = 10;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "Jack":
                    cardNum = 100;
                    DealerScore = DealerScore + cardNum;
                    break;

                case "JOKER":
                    cardNum = 0;
                    jokerDeal++;
                    break;

            }

            //Creates new card
            Card card = new Card()
            {
                CardVal = cardVal,
                CardSuit = cardSuit,
                CardNum = cardNum
            };

            //Prints new card to the format in override method in Card Class
            Console.WriteLine(card);
        }

    }

}