using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneCanvas : MonoBehaviour
{
    public void GoToRoundScene()
    {
        GameManager.instance.SceneManagerP.GoToRoundSelect();
    }

    public void Quit()
    {
        GameManager.instance.GameQuit();
    }
}
