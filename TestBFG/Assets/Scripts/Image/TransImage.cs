using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransImage : MonoBehaviour
{
    public static TransImage S;

    [SerializeField]
    private Texture2D colorTxtr;
    private Color[,] color;
    private Color[,] colorBlack;

    private void Start()
    {
        S = this;
        color = new Color[colorTxtr.width, colorTxtr.height];
        colorBlack = new Color[colorTxtr.width, colorTxtr.height];

        for (int x = 0; x < color.GetLength(0); x++)
        {
            for (int y = 0; y < color.GetLength(1); y++)
            {
                color[x, y] = colorTxtr.GetPixel(x, y);
                colorBlack[x, y] = SetBlackWhite(color[x, y]);
            }
        }
    }

    private Color SetBlackWhite(Color clr)
    {
        clr.r = clr.g = clr.b = (clr.r + clr.g + clr.b) / 3.0f;
        return clr;
    }

    private void ChangeColor(Color[,] clr)
    {
        for (int x = 0; x < colorTxtr.width; x++)
        {
            for (int y = 0; y < colorTxtr.height; y++)
            {
                colorTxtr.SetPixel(x, y, clr[x,y]);
            }
        }
        colorTxtr.Apply();
    }

    public void Change(int i)
    {
        if (i == 0)
        {
            ChangeColor(colorBlack);
        }
        else if (i == 1)
        {
            ChangeColor(color);
        }
    }
}

