using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaningTask
{
    public partial class Form1 : Form
    {
        const string FILENAME = "C:\\Users\\Refodeus\\source\\repos\\PlaningTask\\PlaningTask\\Saves.txt";
        DateTime daytime1 = DateTime.MinValue, daytime2 = DateTime.MinValue;
        DateTime[] DAY_TIMES1, DAY_TIMES2;

        bool Access = false, lever = true;
        int count = 1, numberOfAllPages;
        private void Rewrite()
        {
        }
        private void Save()
        {

            string[] Data = new string[8];
            if(lever)
            {
                Data[0] = numberOfAllPages.ToString();
                lever = false;
            }
            int index = 1;
            Data[index++] = daytime1.Year + "." + daytime1.Month + "." + daytime1.Day;
            Data[index++] += daytime2.Year + "." + daytime2.Month + "." + daytime2.Day;
            Data[index++] += numericUpDown1.Value.ToString();
            Data[index++] += numericUpDown2.Value.ToString();
            Data[index++] += numericUpDown4.Value.ToString();
            Data[index++] += numericUpDown3.Value.ToString();
            Data[index++] += textBox1.Text;
            File.AppendAllLines(FILENAME, Data);
            string[] temp = File.ReadAllLines(FILENAME);
            temp[0] = numberOfAllPages.ToString();
            File.WriteAllLines(FILENAME, temp);
        }
        private void CreatePage()
        {
            TabPage newPage = new TabPage();
            newPage.Text = "Задача №" + count++;
            Label task = new Label();
            Label period = new Label();
            Label title = new Label();

            title.Location = new Point(3, 33);
            title.Text = "Текущая планируемая задача: ";
            title.AutoSize = true;

            period.Location = new Point(3, 3);
            period.Text = "Дата: от " + daytime1.Year + "." + daytime1.Month + "." + daytime1.Day + " до " + daytime2.Year + "." + daytime2.Month + "." + daytime2.Day + "\n";
            period.Text += "Время: от " + numericUpDown1.Value + ":" + numericUpDown2.Value + " до " + numericUpDown4.Value + ":" + numericUpDown3.Value + "\n";
            period.AutoSize = true;

            task.Location = new Point(3, 53);
            task.Text = textBox1.Text;
            task.AutoSize = true;


            newPage.Controls.Add(title);
            newPage.Controls.Add(task);
            newPage.Controls.Add(period);

            comboBox1.Items.Add(newPage.Text);
            tabControl.TabPages.Add(newPage);
        }
        private void DownloadingSaves()
        {
            string[] lines = File.ReadAllLines(FILENAME);
            int AllElements = Convert.ToInt32(lines[0]), index = 1;
            for(int i = 1; i < AllElements + 1; i++)
            {
                numberOfAllPages++;
                daytime1 = Convert.ToDateTime(lines[index++]);
                daytime2 = Convert.ToDateTime(lines[index++]);
                numericUpDown1.Value = Convert.ToInt32(lines[index++]);
                numericUpDown2.Value = Convert.ToInt32(lines[index++]);
                numericUpDown4.Value = Convert.ToInt32(lines[index++]);
                numericUpDown3.Value = Convert.ToInt32(lines[index++]);
                textBox1.Text = lines[index++];
                index++;
                CreatePage();
            }
        }
        public Form1()
        {
            InitializeComponent();
            DownloadingSaves();
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar2.MaxSelectionCount = 1;
        }
        private void comparingDates()
        {
            if(daytime1 != DateTime.MinValue && daytime2 != DateTime.MinValue) 
            {
                if (daytime1 > daytime2)
                {
                    label10.Visible = true;
                    Access = false;
                }
                else
                {
                    label10.Visible = false;
                    Access = true;
                }
                if (daytime1 == daytime2)
                    if(numericUpDown1.Value >= numericUpDown4.Value) 
                    {
                        if (numericUpDown1.Value == numericUpDown4.Value)
                            if(numericUpDown2.Value >= numericUpDown3.Value)
                            {
                                label10.Visible = true;
                                Access = false;
                            }
                            else
                            {
                                label10.Visible = false;
                                Access = true;
                            }
                        else
                        {
                            label10.Visible = true;
                            Access = false;
                        }
                    }
            }
        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            label8.Text = "Дата: ";
            daytime1 = monthCalendar1.SelectionRange.Start;
            //monthCalendar1.SelectionRange.Start = Convert.ToDateTime("fjs");
            label8.Text += daytime1.Year + "/" + daytime1.Month + "/" + daytime1.Day;
            comparingDates();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Access && textBox1.Text != string.Empty)
            {
                numberOfAllPages++;
                CreatePage();
                Save();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                numberOfAllPages--;
                int index = 0;
                for(int i = 0; i < tabControl.TabPages.Count; i++)
                    if (tabControl.TabPages[i].Text == comboBox1.Text)
                    {
                        index = i;
                        break;
                    }
                tabControl.TabPages.Remove(tabControl.TabPages[index]);
                comboBox1.Items.RemoveAt(index - 1);
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            comparingDates();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            comparingDates();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            comparingDates();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            comparingDates();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите сохранить задачи?", "Сохранение задач", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                
            }
        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            label9.Text = "Дата: ";
            daytime2 = monthCalendar2.SelectionRange.Start;
            label9.Text += daytime2.Year + "/" + daytime2.Month + "/" + daytime2.Day;
            comparingDates();
        }
    }
}
