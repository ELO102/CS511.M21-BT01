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
        }

        private string strFolder = @"E:\Study\CS511.M21\CS511.M21-BT01\BT01\";
        private DataTable dt_ListMovie = GetMovieDataTable();
        private int curChosen_idx = 0;

        /// MANUAL CODE /////////////////////////////////////////////////////////////////
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
                string path_img = dr.Field<string>(3); // get path_poster at col[3] and set as string

                PictureBox pb = pan.Controls.OfType<PictureBox>().First();
                pb.ImageLocation = Path.Combine(strFolder,path_img);
                Label label_name = pan.Controls.OfType<Label>().First();
                label_name.Text = dr["name"].ToString();

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

        // Click button_play -> show Movie_Player Form
        private void button_play_Click(object sender, EventArgs e)
        {
            Movie_Player movie_Player = new Movie_Player();
            movie_Player.ShowDialog();
        }

        // Show Poster and update curChosen
        private void MoreDetail_update(Panel pan)
        {
            // update curChosen
            curChosen_idx = pan.TabIndex;

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
        }

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
    }
}
