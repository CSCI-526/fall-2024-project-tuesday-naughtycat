using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public float bullet_speed = 20f; 
    public float max_distance = 20f;  
    public LayerMask enemy_layer_mask; 

    private Vector3 start_position; 
    ObjectGenerator generator;

    void Start()
    {
        start_position = transform.position;
        generator = ObjectGenerator.ins; 
    }

    void Update()
    {
        // Destory the bullet if it is out of bounds
        // Raycast to detect if the bullet is close to an enemy
        // If the ray hit an enemy, destory the enemy and remove it from the Created Enemies list
        // After that, destory the bullet itself
        // Win the game if there are no enemies left 
        transform.Translate(Vector3.up * bullet_speed * Time.deltaTime);

        if (Vector3.Distance(start_position, transform.position) >= max_distance)
        {
            Destroy(gameObject);  
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, bullet_speed * Time.deltaTime, enemy_layer_mask);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
                generator.RemoveObject(hit.collider.gameObject, generator.created_enemies);
                Destroy(gameObject);
                if (generator.created_enemies.Count == 0)
                {
                    FindObjectOfType<PlayerEat>().WinGame();
                }
            }
        }
    }
}
