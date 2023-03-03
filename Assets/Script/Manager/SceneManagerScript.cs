using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private bool _isGatheringSpot;
    private bool _isMakingSpot;
    private bool _isOnBattleField;
    private string _sceneName;
    public bool IsGatheringSpot
    {
        get { return _isGatheringSpot; }
    }

    public bool IsMakingSpot
    {
        get { return _isMakingSpot; }
    }

    public bool IsOnBattleField
    {
        get { return _isOnBattleField; }
        set { _isOnBattleField = value; }
    }

    public string SceneName
    {
        get { return _sceneName; }
        set { _sceneName = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Onsceneload = " + scene.name);

        if (scene.name.Equals("Battlefield"))
        {
            GameManager.instance.SoundManager.BGM.BGM(2);
            Character.instance.gameObject.transform.position = new Vector3(0, 1, 0);
            Character.instance.Move.ChangeController("SwordAndShield");
            Character.instance.inventory.HasArrow();
            SaveScript.instance.SaveData();
        }
        else if (scene.name.Equals("Make"))
        {
            GameManager.instance.SoundManager.BGM.BGM(1);
            Character.instance.gameObject.transform.position = new Vector3(0, 1, 0);
            Character.instance.Move.ChangeController("Normal");
        }
        else if (scene.name.Equals("island"))
        {
            GameManager.instance.SoundManager.BGM.BGM(1);
            Character.instance.Move.ChangeController("Normal");
        }
        else if (scene.name.Equals("Loading"))
        {
            Debug.Log("·ÎµùÁß");
        }
        else
        {
            GameManager.instance.SoundManager.BGM.BGM(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("island"))
        {
            if(GameManager.instance.Player != null)
                Character.instance.gameObject.SetActive(true);
            _isGatheringSpot = true;
            _isMakingSpot = false;
            _isOnBattleField = false;
        }
        else if (SceneManager.GetActiveScene().name.Equals("Make"))
        {
            if (GameManager.instance.Player != null)
                Character.instance.gameObject.SetActive(true);
            _isGatheringSpot = false;
            _isMakingSpot = true;
            _isOnBattleField = false;
        }
        else if (SceneManager.GetActiveScene().name.Equals("Battlefield"))
        {
            if (GameManager.instance.Player != null)
                Character.instance.gameObject.SetActive(true);
            _isGatheringSpot = false;
            _isMakingSpot = false;
            _isOnBattleField = true;
        }
        else if (SceneManager.GetActiveScene().name.Equals("RoundSelect"))
        {
            if (GameManager.instance.Player != null)
                Character.instance.gameObject.SetActive(false);
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Loading");
    }

    public void ChangeSceneName(string name)
    {
        _sceneName = name;
    }

    public void GoToMainScene()
    {
        GameManager.instance.ControlTimeScale(true);
        SceneManager.LoadScene("StartScene");
    }

    public void RestartScene()
    {
        SaveScript.instance.LoadData();
        ChangeSceneName(SceneManager.GetActiveScene().name);
        ChangeScene();
        GameManager.instance.ControlTimeScale(true);
        MenuCanvasScript.instance.CloseAll();
        Character.instance.Move.ResetFunc();
    }

    public void GoToRoundSelect()
    {
        SceneManager.LoadScene("RoundSelect");
    }
}
