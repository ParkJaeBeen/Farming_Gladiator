using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPriceScript : MonoBehaviour
{
    [SerializeField]
    private List<Transform> ShopList;
    public List<TextMeshProUGUI> WoodText, RockText, OreText, HerbText, FlowerText, MushroomText;
    // Start is called before the first frame update
    void Start()
    {
        AddWoodText();
    }

    public void TotalChangeText()
    {
        ChangeText("WoodText");
        ChangeText("RockText");
        ChangeText("OreText");
        ChangeText("HerbText");
        ChangeText("FlowerText");
        ChangeText("MushroomText");
    }

    public void ChangeWepArm()
    {
        ChangeText("WoodText");
        ChangeText("RockText");
        ChangeText("OreText");
    }

    public void ChangeETC()
    {
        ChangeText("HerbText");
        ChangeText("FlowerText");
        ChangeText("MushroomText");
    }

    public void ChangeText(string listname)
    {
        List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>();
        int count = 0;
        switch (listname)
        {
            case "WoodText":
                textList = WoodText;
                count = Character.instance.inventory.ResourceArray[0];
                break;
            case "RockText":
                textList = RockText;
                count = Character.instance.inventory.ResourceArray[1];
                break;
            case "OreText":
                textList = OreText;
                count = Character.instance.inventory.ResourceArray[2];
                break;
            case "HerbText":
                textList = HerbText;
                count = Character.instance.inventory.ResourceArray[3];
                break;
            case "FlowerText":
                textList = FlowerText;
                count = Character.instance.inventory.ResourceArray[4];
                break;
            case "MushroomText":
                textList = MushroomText;
                count = Character.instance.inventory.ResourceArray[5];
                break;
        }

        for(int i = 0; i < textList.Count; i++)
        {
            textList[i].text = count.ToString();
        }
    }

    void AddWoodText()
    {
        for(int i = 0; i < ShopList.Count; i++)
        {
            for(int j = 0; j < ShopList[i].childCount; j++)
            {
                if (ShopList[i].GetChild(j).GetChild(0).name.Contains("Wood"))
                    WoodText.Add(ShopList[i].GetChild(j).GetChild(0).GetComponent<TextMeshProUGUI>());

                if (ShopList[i].GetChild(j).GetChild(1).name.Contains("Rock"))
                    RockText.Add(ShopList[i].GetChild(j).GetChild(1).GetComponent<TextMeshProUGUI>());

                if (ShopList[i].GetChild(j).GetChild(2).name.Contains("Ore"))
                    OreText.Add(ShopList[i].GetChild(j).GetChild(2).GetComponent<TextMeshProUGUI>());

                if (ShopList[i].GetChild(j).GetChild(0).name.Contains("Herb"))
                    HerbText.Add(ShopList[i].GetChild(j).GetChild(0).GetComponent<TextMeshProUGUI>());

                if (ShopList[i].GetChild(j).GetChild(1).name.Contains("Flower"))
                    FlowerText.Add(ShopList[i].GetChild(j).GetChild(1).GetComponent<TextMeshProUGUI>());

                if (ShopList[i].GetChild(j).GetChild(2).name.Contains("Mushroom"))
                    MushroomText.Add(ShopList[i].GetChild(j).GetChild(2).GetComponent<TextMeshProUGUI>());
            }
        }

    }
}
