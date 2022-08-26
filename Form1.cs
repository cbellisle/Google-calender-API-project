using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Security.Policy;
using System.Threading;
using System.Windows.Forms;

namespace googleProject
{
    
    public partial class Form1 : Form
    {

        public string path;
        public Color color1;
        public int nEvents;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Calender Creator";
        }
        //file dialog button
        private void button1_Click(object sender, EventArgs e)
        {
            string filepath;
            OpenFileDialog newBox = new OpenFileDialog();
            newBox.ShowDialog();
            switch ((DialogResult)
                DialogResult.OK)
            {
                case DialogResult.OK:
                    newBox.OpenFile();
                    //File.path
                    textBox1.Text = newBox.FileName;
                    filepath = newBox.FileName;
                    path = filepath;
                    StreamReader reader = new StreamReader(filepath);


                    while (!reader.EndOfStream)
                    {
                        reader.ReadLine();
                        nEvents++;
                    }

                    reader.Close();
                    label1.Text = $"Tasks loaded: {nEvents.ToString()}";
                    break;
                case DialogResult.Cancel:
                    break;

            };


        }
        void loadFile(string filepath)
        {
            // file format
            // Task;Date;Description
            // example:
            // Quiz;10/22/2022 12:35:00;Quiz in class

            //check file length;;
     


            StreamReader reader2 = new StreamReader(filepath);

            for (int i = 0; i < nEvents; i++)
            {
                string line = reader2.ReadLine();
                string[] strings = line.Split(';');


                Event @event = new Event()
                {
                    Summary = strings[0],
                    Start = new EventDateTime()
                    {
                        DateTime = DateTime.Parse(strings[1])
                        //TimeZone = "America/New_York",
                    },

                    End = new EventDateTime()
                    {
                        DateTime = DateTime.Parse(strings[1])
                    },

                    Description = strings[2],

                    ColorId = colorBox.Text,
                    
                    

                };
                Program.Send(@event);

                //var calenderID = "primary";
                // MessageBox.Show("lol i hope it worked");

            }
        }
        //add button
        private void button3_Click(object sender, EventArgs e)
        {
            if(path != null)
                loadFile(path);

        }


        private void colorBox_TextChanged(object sender, EventArgs e)
        {

        }
    }

}