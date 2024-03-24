using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MagnetColor
{
    // Start is called before the first frame update

    [SerializeField] int sceneIndex;
    
    int playerCount;

    [SerializeField] PolarColor color;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogWarning(name+ ": "+ playerCount);
        if(playerCount == 2)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && color == other.GetComponentInChildren<Magnet>().GetMagnetColor())
        {
            playerCount++;

        }
    }

 
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(playerCount <= 1)
            {
                playerCount--;
            }
            

        }
    }
}
