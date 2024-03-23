using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogError("Collected Stay");
            Destroy(this.gameObject);
            other.GetComponent<Movement>().AddScore();
        }
    }

    
}
