using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int score;
    [SerializeField]
    protected float fireRate = 0.5f;
    protected float nextFire;

    [SerializeField]
    protected GameObject bullet;

    protected SpawnManager spawnManager;
    protected UIManager uIManager;
    protected Player player;

    public virtual void Init() 
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }

        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        if (uIManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }
    }

    private void Start() 
    {
        Init();
    }

    public virtual void Update() 
    {
        if (player != null)
        {
            FireBullets();
            Movement();
        }
    }

    public virtual void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.LookAt(player.transform.position);
    }

    public virtual void FireBullets()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, transform.position, transform.localRotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            hit.Damage();
            Destroy(this.gameObject);
        }
    }


}
