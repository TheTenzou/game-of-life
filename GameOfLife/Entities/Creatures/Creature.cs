using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.GameField;
using GameOfLife.Entities.Foods;

namespace GameOfLife.Entities.Creatures
{
    class Creature : AbstractEntity, ICreature, IEntity
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

        public IEntity Target { get; set; }

        public Status Status { get; set; }

        private int width = 320;
        private int hight = 240;

        public Creature(Gender gender)
        {
            this.Gender = gender;
            this.Status = Status.ALIVE;
            this.FoodAmount = 20;
        }

        public Creature(Gender gender, int width, int hight) : this(gender)
        {
            this.width = width;
            this.hight = hight;
        }

        public void PickTarget(List<IEntity> targets)
        {

            if (this.Target == null)
            {
                pickRandomTarget(targets);
                return;
            }

            if (this.FoodAmount > 10)
            {
                return;
            }

            List<IEntity> listOfFood = targets.Where(t => t is Food).ToList();

            if (listOfFood.Count > 0)
            {
                Target = lookForFood(targets);
            }
        }

        private IEntity lookForFood(List<IEntity> listOfFood)
        {
            IEntity closestFood = listOfFood.First();

            double distanceToClosest = Double.MaxValue;

            foreach(IEntity target in listOfFood)
            {
                if (distanceToClosest > this.Distance(target) && target != this)
                {
                    closestFood = target;
                    distanceToClosest = this.Distance(target);
                }
            }
            return closestFood;
        }

        private void pickRandomTarget(List<IEntity> targets)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                int index = random.Next(0, targets.Count);

                IEntity newTarget = targets[index];
                
                if (isTargetValid(newTarget))
                {
                    this.Target = newTarget;
                    return;
                }
            }
        }

        private bool isTargetValid(IEntity target)
        {
            if (target is Food)
            {
                return true;
            }
            else if (target is IEntity && ((Creature)target).Gender != this.Gender)
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
                return;
            }

            double len = this.Distance(Target);

            if (len < 2.0)
            {
                interactWithTarget();
                return;
            }

            int incX = (int) ((Target.Position.X - this.Position.X) / len * 2);
            int incY = (int) ((Target.Position.Y - this.Position.Y) / len * 2);

            inverIfOutOfBounce(ref incX, ref incY);

            this.Position.X += incX;
            this.Position.Y += incY;

            this.FoodAmount -= (int)len;
        }

        private void interactWithTarget()
        {
            if (this.Target is Food)
            {
                //Console.WriteLine($"Gender {this.Gender}");
                //Console.WriteLine($"Before eating food: {this.FoodAmount}");

                Food food = (Food)this.Target;
                this.FoodAmount += food.Amount;
                Field.GetInstance().RemoveEntity(food);
                this.Target = null;

                //Console.WriteLine($"After eating food: {this.FoodAmount}");
                //Console.WriteLine("======================================");
            }
            else if (this.Target is Creature)
            {
                Creature creature = (Creature)this.Target;
                
            }
        }

        private void moveInRandomDirection()
        {
            int velocity = 2;
            double direction = random.NextDouble() * 2 * Math.PI;

            int incX = (int)(velocity * Math.Cos(direction));
            int incY = (int)(velocity * Math.Sin(direction));

            inverIfOutOfBounce(ref incX, ref incY);

            this.Position.X += incX;
            this.Position.Y += incY;
        }

        private void inverIfOutOfBounce(ref int incX, ref int incY)
        {
            if (Position.X + incX < 0) incX = -incX;
            if (Position.X + incX > width) incX = -incX;
            if (Position.Y + incY < 0) incY = -incY;
            if (Position.Y + incY > hight) incY = -incY;
        }
    }
}
