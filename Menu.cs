using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    bool isVolumeOn = true;

    [SerializeField] int casual_scene_index;
    [SerializeField] int survival_scene_index;
    [SerializeField] int rai_scene_index;
    [SerializeField] int ipa_scene_index;

    [SerializeField] Button replay_button;
    [SerializeField] Button casual_button;
    [SerializeField] Button survival_button;
    [SerializeField] Button volume_button;
    [SerializeField] Button about_click;
    [SerializeField] Button exit_button;
    [SerializeField] Button about_close_button;

    [SerializeField] Sprite volumeOn_image, volumeOff_image;
    

    [SerializeField] GameObject dev_panel, about_panel, exit_panel;
    [SerializeField] Button rai_button;
    [SerializeField] Button ipa_button;

    [SerializeField] Animator sceneFadeAnimation;
    [SerializeField] BackgroundMusic music;
    // Start is called before the first frame update
    void Start()
    {
        isVolumeOn = (PlayerPrefs.GetInt("isVolumeOn", 0) ==1);
        //if(previousSceneIndex==null) replay_button.SetActive(false);
        replay_button.onClick.AddListener(delegate { ReplayClick(); });
        casual_button.onClick.AddListener(delegate { CasualClick(); });
        survival_button.onClick.AddListener(delegate { SurvivalClick(); });
        volume_button.onClick.AddListener(delegate { VolumeClick(); });
        about_click.onClick.AddListener(delegate { AboutClick(); });
        exit_button.onClick.AddListener(delegate { ExitClick(); });
        about_close_button.onClick.AddListener(delegate { AboutCloseClick(); });
        UpdateVolumeButton();
#if UNITY_EDITOR
        rai_button.onClick.AddListener(delegate { RaiClick(); });
        ipa_button.onClick.AddListener(delegate { IpaClick(); });
#else
      dev_panel.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitClick();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(about_panel.activeSelf)
            {
                about_panel.SetActive(false);
            }
        }
    }

    void ReplayClick()
    {
        //load last scene
    }

    void CasualClick()
    {
        if (casual_scene_index != 0)
        {
            if (sceneFadeAnimation.GetCurrentAnimatorStateInfo(0).IsName("screen_fade_out_black"))
            {
                StartCoroutine(LoadScene(casual_scene_index));
            }
        }
    }

    void SurvivalClick()
    {
        if (survival_scene_index != 0)
        {
            if (sceneFadeAnimation.GetCurrentAnimatorStateInfo(0).IsName("screen_fade_out_black"))
            {
                StartCoroutine(LoadScene(survival_scene_index));
            }
        }
    }

    void VolumeClick()
    {
        isVolumeOn = !isVolumeOn;
        PlayerPrefs.SetInt("isVolumeOn", isVolumeOn ? 1 : 0);
        UpdateVolumeButton();
    }

    void AboutClick()
    {
        about_panel.SetActive(true);
    }

    void AboutCloseClick()
    {
        about_panel.SetActive(false);
    }

    void ExitClick()
    {
#if UNITY_EDITOR
        {
            if (about_panel.activeSelf)
            {
                about_panel.SetActive(false);
            }
            else
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
#else
        {
            if(about_panel.activeSelf)
            {
               about_panel.SetActive(false);
            }
            else
            {
              Application.Quit();
            }
        }
#endif
    }

    void RaiClick()
    {
        if (rai_scene_index != 0)
        {
            if (sceneFadeAnimation.GetCurrentAnimatorStateInfo(0).IsName("screen_fade_out_black"))
            {
                StartCoroutine(LoadScene(rai_scene_index));
            }
        }
    }

    void IpaClick()
    {
        if (ipa_scene_index != 0)
        {
            if (sceneFadeAnimation.GetCurrentAnimatorStateInfo(0).IsName("screen_fade_out_black"))
            {
                StartCoroutine(LoadScene(ipa_scene_index));
            }
        }
    }

    void UpdateVolumeButton()
    {
        if (isVolumeOn)
        {
            volume_button.image.sprite = volumeOn_image;
            music.SetMusicVolume(PlayerPrefs.GetInt("isVolumeOn"));
        }
        else
        {
            volume_button.image.sprite = volumeOff_image;
            music.SetMusicVolume(PlayerPrefs.GetInt("isVolumeOn"));
        }
    }

    IEnumerator LoadScene(int _buildIndex)
    {
        sceneFadeAnimation.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        music.SaveMusicTime();
        SceneManager.LoadSceneAsync(_buildIndex);

    }
}
