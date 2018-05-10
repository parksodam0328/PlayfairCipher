using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 정보보호
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
			encryption = new Encryption();
            //program.initialize();
        }
        private Encryption encryption;

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{

			if (textBox1.Text == "")
			{
				//암호키 텍스트 박스가 공백일 경우
				MessageBox.Show("암호키를 입력해주세요");
				return;
			}
			else if (textBox2.Text == "")
			{
				//평문 텍스트 박스가 공백일 경우
				MessageBox.Show("평문을 입력해주세요");
				return;
			}

			textBox3.Text = encryption.MultipleCipher(textBox1.Text, textBox2.Text);
			textBox4.Text = encryption.MultipleEncryption(textBox1.Text, textBox2.Text);
			
		}
	}
}
