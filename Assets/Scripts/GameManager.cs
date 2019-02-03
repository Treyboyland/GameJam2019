using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    /// <summary>
    /// Event fired when the player hould go to the secret room;
    /// </summary>
    public GameEvents.SystemEvents.GoToSecretRoom OnGoToSecretRoom;

    /// <summary>
    /// Event invoked to mute background audio
    /// </summary>
    public GameEvents.SystemEvents.MuteBackgroundAudio OnMuteBackgroundAudio;

    /// <summary>
    /// Event invoked to play alien theme
    /// </summary>
    public GameEvents.SystemEvents.PlayAlienTheme OnPlayAlienTheme;


    /// <summary>
    /// Event fired when player jumps
    /// </summary>
    public GameEvents.Player.PlayerJumped OnPlayerJumped;

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
    public bool IsGameComplete
    {
        get
        {
            return gameComplete;
        }
    }

    /// <summary>
    /// True if the target number of coins has been set for the game
    /// </summary>
    public bool IsTargetSet
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
        OnGameComplete.AddListener(() => gameComplete = true);
        Cursor.visible = false;
    }

    public void CompleteGame()
    {
        OnGameComplete.Invoke();
    }

    void Update()
    {
        CheckInput();
    }

    void CloseGame()
    {
        Application.Quit();
    }

    void CheckInput()
    {
        if(Input.GetButton("Reset"))
        {
            ResetGame();
        }
        else if(Input.GetButton("Quit"))
        {
            CloseGame();
        }
    }

    void ResetGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
