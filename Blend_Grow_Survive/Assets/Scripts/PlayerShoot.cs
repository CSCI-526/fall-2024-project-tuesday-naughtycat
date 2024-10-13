using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using TMPro;
using UnityEngine;


public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public GameObject bullet; 
    public Transform bullet_start_point; 
    //public float bullet_speed = 20f;
    ObjectGenerator generator;
    private bool has_shot = false; // To ensure player can only shoot once
    private Vector2 mouse_position;
    private Vector2 direction;
    PlayerEat player_eat;
    public TextMeshProUGUI hpText;

    void Start()
    {
        player_eat = GetComponent<PlayerEat>();
        generator = ObjectGenerator.ins;
        generator.players.Add(gameObject);
    }
    void Update()
    {
        HandleGunRotation();
        // If click the left mouse, player hasn't shot yet and player eat the ammo
        if (Input.GetMouseButtonDown(0) && !has_shot && player_eat.eat_ammo) 
        {
            Shoot();
            generator.DestroyPlayerBullet();
        }
    }

    // handle the dirction of bullet followed by the mouse pointer
    void HandleGunRotation()
    {
        mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mouse_position - (Vector2)player.transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); 
    }

    // use the mouse position as the target
    // calculate the direction from the player to the target
    // create the bullet instance and bullet's velocity
    void Shoot()
    {
        mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mouse_position - (Vector2)player.transform.position).normalized;
        GameObject bullet_instance = Instantiate(bullet, bullet_start_point.position, player.transform.rotation);
        Rigidbody2D rb = bullet_instance.GetComponent<Rigidbody2D>();
        //rb.velocity = direction * bullet_speed;
        GameManager.instance.DeductHP(5);
        Debug.Log("HP deducted by 5%");
        hpText.text = "HP: " + GameManager.instance.playerHP.ToString();
        has_shot = true;

    }
}
