using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBoat : Enemy, IDamageable
{
    private bool _rotateRight = true;

    public override void Init()
    {
        base.Init();
    }
    
    public override void Movement()
    {
        if (_rotateRight == true)
        {
            transform.RotateAround(player.transform.position, Vector3.up, 5 * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(player.transform.position, Vector3.up, -5 * Time.deltaTime);
        }
        
        transform.LookAt(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            _rotateRight = ! _rotateRight;
        }
    }

    public void Damage()
    {
        health--;

        if (health < 1)
        {
            spawnManager.ReduceSlowBoatCount();
            Destroy(this.gameObject);
        }
    }
}
