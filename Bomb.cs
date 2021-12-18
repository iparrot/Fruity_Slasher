using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Game game;
    private ScoreManager _scoreManager;
    HealthManager healthManager;
    [SerializeField] ParticleSystem explosionPS;
    AudioSource slashAudio;
    bool isExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        slashAudio = GetComponent<AudioSource>();
        slashAudio.volume = PlayerPrefs.GetInt("isVolumeOn");
        game = GameObject.Find("EventSystem").GetComponent<Game>();
        _scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
        healthManager = GameObject.Find("Main Camera").GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        FruitSpawner._spawnedFruits--;
        Destroy(gameObject);
    }

    public void SlashMe()
    {
        if (!isExploded)
        {
            isExploded = true;
            switch (game.gameMode)
            {
                case GameMode.Casual:
                    _scoreManager.Score(-10);
                    break;
                case GameMode.Survival:
                    healthManager.SetLifesToZero();
                    break;
                default:
                    break;
            }
            explosionPS.Play();
            StartCoroutine(DestroyBomb());
        }
    }

    IEnumerator DestroyBomb()
    {
        //transform.GetComponent<Renderer>().material.color.a = 0.5f;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        slashAudio.volume = PlayerPrefs.GetInt("isVolumeOn");
        slashAudio.Play();
        yield return new WaitForSeconds(1f);
        transform.gameObject.SetActive(false);
    }
}
