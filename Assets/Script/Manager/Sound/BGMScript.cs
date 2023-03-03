using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    [SerializeField] AudioClip First, Gather, Battle;

    private AudioSource BGMSource;

    public AudioSource BGMSourceP { get => BGMSource; set => BGMSource = value; }

    private void Awake()
    {
        BGMSource = GetComponent<AudioSource>();
    }

    public void BGM(int num)
    {
        Debug.Log(num);
        switch(num)
        {
            case 0:
                BGMSource.clip = First;
                break;
            case 1:
                BGMSource.clip = Gather;
                break;
            case 2:
                BGMSource.clip = Battle;
                break;
        }

        BGMSource.Play();
    }
}
