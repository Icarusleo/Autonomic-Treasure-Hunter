using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otonom_Hazine_Avcısı.Chests
{
    public class copperchest:PictureBox
    {
        public int boyut;

        public int x { get; set; }
        public int y { get; set; }
        public Random rnd = new Random();
        public Location location = new Location();
        public int görüş_alani = 9;
        public Form2 form { get; set; }
        public PictureBox background = new PictureBox();
        public int oran;
        public PictureBox chestPicturebox = new PictureBox();

        public string name = "Copper Chest";
        public copperchest(int x, int y, int oran, int boyut, Form2 form)
        {
            this.boyut = boyut;
            this.Image = Image.FromFile(@"copperchest.png");
            this.Size = new Size(boyut * oran, boyut * oran);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Location = new Point(x, y);
            this.x = x;
            this.y = y;
            this.form = form;
            this.oran = oran;

        }

        public void addto_map()
        {

            chestPicturebox.Size = new Size(boyut * oran, boyut * oran);
            chestPicturebox.Image = Image.FromFile(@"copperchest.png");
            chestPicturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            chestPicturebox.BackColor = Color.Yellow;
            chestPicturebox.Location = new Point(x, y);

            form.Controls.Add(chestPicturebox);

        }
    }
}
