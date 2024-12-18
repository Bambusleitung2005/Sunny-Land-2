using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumEnemy : GroundEnemy, IDamageable
{
    public int Health{get; set;}
    public int Knockback{get; set;}
    [SerializeField] private int knockback;

    public override void Init()
    {
        Knockback = knockback;
        base.Init();
        Health = base.health;
    }

    void Start()
    {
        Init();
    }

    public override void Damage()
    {
        base.Damage();
        Debug.Log("Damage");
        if (base.health < 1)
        {
            _anim.SetBool("Dead", true);
            Destroy(transform.parent.gameObject, 0.5f);

        }
    }

    public override void Movement()
    {
        base.Movement();
    }
}
