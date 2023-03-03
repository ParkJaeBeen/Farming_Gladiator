using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MakeCanvasScript : MonoBehaviour
{
    public static MakeCanvasScript instance;

    [SerializeField]
    private Image EquipPanel, ShopPanel, EPanel, BuyPopup, YesNoPopup;
    [SerializeField]
    private Texture2D cursorImg;
    [SerializeField]
    EquipmentInventory EquipInventory;
    [SerializeField]
    StatusUIScript statusUIScript;
    [SerializeField]
    ShopPriceScript shopPriceScript;
    [SerializeField]
    TextMeshProUGUI ErrorText;

    private bool _IsShopOpen, _IsBagOpen;

    public bool IsShopOpen
    {
        get { return _IsShopOpen; }
        set { _IsShopOpen = value; }
    }

    public StatusUIScript StatusUIScript { get => statusUIScript; set => statusUIScript = value; }
    public ShopPriceScript ShopPriceScript { get => shopPriceScript; set => shopPriceScript = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !_IsBagOpen && !_IsShopOpen)
        {
            EquipPanel.gameObject.SetActive(true);
            EquipInventory.InitSlot();
            EquipInventory.InitResourceTextList();
            EquipInventory.InitUseableTextList();
            EquipInventory.ChangeSlotIcon();
            statusUIScript.InitStatus();

            _IsBagOpen = true;
            Cursor.lockState = CursorLockMode.None;
            if (Character.instance.isFPSP)
                Character.instance.FpsMouseScript.enabled = false;
            else if (Character.instance.isTPSP)
                Character.instance.TpsMouseScript.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && _IsBagOpen)
        {
            _IsBagOpen = false;
            EquipPanel.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            if (Character.instance.isFPSP)
                Character.instance.FpsMouseScript.enabled = true;
            else if (Character.instance.isTPSP)
                Character.instance.TpsMouseScript.enabled = true;
        }
    }

    public void EPanelActivate(bool tf)
    {
        if(tf)
            EPanel.gameObject.SetActive(true);
        else
            EPanel.gameObject.SetActive(false);
    }

    public void ShopPanelActivate(bool tf)
    {
        if (tf)
        {
            shopPriceScript.TotalChangeText();
            ShopPanel.gameObject.SetActive(true);
            _IsShopOpen = true;
            Cursor.lockState = CursorLockMode.None;
            if (Character.instance.isTPSP)
                Character.instance.TpsMouseScript.enabled = false;
            else if (Character.instance.isFPSP)
                Character.instance.FpsMouseScript.enabled = false;
        }
        else
        {
            ShopPanel.gameObject.SetActive(false);
            _IsShopOpen = false;
        }
    }

    // shop ÀÇ x¹öÆ°
    public void CloseShop()
    {
        _IsShopOpen = false;
        ShopPanel.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        if (Character.instance.isTPSP)
            Character.instance.TpsMouseScript.enabled = true;
        else if (Character.instance.isFPSP)
            Character.instance.FpsMouseScript.enabled = true;
    }

    public void YNPopupActive(bool tf)
    {
        if (tf)
        {
            YesNoPopup.gameObject.SetActive(true);
            _IsShopOpen = true;
            Cursor.lockState = CursorLockMode.None;
            if (Character.instance.isTPSP)
                Character.instance.TpsMouseScript.enabled = false;
            else if (Character.instance.isFPSP)
                Character.instance.FpsMouseScript.enabled = false;
        }
        else
        {
            YesNoPopup.gameObject.SetActive(false);
            _IsShopOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (Character.instance.isTPSP)
                Character.instance.TpsMouseScript.enabled = true;
            else if (Character.instance.isFPSP)
                Character.instance.FpsMouseScript.enabled = true;
        }
    }

    public void GotoBattleField()
    {
        if (Character.instance.isTPSP)
            Character.instance.TpsMouseScript.enabled = true;
        else if (Character.instance.isFPSP)
            Character.instance.FpsMouseScript.enabled = true;
        GameManager.instance.SceneManagerP.ChangeSceneName("Battlefield");
        GameManager.instance.SceneManagerP.ChangeScene();
    }

    public void StartETCor(string name)   
    {
        StartCoroutine(ChangeErrorText(name));
    }

    IEnumerator ChangeErrorText(string name)
    {
        StopCoroutine("ChangeErrorText");
        ErrorText.gameObject.SetActive(true);
        ErrorText.text = name;
        float fadeOut = 1.0f;
        while(fadeOut > 0)
        {
            fadeOut -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            ErrorText.color = new Color(255, 239, 0, fadeOut);
            if(fadeOut <= 0.05f)
                ErrorText.gameObject.SetActive(false);
        }
    }
}
