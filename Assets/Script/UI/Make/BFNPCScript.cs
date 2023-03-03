using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFNPCScript : MonoBehaviour
{
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = Character.instance.gameObject;
    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= 3.0f)
        {
            if (!MakeCanvasScript.instance.IsShopOpen && !Character.instance.IsUIOpened)
                MakeCanvasScript.instance.EPanelActivate(true);
        }

    }

    private void OnMouseExit()
    {
        MakeCanvasScript.instance.EPanelActivate(false);
    }
}
