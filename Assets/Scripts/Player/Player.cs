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
        
        // Initialize input actions here since Awake runs before OnEnable
        movement = inputActions.Player.Move;
    }

    private void Start()
    {
        playerManager = PlayerManager.instance;
        if (playerManager == null)
        {
            Debug.LogError("PlayerManager instance not found.");
        }
    }

    public Vector2 GetMoveDir()
    {
        return moveDir;
    }

    private void FixedUpdate()
    {
        if (movement != null && playerManager != null)
        {
            moveDir = movement.ReadValue<Vector2>();
            rb.linearVelocity = new Vector2(moveDir.x * playerManager.Speed, moveDir.y * playerManager.Speed);
        }
    }

    private void OnEnable()
    {
        // Add null check to prevent errors
        if (movement != null)
        {
            movement.Enable();
        }
    }

    private void OnDisable()
    {
        // Add null check to prevent errors
        if (movement != null)
        {
            movement.Disable();
        }
    }
}