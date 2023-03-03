using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    SceneManagerScript sceneManager;
    [SerializeField]
    SoundManager _soundManager;
    [SerializeField]
    Character character;

    public bool _isGameStart;
    public bool _isGameOver;
    public bool _isGameClear;

    public Character Player;

    public SceneManagerScript SceneManagerP
    {
        get { return sceneManager; }
        set { sceneManager = value; }
    }

    public SoundManager SoundManager { get => _soundManager; set => _soundManager = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    IEnumerator Test()
    {
        while (true)
        {
            yield return null;
            if (sceneManager.IsGatheringSpot)
            {
                Debug.Log("Test ³¡");
                Character();
                break;
            }
        }
    }

    public void ControlTimeScale(bool tf)
    {
        if (tf)
            Time.timeScale = 1.0f;
        else if(!tf)
            Time.timeScale = 0;
    }

    void Character()
    {
        Player = Instantiate(character, Vector3.zero, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test());
        //Character();
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
