using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    Game game;
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] [Range(0.1f, 5f)] private float _maxDelay;
    [SerializeField] [Range(0.0f,1f)] private float _spawnChance;
    [SerializeField] private int _maxFruits;
    [SerializeField] [Range(0.1f, 10f)] float _maxForceX;
    [SerializeField] [Range(0.1f, 20f)] float _minForceY;
    [SerializeField] [Range(5f, 20f)] float _maxForceY;
    private float _lastSpawn;
    public static int _spawnedFruits;
    void Start()
    {
        game = GameObject.Find("EventSystem").GetComponent<Game>();
        _spawnedFruits = 0;
        _lastSpawn = Time.time;
    }

    private void FixedUpdate()
    {
        if (game.isGameStarted)
        {
            if ((Random.value <= _spawnChance || Time.time > _lastSpawn + _maxDelay) && _spawnedFruits < _maxFruits)
            {
                var go = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], transform.position + new Vector3(Random.Range(-10f, 10f), 0f, 0f), Quaternion.identity, transform);
                var go_rb = go.GetComponent<Rigidbody>();
                _spawnedFruits++;
                _lastSpawn = Time.time;
                go_rb.AddForce(new Vector3(Random.Range(-_maxForceX, _maxForceX), Random.Range(_minForceY, _maxForceY), 0f) * 100f);
            }
        }
    }
}
