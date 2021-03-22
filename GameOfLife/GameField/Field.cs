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

        public Field(int width, int hight)
        {
            this.width = width;
            this.hight = hight;
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
            throw new NotImplementedException();
        }
    }
}
