using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLULisp
{
    class varRecord
    {
        public String varName { get; set; }

        public String varValue { get; set; }


        public varRecord(String name, String value)
        {
            varName = name;

            varValue = value;
        
        }


    }
}
