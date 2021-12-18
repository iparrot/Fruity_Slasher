using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    Game game;
    [SerializeField] Text timeUI;
    [SerializeField] int maxTime = 60;
    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("EventSystem").GetComponent<Game>();
        timeUI.text = "";
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        switch (game.gameMode)
        {
            case GameMode.Casual:
                if (game.isGameStarted)
                {
                    timeLeft -= Time.fixedDeltaTime;
                    timeUI.text = "Time left: " + Mathf.RoundToInt(timeLeft);
                    if (timeLeft <= 0)
                    {
                        timeLeft = 0;
                        game.SetGameOver();
                    }
                }
                break;
            default:
                break;
        }
    }
}
