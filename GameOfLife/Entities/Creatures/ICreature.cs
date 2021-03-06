using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Entities;
using GameOfLife.Entities.Creatures;

namespace GameOfLife.Entities.Creatures
{
    interface ICreature : IEntity
    { 
        Gender Gender { get; }

        int FoodAmount { get; set; }

        IEntity Target { get; set; }

        Status Status { get; set; }

        void PickTarget(List<IEntity> targets);

        void MoveToTarget();
    }
}
