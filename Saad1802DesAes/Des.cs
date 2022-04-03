using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace DesAes
{

 
    public partial class Des : Form
    {

        public Des()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {



        }

        private void button2_Click(object sender, EventArgs e)
        {
            string textBox1 = "qwerty12";
            string checkstring = textBox1;
            bool checker = true;
                if (checkstring.Length != 8)
                {
                    checker = false;

                    MessageBox.Show("Размер ключа не соотвествует 8 символам");
                }
                else checker = true;

            

          
            if (checker == true)
            {
                try
                {
                   
                    var sha = SHA512.Create();
                    var fullhash = sha.ComputeHash(UTF8Encoding.UTF8.GetBytes(textBox1));//hash




                    var truehash = new byte[8];
                    Array.Copy(fullhash, truehash, 8);
                    openFileDialog1.Filter = "txt files|*.txt";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        string source = openFileDialog1.FileName;
                       
                        saveFileDialog1.Filter = " txt.enc files |*.txt.enc";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string destination = saveFileDialog1.FileName;
                            EncryptFile(source, destination, truehash);

                        }

                    }
                    sha.Dispose();
                   // MessageBox.Show("Все получилось!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }      
                catch(Exception ex)
                {

                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
        }
        private void EncryptFile(string source, string destination, byte[] truehash)
        {
            FileStream fsInput = new FileStream(source, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(destination, FileMode.Create, FileAccess.Write);
            var DES = new DESCryptoServiceProvider(); 
            try
            {
                DES.Key = truehash;
                DES.IV = truehash;
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Close();


            }
            catch
            {

                MessageBox.Show("Ошибка при шифровании файла.");
            }

            fsInput.Close();
            fsEncrypted.Close();

        }


        private void DencryptFile(string source, string destination, byte[] truehash)
        {
            FileStream fsInput = new FileStream(source, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(destination, FileMode.Create, FileAccess.Write);
            var DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = truehash;
                DES.IV = truehash;
                ICryptoTransform desencrypt = DES.CreateDecryptor();
                CryptoStream cryptoStream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Close();

                //MessageBox.Show("Все получилось!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
               
                MessageBox.Show("Не удалось правильно расшифровать файл. Видимо он зашифрован другим ключевым словом.");
            }

            fsInput.Close();
            fsEncrypted.Close();

        }







        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           



        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string textBox1 = "qwerty12";
            string checkstring = textBox1;
            bool checker = true;
            if (checkstring.Length != 8)
            {
                checker = false;

                MessageBox.Show("Размер ключа не соотвествует 8 символам");
            }
            else checker = true;




            if (checker == true)
            {
                try
                {
                    var sha = SHA512.Create();
                    var fullhash = sha.ComputeHash(UTF8Encoding.UTF8.GetBytes(textBox1));




                    var truehash = new byte[8];
                    Array.Copy(fullhash, truehash, 8);
                    openFileDialog1.Filter = "txt.enc files|*.txt.enc";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        string source = openFileDialog1.FileName;
                        saveFileDialog1.Filter = " txt.dec files |*.txt.dec";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string destination = saveFileDialog1.FileName;
                            DencryptFile(source, destination, truehash);

                        }

                    }
                    sha.Dispose();
                   
                }
                catch (Exception ex)
                {

                    throw ex;
                }



            }

        }

        private void Des_Load(object sender, EventArgs e)
        {

        }
    }
}
