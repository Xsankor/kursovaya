using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace AppGraphs
{
    public partial class MainForm : Form
    {
        Color selectedColor;
        List<int> Checked = new List<int>();

        double maxSizeEdge = 10, minSizeEdge = -10, step = 0.01;
        double a, b, c, d;

        public MainForm()  
        {
            InitializeComponent();

            HideAllSeries(); // скрываем все графики
        }

        private void ReDraw() // функция отбора только выбранных графиков
        {
            HideAllSeries(); // скрываем все графики
            for (int i = 0; i < Checked.Count; ++i)
            {
                GetAllData(Checked[i]); // собираем все данные относящиеся к выбранным графикам
            }
            RevialCheckSeries(); // показать только выбранные графики
        }

        private void BuildGraph(int index) // функция вызова функции расчёта по формуле и вывода графика 
        {
            double y;
            chart1.Series[index].IsVisibleInLegend = true;
            for (double x = minSizeEdge; x <= maxSizeEdge; x += step)
            {
                x = Math.Round(x, 2);
                if (x == 0) continue;
                y = Math.Round(СalcFunction(x, index), 2); // рассчёт координаты Y в соответствии с выбранным графиком
                chart1.Series[index].Points.AddXY(x, y);
            }
        }

        private double СalcFunction(double x, int option = 0) // расчёт по формуле координаты Y
        {
            switch (option)
            {
                case 0: return a * (x * x) + b * x + c;
                case 1: return 1 * a * x + 1 * b;
                case 2: return 1 * a * (x * x * x) + b * (x * x) + 1 * c * x + d;
                case 3: return a / (x * x * x);
                case 4: return x * x * x;
                case 5: return Math.Pow(Math.Exp(1), x);
                case 6: return Math.Log(Math.Abs(x));
                case 7: return Math.Sin(x);
                case 8: return Math.Tan(x);
                case 9: return Math.Atan(x);
                default: return 1;
            }
        }

        private void HideAllSeries() // скрывает все графики
        {
            for (int i = 0; i < chart1.Series.Count; ++i)
            {
                chart1.Series[i].IsVisibleInLegend = false;
                chart1.Series[i].Points.Clear();
            }
        }

        private void RevialCheckSeries() // отображает выбранные графики
        {
            foreach (int i in Checked)
            {
                chart1.Series[i].IsVisibleInLegend = true;
            }
        }

        private void GetAllData(int index) // сбор данных у соответствующих графиков
        {
            Color color = Color.Black;
            switch (index)
            {
                case 0:
                {
                    textBox3.Text = textBox3.Text.Length < 1 ? "0" : textBox3.Text; 
                    a = Convert.ToInt32(textBox3.Text);

                    textBox4.Text = textBox4.Text.Length < 1 ? "0" : textBox4.Text;
                    b = Convert.ToInt32(textBox4.Text);

                    textBox5.Text = textBox5.Text.Length < 1 ? "0" : textBox5.Text;
                    c = Convert.ToInt32(textBox5.Text);
                    color = pictureBox2.BackColor;
                }
                break;
                case 1:
                {
                    textBox2.Text = textBox2.Text.Length < 1 ? "0" : textBox2.Text;
                    a = Convert.ToInt32(textBox2.Text);
                        
                    textBox1.Text = textBox1.Text.Length < 1 ? "0" : textBox1.Text;
                    b = Convert.ToInt32(textBox1.Text);
                    color = pictureBox3.BackColor;
                }
                break;
                case 2:
                {
                    textBox8.Text = textBox8.Text.Length < 1 ? "0" : textBox8.Text;
                    a = Convert.ToInt32(textBox8.Text);

                    textBox7.Text = textBox7.Text.Length < 1 ? "0" : textBox7.Text;
                    b = Convert.ToInt32(textBox7.Text);

                    textBox6.Text = textBox6.Text.Length < 1 ? "0" : textBox6.Text;
                    c = Convert.ToInt32(textBox6.Text);

                    textBox9.Text = textBox9.Text.Length < 1 ? "0" : textBox9.Text;
                    d = Convert.ToInt32(textBox9.Text);
                    color = pictureBox4.BackColor;
                }
                break;
                case 3:
                {
                    textBox10.Text = textBox10.Text.Length < 1 ? "0" : textBox10.Text;
                    a = Convert.ToInt32(textBox10.Text);
                    color = pictureBox5.BackColor;
                }
                break;
                case 4:
                {
                    color = pictureBox6.BackColor;
                }
                break;
                case 5:
                {
                    color = pictureBox7.BackColor;
                }
                break;
                case 6:
                {
                    color = pictureBox8.BackColor;
                }
                break;
                case 7:
                {
                    color = pictureBox9.BackColor;
                }
                break;
                case 8:
                {
                    color = pictureBox10.BackColor;
                }
                break;
                case 9:
                {
                    color = pictureBox11.BackColor;
                }
                break;
            }
            chart1.Series[index].Color = color;
            BuildGraph(index); // вывод графика
        }

        private Color GetColor() // выбор нужного цвета для графика
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorDialog1.Color;
            }
            return selectedColor;
        }

        private void TB_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 45)
            {
                e.Handled = true;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Checked.Clear();
            foreach (int item in checkedListBox1.CheckedIndices)
            {
                Checked.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.Minimum = minSizeEdge;
            chart1.ChartAreas[0].AxisY.Minimum = minSizeEdge;

            chart1.ChartAreas[0].AxisX.Maximum = maxSizeEdge;
            chart1.ChartAreas[0].AxisY.Maximum = maxSizeEdge;
            ReDraw();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
            if (numericUpDown1.Value == numericUpDown2.Value)
            {
                numericUpDown2.Value--;
            }
            if (numericUpDown1.Value < numericUpDown2.Value) 
            {
                numericUpDown1.Value = numericUpDown2.Value + 1;
            }
            maxSizeEdge = (int)numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (int i in checkedListBox1.CheckedIndices)
            {
                checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
            }
            Checked.Clear();
            ReDraw();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            double num = (double)trackBar.Value;
            num = (num / 100);
            if (num == 0 || num == 1) return;
            step = num;
            label12.Text = num.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value == numericUpDown1.Value) 
            {
                numericUpDown1.Value++;
            }
            if (numericUpDown2.Value > numericUpDown1.Value) 
            {
                numericUpDown1.Value = numericUpDown2.Value + 1;
            }
            minSizeEdge = (int)numericUpDown2.Value;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.BackColor = GetColor();
        }

        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            pictureBox6.BackColor = GetColor();
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            pictureBox5.BackColor = GetColor();
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            pictureBox4.BackColor = GetColor();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            pictureBox3.BackColor = GetColor();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form_reference = new FormReference();
            form_reference.Show();
        }

        private void оРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form_info = new FormInfo();
            form_info.Show();
        }

        private void pictureBox7_Click_1(object sender, EventArgs e)
        {
            pictureBox7.BackColor = GetColor();
        }

        private void pictureBox8_Click_1(object sender, EventArgs e)
        {
            pictureBox8.BackColor = GetColor();
        }

        private void pictureBox9_Click_1(object sender, EventArgs e)
        {
            pictureBox9.BackColor = GetColor();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            pictureBox10.BackColor = GetColor();
        }

        private void pictureBox11_Click_1(object sender, EventArgs e)
        {
            pictureBox11.BackColor = GetColor();
        }
    }
}
