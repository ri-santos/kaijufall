using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp,
        Board,
    }

    //Storing the current and previous game states
    public GameState currentState;
    public GameState previousState;

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;
    public GameObject levelUpScreen;
    public GameObject boardScreen;
    public GameObject inventory;
    public GameObject xpBar;


    //current stats display
    [Header("Current Stat Display")]
    public Text currentHealthDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentSoulsDisplay;
    public Text currentMagnetDisplay;

    [Header("Results Screen Display")]
    public Image chosenCharacterImage;
    public Text chosenCharacterName;
    public Text levelReachedDisplay;
    public Text timeSurvivedDisplay;
    public List<Image> chosenWeaponsUI = new List<Image>(6);
    public List<Image> chosenPassiveItemsUI = new List<Image>(6);


    [Header("Stopwatch")]
    public float timeLimit;
    float stopwatchTime;
    public Text stopwatchDisplay;

    public bool isGameOver = false;

    public bool choosingUpgrade;

    public GameObject playerObject;
    public GameObject enemySpawner;
    public GameObject bigKaiju;

    public System.Action onChangeToBoard;
    public System.Action onChangeToPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of GameManager found. Destroying the new one.");
            Destroy(gameObject); // Ensure only one instance exists
        }
        DisableScreens();
    }

    private void Update()
    {

        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                CheckForBoard(); // Check for board state switch
                UpdateStopwatch(); // Update the stopwatch time
                break;
            case GameState.Board:
                if (bigKaiju == null)
                {
                    isGameOver = true;
                    GameOver(); // If game is over, switch to GameOver state
                }
                CheckForPauseAndResume();
                CheckForBoard();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f; // Stop the game time
                    Debug.Log("Game Over! Displaying results.");
                    DisplayResults(); // Call to display results when game over
                }
                break;
            case GameState.LevelUp:
                if (!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0f; // Stop the game time
                    levelUpScreen.SetActive(true); // Show the level up screen UI
                    Debug.Log("Level Up! Displaying level up options.");
                }
                break;
            default:
                Debug.LogWarning("Unhandled game state: " + currentState);
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if(currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f; // Stop the game time
            pauseScreen.SetActive(true); // Show the pause screen UI
            Debug.Log("Game Paused");
        }
    }

    public void ResumeGame()
    {
        if(currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f; // Resume the game time
            pauseScreen.SetActive(false); // Hide the pause screen UI
            Debug.Log("Game Resumed");
        }
    }

    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void CheckForBoard()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentState == GameState.Gameplay)
            {
                StartBoard();
            }
            else if (currentState == GameState.Board)
            {
                ChangeToPlayer();
            }
            else
            {
                Debug.LogWarning("Cannot switch to Board state from " + currentState);
            }
        }
    }

    public void StartBoard()
    {
        onChangeToBoard?.Invoke();
        ChangeState(GameState.Board);
        DisableRoguelikeScreens();
        boardScreen.SetActive(true);
        Debug.Log("Board State Started");
        playerObject.SetActive(false); // Hide the player object when switching to board state
        //enemySpawner.SetActive(false);
        bigKaiju.SetActive(true); // Show the big kaiju when switching to board state
    }

    public void ChangeToPlayer()
    {
        onChangeToPlayer?.Invoke();
        ChangeState(GameState.Gameplay);
        RogueLikeMode();
        Time.timeScale = 1f; // Resume the game time
        Debug.Log("Switched back to Player State");
        bigKaiju.SetActive(false); // Hide the big kaiju when switching back to gameplay state
    }

    void DisableRoguelikeScreens()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
        levelUpScreen.SetActive(false);
        inventory.SetActive(false);
        xpBar.SetActive(false);
    }

    void RogueLikeMode()
    {
        DisableBoardScreens();
        inventory.SetActive(true);
        xpBar.SetActive(true);
        playerObject.SetActive(true); // Show the player object when switching back to gameplay state
        enemySpawner.SetActive(true); // Show the enemy spawner when switching back to gameplay state
        currentState = GameState.Gameplay;
        Time.timeScale = 1f; // Ensure the game time is running
        Debug.Log("RogueLike Mode Activated");
    }

    void DisableBoardScreens()
    {
        boardScreen.SetActive(false);
    }

    void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
        levelUpScreen.SetActive(false);
        boardScreen.SetActive(false);
    }

    public void GameOver()
    {
        onChangeToPlayer?.Invoke();
        DisableBoardScreens();
        timeSurvivedDisplay.text = stopwatchDisplay.text; // Assign the stopwatch time to the results display
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
         resultsScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }

    public void AssignChosenWeaponsAndPassiveItemsUI(List<Image> chosenWeaponsData, List<Image> chosenPassiveItemsData)
    {
        if(chosenWeaponsData.Count != chosenWeaponsUI.Count || chosenPassiveItemsData.Count != chosenPassiveItemsUI.Count)
        {
            Debug.LogError("Mismatch in the number of weapons or passive items assigned to UI elements.");
            return;
        }

        for (int i = 0; i < chosenWeaponsUI.Count; i++)
        {
            if (chosenWeaponsData[i].sprite)
            {
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponsData[i].sprite;
            }
            else
            {
                chosenWeaponsUI[i].enabled = false;
            }
        }

        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            if (chosenPassiveItemsData[i].sprite)
            {
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].sprite;
            }
            else
            {
                chosenPassiveItemsUI[i].enabled = false;
            }
        }
    }

    void UpdateStopwatch()
    {
        stopwatchTime += Time.deltaTime;

        UpdateStopwatchDisplay(); // Update the stopwatch display

        if (stopwatchTime >= timeLimit)
        {
            GameOver();
        }
    }

    void UpdateStopwatchDisplay()
    {
        int minutes = Mathf.FloorToInt(stopwatchTime / 60);
        int seconds = Mathf.FloorToInt(stopwatchTime % 60);
        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp()
    {
        choosingUpgrade = false;
        Time.timeScale = 1f; // Resume the game time
        levelUpScreen.SetActive(false); // Hide the level up screen UI
        ChangeState(GameState.Gameplay); // Change back to gameplay state
    }


}
