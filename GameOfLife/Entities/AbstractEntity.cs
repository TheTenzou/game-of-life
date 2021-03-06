using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Entities
{
    abstract class AbstractEntity : IEntity
    {
        public Point Position { get; set; }

        public double Distance(IEntity target)
        {
            return Math.Sqrt(Math.Pow(this.Position.X - target.Position.X, 2) + Math.Pow(this.Position.Y - target.Position.Y, 2));
        }
    }
}
