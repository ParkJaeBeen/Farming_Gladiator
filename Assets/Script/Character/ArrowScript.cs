using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player"))
        {
            Debug.Log("플레이어 아닌것과 충돌 = " + collision.collider.name);
            ArrowParentScript aps = this.transform.GetComponentInParent<ArrowParentScript>();
            aps.DestroyObj();
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("충돌 = " + collision.collider.name);
            // 여기에다가 화살 공격 함수
            ArrowParentScript aps = this.transform.GetComponentInParent<ArrowParentScript>();
            aps.DestroyObj();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 10);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (-transform.right * 10));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (-transform.forward * 10));
    }
}
