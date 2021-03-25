using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Entities;
using GameOfLife.Entities.Creatures;
using GameOfLife.Entities.Foods;

namespace GameOfLife.GameField
{
    class Field
    {
        private int width = 320;
        private int hight = 240;
        public List<IEntity> entities { get; }
        private Random random = new Random();

        private static Field instance;

        private Field()
        {
            entities = new List<IEntity>();

            Creature entityMale = new Creature(Gender.MALE);
            entityMale.Position = new Point(20, 50);
            entityMale.FoodAmount = 20;

            entities.Add(entityMale);

            Creature entityFemale = new Creature(Gender.FEMALE);
            entityFemale.Position = new Point(202, 180);
            entityFemale.FoodAmount = 20;

            entities.Add(entityFemale);

            GenerateFood();
        }

        public static Field GetInstance()
        {
            if (instance == null)
            {
                instance = new Field();
            }
            return instance;
        }

        public void AddEntitiy(IEntity entity)
        {
            this.entities.Add(entity);
        }

        public void UpdateField()
        {
            foreach(IEntity target in entities)
            {
                if (target is Creature)
                {
                    ICreature entity = (ICreature)target;
                    entity.PickTarget(entities);
                    entity.MoveToTarget();
                }
                
            }
        }

        public void GenerateFood()
        {
            int foodAmount = random.Next(0, 100);
            for (int i = 0; i < foodAmount; i++)
            {
                IEntity target = new Food(
                    random.Next(0, 30), 
                    random.Next(0, width), 
                    random.Next(0, hight)
                );
                if (target.Position == null)
                {
                    Console.WriteLine("what !!!!!");
                }
                entities.Add(target);
            }
        }
    }
}
