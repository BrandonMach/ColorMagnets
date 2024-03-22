using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum PlayerIndex
    {
        Player1,
        Player2
    }

    [SerializeField] PlayerIndex _playerIndex;
    public CharacterController controller;
    [SerializeField] float _speed;
    Vector3 move;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerIndex== PlayerIndex.Player1)
        {
            move = new Vector3(Input.GetAxis("ArrowHorizontal"),0, Input.GetAxis("ArrowVertical"));

           
        }
        if (_playerIndex == PlayerIndex.Player2)
        {
            move = new Vector3(Input.GetAxis("WASDHorizontal"), 0, Input.GetAxis("WASDVertical"));

            
        }

        controller.Move(move * Time.deltaTime * _speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 0.75f);

    }
}
