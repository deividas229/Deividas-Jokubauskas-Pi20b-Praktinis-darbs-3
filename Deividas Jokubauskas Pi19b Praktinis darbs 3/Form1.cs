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

namespace Deividas_Jokubauskas_Pi19b_Praktinis_darbs_3
{
    public partial class Form1 : Form
    {
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        byte[] plaintext;
        byte[] encryptedtext;
        public Form1()
        {
            InitializeComponent();
        }
        static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            plaintext = ByteConverter.GetBytes(textBox1.Text);
            encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);
            textBox2.Text = ByteConverter.GetString(encryptedtext);
            File.WriteAllText(@"C:\Users\deivi\OneDrive\Desktop\kolegija\sauga\Deividas Jokubauskas Pi19b Praktinis darbs 3\txt.txt", String.Empty);
            using (StreamWriter file =
            new StreamWriter(@"C:\Users\deivi\OneDrive\Desktop\kolegija\sauga\Deividas Jokubauskas Pi19b Praktinis darbs 3\txt.txt", true))
            {
                file.WriteLine(ByteConverter.GetString(encryptedtext));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] decryptedtex = Decryption(encryptedtext,
            RSA.ExportParameters(true), false);
            textBox3.Text = ByteConverter.GetString(decryptedtex);
            File.WriteAllText(@"C:\Users\deivi\OneDrive\Desktop\kolegija\sauga\Deividas Jokubauskas Pi19b Praktinis darbs 3\txt.txt", String.Empty);
            using (StreamWriter file =
            new StreamWriter(@"C:\Users\deivi\OneDrive\Desktop\kolegija\sauga\Deividas Jokubauskas Pi19b Praktinis darbs 3\txt.txt", true))
            {
                file.WriteLine(ByteConverter.GetString(decryptedtex));
            }
        }

    }
}
