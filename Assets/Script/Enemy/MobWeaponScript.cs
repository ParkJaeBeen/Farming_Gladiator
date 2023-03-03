using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobWeaponScript : MonoBehaviour
{
    private Collider wep;

    private void Awake()
    {
        wep = this.GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && RoundOneMobScript.instance.IsAttack)
        {
            Character player = collision.collider.GetComponent<Character>();

            Debug.Log("공격당함");
            if (Character.instance.Move.IsBlocking)
            {
                GameManager.instance.SoundManager.Effect.EffectSound(0);
                Debug.Log("방패");
                RoundOneMobScript.instance.Stun();
                return;
            }
            else
            {
                StartCoroutine(Attack());
                battleFieldCanvasScript.instance.StartCor("HitEffect");
                Character.instance.status.GetHit(RoundOneMobScript.instance.AtkDamage);
            }
        }
    }

    IEnumerator Attack()
    {
        wep.enabled = false;
        GameManager.instance.SoundManager.Effect.EffectSound(1);
        yield return new WaitForSeconds(1.0f);
        wep.enabled = true;
    }
}
