using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameMode { Casual, Survival, Experimental, Menu }
public class Game : MonoBehaviour
{
    [SerializeField] public GameMode gameMode;
    public bool isGameStarted;
    public bool isGameOver;

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject game_over_panel;
    [SerializeField] Button play_again_button;
    [SerializeField] Button leave_button;

    [SerializeField] GameObject pause_panel;
    [SerializeField] Button pause_exit_button;
    [SerializeField] Button pause_restart_button;
    [SerializeField] Button pause_continue_button;

    [SerializeField] Animator sceneFadeAnimation;
    [SerializeField] Animator readyAnimation;
    [SerializeField] Animator goAnimation;

    AudioSource gameoverAudio;
    [SerializeField] BackgroundMusic music;

    // Start is called before the first frame update
    void Start()
    {
        if (gameMode == GameMode.Menu)
        {
            isGameStarted = true;
        }
        else
        {
            gameoverAudio = GetComponent<AudioSource>();
            gameoverAudio.volume = PlayerPrefs.GetInt("isVolumeOn");
            isGameOver = false;
            play_again_button.onClick.AddListener(delegate { PlayAgainClick(); });
            leave_button.onClick.AddListener(delegate { LeaveClick(); });

            pause_exit_button.onClick.AddListener(delegate { PauseExitClick(); });
            pause_restart_button.onClick.AddListener(delegate { PauseRestartClick(); });
            pause_continue_button.onClick.AddListener(delegate { PauseContinueClick(); });

            StartCoroutine(StartGame());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMode != GameMode.Menu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGameStarted && !isGameOver)
                {
                    if (pause_panel.activeSelf)
                    {
                        PauseContinueClick();
                    }
                    else
                    {
                        pause_panel.SetActive(true);
                        Time.timeScale = 0;
                    }
                }
            }
        }
    }

    public void SetGameOver()
    {
        isGameStarted = false;
        isGameOver = true;
        gameoverAudio.volume = PlayerPrefs.GetInt("isVolumeOn");
        gameoverAudio.Play();
        game_over_panel.SetActive(true); // if no more fruits in air?

    }

    private void PlayAgainClick()
    {
        if (sceneFadeAnimation.GetCurrentAnimatorStateInfo(0).IsName("screen_fade_out_black"))
        {
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
    }

    private void LeaveClick()
    {
        if (sceneFadeAnimation.GetCurrentAnimatorStateInfo(0).IsName("screen_fade_out_black"))
        {
            StartCoroutine(LoadScene(0));
        }
    }

    private void PauseExitClick()
    {
        if (sceneFadeAnimation.GetCurrentAnimatorStateInfo(0).IsName("screen_fade_out_black"))
        {
            PauseContinueClick();
            StartCoroutine(LoadScene(0));
        }
    }

    private void PauseRestartClick()
    {
        if (sceneFadeAnimation.GetCurrentAnimatorStateInfo(0).IsName("screen_fade_out_black"))
        {
            PauseContinueClick();
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));        // we need to test if score values reseted
        }
    }

    private void PauseContinueClick()
    {
        pause_panel.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator LoadScene(int _buildIndex)
    {
        sceneFadeAnimation.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        music.SaveMusicTime();
        SceneManager.LoadSceneAsync(_buildIndex);
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        readyAnimation.Play("Ready_text");
        yield return new WaitForSeconds(1);
        goAnimation.Play("Ready_text");
        yield return new WaitForSeconds(1);
        isGameStarted = true;
    }
}
