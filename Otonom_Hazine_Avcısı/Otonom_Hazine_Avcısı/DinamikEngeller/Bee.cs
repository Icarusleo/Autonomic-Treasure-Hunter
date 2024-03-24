using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otonom_Hazine_Avcısı.DinamikEngeller
{
    public class Bee : PictureBox 
    {
        public int boyut;

        public int oran;
        public int x {  get; set; }
        public int y {  get; set; } 
        public Random rnd = new Random();
        public Location location = new Location();
        public int görüş_alani = 5;
        public PictureBox beePicturebox = new PictureBox();
        public PictureBox background = new PictureBox();
        Form2 form {  get; set; } 
        public Bee(int x, int y, int oran, int boyut,Form2 form)
        {
            this.boyut = boyut;
            this.Image = Image.FromFile(@"bee.png");
            this.Size = new Size(boyut*oran, boyut*oran);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Location = new Point(x, y);
            this.x = x;
            this.y = y;
            this.form = form;
            this.oran= oran;
        }
        
        public void addto_map()
        {

            background.Size = new Size((boyut+5) * oran, boyut * oran);
            background.Image = Image.FromFile(@"background.png");
            background.SizeMode = PictureBoxSizeMode.StretchImage;
            background.BackColor = Color.Red;
            background.Location = new Point(x, y);
            beePicturebox.Size = new Size(boyut*oran,boyut*oran);
            form.Controls.Add(background);
            beePicturebox.Image = Image.FromFile(@"bee.png");
            beePicturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            beePicturebox.BackColor = Color.Red;
            beePicturebox.Location = new Point(x, y);
            
            form.Controls.Add(beePicturebox);
            background.SendToBack();
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Move;
            timer.Start();
            
        }
        bool moveright=true;
        public void Move(object Sender, EventArgs e)
        {
            if(background.Right>=beePicturebox.Right && moveright==true)
            {
                if (background.Right == beePicturebox.Right)
                {
                    moveright = false;
                    return;
                }
                beePicturebox.Left += 1*oran;
            }
            else if(moveright==false)
            {
                if (background.Left == beePicturebox.Left)
                {
                    moveright=true;
                    return;
                }
                beePicturebox.Left -= 1*oran;
                
            }
        }

        
    }
}
