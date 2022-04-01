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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Show_to_ListMovie();
            Disable_FlatAppearance();
            LichSu_Init();
        }

        private string strFolder = @"E:\Study\CS511.M21\CS511.M21-BT01\BT01\";
        private DataTable dt_ListMovie = GetMovieDataTable();
        private int curChosen_idx = -1;
        private bool liked = false;

        /// MANUAL CODE /////////////////////////////////////////////////////////////////
        // Disable FlatAppearance.MouseOverBackColor
        public void Disable_FlatAppearance()
        {
            this.button_like.FlatAppearance.MouseOverBackColor = button_like.BackColor;
            this.button_star.FlatAppearance.MouseOverBackColor = button_star.BackColor;
            this.button_view.FlatAppearance.MouseOverBackColor = button_view.BackColor;
            this.button_like.FlatAppearance.MouseDownBackColor = button_like.BackColor;
            this.button_star.FlatAppearance.MouseDownBackColor = button_star.BackColor;
            this.button_view.FlatAppearance.MouseDownBackColor = button_view.BackColor;
        }

        // Get-functions
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(new string[] {", "}, StringSplitOptions.None);
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(new string[] { ", " }, StringSplitOptions.None);
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }

            return dt;
        }
        private static DataTable GetMovieDataTable()
        {
            DataTable dataTable = new DataTable();
            // put your csv_path down here ---v--- //
            dataTable = ConvertCSVtoDataTable(@"E:\Study\CS511.M21\CS511.M21-BT01\BT01\Movie\MovieInfo.csv");

            return dataTable;
        }

        private void Show_to_ListMovie()
        {
            var index = 0;
            foreach (var pan in tableLayoutPanel4.Controls.OfType<Panel>()) // iter all panel by Add order
            {
                pan.Visible = false; // hide panel

                if (index + 1 > dt_ListMovie.Select().Length) // checking if current_index > num_movie 
                {
                    continue; // if true then pass this panel and don't show it
                }

                pan.Visible = true; // show panel

                DataRow dr = dt_ListMovie.Rows[index]; // get row[idx] in datatable
                pan.Name = dr["ID"].ToString();
                string path_img = dr.Field<string>(3); // get path_poster at col[3] and set as string

                PictureBox pb = pan.Controls.OfType<PictureBox>().First();
                pb.ImageLocation = Path.Combine(strFolder,path_img);
                Label label_name = pan.Controls.OfType<Label>().First();
                label_name.Text = dr["name"].ToString();

                index++;
            }
        }
        private void Show_Finding()
        {
            var index = 0;
            string keyword = textBox1.Text;
            DataRow[] dt_find = dt_ListMovie.Select("name Like '%" + keyword + "%'");
            if (dt_find.Length == 0) { MessageBox.Show("Không tìm thấy phim", "ERROR");}
            foreach (var pan in tableLayoutPanel4.Controls.OfType<Panel>()) // iter all panel by Add order
            {
                pan.Visible = false; // hide panel

                if (index + 1 > dt_find.Length) // checking if current_index > num_movie 
                {
                    continue; // if true then pass this panel and don't show it
                }

                pan.Visible = true; // show panel

                DataRow dr = dt_find[index]; // get row[idx] in datatable
                pan.Name = dr["ID"].ToString();
                string path_img = dr.Field<string>(3); // get path_poster at col[3] and set as string

                PictureBox pb = pan.Controls.OfType<PictureBox>().First();
                pb.ImageLocation = Path.Combine(strFolder, path_img);
                Label label_name = pan.Controls.OfType<Label>().First();
                label_name.Text = dr["name"].ToString();

                index++;
            }
        }
        private void Show_danhmuc()
        {
            var index = 0;
            string keyword = listBox1.SelectedItem.ToString();
            DataRow[] dt_find = dt_ListMovie.Select("gernes Like '%" + keyword + "%'");
            foreach (var pan in tableLayoutPanel4.Controls.OfType<Panel>()) // iter all panel by Add order
            {
                pan.Visible = false; // hide panel

                if (index + 1 > dt_find.Length) // checking if current_index > num_movie 
                {
                    continue; // if true then pass this panel and don't show it
                }

                pan.Visible = true; // show panel

                DataRow dr = dt_find[index]; // get row[idx] in datatable
                pan.Name = dr["ID"].ToString();
                string path_img = dr.Field<string>(3); // get path_poster at col[3] and set as string

                PictureBox pb = pan.Controls.OfType<PictureBox>().First();
                pb.ImageLocation = Path.Combine(strFolder, path_img);
                Label label_name = pan.Controls.OfType<Label>().First();
                label_name.Text = dr["name"].ToString();

                index++;
            }
        }
        private void Show_to_ListMovie_My()
        {
            var index = 0;
            DataRow[] dt_My = dt_ListMovie.Select("nation = 'Mỹ'");
            foreach (var pan in tableLayoutPanel4.Controls.OfType<Panel>()) // iter all panel by Add order
            {
                pan.Visible = false; // hide panel

                if (index + 1 > dt_My.Length) // checking if current_index > num_movie 
                {
                    continue; // if true then pass this panel and don't show it
                }

                pan.Visible = true; // show panel

                DataRow dr = dt_My[index]; // get row[idx] in datatable
                pan.Name = dr["ID"].ToString();
                string path_img = dr.Field<string>(3); // get path_poster at col[3] and set as string

                PictureBox pb = pan.Controls.OfType<PictureBox>().First();
                pb.ImageLocation = Path.Combine(strFolder, path_img);
                Label label_name = pan.Controls.OfType<Label>().First();
                label_name.Text = dr["name"].ToString();

                index++;
            }
        }

        private void Show_lichsu()
        {
            var index = 0;
            DataView dt_view = new DataView(LichSu);
            DataTable dt_ls = dt_view.ToTable(true, "id");
            foreach (var pan in tableLayoutPanel4.Controls.OfType<Panel>()) // iter all panel by Add order
            {
                pan.Visible = false; // hide panel

                if (index + 1 > dt_ls.Select().Length) // checking if current_index > num_movie 
                {
                    continue; // if true then pass this panel and don't show it
                }

                pan.Visible = true; // show panel

                DataRow temp_dr = dt_ls.Rows[index];
                int idx = temp_dr.Field<int>(0);
                DataRow dr = dt_ListMovie.Rows[idx]; // get row[idx] in datatable
                pan.Name = dr["ID"].ToString();
                string path_img = dr.Field<string>(3); // get path_poster at col[3] and set as string

                PictureBox pb = pan.Controls.OfType<PictureBox>().First();
                pb.ImageLocation = Path.Combine(strFolder, path_img);
                Label label_name = pan.Controls.OfType<Label>().First();
                DateTime timer = LichSu.Select("id = " + idx.ToString()).Last().Field<DateTime>(1);
                label_name.Text = LichSu.Select("id = "+idx.ToString()).Length + "-" + timer.ToString("MM/dd/yy H:mm:ss");

                index++;
            }
        }

        // Click panel_inListMovie -> 
        private void panel_inListMovie_Click(object sender, EventArgs e)
        {
            MoreDetail_update( (Panel)sender );
        }
        private void panelChild_inListMovie_Click(object sender, EventArgs e)
        {
            MoreDetail_update( (Panel)((Control)sender).Parent );
        }

        // Click button_play -> show Movie_Player Form , view +=1
        private void button_play_Click(object sender, EventArgs e)
        {
            if (curChosen_idx == -1) { pop_up_NoMovieMsg(); return; }
            string path_vid = dt_ListMovie.Rows[curChosen_idx].Field<string>(4);
            string path_full = Path.Combine(strFolder, path_vid);

            int view_count = Int32.Parse(button_view.Text);
            view_count++;
            button_view.Text = view_count.ToString();
            dt_ListMovie.Rows[curChosen_idx]["view"] = view_count.ToString();

            LichSu.Rows.Add(curChosen_idx, DateTime.Now);

            Movie_Player movie_Player = new Movie_Player(path_full);
            movie_Player.ShowDialog();
        }

        // Click button_detail -> show Detail Form
        private void button_detail_Click(object sender, EventArgs e)
        {
            if (curChosen_idx == -1) { pop_up_NoMovieMsg(); return; }
            Detail_Form detail_form = new Detail_Form(dt_ListMovie.Rows[curChosen_idx]);
            detail_form.ShowDialog();
        }

        // Show Poster and update curChosen
        private void MoreDetail_update(Panel pan)
        {
            // update curChosen
            curChosen_idx = Int32.Parse(pan.Name);

            // Show Poster
            DataRow dr = dt_ListMovie.Rows[curChosen_idx]; // get row[idx] in datatable
            string path_img = dr.Field<string>(3); // get path_poster at col[3] and set as string

            pictureBox_Poster.ImageLocation = Path.Combine(strFolder, path_img);
            label_MovieName.Text = dr["name"].ToString();

            string newLine = Environment.NewLine;
            textBox_Detail.Text = "Năm sản xuất : " + dr["year"].ToString() + newLine;
            textBox_Detail.Text += "Trạng thái : " + dr["status"].ToString() + newLine;
            textBox_Detail.Text += "Đạo diễn : " + dr["director"].ToString() + newLine;
            textBox_Detail.Text += "Thời gian : " + dr["duration"].ToString() + " phút " + newLine;
            string[] gernes = dr["gernes"].ToString().Split('_');
            textBox_Detail.Text += "Thể loại: " + newLine;
            for (int i = 0; i < gernes.Length-1; i++)
            {
                textBox_Detail.Text += gernes[i] + ','+' ';
            }
            textBox_Detail.Text += gernes[gernes.Length - 1] + newLine;

            button_like.Text = dr["like"].ToString();
            button_view.Text = dr["view"].ToString();
            button_star.Text = "Star: " + dr["star"].ToString();
        }

        // Click button_exit -> msgBox exit ? Yes : No
        private void button_exit_Click(object sender, EventArgs e)
        {
            string message = "Do you want to close this window?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Click button_like -> like += 1
        private void button_like_Click(object sender, EventArgs e)
        {
            if (curChosen_idx == -1) { pop_up_NoMovieMsg(); return; }
            if (liked == false)
            {
                int like_count = Int32.Parse(button_like.Text);
                like_count++;
                button_like.Text = like_count.ToString();
                liked = true;
            }
            else
            {
                int like_count = Int32.Parse(button_like.Text);
                like_count--;
                button_like.Text = like_count.ToString();
                liked = false;
            }
        }

        // "Please chose your movie" msg
        private void pop_up_NoMovieMsg()
        {
            string message = "Xin hãy chọn phim bạn muốn";
            string title = "Error";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, title, buttons);
        }

        // Non-change mouse
        private void mouse_DoNothing(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Show_to_ListMovie_My();
        }

        private void panel_NavBar_Click(object sender, EventArgs e)
        {
            Show_to_ListMovie();
        }

        private DataTable LichSu = new DataTable();
        
        private void LichSu_Init()
        {
            LichSu.Columns.Add("id", typeof(int));
            LichSu.Columns.Add("last", typeof(DateTime));
        }

        private void LichSu_Click(object sender, EventArgs e)
        {
            Show_lichsu();
        }

        private void button_finding_Click(object sender, EventArgs e)
        {
            Show_Finding();
        }

        private void button_danhmuc_Click(object sender, EventArgs e)
        {
            Show_danhmuc();
        }
    }
}
