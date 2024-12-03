using BattleshipLiteLibrary.Models;
using BattleshipLiteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace BattleshipLite
{
    public static class DisplayLogic
    {

        public static void WelcomeMessage()
        {
            Console.WriteLine("+=================================================================+\r\n|    ____        __  __  __          __    _       __    _ __     |\r\n|   / __ )____ _/ /_/ /_/ /__  _____/ /_  (_)___  / /   (_) /____ |\r\n|  / __  / __ `/ __/ __/ / _ \\/ ___/ __ \\/ / __ \\/ /   / / __/ _ \\|\r\n| / /_/ / /_/ / /_/ /_/ /  __(__  ) / / / / /_/ / /___/ / /_/  __/|\r\n|/_____/\\__,_/\\__/\\__/_/\\___/____/_/ /_/_/ .___/_____/_/\\__/\\___/ |\r\n|                                       /_/                       |\r\n+=================================================================+");
            //Console.WriteLine("+==========================================================+\r\n|░█▀▄░█▀█░▀█▀░▀█▀░█░░░█▀▀░█▀▀░█░█░▀█▀░█▀█░░░█░░░▀█▀░▀█▀░█▀▀|\r\n|░█▀▄░█▀█░░█░░░█░░█░░░█▀▀░▀▀█░█▀█░░█░░█▀▀░░░█░░░░█░░░█░░█▀▀|\r\n|░▀▀░░▀░▀░░▀░░░▀░░▀▀▀░▀▀▀░▀▀▀░▀░▀░▀▀▀░▀░░░░░▀▀▀░▀▀▀░░▀░░▀▀▀|\r\n+==========================================================+");
            //Console.WriteLine("+-----------------------------------------------------------------+\r\n| _       __     __                             ______            |\r\n|| |     / /__  / /________  ____ ___  ___     /_  __/___         |\r\n|| | /| / / _ \\/ / ___/ __ \\/ __ `__ \\/ _ \\     / / / __ \\        |\r\n|| |/ |/ /  __/ / /__/ /_/ / / / / / /  __/    / / / /_/ /        |\r\n||__/|__/\\___/_/\\___/\\____/_/ /_/ /_/\\___/ _  /_/  \\____/_ __     |\r\n|   / __ )____ _/ /_/ /_/ /__  _____/ /_  (_)___  / /   (_) /____ |\r\n|  / __  / __ `/ __/ __/ / _ \\/ ___/ __ \\/ / __ \\/ /   / / __/ _ \\|\r\n| / /_/ / /_/ / /_/ /_/ /  __(__  ) / / / / /_/ / /___/ / /_/  __/|\r\n|/_____/\\__,_/\\__/\\__/_/\\___/____/_/ /_/_/ .___/_____/_/\\__/\\___/ |\r\n|                                       /_/                       |\r\n+-----------------------------------------------------------------+");
        }

        public static void PrintDemoGrid()
        {

            Console.WriteLine("     -- GRID -- ");
            Console.WriteLine("---------------------");
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    switch (i)
                    {
                        case 1:
                            Console.Write($" A{j} ");
                            if (j == 5) Console.WriteLine("\n");
                            break;
                        case 2:
                            Console.Write($" B{j} ");
                            if (j == 5) Console.WriteLine("\n");
                            break;
                        case 3:
                            Console.Write($" C{j} ");
                            if (j == 5) Console.WriteLine("\n");
                            break;
                        case 4:
                            Console.Write($" D{j} ");
                            if (j == 5) Console.WriteLine("\n");
                            break;
                        case 5:
                            Console.Write($" E{j} ");
                            if (j == 5) Console.WriteLine("\n");
                            break;
                    }
                }
            }
            Console.WriteLine("---------------------");
            Console.WriteLine();
        }

        public static PlayerModel CreatePlayer()
        {
            DisplayLogic.PrintDemoGrid();

            PlayerModel player = new PlayerModel();
            player.Name = GetPlayerName().ToUpper();
            player.MyShips = Get5Ships(player);
            Console.WriteLine();
            Console.WriteLine("Next Player...");
            DisplayLogic.Wait();
            Console.WriteLine();
            Console.Clear();
            return player;
        }

        public static string GetPlayerName()
        {
            Console.WriteLine("--- Player Setup ---");

            // get name from user , validate name, return as string
            Console.WriteLine($"Welcome Player {PlayerModel.Count}.");
            bool isGoodName = false;
            do
            {
                Console.Write($"What is your name? ");
                string name = Console.ReadLine();
                string pattern = "^[A-Za-z]{2,12}$";
                isGoodName = Regex.IsMatch(name, pattern);
                if (isGoodName)
                {
                    Console.WriteLine($"Thank you {name.ToUpper()}. \n");
                    return name;
                }
                else
                {
                    Console.WriteLine("Invalid name... Please enter only letters.");
                }
            } while (!isGoodName);
            return "";
        }

        public static List<GridSpotModel> Get5Ships(PlayerModel player)
        {
            List<GridSpotModel> output = new List<GridSpotModel>();
            int shipNum = 1;
            do
            {
                //ask for 1 ship spot
                Console.Write($"{player.Name}, Please enter ship spot #{shipNum} (ex: D3): -->  ");
                string shipSpot = Console.ReadLine();
                //validate 1 ship spot
                bool spotValid = GameLogic.ValidateSpot(shipSpot);
                if (spotValid)
                {
                    //create obj and confirm entry
                    GridSpotModel gsm = GameLogic.StringToGridSpot(shipSpot);
                    Console.WriteLine($"You entered: --> {gsm.SpotLetter}{gsm.SpotNumber}");
                    Console.WriteLine();
                    //check if gsm in myships
                    bool spotTaken = GameLogic.IsGridSpotInList(gsm, player.MyShips);
                    if (!spotTaken)
                    {
                        //add to MyShips
                        output.Add(gsm);
                        player.AddShip(gsm);
                        player.PrintMyShips();
                        Console.WriteLine();
                        Console.WriteLine($"Valid entry. Ship spot #{shipNum} added at: --> {gsm.ToString()}.\n");
                        Console.WriteLine();
                        shipNum++;
                    }
                    else
                    {
                        Console.WriteLine("Spot Taken, please enter another spot.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid entry... Try again.\n");
                }

            } while (shipNum <= 5);

            Console.WriteLine($"Saving {player.Name}'s ships...");
            player.PrintMyShips();
            Console.WriteLine($"Thank you {player.Name}. Set up complete.");
            DisplayLogic.Wait();

            return output;
        }

        public static (PlayerModel, PlayerModel) RandomNumberToDecideWhoBegins(PlayerModel p1, PlayerModel p2)
        {
            // random number who ever is closest goes first 
            // Generates a random number between 1 and 100
            Console.WriteLine("\n\nTo determine who will go first, \n" +
                "each player will guess a number between 1 and 100.\n" +
                "Who ever is closest begins.");
            Random rand = new Random();
            int randomNumber = rand.Next(1, 101);
            Console.Write("Generating random number...\n");
            Wait();
            Console.WriteLine();


            // ask users for their guess
            Console.WriteLine();
            Console.Write($"{p1.Name} please enter your guess: ");
            p1.Guess = int.Parse(Console.ReadLine());
            Console.Write($"{p2.Name} please enter your guess: ");
            p2.Guess = int.Parse(Console.ReadLine());
            // check who is closest
            Console.WriteLine($"The random number was: {randomNumber}");
            int p1difference = Math.Abs(randomNumber - p1.Guess);
            int p2difference = Math.Abs(randomNumber - p2.Guess);
            (PlayerModel, PlayerModel) output = (null, null);
            if (p1difference < p2difference)
            {
                Console.WriteLine($"{p1.Name} is the attacker. They will begin.");
                output = (p1, p2);
            }
            else if (p1difference > p2difference)
            {
                Console.WriteLine($"{p2.Name} is the attacker. They will begin.");
                output = (p2, p1);
            }
            else
            {
                Console.WriteLine("It is a tie!");
                Console.WriteLine("Lets try again...");
                RandomNumberToDecideWhoBegins(p1, p2);
            }
            return output;
        }

        public static void Wait()
        {
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
        }



        public static bool Move(PlayerModel attacker, PlayerModel defender)
        {
            // attacker takes shot on defender - if hit attacker gets another Move
            GridSpotModel validatedShot = DisplayLogic.TakeShot(attacker);
            // determine if shot hit or miss on defender
            bool attackerShotHit = GameLogic.IsHit(validatedShot, defender);
            if (attackerShotHit)
            {
                GameLogic.ShotHitEvent(attacker, validatedShot, defender);
                GameLogic.GG = DisplayLogic.CheckGameOver(attacker, defender);
                Console.WriteLine(validatedShot.ToString() + " is a HIT!");
                Console.WriteLine($"{defender.Name} has {GameLogic.GetShipsRemaining(defender)} ship(s) remaining.");
                Console.WriteLine();
                return true;
            }
            else
            {
                GameLogic.ShotMissEvent(attacker, validatedShot, defender);
                Console.WriteLine(validatedShot.ToString() + " is a MISS!");
                Console.WriteLine($"{defender.Name} has {GameLogic.GetShipsRemaining(defender)} ship(s) remaining.");
                Console.WriteLine($"\nPlayer switch -- {defender.Name}'s turn...");
                DisplayLogic.Wait();
                Console.Clear();

                return false;
            }

        }

        public static GridSpotModel TakeShot(PlayerModel attacker)
        {
            bool alreadyShotAtThisSpot = true;
            GridSpotModel validatedShot;
            do
            {
                // ask attacker for shot - return GSM
                validatedShot = AskForShotSpot(attacker);
                // check if attacker already shot at this spot - if true then ask again
                alreadyShotAtThisSpot = GameLogic.CheckIfAttackerAlreadyShotAtThisSpot(attacker, validatedShot);
                if (alreadyShotAtThisSpot)
                {
                    Console.WriteLine($"Already shot at this spot {attacker.Name}! \nTry another spot.");
                }
            } while (alreadyShotAtThisSpot);
            return validatedShot;
        }

        private static GridSpotModel AskForShotSpot(PlayerModel player)
        {
            bool validShotSpot = false;
            do
            {
                // ask for shot
                Console.Write($"{player.Name.ToUpper()}, Please enter your shot #{player.MyShots.Count() + 1}: --> ");
                string shotSpot = Console.ReadLine();
                // validate
                validShotSpot = GameLogic.ValidateSpot(shotSpot);
                if (validShotSpot)
                {
                    // create OBJ and confirm entry and return valid shot
                    GridSpotModel validShot = GameLogic.StringToGridSpot(shotSpot);
                    Console.WriteLine($"You entered: --> {validShot.SpotLetter}{validShot.SpotNumber.ToString()}");
                    return validShot;
                }
                else
                {
                    Console.WriteLine($"You entered: --> {shotSpot}");
                    Console.WriteLine("Invalid entry... Try again.");
                }
            } while (!validShotSpot);
            return null;

        }



        public static void GoodbyeMessage()
        {
            Console.WriteLine("\n\n+=============================================================================================+\r\n|  ________                __            ____                      __            _            |\r\n| /_  __/ /_  ____ _____  / /_______    / __/___  _____     ____  / /___ ___  __(_)___  ____ _|\r\n|  / / / __ \\/ __ `/ __ \\/ //_/ ___/   / /_/ __ \\/ ___/    / __ \\/ / __ `/ / / / / __ \\/ __ `/|\r\n| / / / / / / /_/ / / / / ,< (__  )   / __/ /_/ / /       / /_/ / / /_/ / /_/ / / / / / /_/ / |\r\n|/_/ /_/ /_/\\__,_/_/ /_/_/|_/____/   /_/  \\____/_/       / .___/_/\\__,_/\\__, /_/_/ /_/\\__, /  |\r\n|                                                       /_/            /____/        /____/   |\r\n+=============================================================================================+");
        }

        public static void PrintMessage(string message)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine(message);
            Console.WriteLine("-----------------");
        }

        public static void PrintGameStats(PlayerModel attacker, PlayerModel defender)
        {
            Console.WriteLine($"\n---- Player Stats ----");
            Console.WriteLine($" --- {attacker.Name} ---");
            Console.WriteLine($" Number of shots taken:  {attacker.MyShots.Count()}");
            Console.WriteLine($" Number of ships hit:  {defender.GetNumberOfMySunkShips()}");
            Console.WriteLine($" --- {defender.Name} ---");
            Console.WriteLine($" Number of shots taken:  {defender.MyShots.Count()}");
            Console.WriteLine($" Number of ships hit:  {attacker.GetNumberOfMySunkShips()}");
            Console.WriteLine($"---- Player Stats ----\n");
        }

        public static bool CheckGameOver(PlayerModel attacker, PlayerModel defender)
        {
            bool gg = GameLogic.IsGameOver(attacker, defender);
            // if gg then set GameLogic.GG GameLogic.Winner
            if (gg)
            {
                GameLogic.GG = true;
                GameLogic.Winner = attacker;
                GameLogic.Loser = defender;
                return true;
            }
            return false;
        }

        public static void GameOverEvent(PlayerModel attacker, PlayerModel defender)
        {
            Console.WriteLine("------ Game Over ------");
            PrintGameStats(attacker, defender);
            Console.WriteLine("---- Game Over ----");
            Wait();
            Wait();
            GoodbyeMessage();
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
