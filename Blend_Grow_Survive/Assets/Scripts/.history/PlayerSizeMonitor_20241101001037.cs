using UnityEngine;

public class PlayerSizeMonitor : MonoBehaviour
{
    public float minScale = 0.5f; // Minimum scale before player is destroyed

    void Update()
    {
        // Check if the player's scale falls below the minimum threshold
        if (transform.localScale.x < minScale || transform.localScale.y < minScale)
        {
            Destroy(gameObject);
        }
    }
}

