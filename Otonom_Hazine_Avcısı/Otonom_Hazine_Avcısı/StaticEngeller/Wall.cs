using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otonom_Hazine_Avcısı.StaticEngeller
{
    public class Wall: PictureBox
    {
        public int boyut;
        public int[][] alanx;
        public int[][] alany;
        public Random rnd = new Random();
        public Location location = new Location();
        

        public Wall(int x, int y, int oran, int boyut,int en)
        {
            this.boyut = boyut;
            if (x < en * oran / 2)
            {
                this.Image = Image.FromFile(@"winterwall.png");
            }
            else
            {
                this.Image = Image.FromFile(@"wall.png");
            }
            int a = rnd.Next(0, 2);
            if (a == 0) 
                this.Size = new Size(1*oran, boyut * oran);
            else
                this.Size = new Size(boyut*oran,oran);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Location = new Point(x, y);

        }
    }
}
