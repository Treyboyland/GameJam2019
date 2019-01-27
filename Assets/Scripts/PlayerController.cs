using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    Animator animator;

    /// <summary>
    /// Current player score
    /// </summary>
    [SerializeField]
    int score;

    [SerializeField]
    float speed;

    [SerializeField]
    float maxSpeed;

    float updateY;

    [SerializeField]
    float jumpPower;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// Force added on the jump
    /// </summary>
    /// <value></value>
    public float JumpPower
    {
        get
        {
            return jumpPower;
        }
        set
        {
            jumpPower = value;
        }
    }

    /// <summary>
    /// Amount of time that must elapse before the player can jump again.
    /// </summary>
    [SerializeField]
    float jumpDelay;

    System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

    /// <summary>
    /// Multipler for the jump power up
    /// </summary>
    [SerializeField]
    float jumpMultiplier;

    /// <summary>
    /// True if the player has the jump powerup
    /// </summary>
    [SerializeField]
    bool hasJumpPowerUp;

    public float secondsToJump;

    [SerializeField]
    Rigidbody2D playerBody;

    [SerializeField]
    Transform groundCheck;

    /// <summary>
    /// Current player score
    /// </summary>
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    static PlayerController _instance;

    bool jumping = false;

    bool onGround = false;

    /// <summary>
    /// True if the player is Grounded
    /// </summary>
    /// <value></value>
    public bool IsGrounded
    {
        get
        {
            return onGround;
        }
    }

    public bool IsMoving
    {
        get
        {
            return Mathf.Abs(playerBody.velocity.x) > 0 && Mathf.Abs(playerBody.velocity.y) > 0;
        }
    }

    /// <summary>
    /// Reference to the current player
    /// </summary>
    /// <value></value>
    public static PlayerController Player
    {
        get
        {
            return _instance;
        }
    }

    bool acceptingPlayerInput = true;

    public bool AcceptingPlayerInput
    {
        get
        {
            return acceptingPlayerInput;
        }
        set
        {
            acceptingPlayerInput = value;
        }
    }

    void Awake()
    {
        if (_instance != null && this != _instance)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.OnCollectedCoin.AddListener((int add) =>
        {
            score += add;
            //Debug.LogWarning("Score: " + score);
            GameManager.Manager.OnUpdateScore.Invoke(score);
        });

        GameManager.Manager.OnCollectedJumpPowerUp.AddListener(() => hasJumpPowerUp = true);

        GameManager.Manager.OnGoToSecretRoom.AddListener(StopPlayer);
        timer.Start();
    }

    /// <summary>
    /// Stops the player in their tracks
    /// </summary>
    void StopPlayer()
    {
        playerBody.isKinematic = true;
        acceptingPlayerInput = false;
        playerBody.velocity = new Vector2();
    }


    void Update()
    {
        if (acceptingPlayerInput)
        {
            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            UpdateAirAnimBool(!onGround);
        }
        //Debug.Log("On Ground: " + onGround);
    }

    void UpdateAirAnimBool(bool val)
    {
        animator.SetBool("InAir", val);
    }

    void UpdateRightAnimBool(bool val)
    {
        animator.SetBool("MovingRight", val);
    }

    void UpdateLeftAnimBool(bool val)
    {
        animator.SetBool("MovingLeft", val);
    }

    void FixedUpdate()
    {
        if (acceptingPlayerInput)
        {
            PlayerInputForce();
        }
    }

    void PlayerInputForce()
    {
        Vector2 movementVector = new Vector2();
        bool leftBool = false;
        bool rightBool = false;

        if (Input.GetButton("Left"))
        {
            movementVector.x -= speed;
            leftBool = true;
        }
        if (Input.GetButton("Right"))
        {
            movementVector.x += speed;
            rightBool = true;
        }
        if (movementVector.x == 0)
        {
            Vector2 v = playerBody.velocity;
            v.x = 0;
            playerBody.velocity = v;
        }
        if (onGround && Input.GetButtonDown("Jump") && PassedJumpDelay())
        {
            GameManager.Manager.OnPlayerJumped.Invoke();
            RestartJumpTimer();
            movementVector.y += (hasJumpPowerUp ? jumpMultiplier : 1) * jumpPower;
        }

        if (rightBool && leftBool)
        {
            leftBool = false;
            rightBool = false;
        }

        UpdateLeftAnimBool(leftBool);
        UpdateRightAnimBool(rightBool);

        MovePlayerForce(movementVector);
    }

    void RestartJumpTimer()
    {
        timer.Stop();
        timer.Reset();
        timer.Start();
    }

    bool PassedJumpDelay()
    {
        return timer.Elapsed.TotalSeconds >= jumpDelay;
    }

    void Jump()
    {
        playerBody.AddRelativeForce(Vector2.up * jumpPower);
    }

    void MovePlayerForce(Vector2 vector)
    {
        playerBody.AddForce(speed * new Vector2(vector.x, 0), ForceMode2D.Impulse);

        if (vector.y != 0)
        {
            Debug.LogWarning("Jump Force: " + vector.y);
            playerBody.AddForce(new Vector2(0, speed * vector.y), ForceMode2D.Impulse);
        }

        if (Mathf.Abs(playerBody.velocity.x) > maxSpeed)
        {
            playerBody.velocity = new Vector2((playerBody.velocity.x < 0 ? -1 : 1) * maxSpeed, playerBody.velocity.y);
        }
    }

    /// <summary>
    /// Dev method to give the player the powerup
    /// </summary>
    public void PowerUp()
    {
        hasJumpPowerUp = true;
    }

    public void Rotate()
    {
        animator.SetTrigger("Beam Up");
    }

    /// <summary>
    /// Sets Mask for the alien thing
    /// </summary>
    public void SetSpriteMask()
    {
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
    }
}
