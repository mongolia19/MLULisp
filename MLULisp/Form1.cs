using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MLULisp
{
    public partial class Form1 : Form
    {
        static String defun = "defun";

        ArrayList codeSeg;

        String OutputString = "";


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            codeSeg =  Tokenizer.GetCodeSections(SrcFiletextBox.Text);
            for (int i = 0; i < codeSeg.Count; i++)
            {

                if (codeSeg[i].ToString().Contains(defun))
                {
                    OutputString += Tokenizer.DealFuncDef(codeSeg[i].ToString()) + "\r\n";
                }
                else
                {
                    OutputString += Tokenizer.DealExpression(codeSeg[i].ToString()) + "\r\n";
                }
            }
            //OutputString=Tokenizer.excuteFun(codeSeg[0].ToString());

            OutputLabel.Text=OutputString;

        }

        private void Form1_Load(object sender, EventArgs e)
        {


            codeSeg = new ArrayList();
        }
    }
}
