using System;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;

    public Position PlayerPosition { get; private set; }

    public enum Position
    {
        Left,
        Right
    }
    
    private void Start()
    {
        GameEvents.OnCutTheLog += PlayCutAnimation;

        PlayerPosition = Position.Right;
    }

    private void OnDestroy()
    {
        GameEvents.OnCutTheLog -= PlayCutAnimation;
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

    private void PlayCutAnimation()
    {
        // Play
    }
}
