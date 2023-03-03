using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class battleFieldCanvasScript : MonoBehaviour
{
    public static battleFieldCanvasScript instance;

    [SerializeField]
    private Image HitPanel, CurrentHPImg, MobHPImg;
    [SerializeField]
    private Image WoodSelect, IronSelect, BlackSelect;
    [SerializeField]
    private Image LowSelect, MiddleSelect, HighSelect;
    [SerializeField]
    private List<TextMeshProUGUI> ArrowTextList, PotionTextList;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        HPImage();
        InitArrowPotionText();
    }

    public void InitArrowPotionText()
    {
        ArrowTextList[0].text = Character.instance.inventory.UseableArray[3].ToString();
        ArrowTextList[1].text = Character.instance.inventory.UseableArray[4].ToString();
        ArrowTextList[2].text = Character.instance.inventory.UseableArray[5].ToString();

        PotionTextList[0].text = Character.instance.inventory.UseableArray[0].ToString();
        PotionTextList[1].text = Character.instance.inventory.UseableArray[1].ToString();
        PotionTextList[2].text = Character.instance.inventory.UseableArray[2].ToString();
    }

    public void StartCor(string name)
    {
        StartCoroutine(name);
    }

    IEnumerator HitEffect()
    {
        Color col = HitPanel.GetComponent<Image>().color;
        col.a = 0.75f;
        HitPanel.color = col;
        while (col.a >= 0.0f)
        {
            col.a -= 0.01f;
            HitPanel.color = col;
            yield return null;
        }
    }

    public void HPImage()
    {
        CurrentHPImg.fillAmount = Character.instance.status.Health / Character.instance.status.Maxhealth;
    }

    public void MobHPImage()
    {
        MobHPImg.fillAmount = RoundOneMobScript.instance.HP / RoundOneMobScript.instance.MaxHP;
    }

    public void ChangeCurrentArrowImg(string name)
    {
        switch (name)
        {
            case "Wood_Arrow":
                WoodSelect.gameObject.SetActive(true);
                IronSelect.gameObject.SetActive(false);
                BlackSelect.gameObject.SetActive(false);
                break;
            case "Iron_Arrow":
                WoodSelect.gameObject.SetActive(false);
                IronSelect.gameObject.SetActive(true);
                BlackSelect.gameObject.SetActive(false);
                break;
            case "Black_Arrow":
                WoodSelect.gameObject.SetActive(false);
                IronSelect.gameObject.SetActive(false);
                BlackSelect.gameObject.SetActive(true);
                break;
            default:
                WoodSelect.gameObject.SetActive(false);
                IronSelect.gameObject.SetActive(false);
                BlackSelect.gameObject.SetActive(false);
                break;
        }
    }

    public void ChangeCurrentPotionImg(int num)
    {
        switch (num)
        {
            case 0:
                LowSelect.gameObject.SetActive(true);
                MiddleSelect.gameObject.SetActive(false);
                HighSelect.gameObject.SetActive(false);
                break;
            case 1:
                LowSelect.gameObject.SetActive(false);
                MiddleSelect.gameObject.SetActive(true);
                HighSelect.gameObject.SetActive(false);
                break;
            case 2:
                LowSelect.gameObject.SetActive(false);
                MiddleSelect.gameObject.SetActive(false);
                HighSelect.gameObject.SetActive(true);
                break;
            case 3:
                LowSelect.gameObject.SetActive(false);
                MiddleSelect.gameObject.SetActive(false);
                HighSelect.gameObject.SetActive(false);
                break;
        }
    }
}
