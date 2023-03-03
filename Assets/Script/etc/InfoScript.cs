using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InfoScript : MonoBehaviour
{
    public static InfoScript instance;
    private List<string[]> WeaponInfoList, ArmorInfoList, PotionInfoList;

    private Sprite[] _woodWep, _ironWep, _blackWep, _woodArm, _ironArm;

    public Sprite[] WoodWep { get => _woodWep; set => _woodWep = value; }
    public Sprite[] IronWep { get => _ironWep; set => _ironWep = value; }
    public Sprite[] BlackWep { get => _blackWep; set => _blackWep = value; }
    public Sprite[] WoodArm { get => _woodArm; set => _woodArm = value; }
    public Sprite[] IronArm { get => _ironArm; set => _ironArm = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        _woodWep = Resources.LoadAll<Sprite>("Icon/Weapon/wooden_weapon");
        _ironWep = Resources.LoadAll<Sprite>("Icon/Weapon/iron_weapon");
        _blackWep = Resources.LoadAll<Sprite>("Icon/Weapon/Black_Weapon");
        _woodArm = Resources.LoadAll<Sprite>("Icon/Armor/Wood_Armor");
        _ironArm = Resources.LoadAll<Sprite>("Icon/Armor/Iron_Armor");

        WeaponInfoList = ReadCSV("WeaponInfo");
        ArmorInfoList = ReadCSV("ArmorInfo");
        PotionInfoList = ReadCSV("PotionInfo");
    }

    public string[] GetListInfo(string itemName, string type)
    {
        if (type.Equals("Weapon"))
        {
            for(int i = 0; i < WeaponInfoList.Count; i++)
            {
                if (WeaponInfoList[i][0].Equals(itemName))
                {
                    return WeaponInfoList[i];
                }
            }
        }
        else if (type.Equals("Armor"))
        {
            for (int i = 0; i < ArmorInfoList.Count; i++)
            {
                if (ArmorInfoList[i][0].Equals(itemName))
                {
                    return ArmorInfoList[i];
                }
            }
        }
        else if (type.Equals("Potion"))
        {
            for (int i = 0; i < PotionInfoList.Count; i++)
            {
                if (PotionInfoList[i][0].Equals(itemName))
                {
                    return PotionInfoList[i];
                }
            }
        }
        return null;
    }

    public bool WeaponPriceCheck(string name)
    {
        for (int i = 0; i < WeaponInfoList.Count; i++)
        {
            if (WeaponInfoList[i][0].Equals(name))
            {
                if (Character.instance.inventory.ResourceArray[0] >= int.Parse(WeaponInfoList[i][4]) &&
                   Character.instance.inventory.ResourceArray[1] >= int.Parse(WeaponInfoList[i][5]) &&
                   Character.instance.inventory.ResourceArray[2] >= int.Parse(WeaponInfoList[i][6]))
                    return true;
            }
        }
        return false;
    }

    public bool ArmorPriceCheck(string name)
    {
        for (int i = 0; i < ArmorInfoList.Count; i++)
        {
            if (ArmorInfoList[i][0].Equals(name))
            {
                if (Character.instance.inventory.ResourceArray[0] >= int.Parse(ArmorInfoList[i][3]) &&
                   Character.instance.inventory.ResourceArray[1] >= int.Parse(ArmorInfoList[i][4]) &&
                   Character.instance.inventory.ResourceArray[2] >= int.Parse(ArmorInfoList[i][5]))
                    return true;
            }
        }
        return false;
    }

    public bool PotionPriceCheck(string name)
    {
        for (int i = 0; i < PotionInfoList.Count; i++)
        {
            if (PotionInfoList[i][0].Equals(name))
            {
                if (Character.instance.inventory.ResourceArray[3] >= int.Parse(PotionInfoList[i][2]) &&
                   Character.instance.inventory.ResourceArray[4] >= int.Parse(PotionInfoList[i][3]) &&
                   Character.instance.inventory.ResourceArray[5] >= int.Parse(PotionInfoList[i][4]))
                    return true;
            }
        }
        return false;
    }

    public float GetArrowDamage(string name)
    {
        float Damage = 0;

        switch(name)
        {
            case "Wood_Arrow":
                Damage += float.Parse(WeaponInfoList[6][1]);
                break;
            case "Iron_Arrow":
                Damage += float.Parse(WeaponInfoList[13][1]);
                break;
            case "Black_Arrow":
                Damage += float.Parse(WeaponInfoList[19][1]);
                break;
        }

        return Damage;
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
