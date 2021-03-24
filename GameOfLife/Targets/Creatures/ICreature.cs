using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Targets;
using GameOfLife.Targets.Creatures;

namespace GameOfLife.Targets.Creatures
{
    interface ICreature : IEntity
    { 
        Gender Gender { get; }

        int FoodAmount { get; set; }

        IEntity Target { get; set; }

        Status Status { get; set; }

        Point Position { get; set; }

        void PickTarget(List<IEntity> targets);

        void MoveToTarget();
    }
}
