using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float followSpeed, sensitivity, smooth;
    [SerializeField]
    private Transform realCamera;
    // 카메라 회전 제한
    private float clampAngle = 50f;
    [SerializeField]
    // 장애물에 따른 카메라의 최대, 최소거리
    private float minDistance, maxDistance;
    // 마우스 인풋 변수
    private float rotX, rotY;
    private Vector3 dirNormalized;
    private Vector3 finalDir;
    
    private float finalDistance;

    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;
    }
    private void OnEnable()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        rotX += -(Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime);
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        int layerMask = (1 << LayerMask.NameToLayer("character"));  // Everything에서 Player 레이어만 제외하고 충돌 체크함
        layerMask = ~layerMask;

        if (Physics.Linecast(transform.position, finalDir, out hit, layerMask))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }

        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smooth);
    }
}
