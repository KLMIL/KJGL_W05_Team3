using UnityEngine;

public class PlayerActionMove
{
    private float moveSpeed;
    private Vector2 moveInput;
    private Transform targetTransform;

    public PlayerActionMove(Transform target, float speed)
    {
        targetTransform = target;
        moveSpeed = speed;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void Execute(float deltaTime)
    {
        if (targetTransform == null) return;

        if (moveInput.x < 0) targetTransform.rotation = Quaternion.Euler(new(0, 180, 0));
        else if (moveInput.x > 0) targetTransform.rotation = Quaternion.Euler(new(0, 0, 0));
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * deltaTime;
        targetTransform.position += movement;
        SnowDestroyManager.Instance.DestroyTilesInRadius(targetTransform.position, 0.3f);
    }
}
