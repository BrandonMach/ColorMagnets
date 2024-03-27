using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public int countdownTime; //Int for 3,2,1
    public GameObject countdownDisplay;
    [SerializeField] GameObject[] _playerTags;



    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(!GameManager.Instance.StartGame && countdownTime > 0)
        {
            countdownDisplay.GetComponent<TextMeshProUGUI>().text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        StartCoroutine(ShowGoText());
    }

    IEnumerator ShowGoText()
    {
        countdownDisplay.GetComponent<TextMeshProUGUI>().text = "GO!";
        yield return new WaitForSeconds(1f);

       
        countdownDisplay.SetActive(false);
        foreach (var tags in _playerTags)
        {
            tags.SetActive(false);
        }
    }
}
