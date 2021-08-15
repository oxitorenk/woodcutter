using System.Collections;
using Game_Utility;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite cutSprite;

    private SpriteRenderer _playerSpriteRenderer;

    public Position PlayerPosition { get; private set; }

    public enum Position
    {
        Left,
        Right
    }
    
    private void Start()
    {
        GameEvents.OnCutTheLog += OnCutTheLog;

        PlayerPosition = Position.Right;
        _playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        GameEvents.OnCutTheLog -= OnCutTheLog;
    }
    
    private void OnCutTheLog()
    {
        StartCoroutine(SetCutSprite());
    }

    private IEnumerator SetCutSprite()
    {
        _playerSpriteRenderer.sprite = cutSprite;
        yield return new WaitForSeconds(0.1f);
        
        _playerSpriteRenderer.sprite = normalSprite;
    }

    public void MoveToRight()
    {
        if (PlayerPosition == Position.Right) return;
        
        PlayerPosition = Position.Right;
        ChangeRotation(0);
        ChangePosition();
    }

    public void MoveToLeft()
    {
        if (PlayerPosition == Position.Left) return;
        
        PlayerPosition = Position.Left;
        ChangeRotation(180);
        ChangePosition();
    }

    private void ChangePosition()
    {
        var position = playerObject.transform.position;
        
        playerObject.transform.position = new Vector3(position.x * -1, position.y);
    }

    private void ChangeRotation(float rotationY)
    {
        var rotation = playerObject.transform.rotation;

        playerObject.transform.rotation = new Quaternion(rotation.x, rotationY, rotation.z, rotation.w);
    }
}
