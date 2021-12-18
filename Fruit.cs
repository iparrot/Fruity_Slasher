using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    Game game;
    [SerializeField] GameObject part2;
    [SerializeField] ParticleSystem slashPS;
    [SerializeField] private bool isFull = true;
    AudioSource slashAudio;
    private ScoreManager _scoreManager;
    private ComboManager _comboManager;
    private HealthManager _healthManager;
    private Vector3 _v3RotAxis;

    private void Start()
    {
        game = GameObject.Find("EventSystem").GetComponent<Game>();
        slashAudio = GetComponent<AudioSource>();
        slashAudio.volume = PlayerPrefs.GetInt("isVolumeOn");
        _v3RotAxis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0);
        if (game.gameMode != GameMode.Menu)
        {
            _scoreManager = GameObject.Find("Main Camera").GetComponent<ScoreManager>();
            _comboManager = GameObject.Find("Main Camera").GetComponent<ComboManager>();
            _healthManager = GameObject.Find("Main Camera").GetComponent<HealthManager>();
        }
    }

    private void OnBecameInvisible()
    {
        FruitSpawner._spawnedFruits--;
        if (isFull && game.gameMode != GameMode.Menu)
        {
            _healthManager.LoseLife();
        }
        Destroy(gameObject);
    }

    public void SlashMe()
    {
        if (isFull)
        {
            part2.GetComponent<Rigidbody>().isKinematic = false;
            isFull = false;
            _scoreManager.Score(1);
            _comboManager.Combo();
            if (slashPS != null) slashPS.Play();
            if (slashAudio != null)
            {
                slashAudio.volume = PlayerPrefs.GetInt("isVolumeOn");
                slashAudio.Play();
            }
        }
    }

    void FixedUpdate()
    {
        transform.RotateAround(_v3RotAxis, 1 * Time.fixedDeltaTime);
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(10, 10, 100, 30));
    //    if (GUILayout.Button("Slash_fruit")) SlashMe();
    //    GUILayout.EndArea();
    //}
}
