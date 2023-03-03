using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MeleeWeaponScript : MonoBehaviour
{
    private Collider wep;

    [SerializeField]
    private GameObject BloodEffect;

    private void Start()
    {
        wep = this.GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && Character.instance.Move.IsAttack)
        {
            StartCoroutine(Attack(collision.contacts[0].point));
            Debug.Log("적에게 닿음 - "+ collision.contacts[0].point);
            RoundOneMobScript.instance.GetHit(Character.instance.status.AttackDamage);
            battleFieldCanvasScript.instance.MobHPImage();
        }
    }

    IEnumerator Attack(Vector3 point)
    {
        Debug.Log("물리공격이 왜 발동하지?");
        StartCoroutine(HitEffect(point));
        wep.enabled = false;
        GameManager.instance.SoundManager.Effect.EffectSound(1);
        GameManager.instance.SoundManager.Effect.EffectSound(2);
        yield return new WaitForSeconds(0.5f);
        wep.enabled = true;
    }

    IEnumerator HitEffect(Vector3 point)
    {
        Debug.Log("플레이어의 히트이펙트");
        GameObject blood = Instantiate(BloodEffect, point, Quaternion.identity);
        blood.transform.SetParent(this.transform);
        ParticleSystem be = blood.GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(be.main.duration);
        Destroy(blood);
    }

}
