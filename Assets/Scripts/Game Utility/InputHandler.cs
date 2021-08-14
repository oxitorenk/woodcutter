using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private PlayerHandler _playerHandler;

    private bool _isGameOver;

    private void Start()
    {
        GameEvents.OnGameOver += OnGameOver;
        
        _playerHandler = gameObject.GetComponent<PlayerHandler>();
        _isGameOver = false;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameOver -= OnGameOver;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !_isGameOver)
        {
            LeftMouseAction();
        }
    }

    private void LeftMouseAction()
    {
        GameEvents.CutTheLogMethod();
            
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < 0)
        {
            _playerHandler.MoveToLeft();
        }
        else
        {
            _playerHandler.MoveToRight();
        }
    }

    private void OnGameOver()
    {
        _isGameOver = true;
    }
}
