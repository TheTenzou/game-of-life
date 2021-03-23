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
using GameOfLife.GameField.Entities;

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
            field = new Field(320, 240);
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
            foreach(ITarget item in field.Targets)
            {
                drawItems(e, item);
            }

        }

        void drawItems(PaintEventArgs e, ITarget item)
        {
            if (item is Food)
            {
                graphics = e.Graphics;
                graphics.DrawEllipse(Pens.Green, getRectangle(item.Position));
            }
            else if (item is Entity)
            {
                drawEntity(item);
            }
        }

        void drawEntity(ITarget item)
        {
            Entity entity = (Entity)item;
            switch (entity.Gender)
            {
                case Gender.MALE:
                    graphics.DrawEllipse(Pens.LightBlue, getRectangle(item.Position));
                    break;
                case Gender.FEMALE:
                    graphics.DrawEllipse(Pens.Crimson, getRectangle(item.Position));
                    break;
            }
        }

        private Rectangle getRectangle(GameOfLife.GameField.Point point)
        {
            int windowWidth = this.Right;
            int windowHight = this.Bottom;

            int newX = (int)(point.X * (((double)windowWidth) / width));
            int newY = (int)(point.Y * (((double)windowHight) / hight));

            return new Rectangle(newX, newY, 5, 5);
        }
    }
}
