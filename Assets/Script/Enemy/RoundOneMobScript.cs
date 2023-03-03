using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundOneMobScript : MonoBehaviour
{
    public static RoundOneMobScript instance;

    [SerializeField]
    private Animator mobAni;
    [SerializeField]
    private GameObject BloodEffect;


    private Transform target;

    private float moveSpeed;
    private float atkDamage;
    private float _HP;
    private float _maxHP;
    

    private bool isAttack;
    private bool isStun;
    private bool isDead;

    private float attackDelay;

    public bool IsStun { get => isStun; set => isStun = value; }
    public bool IsAttack { get => isAttack; set => isAttack = value; }
    public float AtkDamage { get => atkDamage; set => atkDamage = value; }
    public float HP { get => _HP; set => _HP = value; }
    public float MaxHP { get => _maxHP; set => _maxHP = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        // 몹 능력치
        moveSpeed = 2.0f;
        atkDamage = 15;
        _maxHP = 500;
        _HP = 500;
    }

    // Update is called once per frame
    void Update()
    {
        attackDelay += Time.deltaTime;

        if (target == null)
            FindTarget();

        if(_HP <= 0)
        {
            isDead = true;
            mobAni.SetTrigger("isDead");
        }

        if(target != null)
        {
            if (!isDead)
            {
                if (!isStun)
                {
                    if (Vector3.Distance(target.position, transform.position) < 1.5f)
                    {
                        if (attackDelay >= 1.5f)
                        {
                            isAttack = true;
                            attackDelay = 0;
                            mobAni.SetBool("isMoving", false);
                            int randomInt = Random.Range(1, 3);

                            mobAni.SetInteger("Attack", randomInt);
                        }
                    }
                    else if (Vector3.Distance(target.position, transform.position) > 1.5f && !isAttack)
                    {
                        MoveToTarget();
                    }
                }
            }
        }

    }

    void FindTarget()
    {
        // 몬스터 주변의 오브젝트 불러오기
        Collider[] colls = Physics.OverlapSphere(transform.position, 300.0f);
        //Debug.Log("searching - " + colls[0].name);

        // 몬스터 주변에 오브젝트가 있으면..
        for (int i = 0; i < colls.Length; i++)
        {
            Collider tmpColl = colls[i];
            // 캐릭터가 맞으면, 타겟 설정
            if (tmpColl.gameObject.CompareTag("Player"))
            {
                // 타겟 오브젝트에 넣어주기
                target =  tmpColl.transform;
                break;
            }
        }
    }

    void MoveToTarget()
    {
        mobAni.SetBool("isMoving", true);
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
    }


    public void Dead()
    {
        MenuCanvasScript.instance.GameClearOnOff(true);
    }

    public void Attacked()
    {
        mobAni.SetInteger("Attack", 0);
        isAttack = false;
    }

    public void Stun()
    {
        if (isAttack)
            isAttack = false;
        isStun = true;
        mobAni.SetBool("isStun", true);
        mobAni.SetBool("isMoving", false);
        mobAni.SetInteger("Attack", 0);
    }

    public void StartCor(string name)
    {
        StartCoroutine(name);
    }

    IEnumerator PassOut()
    {
        Debug.Log("코루틴진입");
        yield return new WaitForSeconds(2.0f);
        mobAni.SetBool("isStun", false);
        isAttack = false;
        isStun = false;
    }

    public void GetHit(float damage)
    {
        _HP -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Arrow"))
        {
            ArrowParentScript arrow = collision.collider.transform.parent.GetComponent<ArrowParentScript>();
            GetHit(arrow.Damage);
            battleFieldCanvasScript.instance.MobHPImage();
            StartCoroutine(HitEffect(collision.contacts[0].point));
        }
    }

    IEnumerator HitEffect(Vector3 point)
    {
        Debug.Log("몬스터의 히트이펙트");
        GameObject blood = Instantiate(BloodEffect, point, Quaternion.identity);
        blood.transform.SetParent(this.transform);
        ParticleSystem be = blood.GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(be.main.duration);
        Destroy(blood);
    }
}
