using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT01
{
    public partial class Movie_Player : Form
    {
        public Movie_Player()
        {
            InitializeComponent();
        }

        public Movie_Player(string path_movie)
        {
            InitializeComponent();
            axWindowsMediaPlayer1.URL = path_movie;
        }
    }
}
