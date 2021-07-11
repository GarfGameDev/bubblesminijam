using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float _speed = 9.0f;


    // Update is called once per frame
    public void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            Destroy(this.gameObject);
        }

        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            hit.Damage();
            Destroy(this.gameObject);
        }
    }
}
