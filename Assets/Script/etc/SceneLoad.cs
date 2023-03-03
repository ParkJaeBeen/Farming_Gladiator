using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoad : MonoBehaviour
{
    public static SceneLoad instance;

    public Slider progressbar;
    public TextMeshProUGUI loadtext;

    private string _sceneName;

    public string SceneName
    {
        get { return _sceneName; }
        set { _sceneName = value; }
    }

    private void Awake()
    {
        progressbar = Canvas.FindObjectOfType<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }


    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(GameManager.instance.SceneManagerP.SceneName);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;
            if (progressbar.value < 1f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if (/*progressbar.value >= 1f && */op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
            }
        }
    }
}
