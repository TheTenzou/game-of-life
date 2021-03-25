using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Entities
{
    interface IEntity
    {
        Point Position { get; set; }
    }
}
