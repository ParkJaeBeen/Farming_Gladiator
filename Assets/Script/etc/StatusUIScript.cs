using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUIScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ADText, ASText, SText, HPText, DFText;

    public void InitStatus()
    {
        ADText.text = Character.instance.status.AttackDamage.ToString();
        ASText.text = Character.instance.status.AttackSpeed.ToString();
        HPText.text = Character.instance.status.Maxhealth.ToString();
        DFText.text = Character.instance.status.Defence.ToString();
        SText.text = Character.instance.Move.FinalSpeed.ToString();
    }
}
