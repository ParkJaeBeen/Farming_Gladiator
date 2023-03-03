using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField]
    private float _AttackDamage, _AttackSpeed, _Defence, _Maxhealth, _Health;

    public float AttackDamage { get => _AttackDamage; set => _AttackDamage = value; }
    public float AttackSpeed { get => _AttackSpeed; set => _AttackSpeed = value; }
    public float Defence { get => _Defence; set => _Defence = value; }
    public float Maxhealth { get => _Maxhealth; set => _Maxhealth = value; }
    public float Health { get => _Health; set => _Health = value; }


    public void InitHP()
    {
        _Health = _Maxhealth;
    }

    public void GetHit(float damage)
    {
        _Health -= damage;
    }

    public void DrinkPotion(float hp)
    {
        if((hp + _Health) > _Maxhealth)
        {
            float curePoint = hp - ((hp + _Health) - _Maxhealth);
            Debug.Log("채워지는 체력 - " + curePoint);
            _Health += curePoint;
        }
        else
        {
            Debug.Log("체력 회복 - "+ hp);
            _Health += hp;
        }
    }
}
