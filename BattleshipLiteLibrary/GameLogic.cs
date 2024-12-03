using BattleshipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BattleshipLiteLibrary
{
    public static class GameLogic
    {
        public static bool GG = false;

        public static PlayerModel Winner;

        public static PlayerModel Loser;

        public static bool ValidateSpot(string spot)
        {
            string pattern = "^[A-Ea-e][1-5]$";
            bool isValidSpot = Regex.IsMatch(spot, pattern);
            return isValidSpot;
        }

        public static GridSpotModel StringToGridSpot(string spot)
        {
            GridSpotModel gsm = new GridSpotModel(spot[0].ToString(), int.Parse(spot[1].ToString()), Enums.GridSpotStatus.Ship);
            return gsm;
        }

        public static bool IsGridSpotInList(GridSpotModel spot, List<GridSpotModel> list)
        {
            foreach (GridSpotModel x in list)
            {
                if (x.SpotLetter.Equals(spot.SpotLetter) && x.SpotNumber == spot.SpotNumber)
                {
                    return true;
                }
            }
            return false;
        }

        public static GridSpotModel GetGridSpotFromList(GridSpotModel gridSpot, List<GridSpotModel> list)
        {
            foreach (var spot in list)
            {
                if (spot.SpotLetter.Equals(gridSpot.SpotLetter) && spot.SpotNumber == gridSpot.SpotNumber)
                {
                    return spot;
                }
            }
            return null;
        }

        public static bool IsHit(GridSpotModel validatedShot, PlayerModel defender)
        {
            // Check if validatedShot is in defender's ship list - if so its a hit
            GridSpotModel defenderSpot = GameLogic.GetGridSpotFromList(validatedShot, defender.MyShips);
            if (defenderSpot != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool CheckIfAttackerAlreadyShotAtThisSpot(PlayerModel attacker, GridSpotModel validatedShot)
        {
            GridSpotModel foundSpot = GetGridSpotFromList(validatedShot, attacker.MyShots);
            if (foundSpot != null)
            {
                return true;
            }
            return false;
        }

        public static int CheckGameOver(PlayerModel attacker, PlayerModel defender)
        {
            int count = 0;
            // if defender has no more ships to defend --> GG
            foreach (GridSpotModel spot in defender.MyShips)
            {
                if (spot.Status == Enums.GridSpotStatus.Ship)
                {
                    count++;
                }

            }
            return count;
        }

        public static bool ValidateGuess(string guess)
        {
            string pattern = "^[0-9][1-2]$";
            bool isValidGuess = Regex.IsMatch(guess, pattern);
            return isValidGuess;
        }

        public static bool IsGameOver(PlayerModel attacker, PlayerModel defender)
        {
            // check for gamover - if defender.MyShips has no GridSpots that are Status SHIP - GameOver
            // return int of how many ships defender has remaining
            int shipsRemaining = GameLogic.CheckGameOver(attacker, defender);
            if (shipsRemaining == 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public static int GetShipsRemaining(PlayerModel player)
        {
            int count = 0;
            foreach (GridSpotModel ship in player.MyShips)
            {
                if (ship.Status == Enums.GridSpotStatus.Ship)
                {
                    count++;
                }
            }
            return count;
        }


        public static void ShotHitEvent(PlayerModel attacker, GridSpotModel validatedShot, PlayerModel defender)
        {
            // change spot in defender.MyShips - status hit
            GridSpotModel defenderSpot = GameLogic.GetGridSpotFromList(validatedShot, defender.MyShips);
            defenderSpot.Status = Enums.GridSpotStatus.Hit;
            // add shot to attacker.MyShots - status hit
            attacker.AddShot(defenderSpot);
        }

        public static void ShotMissEvent(PlayerModel attacker, GridSpotModel validatedShot, PlayerModel defender)
        {
            // set spot - status miss
            validatedShot.Status = Enums.GridSpotStatus.Miss;
            // add new shot to defender.MyShips - status miss
            defender.AddShip(validatedShot);
            // add new shot to attacker.MyShots - status miss
            attacker.AddShot(validatedShot);
        }

        public static void GameOverEvent(PlayerModel attacker, PlayerModel defender)
        {

            GameLogic.Winner = attacker;
            GameLogic.Loser = defender;

        }
    }

}
