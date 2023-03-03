using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class Farming : MonoBehaviour
{
    [SerializeField]
    private GameObject Axe, PickAxe;
    [SerializeField]
    private Animator ani;

    private bool _IsChopping;
    private bool _IsMining;
    private bool _IsGathering;

    private float chopTime = 3.0f, mineTime = 3.0f, gatherTime = 1.0f;

    private UIManager Canvas;

    public bool IsChopping
    {
        get { return _IsChopping; }
        set { _IsChopping = value; }
    }

    public bool IsMining
    {
        get { return _IsMining; }
        set { _IsMining = value; }
    }
    public bool IsGathering
    {
        get { return _IsGathering; }
        set { _IsGathering = value; }
    }

    private void Start()
    {
        Canvas = GameObject.Find("Canvas").GetComponent<UIManager>();
    }


    void Behaviour(string type, Collider target)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= 3.0f)
            {
                if (type.Equals("Tree"))
                {
                    Axe.SetActive(true);
                    Character.instance.Move.IsMove = true;
                    ani.SetBool("IsChopping", true);
                    GatherObj("Tree", target);
                }
                else if (type.Equals("Rock"))
                {
                    PickAxe.SetActive(true);
                    Character.instance.Move.IsMove = true;
                    ani.SetBool("IsMining", true);
                    GatherObj("Rock", target);
                }
            }
            
            if (Vector3.Distance(transform.position, target.transform.position) <= 5.0f)
            {
                if (type.Equals("Gather"))
                {
                    Character.instance.Move.IsMove = true;
                    ani.SetBool("IsGathering", true);
                    GatherObj("Gather", target);
                }
            }
        }
        // 도중에 취소시
        else
        {
            if (type.Equals("Tree"))
            {
                Axe.SetActive(false);
                ani.SetBool("IsChopping", false);
                chopTime = 3.0f;
            }
            else if (type.Equals("Rock"))
            {
                PickAxe.SetActive(false);
                ani.SetBool("IsMining", false);
                mineTime = 3.0f;
            }
            else if (type.Equals("Gather"))
            {
                ani.SetBool("IsGathering", false);
                gatherTime = 1.0f;
            }
            Canvas.TimePanelOnOff(false);
            Character.instance.Move.IsMove = false;
            Character.instance.Move.toggleCameraRotationP = false;
        }
    }

    void GatherObj(string type, Collider target)
    {
        if (type.Equals("Tree"))
        {
            chopTime -= Time.deltaTime;
            Canvas.TimePanelActive(3, chopTime);
            if (chopTime <= 0f)
            {
                Axe.SetActive(false);
                Character.instance.Move.IsMove = false;
                ani.SetBool("IsChopping", false);
                Character.instance.inventory.ResourceArray[0] = Character.instance.inventory.ResourceArray[0] + Calculate();
                Debug.Log(Character.instance.inventory.ResourceArray[0]);
                Canvas.ElementCount(0, Character.instance.inventory.ResourceArray[0]);
                Destroy(target.gameObject);
                UIManager.instance.ChangeCursorImg();
            }
        }
        else if (type.Equals("Rock"))
        {
            mineTime -= Time.deltaTime;
            Canvas.TimePanelActive(3, mineTime);
            if (mineTime <= 0f)
            {
                PickAxe.SetActive(false);
                Character.instance.Move.IsMove = false;
                ani.SetBool("IsMining", false);
                Character.instance.inventory.ResourceArray[1] += Calculate();
                Character.instance.inventory.ResourceArray[2] += OreCalculate();
                Canvas.ElementCount(1, Character.instance.inventory.ResourceArray[1]);
                Canvas.ElementCount(2, Character.instance.inventory.ResourceArray[2]);
                Destroy(target.gameObject);
                UIManager.instance.ChangeCursorImg();
            }
        }
        else if (type.Equals("Gather"))
        {
            gatherTime -= Time.deltaTime;
            Canvas.TimePanelActive(1, gatherTime);
            if (gatherTime <= 0f)
            {
                Character.instance.Move.IsMove = false;
                ani.SetBool("IsGathering", false);

                if (target.CompareTag("Flower"))
                {
                    Character.instance.inventory.ResourceArray[3] += Calculate();
                    Canvas.ElementCount(3, Character.instance.inventory.ResourceArray[3]);
                }
                else if (target.CompareTag("Mushroom"))
                {
                    Character.instance.inventory.ResourceArray[4] += Calculate();
                    Canvas.ElementCount(4, Character.instance.inventory.ResourceArray[4]);
                }
                else if (target.CompareTag("Herb"))
                {
                    Character.instance.inventory.ResourceArray[5] += Calculate();
                    Canvas.ElementCount(5, Character.instance.inventory.ResourceArray[5]);
                }
                Destroy(target.gameObject);
                UIManager.instance.ChangeCursorImg();
            }
        }
        Character.instance.Move.toggleCameraRotationP = false;
    }

    int Calculate()
    {
        int count = Random.Range(1,101);
        Debug.Log("랜덤숫자 = "+count);
        if (count >= 31 && count <= 100)
            return 1;
        else if (count <= 30 && count >= 11)
            return 2;
        else
            return 3;
    }

    int OreCalculate()
    {
        int oreCount = Random.Range(1, 101);

        if (oreCount >= 31 && oreCount <= 100)
            return 0;
        else if (oreCount <= 30 && oreCount >= 11)
            return 1;
        else
            return 2;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        int CameraDistance = Character.instance.isTPSP ? 10 : 3;

        if (Physics.Raycast(ray,out hitInfo, CameraDistance))
        {
            if (hitInfo.collider.CompareTag("Tree"))
            {
                Behaviour("Tree",hitInfo.collider);
            }
            else if (hitInfo.collider.CompareTag("Rock"))
            {
                Behaviour("Rock", hitInfo.collider);
            }
            else if (hitInfo.collider.CompareTag("Flower") || hitInfo.collider.CompareTag("Mushroom") || hitInfo.collider.CompareTag("Herb"))
            {
                Behaviour("Gather", hitInfo.collider);
            }
        }
    }

    public void DeActivateAll()
    {
        Character.instance.Move.IsMove = false;
        Axe.SetActive(false);
        PickAxe.SetActive(false);
        ani.SetBool("IsChopping", false);
        ani.SetBool("IsMining", false);
        ani.SetBool("IsGathering", false);
    }
}
