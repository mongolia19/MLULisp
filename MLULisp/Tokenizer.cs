using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MLULisp
{
    class Tokenizer
    {

        static String[] TokenList = { "+","-","*","/","print" };

        static String[] KeyWords = { "let" };
        private static string ERROR="snyax error";

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


        public static String DealExpression(String expression)


        {


            // should simpily vars then  do the calulations
            ///////////////////////////////
            if (false==(expression[0].Equals("(")&&expression[expression.Length-1].Equals(")")))
            {
                return ERROR;

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


        public static String excuteFun(String basic_expression) 
        {


            String[] tokens;

            if (basic_expression.Length >= 3)
            {


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


     
    }
}
