using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MLULisp
{
    class ArrayListTools
    {

        public static ArrayList StateMentCleaner(ArrayList arr) ///remove spaces and \r\n s and empty ones
        {

            for (int i = 0; i < arr.Count; i++)
            {
                arr[i] = arr[i].ToString().Trim().Replace("\r\n", "");
                if (arr[i].ToString().Length <= 0)
                {

                    arr.RemoveAt(i);

                
                }
            }

            return arr;

        }
        public static ArrayList StringArrayToArrayList(String[] array) 
        {
            ArrayList List = new ArrayList();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i].Length>0&&(array[i].Equals("")==false))
                {
                    List.Add(array[i]);
                }
            }


            return List;
        
        }

        public static ArrayList GetMatchedList(ArrayList origin ,String [] pattern)
        {
            ArrayList MatchedList = new ArrayList();

            for (int i = 0; i < origin.Count; i++)
            {
                for (int j = 0; j < pattern.GetLength(0); j++)
                {
                    if (origin[i].ToString().Contains(pattern[j]))
                    {
                        MatchedList.Add(origin[i]);
                        break;
                    }
                }

            }
            return MatchedList;

        
        }



    }
}
