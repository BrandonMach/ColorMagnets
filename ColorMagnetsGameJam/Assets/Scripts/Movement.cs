using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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


    [Header("Score")]
    float _score;
    [SerializeField] TextMeshProUGUI _ScoreText;
    public float DefaultSpeed()
    {
        return 1200f;
    }
    Vector3 move;


    [Header("New input system")]
    public PlayerControls playerControls;
    private InputAction moveInput;
    [SerializeField] bool _isQuickTurned;
    private InputAction quickTurn;



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
                quickTurn = playerControls.Player1.QuickTurn;
                transform.position = new Vector3(20, 0, -20);

                break;
            case PlayerIndex.Player2:
                moveInput = playerControls.Player2.Move;
                quickTurn = playerControls.Player2.QuickTurn;
                transform.position = new Vector3(-20, 0, 20);
                break;
            default:
                break;
        }

        moveInput.Enable();
        quickTurn.Enable();

        quickTurn.performed += PerformeQuickTurn;
        quickTurn.canceled += CancelQuickTurn;

    }

    private void OnDisable()
    {
        moveInput.Disable();
    }



    void Start()
    {
        controller = GetComponent<CharacterController>();
        _speed = DefaultSpeed();

    }

    




    // Update is called once per frame
    void Update()
    {
        _ScoreText.text = _playerIndex.ToString() + ": " + _score;
   
        move = new Vector3(moveInput.ReadValue<Vector2>()[0], 0, moveInput.ReadValue<Vector2>()[1]);

        controller.SimpleMove(move * Time.deltaTime * _speed);


        //Turn around to better aim
       
        //if(_isQuickTurned)
        //{
        //    move = -move; 
        //    Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1080f * Time.deltaTime);

        //}

        //Rotate towards move direction
        if (move != Vector3.zero)
        {

            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime);
        }




    }


    //reduce speed with more barrels
    public void ChangeMoveSpeed(float newSpeed)
    {
        if(newSpeed > 200)
        {
            _speed = newSpeed;
        }
        
    }


    private void PerformeQuickTurn(InputAction.CallbackContext context)
    {
        _isQuickTurned = true;
        Debug.Log( _isQuickTurned+ "hjbk");
    }
    private void CancelQuickTurn(InputAction.CallbackContext context)
    {
        _isQuickTurned = false;
        Debug.Log("hjbk");
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
    
}
