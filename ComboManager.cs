using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    private int _comboCount;
    private int _highestComboCount;
    [SerializeField] [Range(0.1f,1f)] float _comboTimeMax;
    private float _comboTime;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] [Range(0.1f, 3f)] float _comboUITimeMax;
    [SerializeField] Text comboUI;
    [SerializeField] Animator comboAnimator;
    [SerializeField] Text highestComboUI;
    float comboUITime;

    private void Start()
    {
        _comboCount = 0;
        _highestComboCount = 0;
        _scoreManager = GetComponent<ScoreManager>();
        comboUI.text = "";
    }
    public void Combo()
    {
        if (_comboTime <= 0f)
            _comboTime = _comboTimeMax;
        _comboCount++;
    }

    void EndCombo()
    {
        // Add visual effects
        if (_comboCount > 2)
        {
            _scoreManager.Score(_comboCount);
            comboUI.text = "Combo x" + _comboCount;
            comboUITime = _comboUITimeMax;
            comboAnimator.Play("Combo_text");
            if(_highestComboCount < _comboCount)
            {
                _highestComboCount = _comboCount;
                highestComboUI.text = "Best combo: " + _highestComboCount;
            }
        }
        _comboCount = 0;
        _comboTime = 0f;
 
    }

    void FixedUpdate()
    {
        if (_comboTime > 0f)
        {
            _comboTime -= Time.fixedDeltaTime;
            if (_comboTime <= 0f) EndCombo();
        }
        if(comboUITime >0f)
        {
            comboUITime -= Time.fixedDeltaTime;
            if(comboUITime <=0f)
            {
                comboUI.text = "";
            }
        }
    }
}
