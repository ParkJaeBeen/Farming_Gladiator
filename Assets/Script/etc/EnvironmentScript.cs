using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnvironmentScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Tree, Rock, Mushroom, Flower, Herb;

    // Start is called before the first frame update
    void Start()
    {
        TotalAdd();
    }

    public void TotalAdd()
    {
        AddList(Tree);
        AddList(Rock);
        AddList(Mushroom);
        AddList(Flower);
        AddList(Herb);
    }

    void AddList(GameObject obj)
    {
        for(int i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).AddComponent<AddScript>();
        }
    }

}
