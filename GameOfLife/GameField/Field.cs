using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Targets;
using GameOfLife.Targets.Entities;
using GameOfLife.Targets.Foods;

namespace GameOfLife.GameField
{
    class Field
    {
        private int width = 320;
        private int hight = 240;
        public List<ITarget> Targets { get; }
        private Random random = new Random();

        private static Field instance;

        private Field()
        {
            Targets = new List<ITarget>();

            Entity entityMale = new Entity(Gender.MALE);
            entityMale.Position = new Point(20, 50);
            entityMale.FoodAmount = 20;

            Targets.Add(entityMale);

            Entity entityFemale = new Entity(Gender.FEMALE);
            entityFemale.Position = new Point(202, 180);
            entityFemale.FoodAmount = 20;

            Targets.Add(entityFemale);

            GenerateFood();
        }

        public static Field getInstance()
        {
            if (instance == null)
            {
                instance = new Field();
            }
            return instance;
        }


        public void UpdateField()
        {
            foreach(ITarget target in Targets)
            {
                if (target is Entity)
                {
                    IEntity entity = (IEntity)target;
                    entity.PickTarget(Targets);
                    entity.MoveToTarget();
                }
                
            }
        }

        public void GenerateFood()
        {
            int foodAmount = random.Next(0, 100);
            for (int i = 0; i < foodAmount; i++)
            {
                ITarget target = new Food(
                    random.Next(0, 30), 
                    random.Next(0, width), 
                    random.Next(0, hight)
                );
                if (target.Position == null)
                {
                    Console.WriteLine("what !!!!!");
                }
                Targets.Add(target);
            }
        }
    }
}
