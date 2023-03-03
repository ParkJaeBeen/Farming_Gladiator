using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowParentScript : MonoBehaviour
{
    private float _damage;

    public float Damage { get => _damage; set => _damage = value; }

    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        transform.name = transform.name.Split("(")[0];
        _damage = InfoScript.instance.GetArrowDamage(transform.name);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
