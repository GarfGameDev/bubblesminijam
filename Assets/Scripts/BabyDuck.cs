using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyDuck : MonoBehaviour
{
    private bool _collected = false;
    private bool _safe = false;
    private float _speed = 8.0f;
    private Transform _player;
    private Animator _anim;
    private float _playerDistance;
    private float _babyDuckDistance;
    private int _count;
    private int _id;
    private int score = 200;
    private GameObject _babyDuck;
    private GameObject _centre;
    private GameObject _duckContainer;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is null");
        }

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        
        if (_spawnManager == null) 
        {
            Debug.LogError("Spawn Manager is null");
        }

        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is null");
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.layer = 11;
            Player playerComponent = other.GetComponent<Player>();
            _count = playerComponent.FollowCount();
            if (_count < 1)
            {
                _player = other.transform;
            }

            else if (_count >= 1)
            {
                _babyDuck = GameObject.Find("BabyDuck" + _count.ToString());
                if (_babyDuck == null)
                {
                    Debug.LogError("Baby Duck is null");
                    return;
                }
            }
            playerComponent.UpdateFollow();
            _count = playerComponent.FollowCount();
            playerComponent.UpdateTotalFollow();
            this.gameObject.name = "BabyDuck" + _count;
            _anim.SetBool("Collected", true);
            StartCoroutine(WaitForAnimation());


            playerComponent.ReduceSpeed();
            //Debug.Log(_count);
            

            _collected = true;
        }

        if (other.tag == "Safe Zone")
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            this.gameObject.name = "Baby";
            _centre = GameObject.Find("Centre");

            player.ReduceFollowCount();
            _count = player.FollowCount();
            player.IncreaseSpeed();
            player.PermanentIncrease();
            _spawnManager.ReduceDuckCount();
            _uiManager.UpdateScore(score);

            _collected = false;
            _safe = true;
            float randomSide = Random.value;
            transform.position = new Vector3(Random.Range(-3.4f, 3.7f), -0.05f, Random.Range(5.7f, 11.8f));
            _duckContainer = GameObject.Find("DuckContainer");
            this.transform.parent = _duckContainer.transform;



        }
    }

    private void Update() 
    {
        if (_safe == false) 
        {
            CalculateDistance();
            Movement();
        }
        else 
        {
            IdleRotate();
        }
        
    }

    private void Movement()
    {
        if (_collected == true) 
        {
            if (_playerDistance > 1.7f && _count == 1)
            {
                if (_player != null) 
                {
                    transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
                    transform.LookAt(_player.position);
                }

            }

            else if (_babyDuckDistance > 1.7f && _count > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, _babyDuck.transform.position, _speed * Time.deltaTime);
                transform.LookAt(_babyDuck.transform.position);
            }

        }

    }

    private void IdleRotate()
    {
        if (_safe == true)
        {
            transform.RotateAround(_centre.transform.position, Vector3.up, 5 * Time.deltaTime);
        }
    }

    private void CalculateDistance()
    {
        if (_collected == true)
        {
            if (_count == 1)
            {
                if (_player != null)
                {
                    _playerDistance = Vector3.Distance(_player.position, transform.position);
                }
                
            }
            else if (_count > 1)
            {
                _babyDuckDistance = Vector3.Distance(_babyDuck.transform.position, transform.position);
            }
            
            
        }
        
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1.1f);
        _anim.speed = 0;
    }
}
