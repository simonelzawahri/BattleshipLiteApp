using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleshipLiteLibrary.Models.Enums;

namespace BattleshipLiteLibrary.Models
{
    public class GridSpotModel
    {
        public string SpotLetter { get; set; }
        public int SpotNumber { get; set; }
        public GridSpotStatus Status { get; set; } = GridSpotStatus.Empty;

        private string spotLetter;

        //public string SpotLetter
        //{
        //    get
        //    {
        //        return spotLetter;
        //    }
        //    set
        //    {
        //        spotLetter = value;
        //    }
        //}

        //private int spotNumber;

        //public int SpotNumber
        //{
        //    get
        //    {
        //        return spotNumber;
        //    }
        //    set
        //    {
        //        spotNumber = value;
        //    }
        //}



        // for each given status, change color of grid spot or replace with X or O
        // Empty = color, Full = color, Miss = 2, Hit = 3
        public GridSpotModel(string letter, int number, GridSpotStatus status)
        {
            SpotLetter = letter.ToUpper();
            SpotNumber = number;
            Status = status;
        }

        public GridSpotModel(string letter, int number)
        {
            SpotLetter = letter.ToUpper();
            SpotNumber = number;
            Status = GridSpotStatus.Empty;
        }



        public bool Equals(GridSpotModel spot)
        {
            if (this.SpotLetter.ToUpper().Equals(spot.SpotLetter.ToUpper()) && this.SpotNumber == spot.SpotNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            string x = $"[{SpotLetter}{SpotNumber}--{Status.ToString()}]";
            return x;
        }
    }
}
