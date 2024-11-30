using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System;
using System.Threading;

namespace CA2
{
    class Program
    {
        int PlayerScore = 0;
        int DealerScore = 0;
        static void Main(string[] args)
        {
            //Asking player what version of the game they want to play, normal BlackJack or BadJack <--- Additional Feature
            Console.WriteLine("What version of the game do you want to play?");
            Console.Write("BlackJack or BadJacks - 1/2: ");
            string gamein = Console.ReadLine();
            int game = int.Parse(gamein);

            //If player inputs 1, they have selected normal BlackJack
            if (game == 1)
            {
                Console.WriteLine();
                Console.WriteLine("Goal: Get the closest to 21 in card value without going over.");
                Console.WriteLine("Kings, Queens, and Jacks are worth 10, Aces are worth 11 or 1");
                Console.WriteLine("Player Vs. PC Dealer");
                Console.WriteLine();

                //Call Game
            }
            
            //If player inputs 2, they have selected BadJacks <--- Additional Feature
            else if (game == 2)
            {
                Console.WriteLine();
                Console.WriteLine("Goal: Get the closest to 21 in card value without going over.");
                Console.WriteLine("Kings and Queens are worth 10, Aces are worth 11 or 1");
                Console.WriteLine("Be careful: JACKS are an instant loss card. JOKERS switch you and the dealers cards.");
                Console.WriteLine("Player Vs. PC Dealer");
                Console.WriteLine();

                //Call Game
            }
           
            //If anything else is inputed, player gets this message and the application closes
            else
            {
                Console.WriteLine("Not an option");
                Console.WriteLine("Closing game in 5 seconds...");
                Thread.Sleep(5000);
            }
        }

        public void Deal()
        {
            var rand = new Random();
            string[] card = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            int newCate = rand.Next(card.Length);

        }
        public void Stick()
        {
            //If statment for values win or bust
            //dealer now pull cards for itself, must be 17 or over in value

            //compaare dealer and player values
        }
        public void Twist()
        {

        }
    }
}