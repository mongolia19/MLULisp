using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MLULisp
{
    class FuncRecode
    {

        String Name { get; set; }

        String Body { get; set; }

        ArrayList ParamList;

        public FuncRecode(String name, String body) 
        {
            Name = name;

            Body = body;

            ParamList = new ArrayList();

        
        }

        public void AddOneParam(String p) 
        {
            ParamList.Add(p);

        
        }
        public int GetParamNum()
        {
            return ParamList.Count;
        
        }

        public String GetParamN(int i) // return the Nth formal parameter
        {
            if (i>=ParamList.Count)
	        {
		        return "0";
	        }
            else
	        {
                return ParamList[i].ToString();

	        }
       
        
        }
    }
}
