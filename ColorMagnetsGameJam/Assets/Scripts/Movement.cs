using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public enum PlayerIndex
    {
        Player1,
        Player2
    }

    public PlayerIndex _playerIndex;
    public CharacterController controller;
    [SerializeField] float _speed;
    Vector3 move;


    [Header("New input system")]
    public PlayerControls playerControls;
    private InputAction moveInput;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        //playerControls.Enable();
        switch (_playerIndex)
        {
            case PlayerIndex.Player1:
                moveInput = playerControls.Player1.Move;

                break;
            case PlayerIndex.Player2:
                moveInput = playerControls.Player2.Move;
                break;
            default:
                break;
        }
        moveInput.Enable();

    }

    private void OnDisable()
    {
        moveInput.Disable();
    }



    void Start()
    {
        controller = GetComponent<CharacterController>();
        _speed = 10f;
    }

    




    // Update is called once per frame
    void Update()
    {

        GetComponent<Rigidbody>().AddForce(Vector3.down);
   
        move = new Vector3(moveInput.ReadValue<Vector2>()[0], 0, moveInput.ReadValue<Vector2>()[1]);


        controller.Move(move * Time.deltaTime * _speed);

        //Rotate towards move direction
        if(move != Vector3.zero)
        {
            //transform.forward = move;
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime);
        }

    }


    //reduce speed with more barrels
    public void ReduceSpeed(float newSpeed)
    {
        if(newSpeed > 3)
        {
            _speed = newSpeed;
        }
        
    }
}
