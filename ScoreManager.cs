using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    Game game;
    [SerializeField] private Text scoreUI;
    [SerializeField] Text bestScoreUI;
    [SerializeField] Text ggScoreUI;

    private int _score;
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("EventSystem").GetComponent<Game>();
        _score = 0;
        if(PlayerPrefs.GetInt("bestScore" + game.gameMode.ToString(),0) ==0)
        {
            bestScoreUI.text = "";
        }
        else
        {
            bestScoreUI.text = "Best: " + PlayerPrefs.GetInt("bestScore" + game.gameMode.ToString());
        }
    }

    public void Score(int points)
    {
        _score += points;
        if (_score < 0) _score = 0;
        scoreUI.text = "Score: " + _score;
        ggScoreUI.text = "Score: " + _score;
        if(_score > PlayerPrefs.GetInt("bestScore" + game.gameMode.ToString(), 0))
        {
            PlayerPrefs.SetInt("bestScore" + game.gameMode.ToString(), _score);
        }
    }
}
