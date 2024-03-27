using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class MainMenuRobotScript : MonoBehaviour
{
    public enum PlayerIndex
    {
        Player1,
        Player2
    }

    public PlayerIndex _playerIndex;
    public CharacterController controller;
    [SerializeField] float _speed;

    public float DefaultSpeed()
    {
        return 1000f;
    }


    Vector3 move;


    [Header("New input system")]
    //public PlayerControls playerControls;
    //private InputAction moveInput;
    //[SerializeField] bool _isQuickTurned;
    //private InputAction quickTurn;

   


    [SerializeField] Animator[] _animators;


    private void Awake()
    {
        //playerControls = new PlayerControls();

    }

    private void OnEnable()
    {
        //playerControls.Enable();
        switch (_playerIndex)
        {
            case PlayerIndex.Player1:
                //moveInput = playerControls.Player1.Move;
                //quickTurn = playerControls.Player1.QuickTurn;
                transform.position = new Vector3(-39, 0, -18);

                break;
            case PlayerIndex.Player2:
                //moveInput = playerControls.Player2.Move;
                //quickTurn = playerControls.Player2.QuickTurn;
                transform.position = new Vector3(-28, 0, -18);
                break;
            default:
                break;
        }

        //moveInput.Enable();
      

    }

    //private void OnDisable()
    //{
    //    moveInput.Disable();
    //}



    void Start()
    {
        controller = GetComponent<CharacterController>();
        _speed = DefaultSpeed();

    }






    // Update is called once per frame
    void Update()
    {

        Movement();

        RotationAndAnimation();

    }

    private void Movement()
    {
        //move = new Vector3(moveInput.ReadValue<Vector2>()[0], 0, moveInput.ReadValue<Vector2>()[1]);
        if(_playerIndex == PlayerIndex.Player1)
        {
            move = new Vector3(Input.GetAxis("WASDHorizontal"), 0, Input.GetAxis("WASDVertical"));
        }
        if (_playerIndex == PlayerIndex.Player2)
        {
            move = new Vector3(Input.GetAxis("ArrowHorizontal"), 0, Input.GetAxis("ArrowVertical"));
        }


        controller.SimpleMove(move * Time.deltaTime * _speed);
    }

    private void RotationAndAnimation()
    {
        //Rotate towards move direction
        if (move != Vector3.zero)
        {

            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime);

            foreach (var animator in _animators)
            {
                animator.SetFloat("Moving", 1);

            }
        }
        else
        {
            foreach (var animator in _animators)
            {
                
                    animator.SetFloat("Moving", -2);
                
            }
        }
    }

}
