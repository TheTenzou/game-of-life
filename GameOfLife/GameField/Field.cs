﻿using System;
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
        public List<ITarget> Targets { get; }
        private Random random = new Random();

        public Field(int width, int hight)
        {
            this.width = width;
            this.hight = hight;

            Targets = new List<ITarget>();

            Entity entityMale = new Entity(Gender.MALE);
            entityMale.Position = new Point(170, 120);
            entityMale.FoodAmount = 20;

            Targets.Add(entityMale);

            Entity entityFemale = new Entity(Gender.FEMALE);
            entityFemale.Position = new Point(175, 125);
            entityFemale.FoodAmount = 20;

            Targets.Add(entityFemale);

            GenerateFood();
        }

        public Field() : this(320, 240) { } 

        public void UpdateField()
        {
            foreach(ITarget target in Targets)
            {
                if (target is Entity)
                {
                    IEntity entity = (IEntity)target;
                    entity.PickTarget(Targets);
                    entity.MoveToTarget();
                }
                
            }
        }

        public void GenerateFood()
        {
            int foodAmount = random.Next(0, 100);
            for (int i = 0; i < foodAmount; i++)
            {
                ITarget target = new Food(
                    random.Next(0, 30), 
                    random.Next(0, width), 
                    random.Next(0, hight)
                );
                Targets.Add(target);
            }
        }
    }
}
