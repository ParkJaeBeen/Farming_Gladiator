using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private EffectSoundScript _Effect;
    [SerializeField]
    private BGMScript _BGM;

    public EffectSoundScript Effect { get => _Effect; set => _Effect = value; }
    public BGMScript BGM { get => _BGM; set => _BGM = value; }

    private void Awake()
    {
        Debug.Log(_BGM.name);
    }

    private void Start()
    {
        _BGM.BGM(0);
    }
}
