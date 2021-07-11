using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _boat;
    [SerializeField]
    private GameObject _slowBoat;
    [SerializeField]
    private GameObject[] _babyDucks;
    private int _babyDuckCount = 0;
    private int _totalCount = 0;
    private int _boatCount = 0;
    private int _slowBoatCount = 0;
    private float _boatSpawnRate = 5.0f;
    private float _slowBoatSpawnRate = 8.0f;

    [SerializeField]
    private GameObject[] _powerups;
    private int _powerupCount;

    public void StartGame() 
    {
        StartCoroutine(SpawnBoatRoutine());
        StartCoroutine(SpawnSlowBoatRoutine());
    }

    public void Start() 
    {
        StartCoroutine(SpawnBabyDuck());
        StartCoroutine(SpawnBoatRoutine());
        StartCoroutine(SpawnSlowBoatRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnBoatRoutine()
    {
        while (true) 
        {
            if (_boatCount < 20)
            {
                yield return new WaitForSeconds(_boatSpawnRate);
                Vector3 position = new Vector3(Random.Range(-58.0f, 58.0f), -1.7f, Random.Range(-22.0f, 28.0f));
                Instantiate(_boat, position, Quaternion.identity);
                _boatCount++;
            }

            else 
            {
                yield return new WaitUntil(() => _boatCount < 20);
            }

            
        }
    }

    IEnumerator SpawnSlowBoatRoutine()
    {
        while (true) 
        {
            if (_slowBoatCount < 20)
            {
                yield return new WaitForSeconds(_slowBoatSpawnRate);
                Vector3 position = new Vector3(Random.Range(-58.0f, 58.0f), -1.6f, Random.Range(-22.0f, 28.0f));
                Instantiate(_slowBoat, position, Quaternion.identity);
                _slowBoatCount++;
            }
            else 
            {
                yield return new WaitUntil(() => _slowBoatCount < 20);
            }


            
        }
    }

    IEnumerator SpawnBabyDuck()
    {
        while (true) 
        {
            if (_babyDuckCount < 5)
            {
                int randomDuck = Random.Range(0, 5);
                float randomSide = Random.value;

                if (randomSide > 0.5)
                {
                    Vector3 position = new Vector3(Random.Range(-50.0f, -16.0f), -0.05f, Random.Range(-20.0f, 22.0f));
                    Instantiate(_babyDucks[randomDuck], position, Quaternion.identity);
                }
                else if (randomSide <= 0.5)
                {
                    Vector3 position = new Vector3(Random.Range(15.0f, 56.0f), -0.05f, Random.Range(-20.0f, 22.0f));
                    Instantiate(_babyDucks[randomDuck], position, Quaternion.identity);
                }

                
                
                _babyDuckCount++;
                _totalCount++;
                if (_totalCount < 15)
                {
                    _slowBoatSpawnRate -= 0.4f;
                    _boatSpawnRate -= 0.2f;
                }

                yield return new WaitForSeconds(4.0f);
            }
            else 
            {
                yield return new WaitUntil(() => _babyDuckCount < 5);
            }

            

        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(5.0f);
            if (_powerupCount < 2)
            {
                _powerupCount++;
                Vector3 position = new Vector3(Random.Range(-50.0f, 50.0f), -0.18f, Random.Range(-20.0f, 22.0f));
                float randomNumber = Random.value;
                if (randomNumber < 0.3f)
                {
                    Instantiate(_powerups[1], position, Quaternion.identity);
                }
                else if (randomNumber >= 0.3f)
                {
                    Instantiate(_powerups[0], position, Quaternion.identity);
                }
                
            }
            else 
            {
                yield return new WaitUntil(() => _powerupCount < 2);
            }

        }
    }

    public void ReduceDuckCount()
    {
        if (_babyDuckCount > 0)
        {
            _babyDuckCount--;
        }
        
    }

    public void ReducePowerUpCount()
    {

        _powerupCount--;

    }

    public void ReduceBoatCount()
    {
        if (_boatCount > 0)
        {
            _boatCount--;
        }
    }

    public void ReduceSlowBoatCount()
    {
        if (_slowBoatCount > 0)
        {
            _slowBoatCount--;
        }
    }
}
