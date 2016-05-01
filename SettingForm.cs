using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Bounce
{
    public partial class SettingForm : Form
    {
        Game1 game;
        public SettingForm(Game1 game)
        {
            this.game = game;
            InitializeComponent();
            this.numericUpDown1.Value = game.graphics.PreferredBackBufferWidth;
            this.numericUpDown2.Value = game.graphics.PreferredBackBufferHeight;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                game.Window.IsBorderless = true;
                game.Window.Position = Point.Zero;
                game.graphics.PreferredBackBufferWidth = game.GraphicsDevice.DisplayMode.Width;
                game.graphics.PreferredBackBufferHeight = game.GraphicsDevice.DisplayMode.Height;

            }
            else
            {
                game.Window.IsBorderless = false;
                game.graphics.PreferredBackBufferWidth = (int)this.numericUpDown1.Value;
                game.graphics.PreferredBackBufferHeight = (int)this.numericUpDown2.Value;
            }

            game.graphics.ApplyChanges();

            Close();

        }
    }
}
