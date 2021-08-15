using UnityEngine;
using UnityEngine.EventSystems;

namespace Game_Utility
{
    public class InputHandler : MonoBehaviour
    {
        private PlayerHandler _playerHandler;

        private bool _isGameStarted;
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

            if (Input.GetButtonDown("Fire1") && !_isGameStarted && !EventSystem.current.IsPointerOverGameObject())
            {
                _isGameStarted = true;
                GameEvents.StartGameMethod();
            }

            if (Input.GetButtonDown("Fire1") && !_isGameOver && _isGameStarted)
            {
                LeftMouseAction();
            }
        }
    
        private void OnGameOver()
        {
            _isGameOver = true;
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
    }
}
