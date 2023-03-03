using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PotionInfo : MonoBehaviour
{
    public static PotionInfo instance;

    private List<string[]> PotionInfoList;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        PotionInfoList = ReadCSV("PotionInfo");
    }

    public float GetPotionInfo(int num)
    {
        Debug.Log("포션번호 = " + num);
        for(int i = 0; i< PotionInfoList.Count; i++)
        {
            if(i.Equals(num))
                return float.Parse(PotionInfoList[i][1]);
        }

        return 0;
    }

    public List<string[]> ReadCSV(string _fileName)
    {
        var list = new List<string[]>();
        TextAsset readFile = Resources.Load<TextAsset>("CSV/" + _fileName);
        StringReader sr = new StringReader(readFile.text);

        string line = string.Empty;

        int columnLength = sr.ReadLine().Split(",").Length;

        while ((line = sr.ReadLine()) != null)
        {
            string[] stringArray = new string[columnLength];
            //Debug.Log(line);
            string[] readArray = line.Split(",");
            for (int i = 0; i < columnLength; i++)
            {
                stringArray[i] = readArray[i];
            }
            list.Add(stringArray);
        }
        sr.Close();

        return list;
    }
}
