using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MLULisp
{
    class LittleVM
    {

        ArrayList Codes;

        public int ProgramPointer { set; get; }
        Boolean Stop = false;
        public LittleVM(ArrayList src) 
        {
            Codes = src;

        
        }

        public String Excute()
        {
            String output = "";
            while (!StopOrNot())
            {
                String stateI = Codes[ProgramPointer].ToString();

                if (stateI.Substring(0,4).Equals (Tokenizer.KeyWords[5]))//goto 
                {
                    ProgramPointer = Convert.ToInt32(Tokenizer.DealStatement(stateI));
                   
                }
                else if (stateI.Substring(1,2).Equals(Tokenizer.KeyWords[3]))//if
                {
                        String result= Tokenizer.DealStatement(stateI);
                        if (result.Equals("0"))
	                {
		                IncPC();
	                }
                        else
	                {
                        output += result + "\r\n";
                        IncPC();
                        IncPC();
	                }
                   
                }
                else//other 
                {
                    output += Tokenizer.DealStatement(stateI) + "\r\n";

                    IncPC();

                }
            }

            return output;
            
        }

        public void IncPC() 
        {
            if (Codes.Count - 1 > ProgramPointer)
            {
                ProgramPointer++;
            }
            else 
            {
                Stop = true;
                return;
            
            
            }
        
        }

        public Boolean StopOrNot()
        {
            return Stop;
        
        
        
        }



    }
}
