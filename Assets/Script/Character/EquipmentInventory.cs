using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentInventory : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Image selectedIcon;
    [SerializeField]
    private List<TextMeshProUGUI> ResourceTextList, UseableTextList;

    private int selectedSlot;

    private string[] Weapons = new string[] { "Sword", "Mace", "Hammer", "Axe" };
    private string[] Armors = new string[] { "Helmet", "Upper", "Under", "Shoe" };

    public List<Slot> slotList;

    public void InitSlot()
    {
        for (int i = 0; i < 20; i++)
        {
            slotList[i].slotIcon.sprite = null;
        }
    }

    public void InitResourceTextList()
    {
        for (int i = 0; i < ResourceTextList.Count; i++)
        {
            ResourceTextList[i].text = Character.instance.inventory.ResourceArray[i].ToString();
        }
    }

    public void InitUseableTextList()
    {
        for (int i = 0; i < UseableTextList.Count; i++)
        {
            UseableTextList[i].text = Character.instance.inventory.UseableArray[i].ToString();
        }
    }

    public void ChangeSlotIcon()
    {
        List<Sprite> itemSpriteList = new List<Sprite>();

        if (Character.instance.inventory.EquipmentList.Count >= 1)
        {
            for (int i = 0; i < Character.instance.inventory.EquipmentList.Count; i++)
            {
                for (int j = 0; j < InfoScript.instance.WoodWep.Length; j++)
                {
                    if (InfoScript.instance.WoodWep[j].name.Equals(Character.instance.inventory.EquipmentList[i]))
                    {
                        itemSpriteList.Add(InfoScript.instance.WoodWep[j]);
                        break;
                    }
                }
                for (int k = 0; k < InfoScript.instance.IronWep.Length; k++)
                {
                    if (InfoScript.instance.IronWep[k].name.Equals(Character.instance.inventory.EquipmentList[i]))
                    {
                        itemSpriteList.Add(InfoScript.instance.IronWep[k]);
                        break;
                    }
                }
                for (int h = 0; h < InfoScript.instance.BlackWep.Length; h++)
                {
                    if (InfoScript.instance.BlackWep[h].name.Equals(Character.instance.inventory.EquipmentList[i]))
                    {
                        itemSpriteList.Add(InfoScript.instance.BlackWep[h]);
                        break;
                    }
                }
                for (int n = 0; n < InfoScript.instance.WoodArm.Length; n++)
                {
                    if (InfoScript.instance.WoodArm[n].name.Equals(Character.instance.inventory.EquipmentList[i]))
                    {
                        itemSpriteList.Add(InfoScript.instance.WoodArm[n]);
                        break;
                    }
                }
                for (int m = 0; m < InfoScript.instance.IronArm.Length; m++)
                {
                    if (InfoScript.instance.IronArm[m].name.Equals(Character.instance.inventory.EquipmentList[i]))
                    {
                        itemSpriteList.Add(InfoScript.instance.IronArm[m]);
                        break;
                    }
                }
                slotList[i].slotIcon.sprite = itemSpriteList[i];
            }
        }

    }


    // OnPointerDown ????
    void GetSlotInfo(PointerEventData ed, List<Slot> sList)
    {
        for (int i = 0; i < sList.Count; i++)
        {
            if (sList[i].IsInRect(ed.position))
            {
                if (sList[i].slotIcon.sprite != null)
                {
                    Debug.Log("???? ???? ???? = " + sList[i].name);
                    selectedSlot = i;

                    selectedIcon.sprite = sList[i].slotIcon.sprite;
                    selectedIcon.rectTransform.position = ed.position;
                    selectedIcon.gameObject.SetActive(true);
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetSlotInfo(eventData, slotList);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (selectedSlot == -1)
            return;

        int tmpSelectedSlot = -1;
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].IsInRect(eventData.position))
            {
                tmpSelectedSlot = i;
                break;
            }
        }
        // ???? ???? ?????? ???????? ???? ??
        if (tmpSelectedSlot != -1 && tmpSelectedSlot == selectedSlot)
        {
            Debug.Log("???????????????");
            selectedIcon.sprite = null;
            selectedIcon.gameObject.SetActive(false);
            selectedSlot = -1;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (selectedSlot != -1)
            selectedIcon.rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (selectedSlot == -1)
            return;

        EndDragFunc(eventData, slotList);
    }

    void EndDragFunc(PointerEventData ed, List<Slot> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].IsInRect(ed.position))
            {
                // ?????? ???? ???????? ?????? ?????? ?????? ??
                if (!list[i].tag.Equals("Untagged"))
                {
                    Debug.Log("???? ???? ????  = " + list[i].tag);
                    // ?????? ???? ???? ???? ??????
                    // ?????? ???? ???? ???????? ?????????? ???????? ?????????? ???????????? (selectedIcon?? ?????????? ?????? ??)
                    if (!CheckEquipSlot(list[i].tag, selectedIcon.sprite.name))
                    {
                        Debug.Log("???? ????");
                        MakeCanvasScript.instance.StartETCor("?????? ????????????.");
                        return;
                    }
                    // ?????? ???? ?????? ?????? ?????? ??
                    else
                    {
                        Character.instance.WearEquip.ActivateObj(selectedIcon.sprite.name);
                        MakeCanvasScript.instance.StatusUIScript.InitStatus();
                        Character.instance.inventory.RemoveItem(selectedIcon.sprite.name);
                        CheckSlot(list, i);
                    }
                }
                // ?????? ???? ???????? ???????? ???? ?? / ?????? ???? ???????? ?????? ?????? ????????
                else
                {
                    // ?????? ???? ????
                    if (!list[selectedSlot].tag.Equals("Untagged"))
                    {
                        Character.instance.WearEquip.DeActivateObj(selectedIcon.sprite.name);
                        MakeCanvasScript.instance.StatusUIScript.InitStatus();
                    }
                        
                    CheckSlot(list, i);
                }
            }
            else
            {
                // ???????? ?????? ?????? ??
                selectedIcon.gameObject.SetActive(false);
            }
        }
    }

    void CheckSlot(List<Slot> list, int i)
    {
        if (list[i].slotIcon.sprite == null)
        {
            Debug.Log("???????? ???????? ???? ??");

            if(!list[selectedSlot].tag.Equals("Untagged"))
                Character.instance.inventory.AddItem(selectedIcon.sprite.name);

            list[i].slotIcon.sprite = list[selectedSlot].slotIcon.sprite;
            list[selectedSlot].slotIcon.sprite = null;

            selectedIcon.sprite = null;
            selectedIcon.gameObject.SetActive(false);
            selectedSlot = -1;
        }
        else
        {
            Debug.Log("???????? ?????? ??");

            if (selectedIcon.sprite.name.Equals(list[i].slotIcon.sprite.name))
            {
                Debug.Log("???????? ?????????? ??????????");
                list[selectedSlot].slotIcon.sprite = null;
            }
            else
            {
                Debug.Log("???????????? ???????? ????");
                // ?????? ???? ????????
                if (list[selectedSlot].tag.Equals("Untagged"))
                {
                    Character.instance.WearEquip.DeActivateObj(list[i].slotIcon.sprite.name);
                    MakeCanvasScript.instance.StatusUIScript.InitStatus();

                    Sprite tmp = list[i].slotIcon.sprite;
                    list[i].slotIcon.sprite = list[selectedSlot].slotIcon.sprite;
                    list[selectedSlot].slotIcon.sprite = tmp;
                }
                // ???????? ???? ???????? ?????????? ?????? ???? ???????? ?????? ???? ???????? ?? ??
                else
                {
                    // ?????? ???? ???? ??
                    if (!CheckEquipSlot(list[selectedSlot].tag, list[i].slotIcon.sprite.name))
                    {
                        Debug.Log("???? ???????????? ?????? ???? ????");
                        MakeCanvasScript.instance.StartETCor("?????? ????????????.");
                        return;
                    }
                    // ?????? ???? ??
                    else if (CheckEquipSlot(list[selectedSlot].tag, list[i].slotIcon.sprite.name))
                    {
                        Debug.Log("?????? ???? ??????");
                        Character.instance.WearEquip.ActivateObj(list[i].slotIcon.sprite.name);
                        Character.instance.inventory.AddItem(list[selectedSlot].slotIcon.sprite.name);
                        Character.instance.inventory.RemoveItem(list[i].slotIcon.sprite.name);
                        MakeCanvasScript.instance.StatusUIScript.InitStatus();

                        Sprite tmp = list[i].slotIcon.sprite;
                        list[i].slotIcon.sprite = list[selectedSlot].slotIcon.sprite;
                        list[selectedSlot].slotIcon.sprite = tmp;
                    }
                }
            }
            selectedIcon.sprite = null;
            selectedIcon.gameObject.SetActive(false);
            selectedSlot = -1;

        }
    }

    bool CheckEquipSlot(string tag, string iconName)
    {
        string returnStr = string.Empty;

        foreach (string names in Weapons)
        {
            if (iconName.Contains(names))
            {
                returnStr = "Weapon";
            }
        }

        foreach(string names in Armors)
        {
            if (iconName.Contains(names))
            {
                returnStr = names;
            }
        }

        if (iconName.Contains("Bow"))
        {
            returnStr = "Bow";
        }
        else if (iconName.Contains("Shield"))
        {
            returnStr = "Shield";
        }

        if (returnStr.Equals(tag))
            return true;

        return false;
    }
}
