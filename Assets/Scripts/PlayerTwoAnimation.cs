using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoAnimation : MonoBehaviour
{
    private Player _player;

    void Start()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        // moving right
        if (Input.GetKey(KeyCode.Keypad6))
        {
            _player.thrusters[0].SetActive(false);
            _player.thrusters[1].SetActive(true);
            _player.thrusters[2].SetActive(false);
            _player.canMove = true;
        }
        else if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            _player.thrusters[0].SetActive(true);
            _player.thrusters[1].SetActive(false);
        }

        // moving left
        if (Input.GetKey(KeyCode.Keypad4))
        {
            _player.thrusters[0].SetActive(false);
            _player.thrusters[1].SetActive(false);
            _player.thrusters[2].SetActive(true);
            _player.canMove = true;
        }
        else if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            _player.thrusters[0].SetActive(true);
            _player.thrusters[2].SetActive(false);
        }

        // moving left & right = stop 
        if (Input.GetKey(KeyCode.Keypad6) && Input.GetKey(KeyCode.Keypad4))
        {
            _player.thrusters[0].SetActive(false);
            _player.thrusters[1].SetActive(true);
            _player.thrusters[2].SetActive(true);
            _player.canMove = false;
        }
    }
}
