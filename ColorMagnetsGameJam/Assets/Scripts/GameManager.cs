using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerScript[] _playerArray;
    [SerializeField] GameObject _winnerTextgameObject;
    int _winThreashold;

    bool GameOver;


    static GameManager _instance;

    public static GameManager Instance { get => _instance; set => _instance = value; }

    [SerializeField] AudioClip _mainTheme;
    

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
        _winThreashold = 7;
        _winnerTextgameObject.SetActive(false);
        _playerArray = FindObjectsOfType<PlayerScript>();


        StartCoroutine(StartAudio());
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerArray.Any(player => player.GetPlayerScore() == _winThreashold))
        {

            GameOver = true;
        }

        if (GameOver)
        {
            foreach (var item in _playerArray)
            {
                if(item.GetPlayerScore() == _winThreashold)
                {
                    //  Debug.LogError("Player:" + ((int)item._playerIndex + 1 )+ " it the winner");
                    _winnerTextgameObject.SetActive(true);
                    _winnerTextgameObject.GetComponent<TextMeshProUGUI>().text = "Player: " + ((int)item._playerIndex + 1) + " is the winner";
                }
            }

            
        }
    }


    IEnumerator StartAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        foreach (var player in _playerArray)
        {
            player.CanMove = true;
        }
        audio.clip = _mainTheme;
        audio.Play();
        audio.loop = true;
    }


}
