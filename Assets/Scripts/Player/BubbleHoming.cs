using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleHoming : MonoBehaviour
{
    private GameObject[] _enemies;
    [SerializeField]
    private GameObject _homingMissile;
    private Transform _target;
    private float _speed = 17.0f;

    void Init() 
    {
        if (GameObject.FindGameObjectsWithTag("Enemy") != null)
        {
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");
            _target = FireMissile();
            StartCoroutine(DelayedDestroyRoutine());

        }
    }
    void Start()
    {
        Init();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy") != null)
        {
            if ( _target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
                transform.LookAt(_target.position);
            }
            else 
            {
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private Transform FireMissile()
    {
        if (_enemies.Length == 0)
        {
            return null;
        }

        else 
        {
            Transform[] enemyTransform = new Transform[_enemies.Length];
        
            float minDistance = Mathf.Infinity;
            float distance;

            Transform nearestTarget;

            nearestTarget = _enemies[0].transform;

            for (int i = 0; i < _enemies.Length; i++)
            {
                
                if (_enemies[i] == null)
                {
                    return null;
                }

                else 
                {
                    enemyTransform[i] = _enemies[i].transform;
                }

                distance = Vector3.Distance(enemyTransform[i].position, transform.position);

                if (distance < minDistance)
                {
                    nearestTarget = enemyTransform[i];
                    minDistance = distance;

                }
            }

            return nearestTarget;

        }
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
            StartCoroutine(DestroyRoutine());
        }
    }

    IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(_homingMissile, this.transform.position, this.transform.localRotation);
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }

    IEnumerator DelayedDestroyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
    
}
