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

        /// MANUAL CODE /////////////////////////////////////////////////////////////////
        /// Get-functions
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
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
        private DataTable GetMovieDataTable()
        {
            DataTable dataTable = new DataTable();
            // put your csv_path down here ---v--- //
            dataTable = ConvertCSVtoDataTable(@"E:\Study\CS511.M21\BT01\BT01\Data\MovieInfo.csv");

            return dataTable;
        }

        private void Show_to_ListMovie()
        {
            DataTable dt = GetMovieDataTable();

            var index = 0;
            foreach (var pan in tableLayoutPanel4.Controls.OfType<Panel>()) // iter all panel by Add order
            {
                pan.Visible = false; // hide panel

                if (index + 1 > dt.Select().Length) // checking if current_index > num_movie 
                {
                    continue; // if true then pass this panel and don't show it
                }

                pan.Visible = true; // show panel

                DataRow dr = dt.Rows[index]; // get row[idx] in datatable
                string path_img = dr.Field<string>(3); // get path_poster at col[3] and set as string

                PictureBox pb = pan.Controls.OfType<PictureBox>().First();
                pb.Image = Image.FromFile(path_img);
                Label label_name = pan.Controls.OfType<Label>().First();
                label_name.Text = dr["name"].ToString();

                index++;
            }
        }
    }
}
