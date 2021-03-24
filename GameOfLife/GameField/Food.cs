using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.GameField
{
    class Food : AbstractTarget, ITarget
    {
        public int Amount { get; set; }

        public Food(int amount, int x, int y)
        {
            this.Amount = amount;
            this.Position = new Point(x, y);
        }

        public Food(int amount, Point point)
        {
            this.Amount = amount;
            this.Position = point;
        }

        public Food() { }
    }
}
