using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneScript : MonoBehaviour
{
    [SerializeField]
    CinemachineDollyCart cart;
    
    CinemachineSmoothPath path;

    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<CinemachineSmoothPath>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Debug.Log("코루틴실행중");
        Cursor.visible = false;
        yield return new WaitUntil(() => cart.m_Position >= 70f);
        StartCoroutine(UIManager.instance.FadeOut());
    }
}
