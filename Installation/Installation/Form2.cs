using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Installation
{
    public partial class Form2 : Form
    {
        BackgroundWorker wrk = new BackgroundWorker();
        public Form2()
        {
            InitializeComponent();
            wrk.WorkerSupportsCancellation = true; //asinhronnie potoki
            wrk.WorkerReportsProgress = true;
            wrk.ProgressChanged += Worker_ProgressChanged; //dinamicheskii progress
            wrk.DoWork += Worker_DoWork;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            CopyFile(textBox2.Text, textBox1.Text);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            percent.Text = progressBar1.Value.ToString() + "%"; //прогрес
            if (progressBar1.Value == 100)
            {
                MessageBox.Show("File copied to" + " " + textBox2.Text, "Done");
            }
        }

        void CopyFile(string source, string dest)
        {
            FileStream Out = new FileStream(dest, FileMode.Create);
            FileStream In = new FileStream(source, FileMode.Open);
            byte[] bt = new byte[1024 * 1024]; //массив с файлом
            int readByte;

            while((readByte = In.Read(bt,0,bt.Length)) > 0) //bt - local, 0 - offset, length - максимальное количество байт
            {
                Out.Write(bt, 0, readByte);
                wrk.ReportProgress((int)(In.Position * 100 / In.Length)); //проценты выполнения операции
            }
            In.Close();
            Out.Close();
        }

        private void dirButtonClick(object sender, EventArgs e) //Путь скопированного файла
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {

                textBox1.Text = Path.Combine(folder.SelectedPath, Path.GetFileName(textBox2.Text));
            }
        }

        private void fileDir_Click(object sender, EventArgs e) //Выбор файла для копирования
        {
            OpenFileDialog o = new OpenFileDialog();
            {
                if (o.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = o.FileName;
                }
            }
        }

        private void backButtonClick(object sender, EventArgs e)
        {
            Form1 Form = new Form1();
            Form.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void copyClick(object sender, EventArgs e)
        {
            wrk.RunWorkerAsync();
        }
    }
}
