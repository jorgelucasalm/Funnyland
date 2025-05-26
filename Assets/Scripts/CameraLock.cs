using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public Transform player;
    public float minX, maxX, minY, maxY;

    private void FixedUpdate()
    {
        float clampedX = Mathf.Clamp(player.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(player.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        return;
        // transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

}
