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
        private String callfun = "callfun";


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            codeSeg =  Tokenizer.GetCodeSections(SrcFiletextBox.Text);
            for (int i = 0; i < codeSeg.Count; i++)
            {
               String stateI=  codeSeg[i].ToString();
                if (stateI.Contains(defun))
                {
                    OutputString += Tokenizer.DealFuncDef(stateI) + "\r\n";
                }
                else if (stateI.Contains(callfun))
                {
                    OutputString += Tokenizer.DealFuncCall(stateI) + "\r\n";
                }
                else
                {
                    OutputString += Tokenizer.DealExpression(stateI) + "\r\n";
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
