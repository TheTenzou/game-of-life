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
        private IEntity[,] grid;



        public IEntity GetGridEntiry(Point position)
        {
            return grid[position.X, position.Y];
        }
    }
}
