using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{


    public partial class Form1 : Form
    {
        List<Druzyna> druzyny = new List<Druzyna>();

        public Form1()
        {
            InitializeComponent();
            label1.Text = "Witamy!\nWprowadź nazwy drużyny";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            button1.Visible = false;
            groupBox1.Visible = !groupBox1.Visible;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Druzyna druzyna = new Druzyna(textBox1.Text);
            druzyny.Add(druzyna);
            listBox1.Items.Add(druzyna.nazwa);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;

            dataGridView1.ColumnCount = druzyny.Count + 1;
            dataGridView1.RowCount = druzyny.Count;
            dataGridView1.AutoSize = true;

            int i = 1;

            foreach (Druzyna druzyna in druzyny)
            {
                dataGridView1.Columns[i].Name = druzyna.nazwa;
                //String[] newRow = new String[druzyny.Count+1];
                //newRow[0] = druzyna.nazwa;
                //dataGridView1.Rows.Add(newRow);

                dataGridView1.Rows[i - 1].Cells[0].Value = druzyna.nazwa;
                dataGridView1.Rows[i - 1].Cells[i].Value = "Nie Dotyczy";
                dataGridView1.Rows[i - 1].Cells[i].ReadOnly = true;
                i++;
            }





        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (Druzyna druzyna in druzyny)
            {
                foreach (DataGridViewCell cell in dataGridView1.Rows[i++].Cells)
                {
                    if (cell == null || cell.Value == null || cell.Value.ToString() == null || !cell.Value.ToString().Contains(':'))
                    {
                        //ponieważ komórka w tabeli nie ma znaku : to zapewne nie jest prawidłowym wynikiem. Pomijamy ją.
                        continue;
                    }
                    string[] values = cell.Value.ToString().Split(':');

                    int a = int.Parse(values[0]);
                    int b = int.Parse(values[1]);
                    if (a > b)
                    {
                        //wygrana
                        druzyna.punkty = druzyna.punkty + 3;
                    }
                    else if (a == b)
                    {
                        //remis
                        druzyna.punkty = druzyna.punkty + 1;
                    }
                    else
                    {
                        //przegrana nie wpływa na punkty
                    }
                }


            }

            druzyny.Sort(CompareDruzynaByPunkty);
            foreach (Druzyna druzyna in druzyny)
            {
                listBox2.Items.Add(druzyna.nazwa + " " + druzyna.punkty + "pkt");
            }
            groupBox4.Visible = true;
            groupBox2.Visible = false;
        }

        private int CompareDruzynaByPunkty(Druzyna d1, Druzyna d2)
        {
            return d1.punkty.CompareTo(d2.punkty) * -1;
        }

        

        private void button5_Click_1(object sender, EventArgs e)
        {
            FileStream plik = new FileStream("wyniki.txt", FileMode.Create, FileAccess.Write);
            StreamWriter zapisuj = new StreamWriter(plik);
            foreach (string value in listBox2.Items)
            {
                zapisuj.WriteLine(value);
            }
            zapisuj.Close();
            plik.Close();
        }

        

    }
}
