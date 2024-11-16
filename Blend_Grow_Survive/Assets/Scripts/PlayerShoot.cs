using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public GameObject bullet;
    public Transform bullet_start_point;
    public float bullet_speed = 20f;
    public GameObject gun;
    private Gun currentGun;
    private int currentGunIndex = 0;
    ObjectGenerator generator;
    //private bool has_shot = false;
    private Vector2 mouse_position;
    private Vector2 direction;
    PlayerEat player_eat;
    UpgradePanelToggle upgradePanelToggle;

    public TextMeshProUGUI arrow;
    public TextMeshProUGUI outOfBulletText;

    void Start()
    {
        //myGun.transform.SetParent(transform);
        player_eat = GetComponent<PlayerEat>();
        generator = ObjectGenerator.ins;
        generator.players.Add(gameObject);

        upgradePanelToggle = FindObjectOfType<UpgradePanelToggle>();
        // check if this is needed!!!! my branch did not have this code
        currentGun = gun.transform.GetChild(currentGunIndex).GetComponent<Gun>();
        outOfBulletText = GameObject.Find("OutOfBullet").GetComponent<TextMeshProUGUI>();
        outOfBulletText.enabled = false;
    }
    void LateUpdate()
    {
        HandleGunRotation();
        // If click the left mouse, player hasn't shot yet and player eat the ammo
        if (Input.GetMouseButtonDown(0) && (upgradePanelToggle == null || !upgradePanelToggle.IsPanelOpen()))
        {
            if (player_eat.bulletCount == 0)
            {
                //outOfBulletText.transform.position = player.transform.position;
                outOfBulletText.enabled = true;
                outOfBulletText.StartCoroutine(HideOutOfBulletTextAfterDelay(1f));
            }
            else
            {
                Shoot();
            }
            //currentGun.Shoot();
            //generator.DestroyPlayerBullet();
        }
    }

    IEnumerator HideOutOfBulletTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        outOfBulletText.enabled = false; // Disable the text component
    }

    // handle the dirction of bullet followed by the mouse pointer
    void HandleGunRotation()
    {
        mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mouse_position - (Vector2)player.transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        arrow.SetText("↑");
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        //transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        //currentGun.firePoint.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    // use the mouse position as the target
    // calculate the direction from the player to the target
    // create the bullet instance and bullet's velocity
    void Shoot()
    {
        if (generator.created_bullet.Count >= 0)
        {
            mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mouse_position - (Vector2)player.transform.position).normalized;
            GameObject bullet_instance = Instantiate(bullet, bullet_start_point.position, player.transform.rotation);
            //Rigidbody2D rb = bullet_instance.GetComponent<Rigidbody2D>();
            //rb.velocity = direction * bullet_speed;
            //has_shot = true;
            player_eat.bulletCount -= 1;
            player_eat.UpdateBulletText();
        }

    }
}
