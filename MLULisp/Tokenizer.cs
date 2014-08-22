using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace MLULisp
{
    class Tokenizer
    {
        static ArrayList FuncList=new ArrayList();

        static String[] TokenList = { "+","-","*","/","print" };

        static String[] KeyWords = { "let","defun" };
        private static string ERROR="snyax error";

        public  static Boolean IsNumberString(String testStr)
        {
                if (Regex.IsMatch(testStr, "^((\\+|-)\\d)?\\d*$"))
            {
                return true;// MessageBox.Show("all is number!");
            }
            else
            {
                return false;
            }

        }

        public static int[] GetMatchlocation(String str,String pattern)
        {
          
            MatchCollection mc =Regex.Matches(str, pattern);

            int[] MatchLocation=new int[mc.Count];

            for (int i = 0; i < mc.Count; i++)
            {
                MatchLocation[i]=(mc[i]).Index;
            }

            return MatchLocation;



        }
        public static ArrayList GetAllVar(ArrayList CodeSections) 
        {

            return ArrayListTools.GetMatchedList(CodeSections, KeyWords);
            
        
        }


        public static ArrayList GetCodeSections(String rawSRC) 
        {

            String[] sectionArray = rawSRC.Split(';');
            ArrayList sections = new ArrayList();

            sections = ArrayListTools.StringArrayToArrayList(sectionArray);

            return sections;

        
        
        }

        static int get_top_level_end_pair_token(string end_token,Char leftToken,Char rightToken)
        {
            int left_bracket_num = 0;


            for (int i = 0; i < end_token.Length; i++)
            {
                if (end_token[i].Equals(leftToken))
                {
                    left_bracket_num++;
                }
                else
                {
                    if (end_token[i].Equals(rightToken))
                    {
                        left_bracket_num--;
                        if (left_bracket_num == 0)
                        {
                            return i;
                        }
                    }
                }
            }
            return end_token.Length - 1;

        }


        static int get_top_level_end_token(string end_token)
        {
            int left_bracket_num = 0;


            for (int i = 0; i < end_token.Length; i++)
            {
                if (end_token[i].Equals('{'))
                {
                    left_bracket_num++;
                }
                else
                {
                    if (end_token[i].Equals('}'))
                    {
                        left_bracket_num--;
                        if (left_bracket_num == 0)
                        {
                            return i;
                        }
                    }
                }
            }
            return end_token.Length - 1;

        }

        public static String DealFuncDef( String FuncDef) ////process the defination of function
        {
            if (FuncDef.Contains(KeyWords[1]))
            {
                FuncDef = FuncDef.Replace(KeyWords[1], "");

            }
            if (FuncDef.Contains('<') && FuncDef.Contains('>'))
            {
                ///Firstly we need to parse out function name ,paramater,expression,and actual value-- Four parts in all.

                 int funcBodyEnd=get_top_level_end_pair_token(FuncDef, '(', ')');
                 String FuncBody = FuncDef.Substring(0, funcBodyEnd + 1);
                 FuncBody = RemoveTopOutBrackets(FuncBody);  //////here the shape is like: f(x):(exp)

                 String FuncName = FuncBody.Substring(0, FuncBody.IndexOf('('));
                 String FuncParam =RemoveTopOutBrackets( FuncBody.Substring(FuncBody.IndexOf('('), FuncBody.IndexOf(')')-FuncBody.IndexOf('(')+1));

                 String expression = FuncBody.Substring(FuncBody.IndexOf(':') + 1);

                 String ActualParam = RemoveTopOutArrowBrackets( FuncDef.Substring(funcBodyEnd + 1));

                ////add a function recode to function list
                //////////
                 if (FuncList.Count>0)
                 {
                     for (int i = 0; i < FuncList.Count; i++)
                     {
                         if (((FuncRecode)FuncList[i]).Name.Equals( FuncName))
                         {

                         }
                         else 
                         {
                             FuncRecode tempFuncRecode = new FuncRecode(FuncName, FuncDef);

                             FuncList.Add(tempFuncRecode);
                             break;
                         }
                        
                     }

                 }
                 else
                 {
                     FuncRecode tempFuncRecode = new FuncRecode(FuncName, FuncDef);

                     FuncList.Add(tempFuncRecode);
                 }
                
             

                 
                 expression = expression.Replace(FuncParam, ActualParam);
                
                 if (!expression.Contains(FuncName))  ///none recursive
                 {
                   

                     return DealExpression(expression);//Should be Deal statement here!
                 }
                 else   /// currying defination
                 {
                     expression = RemoveTopOutBrackets(expression);
                     return DealExpression(DealFuncDef(expression));
                 }


            }

            else 
            {

                return ERROR;
            
            
            }
        
        
        
        }


        public static int GetFuncInFuncList(String FuncName, ArrayList Flist) 
        {

            for (int i = 0; i < Flist.Count; i++)
            {

                if (((FuncRecode)Flist[i]).Name==FuncName)
                {
                    return i;
                }

            }
            return -1;/////it means function name does not exist in this list


        
        
        }

        public static String DealFuncCall(String callStatement) 
        {////
          ///////////The form should be :f<><><>...<Param n>

            int FunNameEnd=callStatement.IndexOf('<');

            String ParamString=callStatement.Substring(FunNameEnd);

            ArrayList pList =GetAllParams(ParamString);

            String GetFuncName=callStatement.Substring(0,FunNameEnd);


            if (GetFuncInFuncList(GetFuncName, Tokenizer.FuncList) != -1)//find func name in list
            {
                int[] fnameEndLocation = GetMatchlocation(callStatement, ":");

                for (int i = 0; i < fnameEndLocation.GetLength(0); i++)
                {
                    ReplaceParamN(i,pList[i].ToString());
                }
            
            }
            else 
            {
                return ERROR;

            
            }
        
        
        }


        public static String DealExpression(String expression)


        {


            // should simpily vars then  do the calulations
            ///////////////////////////////
            if (false==(expression[0].Equals('(')&&expression[expression.Length-1].Equals(')')))
            {
                if (IsNumberString(expression))
                {
                    return expression;
                }
                else
                {

                    return ERROR;
                }
            }
            else if ( false==excuteFun( expression).Equals(ERROR))
            {
                return excuteFun(expression);
            }
            else
            {
                ///////remove the top top brackets
                expression = expression.Substring(0, expression.Length - 1);
                expression = expression.Substring(1);

                String Operation = expression[0].ToString();

                int FirstleftBracket = expression.IndexOf('(');
                int FirstRightBracket = get_top_level_end_pair_token(expression, '(', ')');

                String leftExp = expression.Substring(FirstleftBracket, FirstRightBracket - FirstleftBracket + 1);

                String RightExp = expression.Substring(FirstRightBracket + 2);

                String leftResult = excuteFun(leftExp);
                if (leftResult.Equals(ERROR))
                {
                    leftResult = DealExpression(leftExp);


                }
                String rightResult = excuteFun(RightExp);

                if (rightResult.Equals(ERROR))
                {
                    rightResult = DealExpression(RightExp);
                }


                return DealExpression("(" + Operation + " " + leftResult + " " + rightResult + ")");

            }
        
        
        }

        public static String RemoveTopOutBrackets(String exp) /////eg. (2)--->2
        {
            if (exp.Length<2)
            {
                return exp;
            }
            else if (exp[0].Equals('(') && exp[exp.Length-1].Equals(')'))
            {
                exp = exp.Substring(0, exp.Length - 1);
                exp = exp.Substring(1);

            }
            return exp;
            
        
        }

        public static String RemoveTopOutArrowBrackets(String exp) /////eg. (2)--->2
        {
            if (exp.Length < 2)
            {
                return exp;
            }
            else if (exp[0].Equals('<') && exp[exp.Length - 1].Equals('>'))
            {
                exp = exp.Substring(0, exp.Length - 1);
                exp = exp.Substring(1);

            }
            return exp;


        }

        public static String excuteFun(String basic_expression) 
        {


            String[] tokens;

            if (basic_expression.Length >= 3)
            {
                basic_expression = basic_expression.Substring(0, basic_expression.Length - 1);
                basic_expression = basic_expression.Substring(1);

                tokens = basic_expression.Split(' ');



                if (tokens.GetLength(0) == 1)
                {
                    return basic_expression;
                }
                else if (tokens.GetLength(0) == 2)
                {
                    if (tokens[0].Equals(TokenList[4]))
                    {
                        return tokens[1];

                    }
                    else
                    {
                        return ERROR;
                    }

                }

                else if (tokens.GetLength(0) == 3)
                {
                    if (excuteFun( RemoveTopOutBrackets( tokens[2]))!=ERROR&&excuteFun( RemoveTopOutBrackets( tokens[1]))!=ERROR)
                    {

                        tokens[2] = RemoveTopOutBrackets(tokens[2]);
                        tokens[1] = (RemoveTopOutBrackets(tokens[1]));
                        if (tokens[0].Equals(TokenList[0]))//add
                        {
                            
                            
                            return Convert.ToString(Convert.ToInt32(tokens[2]) + Convert.ToInt32(tokens[1]));
                            
                           
                        }
                        else if (tokens[0].Equals(TokenList[1]))//sub
                        {
                            return Convert.ToString(Convert.ToInt32(tokens[1]) - Convert.ToInt32(tokens[2]));
                        }
                        else if (tokens[0].Equals(TokenList[2]))//multiply
                        {
                            return Convert.ToString(Convert.ToInt32(tokens[1]) * Convert.ToInt32(tokens[2]));
                        }

                        else if (tokens[0].Equals(TokenList[1]))//divide
                        {
                            return Convert.ToString(Convert.ToInt32(tokens[1]) / Convert.ToInt32(tokens[2]));
                        }
                        else
                        {
                            return ERROR;
                        }
                    }
                    else
                    {
                        return ERROR;
                    }

                    

                }
                else
                {
                    return ERROR;
                }


            }
            else
            {
                if (IsNumberString( basic_expression))
                {
                    return basic_expression;
                }
                else
                {
                    return ERROR;
                }
                
            }
        }


     
    }
}
