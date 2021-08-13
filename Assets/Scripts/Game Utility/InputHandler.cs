using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private PlayerHandler _playerHandler;

    private void Start()
    {
        _playerHandler = gameObject.GetComponent<PlayerHandler>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameEvents.CutTheLogMethod();
        }
    }
}
