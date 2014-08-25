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
        static ArrayList VarList = new ArrayList();
        static String[] TokenList = { "+","-","*","/","print" };

        static String[] KeyWords = { "let", "defun", "callfun","if" };
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
            if (FuncDef.Substring(0,5).Equals(KeyWords[1]))
            {
                FuncDef = FuncDef.Substring(5);

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

                 ///////////substitution of var and fun
                ////Should substute the variables here 
                 ActualParam = DealStatement(ActualParam);
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


        public static String DealLet(String LetSentence) 
        {
            String []TokenSegs;
            if (LetSentence.Contains(KeyWords[0]))
            {
                LetSentence=LetSentence.Replace(KeyWords[0], "").Trim();

                TokenSegs= LetSentence.Split('=');
                if (TokenSegs.GetLength(0) == 1)
                {
                    Tokenizer.VarList.Add(new varRecord(TokenSegs[0], "0"));
                    return "0";
                }
                else
                {
                    for (int i = 0; i < TokenSegs.GetLength(0); i++)
                    {
                        TokenSegs[i] = TokenSegs[i].Trim();

                    }

                    Tokenizer.VarList.Add(new varRecord(TokenSegs[0], DealExpression(TokenSegs[1])));
                    return DealExpression(TokenSegs[1]);
                }
               

            
            }

            else
            {
                return ERROR;
            }
        }


        public static String DealStatement(String statement) 
        {
            statement = statement.Trim();
            if (statement.Length < 3)
            {
                return statement;
            }
            else if(statement.Substring(0,1)=='('.ToString())
            {
                return DealExpression(statement);
            }
            else if (statement.Substring(0,1)==TokenList[0]||statement.Substring(0,1)==TokenList[1]||statement.Substring(0,1)==TokenList[2]||statement.Substring(0,1)==TokenList[3])
            {
                return DealExpression(statement);
            }
            else if (statement.Substring(0,3)==KeyWords[0])//let  declear a variable
            {
                return DealLet(statement);
            }
            else if(statement.Substring(0,5)==KeyWords[1])///defun define a function
            {
                return DealFuncDef(statement);
            
            }
            else if (statement.Substring(0, 7) == KeyWords[2])///callfun call a function
            {
                return DealFuncCall(statement);
            }
            else if (statement.Substring(0,2)==KeyWords[3])///if statement
	        {
                return DealIf(statement);
	        }
            
            else
            {
                return ERROR;
            }

        
        }

        private static string DealIf(string expression)
        {

            expression = expression.Substring(3);

                int FirstleftBracket = expression.IndexOf('(');
                int FirstRightBracket = get_top_level_end_pair_token(expression, '(', ')');

                String leftExp = expression.Substring(FirstleftBracket, FirstRightBracket - FirstleftBracket + 1);

                String RightExp = expression.Substring(FirstRightBracket + 2);

                String leftResult = excuteFun(leftExp);
                if (leftResult.Equals(ERROR))
                {
                    //changed
                    leftResult = DealStatement(leftExp);


                }

                if (leftResult != "0")
                {
                    String rightResult = excuteFun(RightExp);

                    if (rightResult.Equals(ERROR))
                    {
                        //changed
                        rightResult = DealStatement(RightExp);
                    }
                    return rightResult;
                }
                else
                
                {
                    return "0";
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


            if (callStatement.Contains("callfun "))
            {
                callStatement = callStatement.Replace("callfun ", "");

            }
            
            int FunNameEnd=callStatement.IndexOf('<');

            String ParamString=callStatement.Substring(FunNameEnd);

            ArrayList pList =GetAllParams(ParamString);

            String GetFuncName=callStatement.Substring(0,FunNameEnd);
         
            if (GetFuncInFuncList(GetFuncName, Tokenizer.FuncList) != -1)//find func name in list
            {

                String FuncBody = ((FuncRecode)Tokenizer.FuncList[GetFuncInFuncList(GetFuncName, Tokenizer.FuncList)]).Body;
                int[] fnameEndLocation = GetMatchlocation(FuncBody, ":");

                for (int i = 0; i < fnameEndLocation.GetLength(0); i++)
                {
                    //substute variables here
                    FuncBody=ReplaceParamN(fnameEndLocation[i],FuncBody,pList[i].ToString());

                }

                ////
                ///After replacing the formal parameters we can use fundefination handler to deal with the function now
                /////

                return DealFuncDef(FuncBody);////Now it is a defun statement
            
            }
            else 
            {
                return ERROR;

            
            }
        
        
        }

        private static String ReplaceParamN(int i, String fBody, String Actualparam)/////(f(x):[you are here]())<p> <--trying to get the p there
        {
            String PartOne = fBody.Substring(0, i + 1);

            String LeftPart = fBody.Substring(i+1);////(expression))    without <pi>...<pi+1><pi+2>
            String ExpressionPart=("("+ LeftPart ) .Substring(0,get_top_level_end_pair_token(("("+LeftPart),'(',')')+1);
            ExpressionPart = ExpressionPart.Substring(1);
            LeftPart = LeftPart.Replace(ExpressionPart, "");
            String PartThree;///the following parameters 

            int FirstEndSimbolIndex = get_top_level_end_pair_token(LeftPart, '<', '>');

            if (LeftPart.Length - 1 <=FirstEndSimbolIndex)
            {
                PartThree = "";
            }
            else
            {
                PartThree= LeftPart.Substring(FirstEndSimbolIndex + 1);///get the following paramters
            }
           
            

            

            String NewfBody = PartOne +ExpressionPart + "<" + Actualparam + ">" + PartThree;
            //ToBeExtracted=ToBeExtracted.Substring(get_top_level_end_pair_token(ToBeExtracted,'(',')'));/////))<p>
            //ToBeExtracted = ToBeExtracted.Substring(ToBeExtracted.IndexOf('<'), get_top_level_end_pair_token(ToBeExtracted, '<', '>') - ToBeExtracted.IndexOf('<')+1);
            //String formParam = RemoveTopOutArrowBrackets(ToBeExtracted);
            //fBody=fBody.Replace(formParam, Actualparam);
            
            return NewfBody;
        }

        private static ArrayList GetAllParams(string pString)
        {
            ArrayList resultList = new ArrayList();
            while (pString.Length > 0)
            {
                String tempParam;
                tempParam=pString.Substring(pString.IndexOf('<'), get_top_level_end_pair_token(pString, '<', '>') - pString.IndexOf('<')+1);
                if (pString.Length-1== get_top_level_end_pair_token(pString, '<', '>'))
                {
                    pString="";
                }
                else
                {
                    pString = pString.Substring(get_top_level_end_pair_token(pString, '<', '>')+1);//get the left in the String
                }
                tempParam = RemoveTopOutArrowBrackets(tempParam);
                resultList.Add(tempParam);
                if (pString.Length<=1)
                {
                    break;
                }
            
            }
            return resultList;
           
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
                    if (expression.Contains('('))
                    {
                            String Operation = expression[0].ToString();

                            int FirstleftBracket = expression.IndexOf('(');
                            int FirstRightBracket = get_top_level_end_pair_token(expression, '(', ')');

                            String leftExp = expression.Substring(FirstleftBracket, FirstRightBracket - FirstleftBracket + 1);

                            String RightExp = expression.Substring(FirstRightBracket + 2);

                            String leftResult = excuteFun(leftExp);
                            if (leftResult.Equals(ERROR))
                            {
                                //changed
                                leftResult = DealStatement(leftExp);


                            }
                            String rightResult = excuteFun(RightExp);

                            if (rightResult.Equals(ERROR))
                            {
                                //changed
                                rightResult = DealStatement(RightExp);
                            }


                            return DealExpression( Operation + " " + leftResult + " " + rightResult );
                    }
                    else
                    {
                        String[] TokenSeg = expression.Split(' ');

                        String Operation = expression[0].ToString();

                        if (Operation.Equals(TokenList[0]))//add
                        {


                            return Convert.ToString(Convert.ToInt32(TokenSeg[2]) + Convert.ToInt32(TokenSeg[1]));


                        }
                        else if (Operation.Equals(TokenList[1]))//sub
                        {
                            return Convert.ToString(Convert.ToInt32(TokenSeg[1]) - Convert.ToInt32(TokenSeg[2]));
                        }
                        else if (Operation.Equals(TokenList[2]))//multiply
                        {
                            return Convert.ToString(Convert.ToInt32(TokenSeg[1]) * Convert.ToInt32(TokenSeg[2]));
                        }

                        else if (Operation.Equals(TokenList[1]))//divide
                        {
                            return Convert.ToString(Convert.ToInt32(TokenSeg[1]) / Convert.ToInt32(TokenSeg[2]));
                        }
                        else
                        {
                            return ERROR;
                        }
                    
                    }
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
                    //changed
                    leftResult = DealStatement(leftExp);


                }
                String rightResult = excuteFun(RightExp);

                if (rightResult.Equals(ERROR))
                {
                    //changed
                    rightResult = DealStatement(RightExp);
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
                basic_expression = DealStatement(basic_expression);
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
