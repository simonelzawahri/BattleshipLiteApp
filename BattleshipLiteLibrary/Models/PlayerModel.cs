using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleshipLiteLibrary.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public List<GridSpotModel> MyShips { get; set; }
        public List<GridSpotModel> MyShots { get; set; }
        public int Guess { get; set; }
        public static int Count { get; set; }


        public PlayerModel()
        {
            Count++;
            Name = "";
            MyShips = new List<GridSpotModel>();
            MyShots = new List<GridSpotModel>();
        }

        public PlayerModel(string name, List<GridSpotModel> myships, List<GridSpotModel> myshots)
        {
            Name = name;
            MyShips = myships;
            MyShots = myshots;
            Count++;
        }

        public int GetNumberOfMySunkShips()
        {
            int count = 0;
            foreach (GridSpotModel gsm in MyShips)
            {
                if (gsm.Status == Enums.GridSpotStatus.Hit)
                {
                    count++;
                }
            }
            return count;
        }

        public void PrintMyShips()
        {
            Console.WriteLine(Name + "'s Ships:");
            int count = 1;
            foreach (GridSpotModel m in MyShips)
            {
                Console.WriteLine($"Ship #{count} --> {m.ToString()}");
                count++;
            }
        }
        public void PrintMyShots()
        {
            Console.WriteLine(Name + "'s Shots:");
            int count = 1;
            foreach (GridSpotModel m in MyShots)
            {
                Console.WriteLine($"Shot #{count} --> {m.ToString()}");
                count++;
            }
        }
        public void AddShip(GridSpotModel gridSpot)
        {
            MyShips.Add(gridSpot);
        }

        public void AddShot(GridSpotModel gridSpot)
        {
            MyShots.Add(gridSpot);
        }







    }
}
