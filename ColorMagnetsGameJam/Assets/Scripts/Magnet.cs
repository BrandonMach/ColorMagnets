using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Magnet : MagnetColor
{
    public float ForceFactor = 200f;
    Transform magnetPoint;

    public List<Rigidbody> MagnetListRB;
    Movement _movementScript;
  
    [SerializeField] PolarColor _currentColorOfMagnet;

    [SerializeField] GameObject[] _allTheFellas = new GameObject[4];

    int playerIndex;

    [Header("New input system")]
    //generated C# script from Player Input action
    public PlayerControls playerControls;

    private InputAction changeToRed;
    private InputAction changeToBlue;
    private InputAction changeToYellow;
    private InputAction changeToGreen;

    private void Awake()
    {
        _movementScript = GetComponentInParent<Movement>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {

        switch (_movementScript._playerIndex)
        {
            case Movement.PlayerIndex.Player1:
                changeToRed = playerControls.Player1.ChangeMagnetColorToRed;
                changeToBlue = playerControls.Player1.ChangeMagnetColorToBlue;
                changeToYellow = playerControls.Player1.ChangeMagnetColorToYellow;
                changeToGreen = playerControls.Player1.ChangeMagnetColorToGreen;
                break;
            case Movement.PlayerIndex.Player2:
                changeToRed = playerControls.Player2.ChangeMagnetColorToRed;
                changeToBlue = playerControls.Player2.ChangeMagnetColorToBlue;
                changeToYellow = playerControls.Player2.ChangeMagnetColorToYellow;
                changeToGreen = playerControls.Player2.ChangeMagnetColorToGreen;
                break;
            default:
                break;
        }
        changeToRed.Enable();
        changeToBlue.Enable(); 
        changeToYellow.Enable(); 
        changeToGreen.Enable();

        changeToRed.performed += ChangeToRed;
        changeToBlue.performed += ChangeToBlue;
        changeToYellow.performed += ChangeToYellow;
        changeToGreen.performed += ChangeToGreen;

    }

    private void OnDisable()
    {
        changeToRed.Disable();
        changeToBlue.Disable();
        changeToYellow.Disable();
        changeToGreen.Disable();
    }






    private void Start()
    {
        magnetPoint = GetComponent<Transform>();
        _currentColorOfMagnet = PolarColor.Red;
    }



    private void FixedUpdate()
    {
        for (int i = 0; i < _allTheFellas.Length; i++)
        {
            _allTheFellas[i].SetActive(i == (int)_currentColorOfMagnet);
        }

        for (int i = 0; i < MagnetListRB.Count; i++)
        {
            if (i == 0)
            {
                MagnetListRB[i].position = Vector3.MoveTowards(MagnetListRB[i].position, magnetPoint.position + new Vector3(0.1f, 0.3f, 0.1f), 30f * Time.deltaTime);
            }
            else
            {
                //MagnetListRB[i].GetComponent<Barrel>().SetOffet(MagnetListRB[i - 1].GetComponent<Transform>(), new Vector3(0.3f, 0, 0));
                MagnetListRB[i].position = Vector3.MoveTowards(MagnetListRB[i].position, MagnetListRB[i - 1].position + new Vector3(0.1f, 0, 0.1f), 30f * Time.deltaTime);
            }

        }


        for (int i = MagnetListRB.Count - 1; i >= 0; i--)
        {
            if (MagnetListRB[i].GetComponent<Barrel>().BarrelColor != _currentColorOfMagnet)
            {
                RepelBarrel(MagnetListRB[i]);
                MagnetListRB.RemoveAt(i);

            }
                
        }
    }

    private void RepelBarrel(Rigidbody barrelRB)
    {
        barrelRB.GetComponent<Barrel>().PickedUp = false;

        barrelRB.constraints = RigidbodyConstraints.FreezePositionY;
        barrelRB.AddExplosionForce(1000f, _movementScript.gameObject.transform.position , 10f);
        


        barrelRB.velocity = _movementScript.gameObject.transform.position * 30f * Time.deltaTime;
        barrelRB.useGravity = true;
        barrelRB.GetComponent<BoxCollider>().isTrigger = false;
        
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

    private void OnTriggerStay(Collider other)
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





    #region Magnet Color Switch 
    private void ChangeToRed(InputAction.CallbackContext context)
    {
        _currentColorOfMagnet = PolarColor.Red;
        
    }
    private void ChangeToBlue(InputAction.CallbackContext context)
    {
        _currentColorOfMagnet = PolarColor.Blue;
    }
    private void ChangeToYellow(InputAction.CallbackContext context)
    {
        _currentColorOfMagnet = PolarColor.Yellow;
    }
    private void ChangeToGreen(InputAction.CallbackContext context)
    {
        _currentColorOfMagnet = PolarColor.Green;
    }

    #endregion
}
