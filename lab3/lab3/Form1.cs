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

namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        ConversionHandler myConverter;

        private void button1_Click(object sender, EventArgs e)
        {
            MACHandler mh = new MACHandler(comboBoxMAC.Text);
            byte[] mac =
           mh.ComputeMAC(myConverter.StringToByteArray(textBoxPlain.
           Text), myConverter.StringToByteArray(textBoxKey.Text));
            textBoxMAC.Text = myConverter.ByteArrayToString(mac);
            textBoxMACHEX.Text = myConverter.ByteArrayToHexString(mac);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            MACHandler mh = new MACHandler(comboBoxMAC.Text);
             if(mh.CheckAuthenticity(myConverter.StringToByteArray(textBoxPlain.Text),
                myConverter.HexStringToByteArray(textBoxMACHEX.Text),
                myConverter.StringToByteArray(textBoxKey.Text)) == true)
            {
                System.Windows.Forms.MessageBox.Show("MAC OK !!!");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("MAC NOT OK !!!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MACHandler smth = new MACHandler(comboBoxMAC.Text);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms,smth.myMAC ,CryptoStreamMode.Write);
            byte[] mes_block = new byte[8];
            long start_time = DateTime.Now.Ticks;
            int count = 10000000;
            for (int i = 0; i < count; i++)
            {
                cs.Write(mes_block, 0, mes_block.Length);
            }
            cs.Close();
            double operation_time = (DateTime.Now.Ticks - start_time);
            operation_time = operation_time / (10 * count);
            labelEncTime.Text = "Time for encryption of a message block: " + operation_time.ToString() + " us";
        }
    }

    class MACHandler
    {
        public HMAC myMAC;
        public MACHandler(string name)
        {
            if (name.CompareTo("SHA1") == 0)
            {
                myMAC = new System.Security.Cryptography.HMACSHA1();
            }
            if (name.CompareTo("MD5") == 0)
            {
                myMAC = new System.Security.Cryptography.HMACMD5();
            }
            if (name.CompareTo("RIPEMD") == 0)
            {
                myMAC = new System.Security.Cryptography.HMACRIPEMD160();
            }
            if (name.CompareTo("SHA256") == 0)
            {
                myMAC = new System.Security.Cryptography.HMACSHA256();
            }
            if (name.CompareTo("SHA384") == 0)
            {
                myMAC = new System.Security.Cryptography.HMACSHA384();
            }
            if (name.CompareTo("SHA512") == 0)
            {
                myMAC = new System.Security.Cryptography.HMACSHA512();
            }
        }

        public bool CheckAuthenticity(byte[] mes, byte[] mac, byte[] key)
        {
            myMAC.Key = key;
            if (CompareByteArrays(myMAC.ComputeHash(mes), mac, myMAC.HashSize /
           8) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public byte[] ComputeMAC(byte[] mes, byte[] key)
        {
            myMAC.Key = key;
            return myMAC.ComputeHash(mes);
        }
        public int MACByteLength()
        {
            return myMAC.HashSize / 8;
        }
        private bool CompareByteArrays(byte[] a, byte[] b, int len)
        {
            for (int i = 0; i < len; i++)
                if (a[i] != b[i]) return false;
            return true;
        }

    }

    class ConversionHandler
    {
        public byte[] StringToByteArray(string s)
        {
            return CharArrayToByteArray(s.ToCharArray());
        }
        public byte[] CharArrayToByteArray(char[] array)
        {
            return Encoding.ASCII.GetBytes(array, 0, array.Length);
        }
        public string ByteArrayToString(byte[] array)
        {
            return Encoding.ASCII.GetString(array);
        }
        public string ByteArrayToHexString(byte[] array)
        {
            string s = "";
            int i;
            for (i = 0; i < array.Length; i++)
            {
                s = s + NibbleToHexString((byte)((array[i] >> 4) &
               0x0F)) + NibbleToHexString((byte)(array[i] &
               0x0F));
            }
            return s;
        }
        public byte[] HexStringToByteArray(string s)
        {
            byte[] array = new byte[s.Length / 2];
            char[] chararray = s.ToCharArray();
            int i;
            for (i = 0; i < s.Length / 2; i++)
            {
                array[i] = (byte)(((HexCharToNibble(chararray[2 * i])
               << 4) & 0xF0) | ((HexCharToNibble(chararray[2
               * i + 1]) & 0x0F)));
            }
            return array;
        }
        public string NibbleToHexString(byte nib)
        {
            string s;
            if (nib < 10)
            {
                s = nib.ToString();
            }
            else
            {
                char c = (char)(nib + 55);
                s = c.ToString();
            }
            return s;
        }
        public byte HexCharToNibble(char c)
        {
            byte value = (byte)c;
            if (value < 65)
            {
            value = (byte)(value - 48);

            }
            else

            {
                value = (byte)(value
               - 55);

            }
            return value;

        }

    }
}
