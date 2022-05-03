using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Calculator
{
    static public string Calculate(string input)
    {
        string output = GetExpression(input); 
        string result = Counting(output); 
        return result;
    }

    static private string GetExpression(string input)
    {
        string output = string.Empty; 
        Stack<char> operStack = new Stack<char>(); 

        for (int i = 0; i < input.Length; i++) 
        {
            if (IsDelimeter(input[i]))
                continue;

            if (char.IsDigit(input[i])) 
            {
                while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                {
                    output += input[i]; 
                    i++; 

                    if (i == input.Length) break; 
                }
                output += " "; 
                i--; 
            }

            if (IsOperator(input[i])) 
            {
                if (input[i] == '(') 
                    operStack.Push(input[i]); 
                else if (input[i] == ')') 
                {
                    try
                    {
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    catch
                    {
                        return "Ошибка";
                    }
                }
                else 
                {
                    if (operStack.Count > 0) 
                        if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                            output += operStack.Pop().ToString() + " "; 

                    operStack.Push(char.Parse(input[i].ToString()));

                }
            }
        }
        while (operStack.Count > 0)
            output += operStack.Pop() + " ";

        return output; 
    }

    static private string Counting(string input)
    {
        double result = 0; 
        Stack<double> temp = new Stack<double>(); 

        for (int i = 0; i < input.Length; i++) 
        {
            if (char.IsDigit(input[i]))
            {
                string a = string.Empty;

                while (!IsDelimeter(input[i]) && !IsOperator(input[i])) 
                {
                    a += input[i]; 
                    i++;
                    if (i == input.Length) break;
                }
                temp.Push(double.Parse(a)); 
                i--;
            }
            else if (IsOperator(input[i])) 
            {
                double a;
                double b;
                try
                {
                    a = temp.Pop();
                    b = temp.Pop();
                }
                catch
                {
                    return "Ошибка";
                }

                switch (input[i]) 
                {
                    case '+': result = b + a; break;
                    case '-': result = b - a; break;
                    case '*': result = b * a; break;
                    case '/': result = b / a; break;
                    case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                    case '%': result = 1/(a/b); break;
                }
                temp.Push(result); 
            }
        }

        return temp.Count != 0 ? temp.Peek().ToString() : "Ошибка";
    }

    static private bool IsDelimeter(char c)
    {
        if ((" =".IndexOf(c) != -1))
            return true;
        return false;
    }

    static private bool IsOperator(char с)
    {
        if (("%+-/*^()".IndexOf(с) != -1))
            return true;
        return false;
    }

    static private byte GetPriority(char s)
    {
        switch (s)
        {
            case '(': return 0;
            case ')': return 1;
            case '%': return 2;
            case '+': return 3;
            case '-': return 4;
            case '*': return 5;
            case '/': return 6;
            case '^': return 7;
            default: return 8;
        }
    }
}
