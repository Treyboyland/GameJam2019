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

    public float jumpHeight;

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
            Debug.LogWarning("Score: " + score);
            GameManager.Manager.OnUpdateScore.Invoke(score);
        });


    }


    void Update()
    {
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        Debug.Log("On Ground: " + onGround);
    }

    void FixedUpdate()
    {
        PlayerInputForce();
    }

    void PlayerInput()
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

        

        if (Input.GetButtonDown("Up"))
        {
            StartCoroutine(PerformJump());
            //Jump();
        }

        if (jumping)
        {
            movementVector.y = updateY;
        }

        MovePlayer(movementVector);





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
        if(movementVector.x == 0)
        {
            Vector2 v = playerBody.velocity;
            v.x = 0;
            playerBody.velocity = v;
        }
        if(onGround && Input.GetButtonDown("Jump"))
        {
            movementVector.y += jumpPower;
        }

        MovePlayerForce(movementVector);
    }

    IEnumerator PerformJump()
    {
        jumping = true;
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        timer.Start();

        float startY = gameObject.transform.position.y;


        while (timer.Elapsed.TotalSeconds < secondsToJump)
        {
            //TODO: Jump broke
            updateY = jumpHeight; //* Mathf.Sin(Mathf.PI / 2 + Mathf.PI * (float)timer.Elapsed.TotalSeconds / secondsToJump);
            Debug.LogWarning(Mathf.Sin(Mathf.PI * (float)timer.Elapsed.TotalSeconds / secondsToJump));
            //Vector2 pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + Mathf.Lerp(0, jumpHeight, (float)timer.Elapsed.TotalSeconds / secondsToJump));
            //Debug.LogWarning("Jumping: " + pos);
            //playerBody.MovePosition(pos);
            yield return null;
        }

        Debug.LogWarning("Jump FInihed");

        jumping = false;
    }

    void Jump()
    {
        playerBody.AddRelativeForce(Vector2.up * jumpPower);
    }

    void MovePlayer(Vector2 vector)
    {
        Vector2 pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Debug.Log("NOT Jumping: " + pos);
        playerBody.MovePosition(pos + vector * Time.fixedDeltaTime);
    }

    void MovePlayerForce(Vector2 vector)
    {
        playerBody.AddForce(speed * vector, ForceMode2D.Impulse);

        if(Mathf.Abs(playerBody.velocity.x) > maxSpeed)
        {
            playerBody.velocity = new Vector2((playerBody.velocity.x < 0 ? -1 : 1) * maxSpeed, playerBody.velocity.y);
        }
    }
}
