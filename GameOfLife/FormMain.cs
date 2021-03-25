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
        private int width = 320;
        private int hight = 240;

        public FormMain()
        {
            InitializeComponent();
            field = Field.getInstance();
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
            foreach(IEntity item in field.Targets)
            {
                drawItems(e, item);
            }

        }

        void drawItems(PaintEventArgs e, IEntity item)
        {
            graphics = e.Graphics;
            if (item is Food)
            {
                //graphics.FillEllipse(Brushes.Green, getRectangle(item.Position));
                graphics.FillEllipse(Brushes.Green, item.Position.X, item.Position.Y, 10, 10);
            }
            else if (item is Creature)
            {
                drawEntity(item);
            }
        }

        void drawEntity(IEntity item)
        {
            Creature entity = (Creature)item;
            switch (entity.Gender)
            {
                case Gender.MALE:
                    //graphics.FillEllipse(Brushes.LightBlue, getRectangle(item.Position));
                    graphics.FillEllipse(Brushes.LightBlue, entity.Position.X, entity.Position.Y, 10, 10);
                    break;
                case Gender.FEMALE:
                    //graphics.FillEllipse(Brushes.Crimson, getRectangle(item.Position));
                    graphics.FillEllipse(Brushes.Crimson, entity.Position.X, entity.Position.Y, 10, 10);
                    break;
            }
            if (entity.Target != null)
            {
                //graphics.DrawLine(Pens.Red, convertPoint(entity.Position), convertPoint(entity.Target.Position));
                graphics.DrawLine(Pens.Red, entity.Position.X,entity.Position.Y, entity.Target.Position.X, entity.Target.Position.Y);
            }
        }

        private Rectangle getRectangle(Point point)
        {
            PointSD newPont = convertPoint(point);

            return new Rectangle(newPont.X, newPont.Y, 10, 10);
        }

        private PointSD convertPoint(Point point)
        {
            int windowWidth = this.Right;
            int windowHight = this.Bottom;

            int newX = (int)(point.X * (((double)windowWidth) / width));
            int newY = (int)(point.Y * (((double)windowHight) / hight));

            return new PointSD(newX-5, newY-5);
        }
    }
}
