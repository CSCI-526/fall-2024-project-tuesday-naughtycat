using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject arrowObject;
    public TextMeshProUGUI outOfBulletText;

    //[SerializeField] private GameObject plusSignPrefab;
    //[SerializeField] private float plusSignLifetime = 1f;

    public Texture2D crosshairCursor;
    //private bool isCursorChanged = false;
    void Start()
    {
        //myGun.transform.SetParent(transform);
        player_eat = GetComponent<PlayerEat>();
        generator = ObjectGenerator.ins;
        generator.players.Add(gameObject);
        //ChangeCursorTemporarily();

        upgradePanelToggle = FindObjectOfType<UpgradePanelToggle>();
        // check if this is needed!!!! my branch did not have this code
        currentGun = gun.transform.GetChild(currentGunIndex).GetComponent<Gun>();
        if(1==1)
        {
            outOfBulletText = GameObject.Find("OutOfBullet").GetComponent<TextMeshProUGUI>();
            outOfBulletText.enabled = false;
        }
        
    }
    void LateUpdate()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        if(player_eat.bulletCount > 0)
        {
            ChangeCursorTemporarily();
        } else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        HandleGunRotation();
        // If click the left mouse, player hasn't shot yet and player eat the ammo
        if (Input.GetMouseButtonDown(0) && (upgradePanelToggle == null || !upgradePanelToggle.IsPanelOpen()))
        {
            if (player_eat.bulletCount == 0)
            {
                //outOfBulletText.transform.position = player.transform.position;
                if (SceneManager.GetActiveScene().name.CompareTo("TutorialScene") != 0)
                {
                    outOfBulletText.enabled = true;
                    outOfBulletText.StartCoroutine(HideOutOfBulletTextAfterDelay(1f));
                }
                    
            }
            else
            {
                Shoot();
                //CreatePlusSign();
                /*
                if (!isCursorChanged)
                {
                    StartCoroutine(ChangeCursorTemporarily());
                }
                */
            }
            //currentGun.Shoot();
            //generator.DestroyPlayerBullet();
        }
    }

    /*
    private void ChangeCursorTemporarily()
    {
        Vector2 hotspot = new Vector2(crosshairCursor.width / 2f, crosshairCursor.height / 2f);
        // Change to crosshair cursor
        Cursor.SetCursor(crosshairCursor, hotspot, CursorMode.Auto);
    }*/

    private void ChangeCursorTemporarily()
    {
        
        Texture2D scaledCrosshair = ScaleTexture(crosshairCursor, 0.3f); 
        Vector2 hotspot = new Vector2(scaledCrosshair.width / 2f, scaledCrosshair.height / 2f);

        Cursor.SetCursor(scaledCrosshair, hotspot, CursorMode.Auto);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Utility method to scale a texture
    private Texture2D ScaleTexture(Texture2D source, float scale)
    {
        int newWidth = Mathf.RoundToInt(source.width * scale);
        int newHeight = Mathf.RoundToInt(source.height * scale);

        RenderTexture rt = RenderTexture.GetTemporary(newWidth, newHeight);
        rt.filterMode = FilterMode.Bilinear;

        RenderTexture.active = rt;
        Graphics.Blit(source, rt);

        Texture2D result = new Texture2D(newWidth, newHeight, source.format, false);
        result.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0, 0);
        result.Apply();

        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        return result;
    }

    /*
     private System.Collections.IEnumerator ChangeCursorTemporarily()
     {
         isCursorChanged = true;

         Vector2 hotspot = new Vector2(crosshairCursor.width / 2f, crosshairCursor.height / 2f);
         // Change to crosshair cursor
         Cursor.SetCursor(crosshairCursor, hotspot, CursorMode.Auto);

         // Wait for 1 second
         yield return new WaitForSeconds(0.1f);

         // Revert to the system default cursor
         Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
         isCursorChanged = false;
     }
    */
    /*
    void CreatePlusSign()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        // Instantiate the + sign at the correct position
        GameObject plusSign = Instantiate(plusSignPrefab, mouseWorldPosition, Quaternion.identity);

        Destroy(plusSign, plusSignLifetime);
    }
    */

    IEnumerator HideOutOfBulletTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (SceneManager.GetActiveScene().name.CompareTo("TutorialScene") != 0)
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
        arrowObject.transform.localScale = transform.localScale;
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
