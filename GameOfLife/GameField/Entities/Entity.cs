using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.GameField.Entities
{
    class Entity : IEntity
    {
        public Gender Gender { get; }

        public int FoodAmount
        {
            get => FoodAmount;
            set
            {
                if (value > 100) FoodAmount = 100;
                else if (value < 1) FoodAmount = 0;
                else FoodAmount = value;
            }
        }

        public Point Destination { get; set; }

        public Status Status { get; set; }
        
        public Point Position { get; set; }

        public int Health { get; set; }

        public Entity(Gender gender)
        {

        }
    }
}
