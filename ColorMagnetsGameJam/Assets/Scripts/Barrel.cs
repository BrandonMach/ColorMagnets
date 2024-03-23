using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MagnetColor
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;
    [SerializeField] public bool PickedUp;
    public PolarColor BarrelColor;

    [SerializeField] Material[] materials = new Material[4];
    MeshRenderer _meshRenderer;
    
    
    void Start()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        BarrelColor = (PolarColor)Random.Range(0, 3);
        switch (BarrelColor)
        {
            case PolarColor.Red:
                _meshRenderer.material = materials[0];
                break;
            case PolarColor.Blue:
                _meshRenderer.material = materials[1];
                break;
            case PolarColor.Yellow:
                _meshRenderer.material = materials[2];
                break;
            case PolarColor.Green:
                _meshRenderer.material = materials[3];
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.position + player.TransformDirection(offset);
        //transform.rotation = player.rotation;
    }

    public void SetOffet(Transform player, Vector3 offset)
    {
        transform.position = player.position + player.TransformDirection(offset);
        transform.rotation = player.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!PickedUp)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
