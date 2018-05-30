using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public static class Palyginimai
{
    public static int PalygtintiZodzius(string zodis, string zodis1)
    {
        if (zodis.Length == zodis1.Length)
        {
            for (int i = 0; i < zodis.Length; i++)
            {
                int palyginimas = KurisDidesnisIsSimboliu(zodis[i], zodis1[i]);
                if (palyginimas != 0)
                {
                    return palyginimas; 
                }
            }
            return 0;
        }
        else if (zodis.Length > zodis1.Length)
        {
            for(int i = 0; i < zodis.Length; i++)
            {
                int palyginimas = KurisDidesnisIsSimboliu(zodis[i], zodis1[i]);
                if (palyginimas != 0)
                {
                    return palyginimas;
                }
                if (zodis1.Length - 1 == i)
                {
                    return -1;
                }
            }
            return -1;
        }
        else
        {
            for (int i = 0; i < zodis1.Length; i++)
            {
                int palyginimas = KurisDidesnisIsSimboliu(zodis[i], zodis1[i]);
                if (palyginimas != 0)
                {
                    return palyginimas;
                }
                if (zodis.Length - 1 == i)
                {
                    return 1;
                }
            }
            return 1;
        }    
    }

    private static int KurisDidesnisIsSimboliu(char s1, char s2)
    {
        s1 = char.ToLower(s1);
        s2 = char.ToLower(s2);
        if (s1 > s2)
        {
            return -1;
        }
        else if (s1 < s2)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

}
