using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Making : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        int CameraDistance = Character.instance.isTPSP ? 10 : 3;

        if (Physics.Raycast(ray, out hitInfo, CameraDistance))
        {
            if (hitInfo.collider.name.Equals("BlackSmith"))
            {
                if (Input.GetKeyDown(KeyCode.E) && !MakeCanvasScript.instance.IsShopOpen)
                {
                    MakeCanvasScript.instance.EPanelActivate(false);
                    MakeCanvasScript.instance.ShopPanelActivate(true);
                }
            }
            else if (hitInfo.collider.name.Equals("GoToBattleField"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MakeCanvasScript.instance.EPanelActivate(false);
                    MakeCanvasScript.instance.YNPopupActive(true);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
            MakeCanvasScript.instance.ShopPanelActivate(false);
    }
}
