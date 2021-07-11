using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private float _speed = 8.0f;
    private float _gravity = 5.6f;
    private bool _isInvincible = false;
    private bool _canFireHoming = false;
    [SerializeField]
    private int _health = 3;
    private CharacterController _playerController;
    private Camera _mainCamera;

    [SerializeField]
    private float _cameraSensitivity = 2.0f;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire;

    private int _followCount;
    private int _totalCount;
    private bool _followingPlayer = false;

    [SerializeField]
    private GameObject _bubbleBullet;
    [SerializeField]
    private GameObject _homingBubble;
    [SerializeField]
    private GameObject _bubbleContainer;
    private SpawnManager _spawnManager;
    private Animator _anim;

    void Start()
    {
        _playerController = GetComponent<CharacterController>();

        if (_playerController == null)
        {
            Debug.LogError("Player Controller is null");
        }

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.Log("The Spawn Manager is null");
        }

        _mainCamera = Camera.main;

        if (_mainCamera == null)
        {
            Debug.LogError("Main Camera is null");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.Log("The animator is null");
        }

        Cursor.lockState = CursorLockMode.Locked;
    }
   
   
    void Update()
    {
        CameraController();
        Movement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            GameObject bubble = Instantiate(_bubbleBullet, transform.position, transform.localRotation);

            bubble.transform.parent = _bubbleContainer.transform;
        }

        if (Input.GetKeyDown(KeyCode.F) && _canFireHoming == true)
        {
            _canFireHoming = false;
            Instantiate(_homingBubble, transform.position, transform.localRotation);        
        }

    }

    void Movement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        direction.y -= _gravity * Time.deltaTime;

        direction = transform.TransformDirection(direction);

        _playerController.Move(direction * _speed * Time.deltaTime);
    }

    void CameraController()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + mouseX, transform.localEulerAngles.z);

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _cameraSensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        // Vector3 currentCameraRotation = _mainCamera.gameObject.transform.localEulerAngles;
        // currentCameraRotation.x -= mouseY;
        // _mainCamera.gameObject.transform.localRotation = currentCameraRotation;
        
    }

    public void Damage()
    {
        if (_isInvincible == true) 
        {
            return;
        }

        else if (_isInvincible == false)
        {
            _anim.SetTrigger("Hit");
            StartCoroutine(InvincibleRoutine());
            _health--;
            
            if (_health < 1)
            {
                Destroy(this.gameObject);
            }
        }

    }

    public void UpdateFollow()
    {
        if (_followingPlayer == false)
        {
            _followingPlayer = true;
            _followCount++;
        }
        else 
        {
            _followCount++;
        }
    }

    public void UpdateTotalFollow()
    {
        _totalCount++;

        if (_totalCount > 10)
        {
            Destroy(GameObject.Find("Baby"));
        }
    }

    public void ReduceFollowCount()
    {
        if (_followCount == 0)
        {
            _followingPlayer = false;
        }
        else 
        {
            _followCount--;
        }
    }

    public int FollowCount()
    {
        return _followCount;
    }

    public void ReduceSpeed()
    {
        _speed -= 0.8f;
        _fireRate -= 0.05f;
    }

    public void IncreaseSpeed()
    {
        _speed += 0.8f;
        _fireRate += 0.05f;
    }

    public void HealPlayer()
    {
        _spawnManager.ReducePowerUpCount();
        if (_health < 9)
        {
            _health += 3;
            if (_health > 9)
            {
                _health = 9;
            }
        }
    }

    public void CanFireHomingBubble()
    {
        _spawnManager.ReducePowerUpCount();
        _canFireHoming = true;
    }

    IEnumerator InvincibleRoutine()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(0.7f);
        _isInvincible = false;
    }

}
