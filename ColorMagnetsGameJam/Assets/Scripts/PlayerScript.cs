using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public enum PlayerIndex
    {
        Player1,
        Player2
    }

    public PlayerIndex _playerIndex;
    public CharacterController controller;
    [SerializeField] float _speed;


    [Header("Score")]
    float _score;
    public float GetPlayerScore()
    {
        return _score;
    }
    [SerializeField] TextMeshProUGUI _ScoreText;
    public float DefaultSpeed()
    {
        return 1000f;
    }

    public bool CanMove;
    Vector3 move;


    [Header("New input system")]
    //public PlayerControls playerControls;
    //private InputAction moveInput;
    [SerializeField] bool _isQuickTurned;


    [SerializeField] Animator[] _animators;


    private void Awake()
    {
       // playerControls = new PlayerControls();
        
    }

    private void OnEnable()
    {
        //playerControls.Enable();
        switch (_playerIndex)
        {
            case PlayerIndex.Player1:
               // moveInput = playerControls.Player1.Move;
               
                transform.position = new Vector3(-20, 0, 20);

                break;
            case PlayerIndex.Player2:
               // moveInput = playerControls.Player2.Move;
             
                transform.position = new Vector3(20, 0, -20);
                break;
            default:
                break;
        }

       // moveInput.Enable();

    }

    private void OnDisable()
    {
        //moveInput.Disable();
    }



    void Start()
    {
        controller = GetComponent<CharacterController>();
        _speed = DefaultSpeed();

    }

    




    // Update is called once per frame
    void Update()
    {
       _ScoreText.text=  ((int)_playerIndex+1 )+": " + _score +"/7";

        if (CanMove)
        {
            Movement();
        }

        RotationAndAnimation();
      



    }


    //reduce speed with more barrels
    public void ChangeMoveSpeed(float newSpeed)
    {
        if(newSpeed > 80)
        {
            _speed = newSpeed;
        }
        
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Collectable"))
        {
            Debug.Log("Item Collected");
            Destroy(hit.gameObject);
        }
        
    }
    public void AddScore()
    {
        _score++;
    }

    private void Movement()
    {
        if (_playerIndex == PlayerIndex.Player1)
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
                //animator.SetBool("Moving", true);
                if (animator.isActiveAndEnabled == true)
                {
                    animator.SetFloat("Moving", 1);
                }

            }
        }
        else
        {
            foreach (var animator in _animators)
            {
                if (animator.isActiveAndEnabled == true)
                {
                    animator.SetFloat("Moving", -2);
                }
            }
        }
    }




}
