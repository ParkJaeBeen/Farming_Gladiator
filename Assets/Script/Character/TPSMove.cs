using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPSMove : MonoBehaviour
{
    [SerializeField]
    private Animator ani;
    [SerializeField]
    private Camera Cam;
    [SerializeField]
    private CharacterController charCon;
    [SerializeField]
    private float speed, runSpeed, smooth, jumpSpeed, yVelocity, gravity;
    [SerializeField]
    private Transform model;
    [SerializeField]
    private GameObject HealEffect;

    private float finalSpeed;
    private float blockCoolTime;

    private bool run;
    // 재료 수집시 true 가 되는 변수
    private bool _IsMove;
    // 공격 관련
    private bool _SwordAndShield;
    private bool _Bow;
    private bool _Normal;
    // 근접공격 관련
    private bool _isAttack;
    private bool _isBlocking;
    private bool _isBlockUsed;
    // 활 관련
    private bool _isAiming;
    private bool _isShoot;
    // 포션 관련
    private bool _isPotion;
    private bool _isDrinking;

    private bool _isDead;
    private bool _deadTrigger;

    // 키를 눌렀을 때만 카메라가 돌게
    private bool toggleCameraRotation;

    private RuntimeAnimatorController SSController, BowController, NormalController;


    public bool toggleCameraRotationP
    {
        get { return toggleCameraRotation; }
        set { toggleCameraRotation = value; }
    }

    public bool IsMove
    {
        get { return _IsMove; }
        set { _IsMove = value; }
    }

    public float Speed { get => speed; set => speed = value; }
    public float RunSpeed { get => runSpeed; set => runSpeed = value; }
    public float FinalSpeed { get => finalSpeed; set => finalSpeed = value; }
    public bool IsAiming { get => _isAiming; set => _isAiming = value; }
    public bool IsBlocking { get => _isBlocking; set => _isBlocking = value; }
    public bool IsAttack { get => _isAttack; set => _isAttack = value; }
    public bool IsPotion { get => _isPotion; set => _isPotion = value; }
    public bool IsDrinking { get => _isDrinking; set => _isDrinking = value; }
    public bool SwordAndShield { get => _SwordAndShield; set => _SwordAndShield = value; }
    public bool Bow { get => _Bow; set => _Bow = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }
    public Animator Ani { get => ani; set => ani = value; }
    public bool DeadTrigger { get => _deadTrigger; set => _deadTrigger = value; }

    private void Awake()
    {
        _Normal = true;
        SSController = Resources.Load<RuntimeAnimatorController>("Controller/SwordAndShieldController");
        BowController = Resources.Load<RuntimeAnimatorController>("Controller/BowController");
        NormalController = Resources.Load<RuntimeAnimatorController>("Controller/CharacterController");
    }

    private void Start()
    {
        // 테스트용
        _SwordAndShield = true;
    }
    public void ChangeController(string name)
    {
        if (name.Equals("SwordAndShield"))
        {
            ani.runtimeAnimatorController = SSController;
            _SwordAndShield = true;
            _Bow = false;
            _Normal = false;
        }
        else if (name.Equals("Bow"))
        {
            ani.runtimeAnimatorController = BowController;
            _SwordAndShield = false;
            _Bow = true;
            _Normal = false;
        }
        else
        {
            ani.runtimeAnimatorController = NormalController;
            _SwordAndShield = false;
            _Bow = false;
            _Normal = true;
        }
    }

    public void ResetFunc()
    {
        _isDead = false;
        _SwordAndShield = true;
        _Bow = false;
        _isPotion = false;
        _deadTrigger = false;
    }

    void ChangeCam()
    {
        if (Character.instance.isTPSP)
        {
            Cam = Character.instance.TPSCam;
        }
        else
        {
            Cam = Character.instance.FPSCam;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("isattack = " + _isAttack);

        if (Character.instance.isTPSP)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
                toggleCameraRotation = true;
            else
                toggleCameraRotation = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
            run = true;
        else
            run = false;

        ChangeCam();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 inputPos = new Vector2(h, v);
        Vector3 moveDirection = new Vector3(h, 0, v);

        if (Character.instance.status.Health <= 0)
        {
            _isDead = true;
            if (!_deadTrigger)
            {
                ani.SetTrigger("IsDead");
                _deadTrigger = true;
            }
                
        }

        if (!_isDead)
        {
            if (_IsMove)
                toggleCameraRotation = true;

            if (GameManager.instance.SceneManagerP.IsOnBattleField)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Character.instance.WearEquip.ChangeActiveWeapon("SwordAndShield");
                    ChangeController("SwordAndShield");
                    _isPotion = false;
                    battleFieldCanvasScript.instance.ChangeCurrentPotionImg(3);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    Character.instance.WearEquip.ChangeActiveWeapon("Bow");
                    ChangeController("Bow");
                    _isPotion = false;
                    Character.instance.inventory.HasArrow();
                    battleFieldCanvasScript.instance.ChangeCurrentPotionImg(3);
                    if (Character.instance.inventory.CurrentArrow != null)
                    {
                        Character.instance.inventory.ChangeCurrentArrow(Character.instance.inventory.CurrentArrow.name);
                        battleFieldCanvasScript.instance.ChangeCurrentArrowImg(Character.instance.inventory.CurrentArrow.name);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    // 포션이 있을때
                    if (Character.instance.inventory.HasPotion())
                    {
                        Character.instance.inventory.ActivatePotion();
                        Character.instance.WearEquip.ChangeActiveWeapon("Potion");
                        _isPotion = true;
                    }
                    else
                    {
                        Debug.Log("no potion");
                    }
                }

                if (_isPotion)
                {
                    UsePotion();

                    if (Input.GetKeyDown(KeyCode.Tab))
                    {
                        Character.instance.inventory.CheckPotionCount(Character.instance.inventory.CurrentPotion());
                        battleFieldCanvasScript.instance.ChangeCurrentPotionImg(Character.instance.inventory.CurrentPotion());
                    }
                }
            }

            if (!_IsMove)
            {
                if (_Normal)
                    NormalMove(inputPos, moveDirection);
                else if (_SwordAndShield)
                    MeleeMove(inputPos, moveDirection);
                else if (_Bow)
                    BowMove(inputPos, moveDirection);
            }
        }
    }

    void UsePotion()
    {
        if (Input.GetMouseButtonDown(0) && !_isDrinking)
        {
            if(Character.instance.status.Health >= Character.instance.status.Maxhealth)
            {
                Debug.Log("현재체력이 최대치");
                return;
            }
            _isDrinking = true;
            ani.SetTrigger("IsDrinking");
        }
    }

    void MoveTotal(Vector3 moveDirection)
    {
        finalSpeed = run ? runSpeed : speed;

        moveDirection = toggleCameraRotation ? moveDirection : Cam.transform.TransformDirection(moveDirection);

        moveDirection *= finalSpeed;

        if (charCon.isGrounded)
        {
            yVelocity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
                yVelocity = jumpSpeed;
        }
        yVelocity += gravity * Time.deltaTime;
        moveDirection.y = yVelocity;


        charCon.Move(moveDirection * Time.deltaTime);
    }

    // 1,3인칭 움직임은 통일
    void NormalMove(Vector2 inputPos, Vector3 moveDirection)
    {
        MoveTotal(moveDirection);
        float blendDegree = (run ? 1 : 0.5f) * inputPos.magnitude;
        ani.SetFloat("Blend", blendDegree, 0.1f, Time.deltaTime);
    }

    void MeleeMove(Vector2 inputPos, Vector3 moveDirection)
    {
        MoveTotal(moveDirection);
        float blendDegree = (run ? 1 : 0.5f) * inputPos.magnitude;
        ani.SetFloat("MeleeBlend", blendDegree, 0.1f, Time.deltaTime);

        if (_isBlockUsed)
        {
            blockCoolTime -= Time.deltaTime;

            if (blockCoolTime == 0)
                _isBlockUsed = false;
        }

        if (Input.GetMouseButtonDown(0) && !_isPotion)
        {
            _isAttack = true;
            
            ani.SetBool("IsAttack", true);
        }

        if (Input.GetMouseButton(1) && blockCoolTime <= 0.01f)
        {
            Debug.Log("막기한번썼음");
            _isBlocking = true;
            _isBlockUsed = true;
            blockCoolTime = 15.5f;
            ani.SetBool("IsBlocking",true);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            _isBlocking = false;
            ani.SetBool("IsBlocking", false);
        }
    }

    void BowMove(Vector2 inputPos, Vector3 moveDirection)
    {
        MoveTotal(moveDirection);
        float blendDegree = (run ? 1 : 0.5f) * inputPos.magnitude;
        if(!_isAiming)
            ani.SetFloat("BowBlend", blendDegree, 0.1f, Time.deltaTime);


        if(Character.instance.inventory.CurrentArrow != null)
        {
            if (Input.GetMouseButton(0) && !_isPotion)
            {
                _isAiming = true;
                ani.SetBool("isAiming", true);
                ani.SetFloat("AimBlend", blendDegree, 0.1f, Time.deltaTime);
            }
            else if (Input.GetMouseButtonUp(0) && _isAiming && !_isPotion)
            {
                ani.SetTrigger("isAttack");
                ani.SetBool("isAiming", false);
            }

            if (Input.GetKeyDown(KeyCode.Tab) && !_isPotion)
            {
                if(Character.instance.inventory.CurrentArrow != null)
                    Character.instance.inventory.ChangeCurrentArrow(Character.instance.inventory.CurrentArrow.name);
            }
        }
        else
        {
            Debug.Log("화살없음");
        }
            
    }

    public void DoCoroutine(string name)
    {
        StartCoroutine(name);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.6f);
        ani.SetBool("IsAttack", false);
        _isAttack = false;
    }

    IEnumerator BowAttack()
    {
        _isAiming = false;
        ani.SetBool("isAiming", false);
        GameManager.instance.SoundManager.Effect.EffectSound(3);
        yield return new WaitForSeconds(0.05f);
        Debug.Log("보우어택작동");
        
        Character.instance.inventory.UsePotionOrArrow(Character.instance.inventory.CurrentArrowNum);
        Character.instance.inventory.ChangeCurrentArrow(Character.instance.inventory.CurrentArrow.name);
        Character.instance.inventory.HasArrowTotal();
    }

    public void Dead()
    {
        MenuCanvasScript.instance.GameOverOnOff(true);
    }

    public void Heal()
    {
        GameObject HE = Instantiate(HealEffect, transform.position, transform.rotation);
        HE.transform.SetParent(this.transform);
        ParticleSystem HEP = HE.GetComponent<ParticleSystem>();
        Destroy(HE, HEP.main.duration);
    }

    private void LateUpdate()
    {
        Vector3 TorF = Character.instance.isFPSP ? new Vector3(1, 1, 1) : new Vector3(1, 0, 1);
        Vector3 bowRotation = _isAiming ? Cam.transform.right : Cam.transform.forward;

        if (!toggleCameraRotation)
        {
            Vector3 playerRotate = Vector3.Scale(bowRotation, TorF);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smooth);
        }
    }
}
