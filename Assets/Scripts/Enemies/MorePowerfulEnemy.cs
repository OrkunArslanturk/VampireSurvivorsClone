using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorePowerfulEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();

        moveSpeed = 1f;
    }

    protected override void Death()
    {
        base.Death();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}
