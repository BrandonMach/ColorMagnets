using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float ForceFactor = 200f;
    Transform magnetPoint;

    [SerializeField] List<Rigidbody> barrelsRB;

    private void Start()
    {
        magnetPoint = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        
        for (int i = 0; i < barrelsRB.Count; i++)
        {
            if (i == 0)
            {
                barrelsRB[i].position = magnetPoint.position + new Vector3(0.1f, 0.3f, 0.1f);
            }
            else
            {
                barrelsRB[i].position = barrelsRB[i-1].position + new Vector3(0.5f, 0, 0.5f);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Tag right Color barrel
        if (other.CompareTag("Barrel"))
        {
            barrelsRB.Add(other.GetComponent<Rigidbody>());
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
