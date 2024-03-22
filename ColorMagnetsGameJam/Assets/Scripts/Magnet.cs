using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MagnetColor
{
    public float ForceFactor = 200f;
    Transform magnetPoint;

    public List<Rigidbody> MagnetListRB;
    Movement _movementScript;
  
    [SerializeField] PolarColor _currentColorOfMagnet;

    [SerializeField] GameObject[] _allTheFellas = new GameObject[4];

    private void Start()
    {
        magnetPoint = GetComponent<Transform>();
        _movementScript = GetComponentInParent<Movement>();
        _currentColorOfMagnet = PolarColor.Red;
    }

    private void FixedUpdate()
    {
      
        


        if (_movementScript._playerIndex == Movement.PlayerIndex.Player1)
        {
            Player1Controlls();
        }
        else
        {
            Player2Controlls();
        }


        for (int i = 0; i < MagnetListRB.Count; i++)
        {
            if (i == 0)
            {
                //barrelsRB[i].position = magnetPoint.position + new Vector3(0.1f, 0.3f, 0.1f);

                MagnetListRB[i].GetComponent<Barrel>().SetOffet(magnetPoint, new Vector3(0, 0f, 0f));
            }
            else
            {
                MagnetListRB[i].GetComponent<Barrel>().SetOffet(MagnetListRB[i-1].GetComponent<Transform>(), new Vector3(0.3f,0,0));
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Tag right Color barrel
        var checkBarrel = other.GetComponent<Barrel>();

        //Only pick up barrels that are not already picked up and have the same color 
        if (other.CompareTag("Barrel") && !checkBarrel.PickedUp && checkBarrel.BarrelColor == _currentColorOfMagnet)
        {
            MagnetListRB.Add(other.GetComponent<Rigidbody>());
            checkBarrel.PickedUp = true;
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<BoxCollider>().isTrigger = true;

            _movementScript.ReduceSpeed((10 - (0.7f * MagnetListRB.Count)));
        }
    }

    void Player1Controlls()
    {
        //Fixa för payer 2 och optimizera
        if (Input.GetKeyDown(KeyCode.N))
        {
            _currentColorOfMagnet = PolarColor.Red;
            foreach (var item in _allTheFellas)
            {
                item.SetActive(false);
            }
            for (int i = 0; i < _allTheFellas.Length; i++)
            {
                _allTheFellas[0].SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            _currentColorOfMagnet = PolarColor.Blue;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            _currentColorOfMagnet = PolarColor.Yellow;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _currentColorOfMagnet = PolarColor.Green;
        }
    }

    void Player2Controlls()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            _currentColorOfMagnet = PolarColor.Red;
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            _currentColorOfMagnet = PolarColor.Blue;
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            _currentColorOfMagnet = PolarColor.Yellow;
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            _currentColorOfMagnet = PolarColor.Green;
        }
    }
}
