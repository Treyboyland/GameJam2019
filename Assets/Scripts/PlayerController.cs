using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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
        timer.Start();
    }


    void Update()
    {
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        //Debug.Log("On Ground: " + onGround);
    }

    void FixedUpdate()
    {
        PlayerInputForce();
    }

    void PlayerInputForce()
    {
        Vector2 movementVector = new Vector2();

        if (Input.GetButton("Left"))
        {
            movementVector.x -= speed;
        }
        if (Input.GetButton("Right"))
        {
            movementVector.x += speed;
        }
        if (movementVector.x == 0)
        {
            Vector2 v = playerBody.velocity;
            v.x = 0;
            playerBody.velocity = v;
        }
        if (onGround && Input.GetButtonDown("Jump") && PassedJumpDelay())
        {
            RestartJumpTimer();
            movementVector.y += (hasJumpPowerUp ? jumpMultiplier : 1) * jumpPower;
        }

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
}
