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

namespace GameOfLife
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            new Field(10, 10);
        }
    }
}
