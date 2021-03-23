using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.GameField.Entities
{
    class Entity : AbstractTarget, IEntity, ITarget
    {
        private Random random = new Random();
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

        public void PickTarget(List<ITarget> targets)
        {
            int rand = random.Next() % 100;

            if (this.Target != null)
            {
                if (this.FoodAmount < 10)
                {
                    Target = lookForFood(targets);// add random
                }
                return;
            }
            pickRandomTarget(targets);
        }

        private ITarget lookForFood(List<ITarget> targets)
        {

            List<ITarget> listOfFood = targets.Where(t => t is Food).ToList();

            ITarget closestFood = listOfFood.FirstOrDefault(null);

            double distance = 0.0;

            foreach(ITarget target in listOfFood)
            {
                if 
            }
        }

        private void pickRandomTarget(List<ITarget> targets)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                int index = random.Next(targets.Count);

                ITarget newTarget = targets[index];
                
               if (isTargetValid(newTarget))
                {
                    this.Target = newTarget;
                    return;
                }
            }
        }

        private bool isTargetValid(ITarget target)
        {
            if (target is Food)
            {
                return true;
            }
            else if (target is IEntity && ((IEntity)target).Gender != this.Gender)
            {
                return true;
            }

            return false;
        }
        
        public void MoveToTarget()
        {
            throw new NotImplementedException();
        }
    }
}
