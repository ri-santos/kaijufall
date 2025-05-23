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
    private PlayerManager player;

    [HideInInspector]
    Vector2 moveDir;

    private void Awake()
    {
        inputActions = new InputManager();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = PlayerManager.instance;
        if (player == null)
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
        moveDir = movement.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveDir.x * player.currentMoveSpeed, moveDir.y * player.currentMoveSpeed);
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
