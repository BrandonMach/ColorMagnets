using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _barrelPrefab;
    bool StartSpawn;

    [SerializeField] GameObject _collectablePrefab;
    void Start()
    {
        StartSpawn = false;
        for (int i = 0; i < 220; i++)
        {
            Instantiate(_barrelPrefab, new Vector3(Random.Range(-19, 19), 3.8f, Random.Range(-19, 19)), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_collectablePrefab, new Vector3(Random.Range(-19, 19), 3.8f, Random.Range(-19, 19)), Quaternion.identity);

        }

        if (StartSpawn)
        {
            StartSpawn = false;
            for (int i = 0; i < 220; i++)
            {
                Instantiate(_barrelPrefab, new Vector3(Random.Range(-19, 19), 3.8f, Random.Range(-19, 19)), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));

            }
        }
    }
}
