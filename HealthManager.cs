using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    Game game;
    [SerializeField] Text lifeUI;
    [SerializeField] private int _lives;
    [SerializeField] private int _maxLives;

    private void Start()
    {
        game = GameObject.Find("EventSystem").GetComponent<Game>();
        lifeUI.text = "";

        switch (game.gameMode)
        {
            case GameMode.Survival:
                _lives = _maxLives;
                lifeUI.text = "Lives: " + _lives;
                break;
            default:
                break;
        }
    }

    public void LoseLife()
    {
        if (game.isGameStarted)
        {
            switch (game.gameMode)
            {
                case GameMode.Survival:
                    _lives--;
                    lifeUI.text = "Lives: " + _lives;
                    if (_lives <= 0)
                    {
                        game.SetGameOver();
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void AddLife()
    {
        if (_lives < _maxLives) _lives++;
    }

    public void SetLifesToZero()
    {
        _lives = 0;
        lifeUI.text = "Lives: " + _lives;
        game.SetGameOver();
    }
}
