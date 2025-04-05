using UnityEngine;

public class PlayerActionLook
{
    private Vector2 lookInput;
    private Transform targetTransform;
    private Camera mainCamera;
    private float lastRotationAngle;

    
    public PlayerActionLook(Transform target, Camera camera)
    {
        targetTransform = target;
        mainCamera = camera;
    }

    public void SetLookInput(Vector2 input)
    {
        lookInput = input;
    }

    public void Execute()
    {
        if (targetTransform == null || mainCamera == null) return;

        if (lookInput.sqrMagnitude > 0.01f)
        {
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(lookInput.x, lookInput.y, 0));
            Vector3 direction = (mouseWorldPosition - targetTransform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            lastRotationAngle = angle;
            targetTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public float GetPlayerLookRotationAngle()
    {
        return lastRotationAngle;
    }
}
