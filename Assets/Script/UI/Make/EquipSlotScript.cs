using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlotScript : MonoBehaviour
{
    [SerializeField]
    private Image DefaultIcon, slotImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slotImage.sprite)
            DefaultIcon.gameObject.SetActive(false);
        else
            DefaultIcon.gameObject.SetActive(true);
    }
}
