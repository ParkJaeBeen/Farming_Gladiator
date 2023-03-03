using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherSceneTimeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeLimit());
    }


    // 파밍 제한시간
    IEnumerator TimeLimit()
    {
        // 제한시간
        yield return new WaitForSeconds(3.0f);
        Character.instance.farming.DeActivateAll();
        Character.instance.farming.enabled = false;
        UIManager.instance.FinishPopupActive(true);
        yield return new WaitForSeconds(3.0f);
        UIManager.instance.TimeLimitText.text = "5초후 상점으로 이동합니다.";
        yield return new WaitForSeconds(5.0f);
        UIManager.instance.TimeLimitText.text = "수 집 종 료";
        UIManager.instance.FinishPopupActive(false);
        
        GameManager.instance.SceneManagerP.ChangeSceneName("Make");
        GameManager.instance.SceneManagerP.ChangeScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
