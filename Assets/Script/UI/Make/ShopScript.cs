using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField]
    private Image Shop, InfoPopup, BuyPopup;

    private GameObject SelectedItem;

    private void Start()
    {
        
    }

    public void BuyItem()
    {
        BuyPopup.gameObject.SetActive(true);
        TextMeshProUGUI BuyText = BuyPopup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        BuyText.text = EventSystem.current.currentSelectedGameObject.name;
        SelectedItem = EventSystem.current.currentSelectedGameObject;
    }

    public void ConfirmBuy()
    {
        string ItemName = SelectedItem.transform.Find("Image").GetComponent<Image>().sprite.name;
        Debug.Log("선택한 아이템 이름 = " + ItemName);
        if (Character.instance.inventory.CheckOverLap(ItemName))
        {
            Debug.Log("있는 아이템");
            MakeCanvasScript.instance.StartETCor("중복된 아이템입니다");
            return;
        }

        if (ItemName.Contains("Sword") || ItemName.Contains("Shield") || ItemName.Contains("Hammer") ||
            ItemName.Contains("Mace") || ItemName.Contains("Axe") || ItemName.Contains("Bow"))
        {
            if (InfoScript.instance.WeaponPriceCheck(ItemName))
            {
                string[] WeaponInfo = InfoScript.instance.GetListInfo(ItemName, "Weapon");
                
                Character.instance.inventory.RemoveElement(int.Parse(WeaponInfo[4]), int.Parse(WeaponInfo[5]), int.Parse(WeaponInfo[6]), 0, 0, 0, "Weapon");
                Character.instance.inventory.AddItem(ItemName);
                GameManager.instance.SoundManager.Effect.EffectSound(4);
                MakeCanvasScript.instance.ShopPriceScript.ChangeWepArm();
            }
            else
                MakeCanvasScript.instance.StartETCor("재료가 부족합니다");
        }
        else if (ItemName.Contains("Helmet") || ItemName.Contains("Upper") || ItemName.Contains("Under") || ItemName.Contains("Shoe"))
        {
            if (InfoScript.instance.ArmorPriceCheck(ItemName))
            {
                string[] ArmorInfo = InfoScript.instance.GetListInfo(ItemName, "Armor");
                Character.instance.inventory.RemoveElement(int.Parse(ArmorInfo[3]), int.Parse(ArmorInfo[4]), int.Parse(ArmorInfo[5]), 0, 0, 0, "Armor");
                Character.instance.inventory.AddItem(ItemName);
                GameManager.instance.SoundManager.Effect.EffectSound(4);
                MakeCanvasScript.instance.ShopPriceScript.ChangeWepArm();
            }
            else
                MakeCanvasScript.instance.StartETCor("재료가 부족합니다");
        }
        else if (ItemName.Contains("Arrow"))
        {
            if (InfoScript.instance.WeaponPriceCheck(ItemName))
            {
                string[] ArrowInfo = InfoScript.instance.GetListInfo(ItemName, "Weapon");
                Character.instance.inventory.RemoveElement(int.Parse(ArrowInfo[4]), int.Parse(ArrowInfo[5]), int.Parse(ArrowInfo[6]), 0, 0, 0, "Weapon");
                Character.instance.inventory.AddArrow(ItemName);
                GameManager.instance.SoundManager.Effect.EffectSound(4);
                MakeCanvasScript.instance.ShopPriceScript.ChangeWepArm();
            }
            else
                MakeCanvasScript.instance.StartETCor("재료가 부족합니다");
        }
        else if (ItemName.Contains("Potion"))
        {
            if (InfoScript.instance.PotionPriceCheck(ItemName))
            {
                string[] PotionInfo = InfoScript.instance.GetListInfo(ItemName, "Potion");
                Character.instance.inventory.RemoveElement(0, 0, 0, int.Parse(PotionInfo[2]), int.Parse(PotionInfo[3]), int.Parse(PotionInfo[4]), "Potion");
                Character.instance.inventory.AddPotion(ItemName);
                GameManager.instance.SoundManager.Effect.EffectSound(4);
                MakeCanvasScript.instance.ShopPriceScript.ChangeETC();
            }
            else
                MakeCanvasScript.instance.StartETCor("재료가 부족합니다");
        }

        
        BuyPopup.gameObject.SetActive(false);
    }

    public void CancelBuy()
    {
        BuyPopup.gameObject.SetActive(false);
    }

    public void ChangeShop(int shopNum)
    {
        for(int i = 0; i< Shop.transform.childCount; i++)
        {
            if (i.Equals(shopNum))
                Shop.transform.GetChild(i).gameObject.SetActive(true);
            else
                Shop.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
