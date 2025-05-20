using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputManager inputActions;
    private InputAction movement;

    Rigidbody2D rb;
    private PlayerManager playerManager;

    [HideInInspector]
    Vector2 moveDir;

    private void Awake()
    {
        inputActions = new InputManager();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerManager = PlayerManager.instance;
        if (playerManager == null)
        {
            Debug.LogError("PlayerManager instance not found.");
        }
    }


    private void FixedUpdate()
    {
        moveDir = movement.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveDir.x * playerManager.Speed, moveDir.y * playerManager.Speed);
    }

    private void OnEnable()
    {
        movement = inputActions.Player.Move;
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

}
