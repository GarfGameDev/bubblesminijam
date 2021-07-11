using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Enemy, IDamageable
{
    public override void Init()
    {
        base.Init();

    }

    public void Damage()
    {
        health--;

        if (health < 1)
        {
            spawnManager.ReduceBoatCount();
            Destroy(this.gameObject);
        }
    }

}
