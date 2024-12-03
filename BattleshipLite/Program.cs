using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static BattleshipLiteLibrary.Models.Enums;

namespace BattleshipLite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DisplayLogic.WelcomeMessage();

            PlayerModel p1 = DisplayLogic.CreatePlayer();
            PlayerModel p2 = DisplayLogic.CreatePlayer();

            Console.WriteLine("Players have set up their ships.");
            DisplayLogic.Wait();

            (PlayerModel attacker, PlayerModel defender) = DisplayLogic.RandomNumberToDecideWhoBegins(p1, p2);

            Console.WriteLine("Let the games begin...");
            DisplayLogic.Wait();
            Console.Clear();
            Console.WriteLine("---- Game start ----\n");

            //PrintLiveGrid(attacker.MyShots);

            while (true)
            {
                DisplayLogic.PrintMessage($"{attacker.Name}");
                bool attackerShotHit = false;
                do
                {
                    attackerShotHit = DisplayLogic.Move(attacker, defender);
                    if (GameLogic.GG)
                    {
                        GameLogic.GameOverEvent(attacker, defender);
                        DisplayLogic.GameOverEvent(defender, attacker);
                    }
                    Console.WriteLine($"{attacker.Name}'s turn again...");

                } while (attackerShotHit);


                DisplayLogic.PrintMessage($"{defender.Name}");
                bool defenderShotHit = false;
                do
                {
                    defenderShotHit = DisplayLogic.Move(defender, attacker);
                    if (GameLogic.GG)
                    {
                        GameLogic.GameOverEvent(defender, attacker);
                        DisplayLogic.GameOverEvent(defender, attacker);
                    }
                } while (defenderShotHit); 
            }





        }
    }

}
