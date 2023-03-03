using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<string> _EquipmentList;

    public int[] _ResourceArray = new int[6];
    public int[] _UseableArray = new int[6];

    public List<string> EquipmentList
    {
        get { return _EquipmentList; }
        set { _EquipmentList = value; }
    }

    // 1 : wood, 2 : rock, 3 : ore, 4 : herb , 5 : flower, 6 : mushroom
    public int[] ResourceArray { get => _ResourceArray; set => _ResourceArray = value; }
    // 1:lowpotion, 2:middlepotion, 3:highpotion, 4:woodarrow, 5:ironarrow,6:blackarrow
    public int[] UseableArray { get => _UseableArray; set => _UseableArray = value; }
    public GameObject CurrentArrow { get => _CurrentArrow; set => _CurrentArrow = value; }

    [SerializeField]
    private List<GameObject> Potions, Arrows;
    [SerializeField]
    private GameObject potionPoint, arrowPoint;

    public GameObject _CurrentArrow;
    public int CurrentArrowNum;

    private void Awake()
    {
        _EquipmentList = new List<string>();
    }

    bool CheckListLength()
    {
        if (_EquipmentList.Count >= 20)
            return false;
        else
            return true;
    }

    public bool CheckOverLap(string name)
    {
        for (int i = 0; i < _EquipmentList.Count; i++)
        {
            if (_EquipmentList[i].Equals(name))
                return true;
        }
        return false;
    }

    public void RemoveElement(int wood, int rock, int ore, int herb, int flower, int mushroom,  string type)
    {
        if (type.Equals("Weapon") || type.Equals("Armor"))
        {
            _ResourceArray[0] -= wood;
            _ResourceArray[1] -= rock;
            _ResourceArray[2] -= ore;
        }
        else if (type.Equals("Potion"))
        {
            _ResourceArray[3] -= herb;
            _ResourceArray[4] -= flower;
            _ResourceArray[5] -= mushroom;
        }
    }

    public void AddItem(string name)
    {
        if (CheckListLength())
        {
            Debug.Log(name);
            _EquipmentList.Add(name);
        }
        else
            Debug.Log("아이템 추가 실패");
    }

    public void RemoveItem(string name)
    {
        _EquipmentList.Remove(_EquipmentList.Find(x => x.Equals(name)));
    }

    public void AddArrow(string name)
    {
        if (name.Contains("Wood"))
            UseableArray[3]++;
        else if (name.Contains("Iron"))
            UseableArray[4]++;
        else if (name.Contains("Black"))
            UseableArray[5]++;
    }

    public void AddPotion(string name)
    {
        if (name.Contains("Low"))
            UseableArray[0]++;
        else if (name.Contains("Middle"))
            UseableArray[1]++;
        else if (name.Contains("High"))
            UseableArray[2]++;
    }

    public void ActivatePotion()
    {
        for (int i = 0; i < _UseableArray.Length; i++)
        {
            if (_UseableArray[i] > 0)
            {
                Potions[i].SetActive(true);
                battleFieldCanvasScript.instance.ChangeCurrentPotionImg(i);
                break;
            }
        }
    }

    public void DeActivatePotion()
    {
        for (int i = 0; i < Potions.Count; i++)
        {
            if (Potions[i].activeSelf)
            {
                Potions[i].SetActive(false);
                break;
            }
        }
    }


    public bool HasPotion()
    {
        if (_UseableArray[0] > 0 || _UseableArray[1] > 0 || _UseableArray[2] > 0)
            return true;

        return false;
    }

    // 순서대로 있으면 바로 리턴 - 맨 처음에 있는 화살으로 초기값 결정하는 함수
    public void HasArrow()
    {
        if (_UseableArray[3] > 0)
        {
            ArrowResource("Wood_Arrow");
            return;
        }
        else if(_UseableArray[4] > 0)
        {
            ArrowResource("Iron_Arrow");
            return;
        }
        else if (_UseableArray[5] > 0)
        {
            ArrowResource("Black_Arrow");
            return;
        }
        else
        {
            Debug.Log("no Arrow");
        }
    }

    public bool HasArrow2()
    {
        if (_UseableArray[3] > 0)
        {
            ArrowResource("Wood_Arrow");
            return true;
        }
        else if (_UseableArray[4] > 0)
        {
            ArrowResource("Iron_Arrow");
            return true;
        }
        else if (_UseableArray[5] > 0)
        {
            ArrowResource("Black_Arrow");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HasArrowTotal()
    {
        if (_UseableArray[3] <= 0 && _UseableArray[4] <= 0 && _UseableArray[5] <= 0)
        {
            _CurrentArrow = null;
            CurrentArrowNum = -1;
            battleFieldCanvasScript.instance.ChangeCurrentArrowImg(null);
        }
    }

    // 화살 오브젝트 바꿔주는 함수
    public void ArrowResource(string name)
    {
        _CurrentArrow = Resources.Load<GameObject>("Prefabs/Arrow/" + name);
        switch (_CurrentArrow.name)
        {
            case "Wood_Arrow":
                CurrentArrowNum = 3;
                break;
            case "Iron_Arrow":
                CurrentArrowNum = 4;
                break;
            case "Black_Arrow":
                CurrentArrowNum = 5;
                break;
        }

        battleFieldCanvasScript.instance.ChangeCurrentArrowImg(name);
    }


    public void UsePotionOrArrow(int num)
    {
        if (num != 10)
        {
            // 포션
            if (num < 3)
            {
                Character.instance.status.DrinkPotion(PotionInfo.instance.GetPotionInfo(num));
                _UseableArray[num]--;
            }
            // 화살
            else if (num > 2)
            {
                _UseableArray[num]--;
            }
        }

        else
            Debug.Log("10들어옴");
    }

    public int CurrentPotion()
    {
        for (int i = 0; i < potionPoint.transform.childCount; i++)
        {
            if (potionPoint.transform.GetChild(i).gameObject.activeSelf)
                return i;
        }

        return 10;
    }

    public void CheckPotionCount(int num)
    {
        int[] nums = new int[2];

        switch (num)
        {
            case 0:
                nums = new int[] { 2, 1 };
                break;
            case 1:
                nums = new int[] { 0, 2 };
                break;
            case 2:
                nums = new int[] { 1, 0 };
                break;
        }

        for(int i = 0; i< nums.Length; i++)
        {
            if (_UseableArray[nums[i]] > 0)
            {
                ChangeActivePotion(nums[i]);
            }
            else
            {
                Debug.Log("다른포션 없음");
            }
        }
    }


    // 포션 상태에서 Tab 눌렀을때
    public void ChangeActivePotion(int num)
    {
        Debug.Log("현재 포션 = " + Potions[CurrentPotion()].name);
        Potions[CurrentPotion()].SetActive(false);
        Potions[num].SetActive(true);
        Debug.Log("바뀐 포션 = "+Potions[num].name);
    }


    public void ChangeCurrentArrow(string name)
    {
        int[] nums = new int[2];

        switch (name)
        {
            case "Wood_Arrow":
                nums = new int[] { 5, 4 };
                break;
            case "Iron_Arrow":
                nums = new int[] { 3, 5 };
                break;
            case "Black_Arrow":
                nums = new int[] { 4, 3 };
                break;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            if (_UseableArray[nums[i]] > 0)
            {
                ChangeActiveArrow(nums[i]);
            }
            else
            {
                Debug.Log("다른화살 없음");
            }
        }
    }

    public void ChangeActiveArrow(int num)
    {
        switch (num)
        {
            case 3:
                ArrowResource("Wood_Arrow");
                break;
            case 4:
                ArrowResource("Iron_Arrow");
                break;
            case 5:
                ArrowResource("Black_Arrow");
                break;
        }
    }

    public void InstanceArrow()
    {
        GameObject Arrow = GameObject.Instantiate(_CurrentArrow, arrowPoint.transform.position, arrowPoint.transform.rotation);
        Rigidbody AR = Arrow.transform.GetChild(0).GetComponent<Rigidbody>();
        AR.velocity = Arrow.transform.forward * 20;
    }
}
