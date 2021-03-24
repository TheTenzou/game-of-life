using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Targets.Entities
{
    interface IEntity
    {
        Gender Gender { get; }

        int FoodAmount { get; set; }

        ITarget Target { get; set; }

        Status Status { get; set; }

        Point Position { get; set; }

        void PickTarget(List<ITarget> targets);

        void MoveToTarget();
    }
}
