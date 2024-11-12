using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public GameObject food;
    public GameObject throw_circle;
    public Transform position;
    public float reduce_unit = 0.05f;
    public float shrink_rate = 0.1f;
    PlayerEat player_eat;
    ObjectGenerator generator;

    public float forceMagnitude = 10f;

    //create black circle(throw)
    //increase the players speed and decrease the size of the player
    public void PlayerThrow()
    {
            
        
        if (transform.localScale.x < 1)
        {
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal"); 
        float moveY = Input.GetAxisRaw("Vertical");   
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        

        //Vector2 Direction = transform.position - Camera.main.ScreenToWorldPoint(moveDirection);
        //float Z_Rotation = Mathf.Atan2(-Direction.y, -Direction.x) * Mathf.Rad2Deg + 90f;
        //transform.rotation = Quaternion.Euler(0, 0, Z_Rotation);

        GameObject b = Instantiate(throw_circle, position.position, Quaternion.identity);
        b.GetComponent<ObjectForce>().ApplyForce = true;
        transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
        
        

        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            
            //Vector2 forceDirection = -Direction.normalized; 

            rb.velocity = Vector2.zero;
            Vector2 forceDirection = moveDirection.normalized;
            rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);

            StartCoroutine(StopPlayerAfterDelay(rb, 0.3f)); 
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        player_eat = GetComponent<PlayerEat>();
        generator = ObjectGenerator.ins;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.localScale.x <= 1)
        {
            return;
        }
        //decrease the size of the player after each frame
        transform.localScale -= new Vector3(shrink_rate, shrink_rate, shrink_rate) * Time.deltaTime;
        FindObjectOfType<PlayerEat>().UpdateHealthUI();
    }

    private IEnumerator StopPlayerAfterDelay(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero; // Stop the player's movement
    }
}
