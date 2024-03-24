using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otonom_Hazine_Avcısı
{
    public class Character:PictureBox
    {
        public int boyut;
        Timer timer = new Timer();
        public int x { get; set; }
        public int y { get; set; }
        public static Random rnd = new Random();
        public Location location = new Location();
        public int görüş_alani = 9;
        public Form2 form { get; set; }
        public PictureBox background = new PictureBox();
        public int oran;
        public PictureBox charPicturebox = new PictureBox();
        public List<Point> görüşalanı = new List<Point>();
        
        public Character(int x, int y, int oran, int boyut, Form2 form)
        {
            this.boyut = boyut;
            this.Image = Image.FromFile(@"tarnished.png");
            this.Size = new Size(boyut * oran, boyut * oran);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Location = new Point(x, y);
            this.x = x;
            this.y = y;
            this.form = form;
            this.oran = oran;
            
        }
        List<PictureBox> visiblechests = new List<PictureBox>();
        public void addto_map()
        {

            charPicturebox.Size = new Size(boyut * oran, boyut * oran);
            charPicturebox.Image = Image.FromFile(@"tarnished.png");
            charPicturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            charPicturebox.BackColor = Color.Green;
            charPicturebox.Location = new Point(x, y);

            form.Controls.Add(charPicturebox);
            //background.SendToBack();
            
            timer.Interval = 100;
            timer.Tick += enkısayol;
            timer.Start();
            
        }
        int k = 0;
        int i=0;
        int ilkyol = rnd.Next(0, 4);
        public void enkısayol(object Sender, EventArgs e)
       {
            for(int a = charPicturebox.Location.X/oran-3; a < charPicturebox.Location.X/oran + 4; a++)
            {
                for(int b = charPicturebox.Location.Y / oran - 3; b< charPicturebox.Location.Y/oran + 4; b++)
                {
                    if (görüşalanı.Contains(new Point(a * oran, b * oran)))
                    {
                        continue;
                    }
                    else
                    {
                        görüşalanı.Add(new Point(a * oran, b * oran));
                    }
                }
            }
            if (ilkyol == 0 && i==0)
            {
                charPicturebox.Location = new Point(x, y - 1 * oran);
                i++;
                
            }
            else if(ilkyol == 1 && i == 0)
            {
                charPicturebox.Location = new Point(x, y + 1 * oran);
                i++;
            }
            else if(ilkyol== 2 && i == 0)
            {
                charPicturebox.Location = new Point(x+1*oran, y);
                i++;
            }
            else if (ilkyol == 3 && i == 0)
            {
                charPicturebox.Location = new Point(x-1*oran, y );
                i++;
            }

            
            if (charPicturebox.Location.X == oran)
            {

                charPicturebox.Location = new Point(form.en*oran,charPicturebox.Location.Y);
            }
            int visible_Chests = 0;

            
            int c = 0;
            foreach(PictureBox pictureBox in form.chest_pictureBoxes)
            {
                if (görüşalanı.Contains(pictureBox.Location) ||
                    görüşalanı.Contains(new Point(pictureBox.Location.X + oran, pictureBox.Location.Y)) ||
                    görüşalanı.Contains(new Point(pictureBox.Location.X + 2 * oran, pictureBox.Location.Y)) ||
                    görüşalanı.Contains(new Point(pictureBox.Location.X, pictureBox.Location.Y + oran)) ||
                    görüşalanı.Contains(new Point(pictureBox.Location.X, pictureBox.Location.Y + 2 * oran)) ||
                    görüşalanı.Contains(new Point(pictureBox.Location.X + oran, pictureBox.Location.Y + 2 * oran)) ||
                    görüşalanı.Contains(new Point(pictureBox.Location.X + 2 * oran, pictureBox.Location.Y + oran)) ||
                    görüşalanı.Contains(new Point(pictureBox.Location.X + 2 * oran, pictureBox.Location.Y + 2 * oran))
    )
                {
                    visiblechests.Add(pictureBox);
                    
                    if (k == 0)
                    {
                        form.form_update(pictureBox);
                        
                    }
                    k++;
                    

                }
                c++;
                if (c == 180)
                    break;
            }
            if (visiblechests.Any())
            {
                gotochest1();
            }
            else
            {
                Point newlocation = calculatearea(charPicturebox.Location);
                charPicturebox.Location = newlocation;
            }

            form.ClearCharacterArea(charPicturebox.Location.X, charPicturebox.Location.Y,boyut);
            
        }


        
        
        public void gotochest1()
        {
            Dictionary<Point, double> distances = new Dictionary<Point, double>();

            foreach (PictureBox chest in visiblechests)
            {
                Point chestLocation = chest.Location;
                
                if (!distances.ContainsKey(chestLocation))
                {
                    distances.Add(chestLocation, CalculateDistance(chestLocation, charPicturebox.Location));
                }
            }

            var sortedDistances = distances.OrderBy(kv => kv.Value).ToDictionary(kv => kv.Key, kv => kv.Value);

           
            var closestChestLocation = sortedDistances.First().Key;
            var closestChest = visiblechests.First(chest => chest.Location == closestChestLocation);
            
            
            Timer timer1 = new Timer();
            timer1.Interval = 100; 
            timer1.Tick += (sender, args) =>
            {
                
                if (charPicturebox.Location.X < closestChest.Location.X)
                {
                    charPicturebox.Left += oran;
                }
                else if (charPicturebox.Location.X > closestChest.Location.X)
                {
                    charPicturebox.Left -= oran;
                }

                if (charPicturebox.Location.Y < closestChest.Location.Y)
                {
                    charPicturebox.Top += oran;
                }
                else if (charPicturebox.Location.Y > closestChest.Location.Y)
                {
                    charPicturebox.Top -= oran;
                }

                if (charPicturebox.Location == closestChest.Location)
                {
                    k = 0;
                    form.Controls.Remove(closestChest); 
                    visiblechests.Remove(closestChest);
                    form.chest_pictureBoxes.Remove(closestChest);
                    timer1.Stop();
                    
                }
            };

            timer1.Start();
        }

       
        public Point calculatearea(Point currentlocation)
        {

            Point up = new Point(currentlocation.X, currentlocation.Y - 1 * oran);
            Point down = new Point(currentlocation.X, currentlocation.Y + 1 * oran);
            Point right = new Point(currentlocation.X + 1 * oran, currentlocation.Y);
            Point left = new Point(currentlocation.X - 1 * oran, currentlocation.Y);


            Dictionary<int, Point> distances2 = new Dictionary<int, Point>();
            HashSet<(int, int)> distances1 = new HashSet<(int, int)>();
            List<int> distances = new List<int>();
            int up_distance;
            int down_distance;
            int right_distance;
            int left_distance;

            
            Point up_visible = new Point(currentlocation.X, currentlocation.Y - 4 * oran);
            Point down_visible = new Point(currentlocation.X, currentlocation.Y + 5 * oran);
            Point right_visible = new Point(currentlocation.X + 5 * oran, currentlocation.Y);
            Point left_visible = new Point(currentlocation.X - 4 * oran, currentlocation.Y);
            

            if (!görüşalanı.Contains(up_visible))
            {
                up_distance = Math.Abs(up.Y - currentlocation.Y);
                distances.Add(up_distance);
                distances1.Add((0, up_distance));
                

            }
            else
            {
                up_distance = 100;
                distances1.Add((0, up_distance));
                distances.Add(up_distance);

            }
            if (!görüşalanı.Contains(down_visible))
            {
                down_distance = Math.Abs(down.Y - currentlocation.Y);
                distances1.Add((1, down_distance));
                distances.Add(down_distance);

            }
            else
            {
                down_distance = 100;
                distances.Add(down_distance);
                distances1.Add((1,down_distance));
            }
            if (!görüşalanı.Contains(right_visible))
            {
                right_distance = Math.Abs(right.X - currentlocation.X);
                distances1.Add((2, right_distance));
                distances.Add(right_distance);
            }
            else
            {
                right_distance = 100;
                distances1.Add((2, right_distance));
                distances.Add(right_distance);
            }
            if (!görüşalanı.Contains(left_visible))
            {
                left_distance = Math.Abs(left.X - currentlocation.X);
                distances1.Add((3, left_distance));
                distances.Add(left_distance);
            }
            else
            {
                left_distance = 100;
                distances.Add(left_distance);
                distances1.Add((3, left_distance));
            }

            distances.Sort();
            if(currentlocation.X <= 3 * oran)
            {
                distances.Remove(left_distance);
                distances1.Remove((3,left_distance));
            }
            int x;
            if (distances.Count == 0)
            {
                x = 0;
            }
            else
            {
                x = rnd.Next(0, distances.Count);
            }
            int shortest_distance = distances[0];
            if(right_distance==left_distance && up_distance==down_distance && up_distance==100 && right_distance==100)
            {
                shortest_distance = distances[x];
            }
            if (shortest_distance == up_distance && distances1.Contains((x, shortest_distance)))
            {
                if (görüşalanı.Contains(new Point(currentlocation.X, 0)) )
                {
                    if (form.locations.Contains((down.X,down.Y)))
                    {
                        up = new Point(up.X+oran,up.Y+oran);
                        return calculatearea(up);
                    }
                    else
                        return down;
                }
                else
                {
                    if (form.locations.Contains((up.X, up.Y)) )
                    {
                        down = new Point(down.X+oran,down.Y+oran);
                        return calculatearea(down);
                    }

                    
                        return up;
                }
            }
            else if (shortest_distance == down_distance && distances1.Contains((x, shortest_distance)))
            {
                if (görüşalanı.Contains(new Point(currentlocation.X, form.boy * oran)))
                {
                    if (form.locations.Contains((up.X, up.Y)))
                    {
                        down = new Point(down.X + oran, down.Y + oran);
                        return calculatearea(down);
                    }

                    return up;
                }
                else
                {
                    if (form.locations.Contains((down.X, down.Y)))
                    {
                        up = new Point(up.X + oran, up.Y + oran);
                        return calculatearea(up);
                    }
                    
                        return down;
                }
            }
            else if (shortest_distance == left_distance && distances1.Contains((x, shortest_distance)))
            {
                if (görüşalanı.Contains(new Point(0, currentlocation.Y)))
                {
                    if (form.locations.Contains((right.X, right.Y)) || görüşalanı.Contains(new Point(0, currentlocation.Y)))
                    {
                        left = new Point(left.X + oran, left.Y + oran);
                        return calculatearea(left);
                    }
                    
                        return right ;
                }
                else
                {
                    if (form.locations.Contains((left.X, left.Y)))
                    {right = new Point(right.X + oran, right.Y + oran);
                        return calculatearea(right);
                    }
                    
                    
                        return left;
                }
            }
            else
            {
                if (görüşalanı.Contains(new Point(form.en * oran, currentlocation.Y)) )
                {
                    
                    if (form.locations.Contains((left.X, left.Y)) )
                    {

                        right = new Point(right.X + oran, right.Y + oran);
                        return calculatearea(right);
                        
                    }
                    
                        return left;
                    
                }
                else
                {
                    if (form.locations.Contains((right.X, right.Y)))
                    {left = new Point(left.X + oran, left.Y + oran);
                        return calculatearea(left);
                    }
                    
                    return right;
                }
            }

        }
        
        private double CalculateDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }


    }
}
