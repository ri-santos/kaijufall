using UnityEngine;

public class Player : MonoBehaviour
{
    //private InputManager inputActions;
    //private InputAction movement;

    Rigidbody2D rb;
    private PlayerManager player;

    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector] 
    public float lastVerticalVector;

    [HideInInspector]
    Vector2 moveDir;

    Vector2 dodgeVelocity;
    float dodgeDuration;

    private void Awake()
    {
        //inputActions = new InputManager();
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

    private void Update()
    {
        InputManagement();
        if (dodgeDuration > 0)
        {
            transform.position += (Vector3)dodgeVelocity * Time.deltaTime;
            dodgeDuration -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if(moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
        }

        if(moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dodgeDuration = 0.2f;
            player.Dodge(dodgeDuration);
            dodgeVelocity = (new Vector2(moveDir.x * player.CurrentMoveSpeed, moveDir.y * player.CurrentMoveSpeed)).normalized * 7f;
        }
    }

    void Move()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        rb.linearVelocity = new Vector2(moveDir.x * player.CurrentMoveSpeed, moveDir.y * player.CurrentMoveSpeed);
    }



}
