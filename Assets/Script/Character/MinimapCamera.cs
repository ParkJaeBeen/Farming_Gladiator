using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField]
    private bool x, y, z;

    // Update is called once per frame
    void Update()
    {
        if (Character.instance == null)
            return;

        transform.position = new Vector3((x ? Character.instance.transform.position.x : transform.position.x),
                                         (y ? Character.instance.transform.position.y : transform.position.y),
                                         (z ? Character.instance.transform.position.z : transform.position.z));
    }
}
