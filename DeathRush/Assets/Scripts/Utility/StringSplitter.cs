using UnityEngine;
using System.Collections;

public class StringSplitter
{
    public static string Split(string s)
    {
        var splittedString = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (char.IsUpper(s[i]) && i != 0)
                splittedString += " ";
            splittedString += s[i];
        }
        return splittedString;
    }

    public static string Merge(string s)
    {
        var mergeString = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] != ' ')
                mergeString += s[i];
        }
        return mergeString;
    }
}
