using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameOfLife.GameField;
using GameOfLife.Entities;
using GameOfLife.Entities.Creatures;
using GameOfLife.Entities.Foods;
using PointSD = System.Drawing.Point;
using Point = GameOfLife.Entities.Point;

namespace GameOfLife
{
    public partial class FormMain : Form
    {
        private Field field;
        private int cycleCount = 0;
        private Graphics graphics;

        public FormMain()
        {
            InitializeComponent();
            field = Field.GetInstance();
        }

        private void updateField_Tick(object sender, EventArgs e)
        {
            field.UpdateField();
            if (cycleCount > 100)
            {
                field.GenerateFood();
                cycleCount = 0;
            }
            cycleCount++;
            Invalidate();
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            foreach(IEntity item in field.entities)
            {
                drawEntity(e, item);
            }

        }

        void drawEntity(PaintEventArgs e, IEntity item)
        {
            graphics = e.Graphics;
            if (item is Food)
            {
                graphics.FillEllipse(Brushes.Green, getRectangle(item.Position));
                //graphics.FillEllipse(Brushes.Green, item.Position.X, item.Position.Y, 10, 10);
            }
            else if (item is Creature)
            {
                drawCreature(item);
            }
        }

        void drawCreature(IEntity item)
        {
            Creature entity = (Creature)item;
            if (entity.Status != Status.DEAD)
            {
                drawAliveCreature(entity);
            }
            else
            {
                //graphics.FillEllipse(Brushes.Gray, entity.Position.X, entity.Position.Y, 10, 10);
                graphics.FillEllipse(Brushes.Gray, getRectangle(entity.Position));
            }
        }

        private void drawAliveCreature(Creature entity)
        {
            if (entity.Status == Status.ADULT)
            {
                drawAdult(entity);
            }
            else
            {
                drawChild(entity);
            }

            if (entity.Target != null)
            {
                graphics.DrawLine(Pens.Red, convertPoint(entity.Position), convertPoint(entity.Target.Position));
            }
        }

        private void drawAdult(Creature entity)
        {
            switch (entity.Gender)
            {
                case Gender.MALE:
                    graphics.FillEllipse(Brushes.Blue, getRectangle(entity.Position));
                    break;
                case Gender.FEMALE:
                    graphics.FillEllipse(Brushes.Crimson, getRectangle(entity.Position));
                    break;
            }
        }

        private void drawChild(Creature entity)
        {
            switch (entity.Gender)
            {
                case Gender.MALE:
                    graphics.FillEllipse(Brushes.Aqua, getRectangle(entity.Position));
                    break;
                case Gender.FEMALE:
                    graphics.FillEllipse(Brushes.Pink, getRectangle(entity.Position));
                    break;
            }
        }

        private Rectangle getRectangle(Point point)
        {
            PointSD newPont = convertPoint(point);

            return new Rectangle(newPont.X, newPont.Y, 10, 10);
        }

        private PointSD convertPoint(Point point)
        {
            int windowWidth = this.Width - 10;
            int windowHight = this.Height - 10;

            int newX = (int)(point.X * (((double)windowWidth) / 320));
            int newY = (int)(point.Y * (((double)windowHight) / 240));

            return new PointSD(newX-5, newY-5);
        }
    }
}
