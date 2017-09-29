using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

public class CSVManager
{
    public static CSVManager instance
    {
        get
        {
            if (_instance == null) _instance = new CSVManager();
            return _instance;
        }
    }

    private static CSVManager _instance;
    private Dictionary<string, Dictionary<string, float>> _csvDictionary;

    public CSVManager()
    {
        _csvDictionary = new Dictionary<string, Dictionary<string, float>>();
        ReadData();
    }

    private void ReadData()
    {
        string allData = Resources.Load<TextAsset>(K.CSV_PATH).text;
        string[] rows = allData.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        rows[0] = rows[0].Replace("name,", "");
        string[] headers = rows[0].Split(',');
        foreach (var row in rows.Skip(1))
        {
            string[] cols = row.Split(',');
            _csvDictionary[cols[0]] = new Dictionary<string, float>();
            int headerCount = 0;
            foreach (var col in cols.Skip(1))
            {
                _csvDictionary[cols[0]][headers[headerCount]] = float.Parse(col) / 100;
                headerCount++;
            }
        }
        //DebugData();
    }

    public float getData(string pasive, string modifier)
    {
        if (_csvDictionary.ContainsKey(pasive))
            if (_csvDictionary[pasive].ContainsKey(modifier)) return _csvDictionary[pasive][modifier];
            else return 0;
        else return 0;
    }

    private void DebugData()
    {
        foreach (var aux in _csvDictionary.Keys)
        {
            string debugString = aux;
            foreach (var aux2 in _csvDictionary[aux])
            {
                debugString += ", " + aux2.Key + ": " + aux2.Value;
            }
            Debug.Log(debugString);
        }
    }
}
