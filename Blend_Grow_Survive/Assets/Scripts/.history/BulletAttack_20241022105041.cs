//using UnityEngine;

//public class BulletAttack : MonoBehaviour
//{
//    public float bullet_speed = 30f;
//    public float max_distance = 20f;
//    public LayerMask enemy_layer_mask;

//    private Vector3 start_position;
//    ObjectGenerator generator;

//    void Start()
//    {
//        start_position = transform.position;
//        generator = ObjectGenerator.ins;
//    }

//    void LateUpdate()
//    {
//        // Destory the bullet if it is out of bounds
//        // Raycast to detect if the bullet is close to an enemy
//        // If the ray hit an enemy, destory the enemy and remove it from the Created Enemies list
//        // After that, destory the bullet itself
//        Physics2D.SyncTransforms();
//        transform.Translate(Vector3.up * bullet_speed * Time.deltaTime);

//        if (Vector3.Distance(start_position, transform.position) >= max_distance)
//        {
//            Destroy(gameObject);
//        }

//        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, bullet_speed * Time.deltaTime, enemy_layer_mask);
//        if (hit.collider != null)
//        {
//            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
//            {
//                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
//                if (enemyHealth != null)
//                {
//                    enemyHealth.TakeDamage(1);
//                    Debug.Log("Enemy got a damage ~~~");

//                    float sizeReduction = 1.0f;

//                    Vector3 newScale = enemyHealth.transform.localScale - new Vector3(sizeReduction, sizeReduction, 0f);

//                    float minScale = 1.0f;
//                    newScale.x = Mathf.Max(newScale.x, minScale);
//                    newScale.y = Mathf.Max(newScale.y, minScale);

//                    enemyHealth.transform.localScale = newScale;

//                    if (enemyHealth.currentHealth <= 0)
//                    {
//                        Destroy(hit.collider.gameObject);
//                        generator.RemoveObject(hit.collider.gameObject, generator.created_enemies);
//                        FindObjectOfType<PlayerEat>().GainExperience(10);

//                        if (FindObjectOfType<PlayerEat>().experience >= 100)
//                        {
//                            FindObjectOfType<PlayerEat>().WinGame();
//                        }

//                        //if (generator.created_enemies.Count == 0)
//                        //{
//                        //    FindObjectOfType<PlayerEat>().WinGame();
//                        //}
//                    }
//                }

//                Destroy(gameObject);
//            }
//            else if (hit.collider.CompareTag("Wall"))
//            {
//                Debug.Log("Bullet hit the wall and got destroyed.");
//                Destroy(gameObject);
//            }
//        }
//    }
//}

using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public float bullet_speed = 30f;
    public float max_distance = 20f;
    public LayerMask collision_layer_mask;
    private Vector3 start_position;
    private Rigidbody2D rb;
    ObjectGenerator generator;

    AnalyticsManager analyticsManager;

    void Start()
    {
        start_position = transform.position;
        generator = ObjectGenerator.ins;
        rb = GetComponent<Rigidbody2D>();

        // Set the bullet's velocity in the upward direction
        rb.velocity = transform.up * bullet_speed;

        analyticsManager = FindObjectOfType<AnalyticsManager>();
    }

    void LateUpdate()
    {
        // Destroy the bullet if it exceeds the maximum distance
        if (Vector3.Distance(start_position, transform.position) >= max_distance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collision with Enemy
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                //here if the enemy is hit, enemiesShot+1, but if the same enemy is hit again, enemiesShot will not increase
                // Check if the enemy was hit by a bullet before, and if not, increment enemiesShot
                if (!enemyHealth.hasBeenShot)
                {
                    enemyHealth.hasBeenShot = true; // Mark that this enemy was shot
                    analyticsManager.EnemyShot(); // Increment enemiesShot
                }
                enemyHealth.TakeDamage(1);
                Debug.Log("Enemy got a damage ~~~");

                // Reduce enemy size
                float sizeReduction = 1.0f;
                Vector3 newScale = enemyHealth.transform.localScale - new Vector3(sizeReduction, sizeReduction, 0f);
                float minScale = 1.0f;
                newScale.x = Mathf.Max(newScale.x, minScale);
                newScale.y = Mathf.Max(newScale.y, minScale);
                enemyHealth.transform.localScale = newScale;

                if (enemyHealth.currentHealth <= 0)
                {
                    //here if the enemy is defeated, totalEnemiesDefeated+1. It has nothing to do with enemiesShot
                    analyticsManager.EnemyDefeated();
                    Destroy(collision.gameObject);
                    generator.RemoveObject(collision.gameObject, generator.created_enemies);
                    FindObjectOfType<PlayerEat>().GainExperience(10);
                    if (FindObjectOfType<PlayerEat>().experience >= 100)
                    {
                        FindObjectOfType<PlayerEat>().WinGame();
                    }
                }
            }
            Destroy(gameObject);
        }
        // Check for collision with Wall
        else if (collision.CompareTag("Wall"))
        {
            Debug.Log("Bullet hit the wall and got destroyed.");
            Destroy(gameObject);
        }

    }
}
