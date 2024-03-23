using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticItems : MagnetColor
{
    [SerializeField] Transform player;
    //[SerializeField] Vector3 offset;
    [SerializeField] public bool PickedUp;
    public PolarColor MagnetizeColor;

    [SerializeField] GameObject[] skins = new GameObject[4];
    [SerializeField] float _weight;
    public float GetMagneticItemWeight()
    {
        return _weight;
    }


    void Start()
    {
        foreach (var skin in skins)
        {
            skin.SetActive(false);
        }
        MagnetizeColor = (PolarColor)Random.Range(0, 4);
        switch (MagnetizeColor)
        {
            case PolarColor.Red:
                skins[0].SetActive(true);
                break;
            case PolarColor.Blue:
                skins[1].SetActive(true);
                break;
            case PolarColor.Yellow:
                skins[2].SetActive(true);
                break;
            case PolarColor.Green:
                skins[3].SetActive(true);
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
