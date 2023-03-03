using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundSelectCanvasScript : MonoBehaviour
{
    [SerializeField]
    private Image confirm;

    public void GoToGatherScene()
    {
        GameManager.instance.SceneManagerP.ChangeSceneName("island");
        GameManager.instance.SceneManagerP.ChangeScene();
    }

    public void ConfirmActive(bool tf)
    {
        if (tf)
        {
            confirm.gameObject.SetActive(true);
        }
        else
        {
            confirm.gameObject.SetActive(false);
        }
    }

    public void SelectRound(int num)
    {
        if (num.Equals(1))
        {
            ConfirmActive(true);
        }
    }
}
