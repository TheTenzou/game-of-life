using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.GameField.Entities
{
    interface IEntity
    {
        Gender Gender { get; }

        int FoodAmount { get; set; }

        Point Destination { get; set; }

        Status Status { get; set; }
    }
}
