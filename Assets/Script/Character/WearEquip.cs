using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearEquip : MonoBehaviour
{
    [SerializeField]
    private Transform Weapon, Shield, WoodArmor, IronArmor;

    public List<GameObject> Equipments;
    public List<GameObject> wearedEqList;

    public GameObject _Melee, _Bow, _Shield;

    private Status status;
    private string[] Weapons = new string[] { "Sword", "Mace", "Hammer", "Axe" };
    private string[] Armors = new string[] { "Helmet", "Upper", "Under", "Shoe" };

    private void Awake()
    {
        Equipments = new List<GameObject>();
        wearedEqList = new List<GameObject>();
        AddTransforms(Weapon);
        AddTransforms(Shield);
        AddTransforms(WoodArmor);
        AddTransforms(IronArmor);
    }

    private void Start()
    {
        status = Character.instance.status;
    }

    void AddTransforms(Transform addobj)
    {
        for(int i = 0; i < addobj.childCount; i++)
        {
            Equipments.Add(addobj.GetChild(i).gameObject);
        }
    }

    public void FindActiveObj()
    {
        for(int i = 0; i < wearedEqList.Count; i++)
        {
            if (wearedEqList[i].CompareTag("Weapon"))
                _Melee = wearedEqList[i];
            else if(wearedEqList[i].CompareTag("Shield"))
                _Shield = wearedEqList[i];
            // Bow => Player
            else if (wearedEqList[i].CompareTag("Player"))
                _Bow = wearedEqList[i];
        }
    }

    public void ChangeActiveWeapon(string name)
    {
        if (name.Equals("SwordAndShield"))
        {
            _Melee.SetActive(true);
            _Shield.SetActive(true);
            _Bow.SetActive(false);
            Character.instance.inventory.DeActivatePotion();
        }
        else if (name.Equals("Bow"))
        {
            _Melee.SetActive(false);
            _Shield.SetActive(false);
            _Bow.SetActive(true);
            Character.instance.inventory.DeActivatePotion();
        }
        else if (name.Equals("Potion"))
        {
            _Melee.SetActive(false);
            _Shield.SetActive(false);
            _Bow.SetActive(false);
        }
    }

    public void ActivateObj(string name)
    {
        if (name.Contains("Bow"))
        {
            for (int i = 0; i < Equipments.Count; i++)
            {
                if (Equipments[i].name.Equals(name))
                {
                    wearedEqList.Add(Equipments[i]);
                    FindActiveObj();
                }
            }  
            return;
        }
            

        for(int i = 0; i< Equipments.Count;i++)
        {
            if (Equipments[i].name.Equals(name))
            {
                Equipments[i].SetActive(true);
                wearedEqList.Add(Equipments[i]);
                PlusStatus(name);
                FindActiveObj();
            }
        }
    }

    public void DeActivateObj(string name)
    {
        for (int i = 0; i < Equipments.Count; i++)
        {
            if (Equipments[i].name.Equals(name))
            {
                Equipments[i].SetActive(false);
                wearedEqList.Remove(Equipments[i]);
                MinusStatus(name);
            }
        }
    }

    public void PlusStatus(string name)
    {
        for(int i = 0; i< 4; i++)
        {
            if (name.Contains(Weapons[i]))
            {
                string[] weaponInfo = InfoScript.instance.GetListInfo(name, "Weapon");
                status.AttackDamage += float.Parse(weaponInfo[1]);
                status.Defence += float.Parse(weaponInfo[2]);
                if (!name.Contains("Bow"))
                    status.AttackSpeed = float.Parse(weaponInfo[3]);
            }
            else if (name.Contains(Armors[i]))
            {
                string[] armorInfo = InfoScript.instance.GetListInfo(name, "Armor");
                status.Defence += float.Parse(armorInfo[1]);
                status.Maxhealth += float.Parse(armorInfo[2]);
                status.Health = status.Maxhealth;
            }
        }
    }

    public void MinusStatus(string name)
    {
        for (int i = 0; i < 4; i++)
        {
            if (name.Contains(Weapons[i]))
            {
                string[] weaponInfo = InfoScript.instance.GetListInfo(name, "Weapon");
                status.AttackDamage -= float.Parse(weaponInfo[1]);
                status.Defence -= float.Parse(weaponInfo[2]);
                if (!name.Contains("Bow"))
                    status.AttackSpeed = 1.0f;
            }
            else if (name.Contains(Armors[i]))
            {
                string[] armorInfo = InfoScript.instance.GetListInfo(name, "Armor");
                status.Defence -= float.Parse(armorInfo[1]);
                status.Maxhealth -= float.Parse(armorInfo[2]);
                status.Health = status.Maxhealth;
            }
        }
    }

    
}
