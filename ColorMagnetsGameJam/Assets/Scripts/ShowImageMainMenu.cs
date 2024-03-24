using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowImageMainMenu : MonoBehaviour
{
    [SerializeField] GameObject _imageToShow;

    bool _showImage;


    // Update is called once per frame
    void Update()
    {
        _imageToShow.SetActive(_showImage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _showImage = true;

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _showImage = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _showImage = false;

        }
    }



}
