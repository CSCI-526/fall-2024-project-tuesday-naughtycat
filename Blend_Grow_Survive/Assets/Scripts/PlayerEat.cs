using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerEat : MonoBehaviour
{

    public GameObject[] food;
    public GameObject[] enemies;
    public GameObject[] ammos;
    public Transform player;
    public Text result_text;
    public Button restart_button;
    public bool eat_ammo = false;
    public Text bullet_text;
    private bool has_bullet = false;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI expText;

    public void UpdateFood()
    {
        food = GameObject.FindGameObjectsWithTag("Food");
    }

    public void UpdateEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void UpdateAmmo()
    {
        ammos = GameObject.FindGameObjectsWithTag("Ammo");
    }

    public void RemoveObject(GameObject Object)
    {
        List<GameObject> ObjectList = new List<GameObject>();

        for (int i = 0; i < food.Length; i++)
        {
            ObjectList.Add(food[i]);
        }
        ObjectList.Remove(Object);

        food = ObjectList.ToArray();
    }

    public void AddObject(GameObject Object)
    {
        List<GameObject> ObjectList = new List<GameObject>();

        for (int i = 0; i < food.Length; i++)
        {
            ObjectList.Add(food[i]);
        }
        ObjectList.Add(Object);

        food = ObjectList.ToArray();
    }


    public void Check()
    {
        CheckGameObject(food);
    }
    public void CheckEnemy()
    {
        CheckGameObject(enemies);
    }

    public void CheckAmmo()
    {
        CheckGameObject(ammos);
    }

    // check winning condition
    //check size comparison between player and enemy, if player is smaller, lose the game, else player eat enemy
    //check if player collide with the food/bullet, if yes, destroy the food/bullet
    public void CheckGameObject(GameObject[] Object)
    {
        for (int i = 0; i < Object.Length; i++)
        {
            if (Object[i] == null)
            {
                continue;
            }
            Transform m = Object[i].transform;
            float playerRadius = transform.localScale.x / 2;
            float objectRadius = m.localScale.x / 2;

            if (Vector2.Distance(transform.position, m.position) <= playerRadius + objectRadius)
            {
                if (m.gameObject.CompareTag("Food") || m.gameObject.CompareTag("Ammo"))
                {
                    // Destroy the object first to prevent multiple calls
                    Destroy(m.gameObject);
                    RemoveObject(m.gameObject);
                    PlayerGrow();

                    if (m.gameObject.CompareTag("Food"))
                    {
                        ms.RemoveObject(m.gameObject, ms.created_food);
                        GameManager.instance.AddHP(5);
                        hpText.text = "HP: " + GameManager.instance.playerHP.ToString();
                    }
                    else
                    {
                        ms.RemoveObject(m.gameObject, ms.created_ammos);
                        Destroy(m.gameObject);
                    
                        ms.CreateBullet();
                        eat_ammo = true;
                    }
                }
                else if (m.gameObject.CompareTag("Enemy"))
                {
                    float distance = Vector2.Distance(transform.position, m.position);
                    float overlap = (playerRadius + objectRadius) - distance;

                    // If the player's size is smaller than the enemy's size
                    if (transform.localScale.x < m.localScale.x)
                    {
                        // Check if the overlap is less than 50% of the player's radius
                        if (overlap < playerRadius / 2)
                        {
                            GameManager.instance.DeductHP(20);
                            Debug.Log("HP deducted by 20%");
                            hpText.text = "HP: " + GameManager.instance.playerHP.ToString();
                        }
                        else
                        {
                            // If more than 50% overlap, game over
                            GameOver();
                            Debug.Log("Game Over! Enemy overlapped more than 50%.");
                            hpText.text = "HP: " + GameManager.instance.playerHP.ToString();
                        }
                    }
                    else
                    {
                        RemoveObject(m.gameObject);
                        PlayerGrow();
                        ms.RemoveObject(m.gameObject, ms.created_enemies);
                        Destroy(m.gameObject);
                        GameManager.instance.AddEXP(10);
                        expText.text = "EXP: " + GameManager.instance.playerEXP.ToString();

                        if (ms.created_enemies.Count == 0)
                        {
                            WinGame();
                        }
                    }
                }
            }
        }
    }

    // If the player eat the food or enemy, the player will grow the size
    void PlayerGrow()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    //update the bullet text if get or use the bullet
    public void AddBullet()
    {
        has_bullet = true;
        UpdateBulletText();
    }

    public void RemoveBullet()
    {
        has_bullet = false;
        UpdateBulletText();
    }

    public void UpdateBulletText()
    {
        if (bullet_text != null)
        {
            if (has_bullet)
            {
                bullet_text.text = "# of bullet: 1";
            }
            else
            {
                bullet_text.text = "# of bullet: 0";
            }
            bullet_text.gameObject.SetActive(true);
        }
    }

    //if it is game over, destroy the player, freeze the time and update the text
    public void GameOver()
    {
        CancelInvoke("Check");
        CancelInvoke("CheckEnemy");

        ms.StopGenerating();
        ms.DestroyPlayerBullet();

        result_text.text = "Game Over!";
        result_text.gameObject.SetActive(true);
        restart_button.gameObject.SetActive(true);

        gameObject.SetActive(false);

        Time.timeScale = 0f;
    }
    // If win the game, stop generating anything and update the winning text
    public void WinGame()
    {
        CancelInvoke("Check");
        CancelInvoke("CheckEnemy");

        ms.StopGenerating();

        result_text.text = "You Win!";
        result_text.gameObject.SetActive(true);
        restart_button.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        GameManager.instance.ResetHP();
        GameManager.instance.ResetEXP();

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    ObjectGenerator ms;
    // Start is called before the first frame update
    void Start()
    {
        UpdateFood();
        UpdateEnemy();
        UpdateAmmo();
        InvokeRepeating("Check", 0, 0.1f);
        InvokeRepeating("CheckEnemy", 0, 0.1f);
        InvokeRepeating("CheckAmmo", 0, 0.1f);
        ms = ObjectGenerator.ins;

        ms.players.Add(gameObject);
    }
}