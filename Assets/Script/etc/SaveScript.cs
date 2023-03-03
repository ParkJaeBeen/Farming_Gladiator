using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SaveScript : MonoBehaviour
{
    public static SaveScript instance;

    Character character;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        character = Character.instance;
    }

    public void SaveData()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("HP", character.status.Health);
        
        PlayerPrefs.SetInt("Wood_Arrow", character.inventory.UseableArray[3]);
        PlayerPrefs.SetInt("Iron_Arrow", character.inventory.UseableArray[4]);
        PlayerPrefs.SetInt("Black_Arrow", character.inventory.UseableArray[5]);

        PlayerPrefs.SetInt("Low_Potion", character.inventory.UseableArray[0]);
        PlayerPrefs.SetInt("Middle_Potion", character.inventory.UseableArray[1]);
        PlayerPrefs.SetInt("High_Potion", character.inventory.UseableArray[2]);

        PlayerPrefs.Save();

        Debug.Log("저장된 체력 = "+PlayerPrefs.GetFloat("HP"));
    }

    public void LoadData()
    {
        character.status.Health = PlayerPrefs.GetFloat("HP");

        character.inventory.UseableArray[3] = PlayerPrefs.GetInt("Wood_Arrow");
        character.inventory.UseableArray[4] = PlayerPrefs.GetInt("Iron_Arrow");
        character.inventory.UseableArray[5] = PlayerPrefs.GetInt("Black_Arrow");
        
        character.inventory.UseableArray[0] = PlayerPrefs.GetInt("Low_Potion");
        character.inventory.UseableArray[1] = PlayerPrefs.GetInt("Middle_Potion");
        character.inventory.UseableArray[2] = PlayerPrefs.GetInt("High_Potion");
    }
}
