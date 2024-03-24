using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otonom_Hazine_Avcısı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string boy;
        string en;
        Form2 form;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int sayi;
            if (!int.TryParse(textBox1.Text, out sayi))
            {
                MessageBox.Show("Lütfen bir sayı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = ""; 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            boy = textBox1.Text;
            en = textBox2.Text;
            int sayi;
            if (!int.TryParse(textBox1.Text, out sayi))
            {
                MessageBox.Show("Lütfen bir sayı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = ""; 
            }
            
            if (!int.TryParse(textBox1.Text, out sayi))
            {
                MessageBox.Show("Lütfen bir sayı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = ""; 
                return;
            }
            int boy_sayi = int.Parse(boy);
            int en_sayi= int.Parse(en);
      
              form = new Form2(boy_sayi,en_sayi);   
            form.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int sayi;
            if (!int.TryParse(textBox1.Text, out sayi))
            {
                MessageBox.Show("Lütfen bir sayı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = ""; 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.Close();
            form.Dispose();
            boy = textBox1.Text;
            en = textBox2.Text;
            int sayi;
            if (!int.TryParse(textBox1.Text, out sayi))
            {
                MessageBox.Show("Lütfen bir sayı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
            }

            if (!int.TryParse(textBox1.Text, out sayi))
            {
                MessageBox.Show("Lütfen bir sayı giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                return;
            }
            int boy_sayi = int.Parse(boy);
            int en_sayi = int.Parse(en);

            form = new Form2(boy_sayi, en_sayi);
            form.Show();

        }
    }
}
