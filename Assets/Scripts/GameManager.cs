using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /// <summary>
    /// Event that should be invoked when the player collects a coin
    /// </summary>
    public GameEvents.Player.CollectedCoin OnCollectedCoin;

    /// <summary>
    /// Event fired when the score needs to be updated
    /// </summary>
    public GameEvents.Player.UpdateScore OnUpdateScore;

    /// <summary>
    /// Event fired when the player collects the jump powerup
    /// </summary>
    public GameEvents.Player.CollectedJumpPowerUp OnCollectedJumpPowerUp;

    /// <summary>
    /// Event fired when the target score for the game has been set
    /// </summary>
    public GameEvents.GameScore.SetScore OnTargetScoreSet;

    /// <summary>
    /// Event fired when the game has been completed
    /// </summary>
    public GameEvents.SystemEvents.GameComplete OnGameComplete;

    static GameManager _instance;

    /// <summary>
    /// True if the target number of coins has been set for the game
    /// </summary>
    bool targetSet = false;

    /// <summary>
    /// True if the game has been completed
    /// </summary>
    bool gameComplete = false;

    /// <summary>
    /// True if the game has been completed
    /// </summary>
    /// <value></value>
    public bool GameComplete
    {
        get
        {
            return gameComplete;
        }
    }

    /// <summary>
    /// True if the target number of coins has been set for the game
    /// </summary>
    public bool TargetSet
    {
        get
        {
            return targetSet;
        }
    }

    /// <summary>
    /// Number of coins that the player must collect
    /// </summary>
    int targetScore;

    /// <summary>
    /// Number of coins that the player must collect
    /// </summary>
    public int TargetScore
    {
        get
        {
            return targetScore;
        }
    }

    public static GameManager Manager
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
        OnTargetScoreSet.AddListener((int a) => { targetScore = a; targetSet = true; });
    }

}
