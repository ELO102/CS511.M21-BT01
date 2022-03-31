using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BT01
{
    public partial class Detail_Form : Form
    {
        public Detail_Form()
        {
            InitializeComponent();
        }

        private DataRow MovieChosen = null;
        private string strFolder = @"E:\Study\CS511.M21\CS511.M21-BT01\BT01\";

        public Detail_Form(DataRow dr)
        {
            InitializeComponent();
            this.MovieChosen = dr;
            Display_Star();
            Display_Detail();
            Disable_FlatAppearance();
        }
        public void Disable_FlatAppearance()
        {
            this.button_like.FlatAppearance.MouseOverBackColor = button_like.BackColor;
            this.button_view.FlatAppearance.MouseOverBackColor = button_view.BackColor;
            this.button_like.FlatAppearance.MouseDownBackColor = button_like.BackColor;
            this.button_view.FlatAppearance.MouseDownBackColor = button_view.BackColor;
        }
        private void Display_Star()
        {
            var index = 1;
            int star = Convert.ToInt32(MovieChosen["star"].ToString());
            System.Drawing.Bitmap starFilled = Properties.Resources._2c4PrbW;
            System.Drawing.Bitmap starHalf = Properties.Resources.tHlBqst;
            foreach (var pictureBox in votesPanel.Controls.OfType<PictureBox>())
            {
                pictureBox.Visible = false;
            }
            foreach (var pictureBox in votesPanel.Controls.OfType<PictureBox>())
            {
                if (index <= star)
                {
                    pictureBox.Image = starFilled;
                }
                else if (star % 10 != 0 && index < star + 1)
                {
                    pictureBox.Image = starHalf;
                }
                else
                {
                    break;
                }
                pictureBox.Visible = true;
                index++;
            }
        }

        private void Display_Detail()
        {
            DataRow dr = MovieChosen;
            string path_img = dr.Field<string>("poster"); // get path_poster at col[3] and set as string

            pictureBox_Poster.ImageLocation = Path.Combine(strFolder, path_img);
            label_MovieName.Text = dr["name"].ToString();

            string newLine = Environment.NewLine;
            textBox_Detail.Text = "Năm sản xuất : " + dr["year"].ToString() + newLine;
            textBox_Detail.Text += "Trạng thái : " + dr["status"].ToString() + newLine;
            textBox_Detail.Text += "Đạo diễn : " + dr["director"].ToString() + newLine;
            textBox_Detail.Text += "Thời gian : " + dr["duration"].ToString() + " phút " + newLine;
            string[] gernes = dr["gernes"].ToString().Split('_');
            textBox_Detail.Text += "Thể loại: " + newLine;
            for (int i = 0; i < gernes.Length - 1; i++)
            {
                textBox_Detail.Text += gernes[i] + ',' + ' ';
            }
            textBox_Detail.Text += gernes[gernes.Length - 1] + newLine;

            textBox_Decrip.Text = File.ReadAllText(Path.Combine(strFolder, dr.Field<string>("decrip")));

            button_like.Text = dr["like"].ToString();
            button_view.Text = dr["view"].ToString();

            axWindowsMediaPlayer1.URL = Path.Combine(strFolder, dr.Field<string>("trailer"));
        }
        
        private void Stop_media(object sender, FormClosingEventArgs e)
        {
            axWindowsMediaPlayer1.close();
        }
    }
}
