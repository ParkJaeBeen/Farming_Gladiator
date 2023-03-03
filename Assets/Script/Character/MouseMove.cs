using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private Transform target;

    private float RotationX, RotationY;

    public float FPSCamY;

    public float RotationYP { get => RotationY; set => RotationY = value; }
    public float RotationXP { get => RotationX; set => RotationX = value; }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        RotationX += MouseY * sensitivity * Time.deltaTime;
        RotationY += MouseX * sensitivity * Time.deltaTime;

        if(RotationX > 45f)
        {
            RotationX = 45f;
        }

        if(RotationX < -40f)
        {
            RotationX = -40f;
        }

        transform.eulerAngles = new Vector3(-RotationX, RotationY, 0);

        FPSCamY = transform.rotation.x;
    }
}
