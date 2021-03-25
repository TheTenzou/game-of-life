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
        public Gender Gender { get; set; }

        private int age = 0;

        public int stepsAfterGiveBirth = 100;

        private void decStepsAfterGiveBirth()
        {
            if (stepsAfterGiveBirth > 0)
                stepsAfterGiveBirth--;
        }

        private int food;
        public int FoodAmount
        {
            get => food;
            set
            {
                if (value > 100) food = 100;
                else if (value < 1)
                {
                    food = 0;
                    this.Status = Status.DEAD;
                    //Field.GetInstance().RemoveEntity(this);
                }
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
            this.Status = Status.CHILD;
            this.FoodAmount = 20;
        }

        public Creature(Gender gender, int width, int hight) : this(gender)
        {
            this.width = width;
            this.hight = hight;
        }

        public void PickTarget(List<IEntity> targets)
        {
            if (this.Status == Status.DEAD)
                return;


            if (this.Target == null)
            {
                pickRandomTarget(targets);
                return;
            }

            if (this.FoodAmount > 40)
            {
                List<IEntity> listOfCreatures = targets.Where(it => 
                        it is ICreature 
                        && ((Creature)it).Status == Status.ADULT
                        && ((Creature)it).Gender != this.Gender
                        && this.stepsAfterGiveBirth < 1 
                        && ((Creature)it).stepsAfterGiveBirth < 1).ToList();

                if (listOfCreatures.Count > 0)
                    Target = lookForClosestTarget(listOfCreatures);
                Console.WriteLine(this.Target.GetType());
                return;
            }

            List<IEntity> listOfFood = targets.Where(it => it is Food).ToList();

            if (listOfFood.Count > 0)
            {
                Target = lookForClosestTarget(listOfFood);
            }
        }

        private IEntity lookForClosestTarget(List<IEntity> listOfTargets)
        {
            IEntity closestFood = listOfTargets.First();

            double distanceToClosest = Double.MaxValue;

            foreach(IEntity target in listOfTargets)
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
            
            bool isAdultOppositeGender =
                target is ICreature 
                && ((Creature)target).Status == Status.ADULT
                && ((Creature)target).Gender != this.Gender
                && this.stepsAfterGiveBirth < 1 
                && ((Creature)target).stepsAfterGiveBirth < 1; 
            
            if (isAdultOppositeGender)
            {
                Console.WriteLine(((Creature)target).Status);
                return true;
            }

            return false;
        }

        public void MoveToTarget()
        {
            if (this.Status == Status.DEAD)
                return;

            if (Target == null)
            {
                moveInRandomDirection();
                return;
            }

            double len = this.Distance(Target);

            if (len < 5.0)
            {
                interactWithTarget();
                return;
            }

            int incX = (int) ((Target.Position.X - this.Position.X) / len * 5);
            int incY = (int) ((Target.Position.Y - this.Position.Y) / len * 5);

            inverIfOutOfBounce(ref incX, ref incY);

            this.Position.X += incX;
            this.Position.Y += incY;

            this.FoodAmount -= 1;
            this.age += 1;
            if (this.age > 50)
            {
                this.Status = Status.ADULT;
            }
            decStepsAfterGiveBirth();
        }

        private void interactWithTarget()
        {
            if (this.Target is Food)
            {

                Food food = (Food)this.Target;
                this.FoodAmount += food.Amount;
                Field.GetInstance().RemoveEntity(food);
                this.Target = null;
            }
            bool isAdultOppositeGender =
                this.Target is ICreature 
                && ((Creature)this.Target).Status == Status.ADULT
                && ((Creature)this.Target).Gender != this.Gender
                && this.stepsAfterGiveBirth < 1 
                && ((Creature)this.Target).stepsAfterGiveBirth < 1;
            if ( isAdultOppositeGender)
            {
                Creature creature = (Creature)this.Target;
                reproduce(creature);
                this.Target = null;
            }
        }

        private void reproduce(Creature creature)
        {
            stepsAfterGiveBirth = 50;
            creature.stepsAfterGiveBirth = 50;
            if (this.FoodAmount < 20 && creature.FoodAmount < 20)
            {
                createChild();
                createChild();
            }
            else if (this.FoodAmount < 10 && creature.FoodAmount < 10)
            {
                createChild();
            }
            else if (this.FoodAmount < 40 && creature.FoodAmount < 40)
            {
                createChild();
                createChild();
                createChild();
            }
            else if (this.FoodAmount < 60 && creature.FoodAmount < 60)
            {
                createChild();
                createChild();
                createChild();
                createChild();
            }
        }

        private void createChild()
        {
            Type type = typeof(Gender);
            Array values = type.GetEnumValues();
            int index = random.Next(values.Length);
            Gender value = (Gender)values.GetValue(index);

            int offset = random.Next(-5, 5);

            Creature child = new Creature(value);
            child.Position = new Point(this.Position.X + offset, this.Position.Y + offset);
            child.FoodAmount = 40;

            Field.GetInstance().AddEntitiy(child);
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

            this.FoodAmount -= 1;
            this.age += 1;
            decStepsAfterGiveBirth();
            if (this.age > 50)
            {
                this.Status = Status.ADULT;
            }
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
