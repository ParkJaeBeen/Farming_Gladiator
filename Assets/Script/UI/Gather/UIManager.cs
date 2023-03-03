using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    private Image FadeOutPanel, EPanel, TimePanel, FinishPopup;
    [SerializeField]
    private Texture2D cursorImg;
    [SerializeField]
    private Slider ESlider;
    [SerializeField]
    private TextMeshProUGUI ETimeText, _TimeLimitText;
    [SerializeField]
    private TextMeshProUGUI[] ElementTextList;

    private GameObject CutScene;

    // ¹Ì´Ï¸Êº¯¼ö
    [SerializeField]
    private Camera minimapCam;
    private float zoomMin = 1;
    private float zoomMax = 30;

    [SerializeField]
    private Scrollbar minimapDistance;

    public Image TimePanelP
    {
        get { return TimePanel; }
        set { TimePanel = value; }
    }

    public TextMeshProUGUI TimeLimitText { get => _TimeLimitText; set => _TimeLimitText = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        StartCoroutine(StartGame());
        Debug.Log(ElementTextList[0]);
    }

    // Start is called before the first frame update
    void Start()
    {
        CutScene = GameObject.Find("CutScene");
    }

    public void ChangeCursorImg()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        ZoomInAndOut(zoom * 10);

        minimapDistance.value = minimapCam.orthographicSize / 30;
    }

    public IEnumerator FadeOut()
    {
        Debug.Log("alpha");
        Color panelColor = FadeOutPanel.color;
        
        while (panelColor.a <= 255f)
        {
            panelColor.a += 0.01f;
            FadeOutPanel.color = panelColor;
            if (panelColor.a >= 0.98f)
            {
                StartCoroutine(StartGame());
                StopCoroutine(FadeOut());
            }
            yield return null;
        }
    }

    IEnumerator StartGame()
    {
        Debug.Log("start");
        yield return new WaitForSeconds(0.1f);
        Color alphaZero = FadeOutPanel.color;
        alphaZero.a = 0;
        FadeOutPanel.color = alphaZero;

        Cursor.visible = true;
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.Auto);
        minimapCam.gameObject.SetActive(true);
    }

    void ZoomInAndOut(float mouseScroll)
    {
        if(mouseScroll < 0)
            minimapCam.orthographicSize = Mathf.Max(minimapCam.orthographicSize + mouseScroll, zoomMin);
        else if(mouseScroll > 0)
            minimapCam.orthographicSize = Mathf.Min(minimapCam.orthographicSize + mouseScroll, zoomMax);
    }

    public void EPanelActive(bool tf)
    {
        if(tf)
            EPanel.gameObject.SetActive(true);
        else
            EPanel.gameObject.SetActive(false);
    }

    public void TimePanelOnOff(bool tf)
    {
        if (tf)
            TimePanel.gameObject.SetActive(true);
        else
            TimePanel.gameObject.SetActive(false);
    }

    public void TimePanelActive(int type, float time)
    {
        if(EPanel.gameObject.activeSelf)
            EPanel.gameObject.SetActive(false);

        if (time <= 0.02f)
            TimePanelOnOff(false);
        else
            TimePanelOnOff(true);

        if (type.Equals(3))
            ESlider.value = time / 3;
        else if (type.Equals(1))
            ESlider.value = time;

        ETimeText.text = time.ToString().Substring(0, 3) + "s";
    }

    public void ElementCount(int count, int elementCount)
    {
        ElementTextList[count].text = elementCount.ToString();
    }

    public void FinishPopupActive(bool tf)
    {
        if (tf)
        {
            FinishPopup.gameObject.SetActive(true);
        }
        else
        {
            FinishPopup.gameObject.SetActive(false);
        }
    }
}
