using Otonom_Hazine_Avcısı.Chests;
using Otonom_Hazine_Avcısı.DinamikEngeller;
using Otonom_Hazine_Avcısı.StaticEngeller;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Otonom_Hazine_Avcısı
{
    public partial class Form2 : Form
    {
        public int boy;
        public int en;
        public int oran = 10;

        Random rnd = new Random();

        PictureBox sis = new PictureBox();

        public HashSet<(int,int)> locations = new HashSet<(int, int)>();
        public static List<int> locationsx = new List<int>();
        public static List<int> locationsy = new List<int>();
        public static List<int> bee_locations = new List<int>();
        public static List<int> bird_locations = new List<int>();   

        public List<PictureBox> chest_pictureBoxes = new List<PictureBox>();

        public List <PictureBox> bird_pictureboxes = new List<PictureBox>();
        public Form2(int boy, int en)
        {
            InitializeComponent();
            this.boy = boy;
            this.en = en;
            add_chests();
            ObjeleriOlustur();
            char_ekle(); 
        }


        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);
            for (int i = 0; i <= boy * oran;)
            {
                graphics.DrawLine(pen, i, 0, i, boy * oran);
                graphics.DrawLine(pen, 0, i, en * oran, i);
                i = i + oran;
            }

        }

        public void ObjeleriOlustur()
        {

            int numTrees = rnd.Next(8, 10);
            int numBees = rnd.Next(1, 3);
            int numBirds = rnd.Next(1, 3);
            int numMountains = rnd.Next(2, 5);
            int numWalls = rnd.Next(2, 6);
            int numRocks = rnd.Next(8, 10);

            for (int i = 0; i < numBees; i++)
            {
                int x, y;
                int boyut = 2;

                do
                {
                    x = rnd.Next(0, (en - boyut-7) * oran);
                } while (x % oran != 0);

                do
                {
                    y = rnd.Next(0, (boy - boyut ) * oran);
                } while (y % oran != 0);

                int x_index = x / oran;
                int y_index = y / oran;
                Bee bee = new Bee(x, y, oran, boyut, this);
                if (!EngelleriKontrolEt(x_index, y_index, boyut))
                {
                    for (int a = x_index; a <= x_index +boyut+ bee.görüş_alani; a++)
                    {
                        for (int b = y_index; b <= y_index + boyut; b++)
                        {
                            engelMatrisi[a , b  ] = true;
                            locations.Add((a*oran, b*oran));
                        }
                    }
                    
                    
                    bee.addto_map();

                }
                else
                {

                    i--;
                }

            }

            for (int i = 0; i < numBirds; i++)
            {
                int x, y;
                int boyut = 2;
                do
                {
                    x = rnd.Next(0, (en - boyut) * oran);
                } while (x % oran != 0);

                do
                {
                    y = rnd.Next(0, (boy - boyut - 9) * oran);
                } while (y % oran != 0);


                int x_index = x / oran;
                int y_index = y / oran;
                Bird bird = new Bird(x, y, oran, boyut, this);
                if (!EngelleriKontrolEt(x_index, y_index, boyut))
                {
                    for (int a = x_index; a <= x_index + boyut; a++)
                    {
                        for (int b = y_index; b <= y_index + boyut + bird.görüş_alani; b++)
                        {
                            engelMatrisi[a, b] = true;
                            locations.Add((a * oran, b * oran));
                        }
                    }
                   
                    bird.addto_map();
                    
                }
                else
                {

                    i--;
                }

            }


                for (int i = 0; i < numTrees; i++)
            {

                int x, y;
                int boyut = rnd.Next(2, 6);
                x = rnd.Next(0, en - boyut);
                y = rnd.Next(0, boy - boyut);
                x = x * oran;
                y = y * oran;

                Tree tree = new Tree(x, y, oran, boyut, en);

                PictureBox tree_picturebox = new PictureBox();

                tree_picturebox.Size = tree.Size;
                tree_picturebox.Location = new Point(x, y);

                tree_picturebox.Image = tree.Image;
                tree_picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
                tree_picturebox.BackColor = Color.Black;
                tree.Location = new Point(x, y);

                int x_index = x / oran;
                int y_index = y / oran;

                if (!EngelleriKontrolEt(x_index, y_index, boyut))
                {

                    for (int a = x_index; a <= x_index + boyut; a++)
                    {
                        for (int b = y_index; b <= y_index + boyut; b++)
                        {
                            engelMatrisi[a, b] = true;
                            locations.Add((a * oran, b * oran));
                        }
                    }

                    this.Controls.Add(tree_picturebox);
                    locationsx.Add(x);
                    locationsy.Add(y);

                }
                else
                {
                    i--;
                }


            }

            Location location = new Location(locationsx, locationsy);


            for (int i = 0; i < numRocks; i++)
            {
                int x, y;
                int boyut = rnd.Next(2, 4);
                x = rnd.Next(0, en - boyut);
                y = rnd.Next(0, boy - boyut);
                x = x * oran;
                y = y * oran;



                Kaya rock = new Kaya(x, y, oran, boyut,en);

                PictureBox rock_pictureBox = new PictureBox();

                rock_pictureBox.Size = rock.Size;
                rock_pictureBox.Location = new Point(x, y);

                rock_pictureBox.Image = rock.Image;
                rock_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                rock_pictureBox.BackColor = Color.Black;
                rock.Location = new Point(x, y);

                int x_index = x / oran;
                int y_index = y / oran;

                if (!EngelleriKontrolEt(x_index, y_index, boyut))
                {

                    for (int a = x_index; a <= x_index + boyut; a++)
                    {
                        for (int b = y_index; b <= y_index + boyut; b++)
                        {
                            engelMatrisi[a, b] = true;
                            locations.Add((a * oran, b * oran));
                        }
                    }
                    this.Controls.Add(rock_pictureBox);
                    locationsx.Add(x);
                    locationsy.Add(y);
                    
                }
                else
                {
                    i--;
                }

            }

            for (int i = 0; i < numMountains; i++)
            {
                int x, y;
                int boyut = 15;
                x = rnd.Next(0, en - boyut);
                y = rnd.Next(0, boy - boyut);
                x = x * oran;
                y = y * oran;



                Mountain mountain = new Mountain(x, y, oran, boyut,en);

                PictureBox mountain_pictureBox = new PictureBox();

                mountain_pictureBox.Size = mountain.Size;
                mountain_pictureBox.Location = mountain.Location;
                
                mountain_pictureBox.Image = mountain.Image;
                mountain_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                mountain_pictureBox.BackColor = Color.Black;
                mountain.Location = new Point(x, y);

                int x_index = x / oran;
                int y_index = y / oran;

                if (!EngelleriKontrolEt(x_index, y_index, boyut))
                {
                    for (int a = x_index; a <= x_index + boyut; a++)
                    {
                        for (int b = y_index; b <= y_index + boyut; b++)
                        {
                            engelMatrisi[a, b] = true;
                            locations.Add((a * oran, b * oran));
                        }
                    }
                    this.Controls.Add(mountain_pictureBox);
                    
                    locationsx.Add(x);
                    locationsy.Add(y);
                }
                else
                {

                    i--;
                }

            }

            for (int i = 0; i < numWalls; i++)
            {
                int x, y;
                int boyut = 10;
                x = rnd.Next(0, en - boyut);
                y = rnd.Next(0, boy - boyut);
                x = x * oran;
                y = y * oran;

                Wall wall = new Wall(x, y, oran, boyut, en);

                PictureBox wall_pictureBox = new PictureBox();

                wall_pictureBox.Size = wall.Size;
                wall_pictureBox.Location = new Point(x, y);
                wall_pictureBox.Image = wall.Image;
                wall_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                wall_pictureBox.BackColor = Color.Black;
                wall.Location = new Point(x, y);

                int x_index = x / oran;
                int y_index = y / oran;

                if (!EngelleriKontrolEt(x_index, y_index, boyut))
                {
                    for (int a = x_index; a <= x_index + boyut; a++)
                    {
                        for (int b = y_index; b <= y_index + boyut; b++)
                        {
                            engelMatrisi[a, b] = true;
                            locations.Add((a * oran, b * oran));
                        }
                    }
                    this.Controls.Add(wall_pictureBox);
                    
                    locationsx.Add(x);
                    locationsy.Add(y);
                    
                }
                else
                {
                    i--;
                }
            }
            sis.Image = Image.FromFile(@"sis1.png");
            
            sis.Size = new Size(en*oran, boy*oran);
            sis.Location = new Point(0, 0);
            this.Controls.Add(sis);

        }

        public void add_chests()
        {
            List<int> liste = new List<int>();
            int chestcount = 5;
            int tur = 0;
            for (int i = 0; i < chestcount; i++)
            {
                int x, y;
                int boyut = 2;
                do
                {
                    x = rnd.Next(0, en - boyut);
                    y = rnd.Next(0, boy - boyut);
                    x = x * oran;
                    y = y * oran;
                    int x_index = x / oran;
                    int y_index = y / oran;

                   
                    if (!EngelleriKontrolEt(x_index, y_index, boyut))
                    {
                        goldenchest goldenchest = new goldenchest(x, y, oran, boyut, this);
                        for (int a = x_index; a <= x_index + boyut; a++)
                        {
                            for (int b = y_index; b <= y_index + boyut; b++)
                            {
                                engelMatrisi[a, b] = true;
                                
                            }
                        }
                        goldenchest.chestPicturebox.Name = "Altın Sandık";
                        chest_pictureBoxes.Add(goldenchest.chestPicturebox);
                        goldenchest.addto_map();
                       
                        break;
                    }
                } while (true);
            }
            for (int i = 0; i < chestcount; i++)
            {
                int x, y;
                int boyut = 2;
                do
                {
                    x = rnd.Next(0, en-boyut);
                    y = rnd.Next(0, boy-boyut);
                    x = x * oran;
                    y = y * oran;
                    int x_index = x / oran;
                    int y_index = y / oran;


                    if (!EngelleriKontrolEt(x_index, y_index, boyut))
                    {
                        silverchest silverchest = new silverchest(x, y, oran, boyut, this);
                        for (int a = x_index; a <= x_index + boyut; a++)
                        {
                            for (int b = y_index; b <= y_index + boyut; b++)
                            {
                                engelMatrisi[a, b] = true;
                                
                            }
                        }
                        silverchest.chestPicturebox.Name = "Gümüş Sandık";
                        chest_pictureBoxes.Add(silverchest.chestPicturebox);
                        silverchest.addto_map();
                        
                        break;
                    }
                } while (true);
            }
            for (int i = 0; i < chestcount; i++)
            {
                int x, y;
                int boyut = 2;
                do
                {
                    x = rnd.Next(0, en - boyut);
                    y = rnd.Next(0, boy - boyut);
                    x = x * oran;
                    y = y * oran;
                    int x_index = x / oran;
                    int y_index = y / oran;

                    if (!EngelleriKontrolEt(x_index, y_index, boyut))
                    {
                        emeraldchest character = new emeraldchest(x, y, oran, boyut, this);
                        for (int a = x_index; a <= x_index + boyut; a++)
                        {
                            for (int b = y_index; b <= y_index + boyut; b++)
                            {
                                engelMatrisi[a, b] = true;
                                
                            }
                        }
                        character.chestPicturebox.Name = "Zümrüt Sandık";
                        chest_pictureBoxes.Add(character.chestPicturebox);
                        character.addto_map();
                       
                        break;
                    }
                } while (true);
            }
            for (int i = 0; i < chestcount; i++)
            {
                int x, y;
                int boyut = 2;
                do
                {
                    x = rnd.Next(0, en - boyut);
                    y = rnd.Next(0, boy - boyut);
                    x = x * oran;
                    y = y * oran;
                    int x_index = x / oran;
                    int y_index = y / oran;

                   
                    if (!EngelleriKontrolEt(x_index, y_index, boyut))
                    {
                        copperchest character = new copperchest(x, y, oran, boyut, this);
                        for (int a = x_index; a <= x_index + boyut; a++)
                        {
                            for (int b = y_index; b <= y_index + boyut; b++)
                            {
                                engelMatrisi[a, b] = true;
                                
                            }
                        }
                        character.chestPicturebox.Name = "Bakır Sandık";
                        chest_pictureBoxes.Add(character.chestPicturebox);
                        character.addto_map();
                        
                        break;
                    }
                } while (true);
            }
        }
        public void char_ekle()
        {
            int x, y;
            int boyut = 1;

            do
            {
                x = rnd.Next(0, en);
                y = rnd.Next(0, boy);
                x = x * oran;
                y = y * oran;
                int x_index = x / oran;
                int y_index = y / oran;

               
                if (!EngelleriKontrolEt(x_index, y_index, boyut))
                {
                    Character character = new Character(x, y, oran, boyut, this);
                    for (int a = x_index; a <= x_index + boyut; a++)
                    {
                        for (int b = y_index; b <= y_index + boyut; b++)
                        {
                            engelMatrisi[a, b] = true;
                            locations.Add((a * oran, b * oran));
                        }
                    }

                    character.addto_map();
                    character.charPicturebox.BringToFront();
                    ClearCharacterArea(x, y, boyut);
                    break; 
                }
            } while (true);
        }
            
        public static int maxX = 15000;
        public static int maxY = 15000;

        public HashSet<(int, int)> görüşalanı = new HashSet<(int, int)>();

        Rectangle characterArea;
        public void ClearCharacterArea(int x, int y,int boyut)
        {
            Bitmap image = (Bitmap)sis.Image;
            characterArea = new Rectangle(x-(3*oran), y-(3*oran),(7)*oran ,(7)*oran); 
            using (Graphics g = Graphics.FromImage(image))
            {
                using (SolidBrush brush = new SolidBrush(Color.White)) 
                {
                    g.FillRectangle(brush, characterArea);
                }
            }
            görüşalanı.Add((x , y - (3 * oran)));
            görüşalanı.Add((x + (3 * oran), y ));
            görüşalanı.Add((x - (3 * oran), y ));
            görüşalanı.Add((x, y + (3 * oran)));

            sis.Image = image;
        }
        
        public Location lokasyonlar = new Location(locationsx, locationsy);
      
        public bool[,] engelMatrisi = new bool[maxX, maxY];

        public bool EngelleriKontrolEt(int x, int y, int boyut)
        {
            for (int i = x; i < x + boyut; i++)
            {
                for (int j = y; j < y + boyut; j++)
                {
                    if (engelMatrisi[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool EngelleriKontrolEt(int x, int y)
        {
            for (int i = x; i < x ; i++)
            {
                for (int j = y; j < y ; j++)
                {
                    if (engelMatrisi[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void form_update(PictureBox picturebox)
        {
            listBox1.Items.Add("("+picturebox.Location.X / oran + " , " + picturebox.Location.Y / oran+")" + " konumunda "+picturebox.Name+ " bulundu ");
        }
        
    }
}
