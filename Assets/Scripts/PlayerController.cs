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
    Rigidbody2D playerBody;

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
            GameManager.Manager.OnUpdateScore.Invoke(score);
        });


    }

    void FixedUpdate()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        Vector2 movementVector = new Vector2();

        if(Input.GetButton("Left"))
        {
            movementVector.x -= speed;
        }
        if(Input.GetButton("Right"))
        {
            movementVector.x += speed;
        }


        MovePlayer(movementVector);
    }

    void MovePlayer(Vector2 vector)
    {
        Vector2 pos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        playerBody.MovePosition(pos + vector * Time.fixedDeltaTime);
    }
}
