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

        private LittleVM vm ;
        public Form1()
        {
            InitializeComponent();
        }
        static  void SetDataGridViewWithArrayList(DataGridView DV,ArrayList AL,String col_name,String title)
        {
            DV.Columns.Clear();
            DV.Columns.Add(col_name,title);
            DV.Rows.Clear();
            for (int i = 0; i < AL.Count; i++)
            {
                 DV.Rows.Add();
                DV.Rows[i].Cells[0].Value = AL[i].ToString();
            }


        }

         DataGridView SetVarDataGridViewWithArrayList( DataGridView DV, ArrayList AL, String col_name, String title)
        {

            VarlistDataGridView.Columns.Clear();
            VarlistDataGridView.Columns.Add(col_name, title);
            VarlistDataGridView.Rows.Clear();
            for (int i = 0; i < AL.Count; i++)
            {
                VarlistDataGridView.Rows.Add();
                VarlistDataGridView.Rows[i].Cells[0].Value = (((varRecord)AL[i]).varName + ":" + ((varRecord)AL[i]).varValue).ToString();
            }

            return VarlistDataGridView;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            codeSeg =  Tokenizer.GetCodeSections(SrcFiletextBox.Text);
            vm = new LittleVM(codeSeg);
            Tokenizer.RunningVM = vm;

            
            DebugDataGridView.Columns.Clear();
            DebugDataGridView.Columns.Add("codeCol","src");

            DebugDataGridView.Rows.Clear();
            //DebugDataGridView.DataSource = codeSeg;
            for (int i = 0; i < codeSeg.Count; i++)
            {
                DebugDataGridView.Rows.Add();
                DebugDataGridView.Rows[i].Cells[0].Value = codeSeg[i].ToString();
            }

         
          // VarlistDataGridView= SetVarDataGridViewWithArrayList(VarlistDataGridView, Tokenizer.VarList, "VarTable", "Vars");


            OutputString=vm.Excute();


            VarlistDataGridView.Columns.Clear();
            VarlistDataGridView.Columns.Add("codeCol", "src");

            VarlistDataGridView.Rows.Clear();
            //DebugDataGridView.DataSource = codeSeg;
            for (int i = 0; i < Tokenizer.VarList.Count; i++)
            {
                VarlistDataGridView.Rows.Add();
                VarlistDataGridView.Rows[i].Cells[0].Value = (((varRecord)Tokenizer.VarList[i]).varName + ":" + ((varRecord)Tokenizer.VarList[i]).varValue).ToString();
            }
            //for (int i = 0; i < codeSeg.Count; i++)
            //{
            //   String stateI=  codeSeg[i].ToString();
            //   OutputString+= Tokenizer.DealStatement(stateI) + "\r\n";
            //   vm.IncPC();
            //    //if (stateI.Contains(defun))
            //    //{
            //    //    OutputString += Tokenizer.DealFuncDef(stateI) + "\r\n";
            //    //}
            //    //else if (stateI.Contains(callfun))
            //    //{
            //    //    OutputString += Tokenizer.DealFuncCall(stateI) + "\r\n";
            //    //}
            //    //else
            //    //{
            //    //    OutputString += Tokenizer.DealExpression(stateI) + "\r\n";
            //    //}
            //}
            //OutputString=Tokenizer.excuteFun(codeSeg[0].ToString());

            OutputLabel.Text=OutputString;

        }

        private void Form1_Load(object sender, EventArgs e)
        {


            codeSeg = new ArrayList();

            vm = new LittleVM(codeSeg);

        }
    }
}
