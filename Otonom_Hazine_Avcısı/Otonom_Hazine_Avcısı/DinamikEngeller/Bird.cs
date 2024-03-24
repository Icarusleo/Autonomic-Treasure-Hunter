using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Otonom_Hazine_Avcısı.DinamikEngeller
{
    public class Bird: PictureBox
    {
        public int boyut;
        public int x { get; set; }
        public int y { get; set; }
        public Random rnd = new Random();
        public Location location = new Location();
        public int görüş_alani = 9;
        public Form2 form {  get; set; }
        public PictureBox background =new PictureBox();
        public int oran;
        public PictureBox birdPicturebox = new PictureBox();

        public Bird(int x, int y, int oran, int boyut,Form2 form)
        {
            this.boyut = boyut;
            this.Image = Image.FromFile(@"bird.png");
            this.Size = new Size(boyut * oran, boyut * oran);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Location = new Point(x, y);
            this.x = x;
            this.y = y;
            this.form= form;
            this.oran = oran;
        }

        public void addto_map()
        {

            background.Size = new Size(boyut * oran, (boyut+9) * oran);
            background.Image = Image.FromFile(@"background.png");
            background.SizeMode = PictureBoxSizeMode.StretchImage;
            background.BackColor = Color.Red;
            background.Location = new Point(x, y);
            birdPicturebox.Size = new Size(boyut * oran, boyut * oran);
            form.Controls.Add(background);
            birdPicturebox.Image = Image.FromFile(@"bird.png");
            birdPicturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            birdPicturebox.BackColor = Color.Red;
            birdPicturebox.Location = new Point(x, y);

            form.Controls.Add(birdPicturebox);
            background.SendToBack();
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Move;
            timer.Start();

        }
        bool moveright = true;
        public void Move(object Sender, EventArgs e)
        {
            if (background.Bottom >= birdPicturebox.Bottom && moveright == true)
            {
                if (background.Bottom== birdPicturebox.Bottom)
                {
                    moveright = false;
                    return;
                }
                birdPicturebox.Top+= 1 * oran;
            }
            else if (moveright == false)
            {
                if (background.Top == birdPicturebox.Top)
                {
                    moveright = true;
                    return;
                }
                birdPicturebox.Top -= 1 * oran;

            }
        }
    }
}
