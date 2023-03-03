using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed, jumpSpeed, gravity, yVelocity = 0;
    private Vector3 moveForce;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform character;

    private CharacterController charCon;
    private Animator ani;
    private float blendDegree;
    private bool run;

    private void Awake()
    {
        charCon = character.GetComponent<CharacterController>();
        ani = character.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FPSmovement();
    }

    void FPSmovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 inputPos = new Vector2(h, v);

        Vector3 moveDirection = new Vector3(h, 0, v);

        moveDirection = cameraTransform.TransformDirection(moveDirection);

        moveDirection *= speed;

        if (charCon.isGrounded)
        {
            yVelocity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
                yVelocity = jumpSpeed;
        }

        yVelocity += gravity * Time.deltaTime;
        moveDirection.y = yVelocity;

        charCon.Move(moveDirection * Time.deltaTime);

        float blendDegree = (run ? 1 : 0.5f) * inputPos.magnitude;
        ani.SetFloat("Blend", blendDegree, 0.1f, Time.deltaTime);
    }
}
