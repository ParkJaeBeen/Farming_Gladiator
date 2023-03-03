using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvasScript : MonoBehaviour
{
    public static MenuCanvasScript instance;
    [SerializeField]
    private Image Menu, GameOver, GameClear, Bpanel, Epanel;
    [SerializeField]
    private Slider BGMVol, EffectVol;
    [SerializeField]
    private TextMeshProUGUI BGMText, EffectText;
    [SerializeField]
    private Button BGMMute, BGMMuteOff, EMute, EMuteOff;

    private bool isMenuOpen;

    private string CurrentBGMVolText;
    private float CurrentBGMVolVal;

    private string CurrentEVolText;
    private float CurrentEVolVal;

    public void ControlVolume()
    {
        GameManager.instance.SoundManager.BGM.BGMSourceP.volume = BGMVol.value;
        GameManager.instance.SoundManager.Effect.EffectP.volume = EffectVol.value;

        BGMText.text = (BGMVol.value * 100).ToString().Split(".")[0];
        EffectText.text = (EffectVol.value * 100).ToString().Split(".")[0];
    }

    public void MuteBGMVol(bool tf)
    {
        if (tf)
        {
            BGMMute.gameObject.SetActive(false);
            BGMMuteOff.gameObject.SetActive(true);
            Bpanel.gameObject.SetActive(true);
            CurrentBGMVolVal = BGMVol.value;
            CurrentBGMVolText = BGMText.text;
            BGMVol.value = 0;
        }
        else if (!tf)
        {
            BGMMute.gameObject.SetActive(true);
            BGMMuteOff.gameObject.SetActive(false);
            Bpanel.gameObject.SetActive(false);
            BGMVol.value = CurrentBGMVolVal;
            BGMText.text = CurrentBGMVolText;
        }
    }

    public void MuteEVol(bool tf)
    {
        if (tf)
        {
            EMute.gameObject.SetActive(false);
            EMuteOff.gameObject.SetActive(true);
            Epanel.gameObject.SetActive(true);
            CurrentEVolVal = EffectVol.value;
            CurrentEVolText = EffectText.text;
            EffectVol.value = 0;
        }
        else if (!tf)
        {
            EMute.gameObject.SetActive(true);
            EMuteOff.gameObject.SetActive(false);
            Epanel.gameObject.SetActive(false);
            EffectVol.value = CurrentEVolVal;
            EffectText.text = CurrentEVolText;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if (instance == null)
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void CloseAll()
    {
        isMenuOpen = false;
        Menu.gameObject.SetActive(false);
        GameOver.gameObject.SetActive(false);
        GameClear.gameObject.SetActive(false);
    }

    public void MenuOnOff(bool tf)
    {
        if (tf)
        {
            GameManager.instance.ControlTimeScale(false);
            Cursor.lockState = CursorLockMode.None;
            Menu.gameObject.SetActive(true);
        }
        else if (!tf)
        {
            GameManager.instance.ControlTimeScale(true);
            Cursor.lockState = CursorLockMode.Locked;
            Menu.gameObject.SetActive(false);
        }
    }

    public void GameOverOnOff(bool tf)
    {
        if (tf)
        {
            Cursor.lockState = CursorLockMode.None;
            GameOver.gameObject.SetActive(true);
            GameManager.instance.ControlTimeScale(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameOver.gameObject.SetActive(false);
        }
    }

    public void GameClearOnOff(bool tf)
    {
        if (tf)
        {
            GameManager.instance.ControlTimeScale(false);
            Cursor.lockState = CursorLockMode.None;
            GameClear.gameObject.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameClear.gameObject.SetActive(false);
        }
    }

    public void GotoRoundSelectScene()
    {
        GameManager.instance.ControlTimeScale(true);
        GameManager.instance.SceneManagerP.ChangeSceneName("RoundSelect");
        GameManager.instance.SceneManagerP.ChangeScene();
        CloseAll();
    }

    public void RestartRound()
    {
        GameManager.instance.SceneManagerP.RestartScene();
        CloseAll();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMenuOpen)
        {
            isMenuOpen = true;
            MenuOnOff(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isMenuOpen)
        {
            isMenuOpen = false;
            MenuOnOff(false);
        }

        ControlVolume();
    }
}
