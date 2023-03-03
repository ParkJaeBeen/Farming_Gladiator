using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character instance;

    [SerializeField]
    MouseMove fpsMouseScript;
    [SerializeField]
    TPSCamera tpsMouseScript;
    [SerializeField]
    TPSMove moveScript;
    [SerializeField]
    Farming farmingScript;
    [SerializeField]
    Making makingScript;
    [SerializeField]
    Inventory _inventory;
    [SerializeField]
    Camera FPSCamera, TPSCamera;
    [SerializeField]
    WearEquip _wearEquip;
    [SerializeField]
    Status _status;

    private bool isFPS, isTPS, _IsUIOpened;

    public bool isTPSP
    {
        get { return isTPS; }
    }

    public bool isFPSP
    {
        get { return isFPS; }
    }

    public Camera FPSCam
    {
        get { return FPSCamera; }
    }
    public Camera TPSCam
    {
        get { return TPSCamera; }
    }

    public Farming farming
    {
        get { return farmingScript; }
    }
    public TPSMove Move
    {
        get { return moveScript; }
        set { moveScript = value; }
    }

    public TPSCamera TpsMouseScript
    {
        get { return tpsMouseScript; }
        set { tpsMouseScript = value; }
    }

    public MouseMove FpsMouseScript
    {
        get { return fpsMouseScript; }
        set { fpsMouseScript = value; }
    }

    public bool IsUIOpened
    {
        get { return _IsUIOpened; }
        set { _IsUIOpened = value; }
    }

    public Inventory inventory { get => _inventory; set => _inventory = value; }
    public WearEquip WearEquip { get => _wearEquip; set => _wearEquip = value; }
    public Status status { get => _status; set => _status = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if (instance == null)
            instance = this;

        isFPS = true;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        isFPS = true;
        moveScript.enabled = true;
        farmingScript.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.SceneManagerP.IsGatheringSpot)
        {
            farmingScript.enabled = true;
            makingScript.enabled = false;
        }
        else if (GameManager.instance.SceneManagerP.IsMakingSpot)
        {
            farmingScript.enabled = false;
            makingScript.enabled = true;
        }
        else if (GameManager.instance.SceneManagerP.IsOnBattleField)
        {
            farmingScript.enabled = false;
            makingScript.enabled = false;
        }


        if (!_IsUIOpened)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                isFPS = true;
                isTPS = false;
                FPSorTPS();
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                isTPS = true;
                isFPS = false;
                FPSorTPS();
            }
        }
        Cursor.visible = true;
    }


    void FPSorTPS()
    {
        if (isFPS)
        {
            fpsMouseScript.enabled = true;
            tpsMouseScript.enabled = false;
            FPSCamera.gameObject.SetActive(true);
            TPSCamera.gameObject.SetActive(false);
        }
        else if (isTPS)
        {
            fpsMouseScript.enabled = false;
            tpsMouseScript.enabled = true;
            FPSCamera.gameObject.SetActive(false);
            TPSCamera.gameObject.SetActive(true);
        }
    }
}
