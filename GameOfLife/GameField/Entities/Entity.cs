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

        private int food;
        public int FoodAmount
        {
            get => food;
            set
            {
                if (value > 100) food = 100;
                else if (value < 1) food = 0;
                else food = value;
            }
        }

        public ITarget Target { get; set; }

        public Status Status { get; set; }
        
        public Point Position { get; set; }

        private int width = 320;
        private int hight = 240;

        public Entity(Gender gender)
        {
            this.Gender = gender;
            this.Status = Status.ALIVE;
            this.FoodAmount = 20;
        }

        public Entity(Gender gender, int width, int hight) : this(gender)
        {
            this.width = width;
            this.hight = hight;
        }

        public void PickTarget(List<ITarget> targets)
        {
            int rand = random.Next() % 100;

            if (this.Target != null)
            {
                if (this.FoodAmount < 10)
                {
                    List<ITarget> listOfFood = targets.Where(t => t is Food).ToList();

                    if (listOfFood.Count > 0)
                    {
                        Target = lookForFood(targets);
                    }
                }
                return;
            }
            pickRandomTarget(targets);
        }

        private ITarget lookForFood(List<ITarget> listOfFood)
        {
            ITarget closestFood = listOfFood.FirstOrDefault(null);

            double distanceToClosest = 0.0;

            foreach(ITarget target in listOfFood)
            {
                if (distanceToClosest < this.Distance(target))
                {
                    closestFood = target;
                    distanceToClosest = this.Distance(target);
                }
            }
            return closestFood;
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
            if (Target == null)
            {
                moveInRandomDirection();
            }
            else
            { 
                int velocity = 2;

                double tang = (Target.Position.X - this.Position.X) / (Target.Position.Y - this.Position.Y);

                int incX = (int)(velocity * tang);
                int incY = (int)(velocity * tang);

                if (Position.X + incX < 0) incX = -incX;
                if (Position.X + incX > width) incX = -incX;
                if (Position.Y + incY < 0) incY = -incY;
                if (Position.Y + incY > hight) incY = -incY;

                Position.X += incX;
                Position.Y += incY;
            }

            //Console.WriteLine($"position {Gender}: {Position.X}, {Position.Y}");
            Console.WriteLine($"position {Gender}: target {Target}");
        }

        private void moveInRandomDirection()
        {
            int velocity = 2;
            double direction = random.NextDouble() * 2 * Math.PI;

            int incX = (int)(velocity * Math.Cos(direction));
            int incY = (int)(velocity * Math.Sin(direction));

            if (Position.X + incX < 0) incX = -incX;
            if (Position.X + incX > width) incX = -incX;
            if (Position.Y + incY < 0) incY = -incY;
            if (Position.Y + incY > hight) incY = -incY;

            Position.X += incX;
            Position.Y += incY;
        }
    }
}
