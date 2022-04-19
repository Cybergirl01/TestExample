using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Authorization_with_captcha
{
    public partial class FormAuthorization : Form
    {
        text_ExamDataSet.UsersDataTable users;
        public FormAuthorization()
        {
            InitializeComponent();
        }
        string text = String.Empty;
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Заполните поля!");
            }
            else
            {
                string log = textBox1.Text;
                string pas = textBox2.Text;

                users = usersTableAdapter.GetData();
                var filter = users.Where(rec => rec.Login == log && rec.Password == pas);
                if (filter.Count() == 0)
                {
                    MessageBox.Show("Таких данных нет");
                    return;
                }
                else
                {
                    Classtotal.log = filter.ElementAt(0).Login;
                    Classtotal.id_user = filter.ElementAt(0).ID;
                    Classtotal.id_role = filter.ElementAt(0).IDRole;
                    if (textBox3.Text == text)
                    {
                        switch (Classtotal.id_role)
                        {
                            case 1:
                                MessageBox.Show("Вы вошли как Администратор");
                                break;
                            case 2:
                                MessageBox.Show("Вы зашли как Менеджер");
                                break;
                            case 3:
                                MessageBox.Show("Вы зашли как Клиент");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы неверно ввели капчу!");
                        pictureBoxCaptcha.Image = Captcha(pictureBoxCaptcha.Width, pictureBoxCaptcha.Height);
                        return;
                    }
                }
            }
            
        }

        private Bitmap Captcha(int w, int h)
        {
            Bitmap bmp = new Bitmap(w, h);
            Random random = new Random();

            int x = random.Next(0, w - 50);
            int y = random.Next(15, h - 15);

            Brush[] Colors = { Brushes.RoyalBlue, Brushes.Red, Brushes.Green, Brushes.Black };

            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";

            for (int i = 0; i < 4; i++)
            {
                text += ALF[random.Next(ALF.Length)];
            }

            Graphics g = Graphics.FromImage((Image)bmp);
            g.Clear(Color.Gray);

            g.DrawString(text, new Font("Comic Sans MS", 15), Colors[random.Next(Colors.Length)], new PointF(x, y));
            g.DrawLine(Pens.Black, new Point(0,0), new Point(w-1, h-1));
            g.DrawLine(Pens.Black, new Point(0, h - 1), new Point(w - 1, 0));

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (random.Next() % 20 == 0)
                    {
                        bmp.SetPixel(i, j, Color.White);
                    }
                }
            }
            return bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBoxCaptcha.Image = Captcha(pictureBoxCaptcha.Width, pictureBoxCaptcha.Height);
        }

        private void FormAuthorization_Load(object sender, EventArgs e)
        {
            pictureBoxCaptcha.Image = Captcha(pictureBoxCaptcha.Width, pictureBoxCaptcha.Height);
        }
    }
}
