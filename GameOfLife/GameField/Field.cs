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
        private int width;
        private int hight;
        private List<IEntity> entities;
        private List<Food> food;

        public Field(int width, int hight)
        {
            this.width = width;
            this.hight = hight;
        }

        public void AddEntity(IEntity entity)
        {
            entities.Add(entity);
        }

        public List<IEntity> GetEntities(Point position)
        {
            return entities;
        }


        public void AddFood(Food food)
        {
            this.food.Add(food);
        }

        public List<Food> GetFood()
        {
            return food;
        }

        public void UpdateField()
        {
            throw new NotImplementedException();
        }
    }
}
