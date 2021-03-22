using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.GameField.Entities
{
    class Entity : IEntity, ITarget
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

        public ITarget Target { get; set; }

        public Status Status { get; set; }
        
        public Point Position { get; set; }

        public Entity(Gender gender)
        {
            this.Gender = gender;
            this.Status = Status.ALIVE;
            this.FoodAmount = 20;
        }

        public void PickTarget(List<IEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void MoveToTarget()
        {
            throw new NotImplementedException();
        }
    }
}
