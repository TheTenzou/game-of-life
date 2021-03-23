using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.GameField.Entities;

namespace GameOfLife.GameField
{
    class Field
    {
        private int width;
        private int hight;
        private List<ITarget> targets;
        private Random random = new Random();

        public Field(int width, int hight)
        {
            this.width = width;
            this.hight = hight;

            initFood();
        }

        public Field()
        {
            this.width = 320;
            this.hight = 240;

            initFood();
        }

        private void initFood()
        {
            int foodAmount = random.Next(0, 100);
            for (int i = 0; i < foodAmount; i++)
            {
                ITarget target = new Food(
                    random.Next(0, 10), 
                    random.Next(0, width), 
                    random.Next(0, hight)
                );
                targets.Add(target);
            }
        }

        public void UpdateField()
        {
            foreach(ITarget target in targets)
            {
                if (target is IEntity)
                {
                    IEntity entity = (IEntity)target;
                    entity.PickTarget(targets);
                    entity.MoveToTarget();
                }
            }
        }

        public void GenerateFood()
        {

        }
    }
}
