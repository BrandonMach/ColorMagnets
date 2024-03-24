using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Button : MagnetColor
{
    // Start is called before the first frame update

    [SerializeField] int sceneIndex;
    
    int playerCount;

    [SerializeField] PolarColor color;

    MainMenuRobotScript[] _players;
    
    void Start()
    {
        _players = FindObjectsOfType<MainMenuRobotScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCount <= 0)
        {
            playerCount = 0;
        }
        Debug.LogWarning(name+ ": "+ playerCount);
        if(playerCount >= 2)
        {
            if (_players.All(player => color == player.GetComponentInChildren<Magnet>().GetMagnetColor()))
            {
                SceneManager.LoadScene(sceneIndex);
            }
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
