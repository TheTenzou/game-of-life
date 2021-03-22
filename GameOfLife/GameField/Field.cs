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
        private List<IEntity> entities;
        private List<int> food;

        public Field(int width, int hight)
        {
            this.width = width;
            this.hight = hight;
        }

        public void Setntity(Point position, IEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEntity GetEntiry(Point position)
        {
            throw new NotImplementedException();
        }


        public void SetFood(Point position, int amount)
        {
            throw new NotImplementedException();
        }

        public int GetFood(Point postion)
        {
            throw new NotImplementedException();
        }

        public void UpdateField()
        {
            throw new NotImplementedException();
        }

        private void UpdateCell(int i, int j)
        {
            throw new NotImplementedException();
        }
    }
}
