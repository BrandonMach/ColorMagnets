using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _barrelPrefab;
    [SerializeField] GameObject _carPrefab;
    [SerializeField] GameObject _tankPrefab;
    bool StartSpawn;

    [SerializeField] GameObject[] _collectablePrefab;

    public int TotalCollectables;


    static Spawner _instance;
    public static Spawner Instance {get=>_instance; set=>_instance=value;}

    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }
        Instance = this;
    }
    void Start()
    {
        StartSpawn = false;
        for (int i = 0; i < 180; i++)
        {
            //Instantiate(_barrelPrefab, new Vector3(Random.Range(-19, 19), 3.8f, Random.Range(-19, 19)), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));
            SpawnMagneticItem(_barrelPrefab);
        }
        for (int i = 0; i < 30; i++)
        {
            SpawnMagneticItem(_carPrefab);
        }
        for (int i = 0; i < 20; i++)
        {
            SpawnMagneticItem(_tankPrefab);
        }
        for (int i = 0; i < 2; i++)
        {
            //Instantiate(_collectablePrefab[Random.Range(0, _collectablePrefab.Length)], new Vector3(Random.Range(-19, 19), 0f, Random.Range(-19, 19)), Quaternion.identity);
            SpawnCollectable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_collectablePrefab[Random.Range(0,_collectablePrefab.Length)], new Vector3(Random.Range(-19, 19), 0f, Random.Range(-19, 19)), Quaternion.identity);

        }


        if(TotalCollectables <= 0)
        {
            SpawnCollectable();
            SpawnCollectable();
        }
      
    }


    void SpawnMagneticItem(GameObject itemType)
    {
        Instantiate(itemType, new Vector3(Random.Range(-19, 19), 3.8f, Random.Range(-19, 19)), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));

    }
    void SpawnCollectable()
    {
        TotalCollectables++;
        Instantiate(_collectablePrefab[Random.Range(0, _collectablePrefab.Length)], new Vector3(Random.Range(-19, 19), 1f, Random.Range(-19, 19)), Quaternion.identity);
    }
}
