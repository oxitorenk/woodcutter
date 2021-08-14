using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private int _score = 0;

    private void Start()
    {
        GameEvents.OnCutTheLog += IncreaseScore;

        scoreText.text = _score.ToString();
    }

    private void IncreaseScore()
    {
        _score++;
        scoreText.text = _score.ToString();
    }


}
