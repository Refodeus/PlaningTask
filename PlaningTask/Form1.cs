using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaningTask
{
    public partial class Form1 : Form
    {
        DateTime daytime1 = DateTime.MinValue, daytime2 = DateTime.MinValue;
        bool Access = false;
        int count = 1;
        public Form1()
        {
            InitializeComponent();
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar2.MaxSelectionCount = 1;
        }
        private void comparingDates()
        {
            if(daytime1 != DateTime.MinValue && daytime2 != DateTime.MinValue) 
            {
                if (daytime1 >= daytime2)
                {
                    label10.Visible = true;
                    Access = false;
                }
                else
                {
                    label10.Visible = false;
                    Access = true;
                }
            }
        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            label8.Text = "Дата: ";
            daytime1 = monthCalendar1.SelectionRange.Start;
            label8.Text += daytime1.Year + "/" + daytime1.Month + "/" + daytime1.Day;
            comparingDates();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Access && textBox1.Text != string.Empty)
            {
                label11.Visible = true;
                TabPage newPage = new TabPage();
                newPage.Text = "Задача №" + count++;
                Label task = new Label();
                Label period = new Label();
                Label title = new Label();

                title.Location = new Point(0, 0);
                title.Dock = DockStyle.Top;
                title.Text = "Текущая планируемая задача";

                task.Location = new Point(0, 30);
                task.Dock = DockStyle.Top;
                task.Text = textBox1.Text;

                period.Location = new Point(0, 80);
                period.Dock = DockStyle.Top;
                period.Text = "Дата: от " + monthCalendar1.SelectionRange.Start.ToString() + " до " + monthCalendar2.SelectionRange.Start.ToString();

                comboBox1.Items.Add(newPage.Text);

                newPage.Controls.Add(period);
                newPage.Controls.Add(task);
                newPage.Controls.Add(title);
                tabControl.TabPages.Add(newPage);
            }
            else
            {
                label11.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != string.Empty)
            {
                int index = 0;
                for(int i = 0; i < tabControl.TabPages.Count; i++)
                    if (tabControl.TabPages[i].Text == comboBox1.Text)
                    {
                        index = i;
                        break;
                    }
                tabControl.TabPages.Remove(tabControl.TabPages[index]);
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
