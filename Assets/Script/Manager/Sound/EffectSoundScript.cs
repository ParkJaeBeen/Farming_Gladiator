using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundScript : MonoBehaviour
{
    [SerializeField] AudioClip block, attack, GetHit, Painful, ShootArrow, BuyItem, Clear, Over;

    private AudioSource Effect;

    public AudioSource EffectP { get => Effect; set => Effect = value; }

    private void Awake()
    {
        Effect = this.GetComponent<AudioSource>();
    }

    public void EffectSound(int num)
    {
        switch (num)
        {
            case 0:
                Effect.clip = block;
                break;
            case 1:
                Effect.clip = GetHit;
                break;
            case 2:
                Effect.clip = Painful;
                break;
            case 3:
                Effect.clip = ShootArrow;
                break;
            case 4:
                Effect.clip = BuyItem;
                break;
            case 5:
                Effect.clip = Clear;
                break;
            case 6:
                Effect.clip = Over;
                break;
            case 7:
                Effect.clip = attack;
                break;
        }

        Effect.Play();
    }
}
