using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /// <summary>
    /// Event that should be invoked when the player collects a coin
    /// </summary>
    public GameEvents.Player.CollectedCoin OnCollectedCoin;

    public GameEvents.Player.UpdateScore OnUpdateScore;

    static GameManager _instance;

    public static GameManager Manager
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if(_instance != null && this != _instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
