using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState { CUSTOMIZE, PLAY, WIN_GAME, LOSE_GAME }

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] TextMeshProUGUI remainingAnimalsText;
    [SerializeField] GameObject winMenu, loseMenu;

    GameState gameState = GameState.CUSTOMIZE;
    int remaingingAnimals = 0;


    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) ChangeGameState(GameState.WIN_GAME);
        if (Input.GetKeyDown(KeyCode.F2)) ChangeGameState(GameState.LOSE_GAME);
    }

    public bool GameStateEquals(GameState _state)
    {
        return gameState == _state;
    }

    public void ChangeGameState(GameState _state)
    {
        gameState = _state;

        switch (gameState)
        {
            case GameState.WIN_GAME:
                winMenu.SetActive(true);
                AudioManager.Instance.Play_OST("Win_OST");
                Time.timeScale = 0.00001f;
                break;

            case GameState.LOSE_GAME:
                loseMenu.SetActive(true);
                AudioManager.Instance.Play_OST("Lose_OST");
                Time.timeScale = 0.00001f;
                break;
        }
    }


    public void AddRemainingAnimal()
    {
        remaingingAnimals++;
        remainingAnimalsText.text = "Animals Left: " + remaingingAnimals.ToString();
    }

    public void KillRemainingAnimal()
    {
        remaingingAnimals--;
        remainingAnimalsText.text = remaingingAnimals.ToString();
        if (remaingingAnimals <= 0) ChangeGameState(GameState.WIN_GAME);
    }

}
