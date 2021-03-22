using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.GameField
{
    class Food : ITarget
    {
        public Point Position { get; set; }

        public int Amount { get; set; }
    }
}
