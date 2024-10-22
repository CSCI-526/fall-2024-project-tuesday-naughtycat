using UnityEngine;

public class AmmoProjectile : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        } else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
