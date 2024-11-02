using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoving : MonoBehaviour
{
   // Start is called before the first frame update

   public Transform player; // Reference to the player's position
   public float moveSpeed = 2f;
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      MoveTowardPlayer();
   }

   void MoveTowardPlayer()
   {
      // Calculate the direction from the enemy to the player
      Vector3 direction = (player.position - transform.position).normalized;

      // Move the enemy towards the player
      transform.position += direction * moveSpeed * Time.deltaTime;
   }


   private void OnCollisionEnter2D(Collision2D collision)
   {
      // Check if the object the boss collided with is a wall
      if (collision.gameObject.CompareTag("Wall"))
      {
         Destroy(collision.gameObject); // Destroy the wall
         Debug.Log("Boss destroyed a wall!");
      }
   }
}
