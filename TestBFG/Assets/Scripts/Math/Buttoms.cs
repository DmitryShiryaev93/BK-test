using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Buttoms : MonoBehaviour
{
    [SerializeField]
    private Text txtInput;
    [SerializeField]
    private Text txtResult;
    private string str;
    private bool openCloseBracket = false;

    public void ButtomNumber(string c)
    {
        if (str != null && str[str.Length - 1] == ')')
        {
            str += '*' + c;
        }
        else
        {
            str += c;
        }
        txtInput.text = str;
    }

    public void BackSpace()
    {
        if(str != null && str.Length > 0)
        {
            str = str.Remove(str.Length - 1);
            txtInput.text = str;
        }
    }


    public void Clear()
    {
        txtInput.text = "";
        str = null;
    }

    public void Result()
    {
        if(str != null)
        {
            txtResult.text = Calculator.Calculate(str);
        }
    }


    public void Sqrt()
    {
        if(str != null && str.Length > 0)
        {
            str += "^(1/2)";
            txtInput.text = str;
        }
        else
        {
            txtInput.text = "Введите число";
        }
    }

    public void CreateBracket()
    {
        if(str != null && char.IsDigit(str[str.Length-1]))
        {
            str += "*(";
            txtInput.text = str;
        }
        else
        {
            str += "(";
            txtInput.text = str;
        }
        openCloseBracket = true;
    }

    public void CloseBracket()
    {
        if (openCloseBracket)
        {
            str += ")";
            txtInput.text = str;
        }
        openCloseBracket = false;
    }


    public void ButtomSign(string c)
    {
        str += c;
        txtInput.text = str;
    }
}
