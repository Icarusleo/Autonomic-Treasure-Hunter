﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otonom_Hazine_Avcısı.StaticEngeller
{
    public class Mountain:PictureBox
    {
        public int boyut;
        public int x { get; set; }
        public int y { get; set; }
        public Random rnd = new Random();
        public Location location = new Location();

        public Mountain(int x, int y, int oran, int boyut,int en)
        {
            this.boyut = boyut;
            if (x < en * oran / 2)
            {
                
                this.Image = Image.FromFile(@"wintermountain.png");
            }
            else
            {
                this.Image = Image.FromFile(@"mountain1.png");
            }
            
            this.Size = new Size(boyut * oran, boyut * oran);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Location = new Point(x, y);

        }

    }
}
