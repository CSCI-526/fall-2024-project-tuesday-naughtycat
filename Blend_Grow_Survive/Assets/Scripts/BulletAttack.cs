using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public float bullet_speed = 30f;
    public float max_distance = 20f;
    public LayerMask enemy_layer_mask;

    private Vector3 start_position;
    ObjectGenerator generator;

    void Start()
    {
        start_position = transform.position;
        generator = ObjectGenerator.ins;
    }

    void LateUpdate()
    {
        // Destory the bullet if it is out of bounds
        // Raycast to detect if the bullet is close to an enemy
        // If the ray hit an enemy, destory the enemy and remove it from the Created Enemies list
        // After that, destory the bullet itself
        Physics2D.SyncTransforms();
        transform.Translate(Vector3.up * bullet_speed * Time.deltaTime);

        if (Vector3.Distance(start_position, transform.position) >= max_distance)
        {
            Destroy(gameObject);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, bullet_speed * Time.deltaTime, enemy_layer_mask);
        if (hit.collider != null)
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(1);
                    Debug.Log("Enemy got a damage ~~~");

                    float sizeReduction = 1.0f;

                    Vector3 newScale = enemyHealth.transform.localScale - new Vector3(sizeReduction, sizeReduction, 0f);

                    float minScale = 1.0f;
                    newScale.x = Mathf.Max(newScale.x, minScale);
                    newScale.y = Mathf.Max(newScale.y, minScale);

                    enemyHealth.transform.localScale = newScale;

                    if (enemyHealth.currentHealth <= 0)
                    {
                        Destroy(hit.collider.gameObject);
                        generator.RemoveObject(hit.collider.gameObject, generator.created_enemies);
                        FindObjectOfType<PlayerEat>().GainExperience(10);

                        if (FindObjectOfType<PlayerEat>().experience >= 100)
                        {
                            FindObjectOfType<PlayerEat>().WinGame();
                        }

                        //if (generator.created_enemies.Count == 0)
                        //{
                        //    FindObjectOfType<PlayerEat>().WinGame();
                        //}
                    }
                }

                Destroy(gameObject);
            }
            else if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log("Bullet hit the wall and got destroyed.");
                Destroy(gameObject);
            }
        }
    }
}