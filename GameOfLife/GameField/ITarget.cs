using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.GameField
{
    interface ITarget
    {
        Point Position { get; set; }
    }
}
