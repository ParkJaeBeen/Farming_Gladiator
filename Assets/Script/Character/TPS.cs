using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPS : MonoBehaviour
{
    [SerializeField]
    private Transform character, cameraArm;
    [SerializeField]
    private float speed, yVelocity, jumpSpeed, gravity;

    private Animator ani;
    [SerializeField]
    private CharacterController charCon;

    private void Awake()
    {
        ani = character.GetComponent<Animator>();
        charCon = character.GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        if (Character.instance.isTPSP)
            //Move();
            MoveTest();
    }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        ani.SetBool("isMove", isMove);
        if (isMove)
        {
            Vector3 lookFoward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;

            Vector3 moveDir = lookFoward * moveInput.y + lookRight * moveInput.x;

            character.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * speed;
        }
    }

    void MoveTest()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveDir2 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        moveDir2 *= speed;

        if (charCon.isGrounded)
        {
            Debug.Log("123123");
            yVelocity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
            }
        }

        yVelocity += gravity * Time.deltaTime;
        moveDir2.y = yVelocity;

        bool isMove = moveInput.magnitude != 0;
        ani.SetBool("isMove", isMove);
        if (isMove)
        {
            Vector3 lookFoward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;

            Vector3 moveDir = lookFoward * moveInput.y + lookRight * moveInput.x;

            character.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * speed;
        }
    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        if(x < 180.0f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
