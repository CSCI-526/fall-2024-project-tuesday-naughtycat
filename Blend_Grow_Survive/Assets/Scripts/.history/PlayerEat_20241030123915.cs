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
    public GameObject boss;
    public Transform player;
    public Text result_text;
    public Button restart_button;
    //public bool eat_ammo = false;
    public Text bullet_text;
    //private bool has_bullet = false;
    public int bulletCount = 0;

    // boss inactive
    public bool bossActivated = false;


    //public TextMeshProUGUI hpText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI coinText;

    public int maxHealth = 10;
    public int currentHealth;
    public int experience = 0;

    public Text healthText;
    //public Text experienceText;

    AnalyticsManager analyticsManager;

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

    public void UpdateBoss()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
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
    // public void CheckGameObject(GameObject[] Object)
    // {
    //     for (int i = 0; i < Object.Length; i++)
    //     {
    //         if (Object[i] == null)
    //         {
    //             continue;
    //         }
    //         Transform m = Object[i].transform;
    //         float playerRadius = transform.localScale.x / 2;
    //         float objectRadius = m.localScale.x / 2;

    //         if (Vector2.Distance(transform.position, m.position) <= playerRadius + objectRadius)
    //         {
    //             if (m.gameObject.CompareTag("Food") || m.gameObject.CompareTag("Ammo"))
    //             {
    //                 Destroy(m.gameObject);
    //                 RemoveObject(m.gameObject);
    //                 PlayerGrow();

    //                 if (m.gameObject.CompareTag("Food"))
    //                 {
    //                     ms.RemoveObject(m.gameObject, ms.created_food);
    //                     Destroy(m.gameObject);

    //                     //GainExperience(1); 
    //                     //break;  
    //                 }
    //                 else
    //                 {
    //                     ms.RemoveObject(m.gameObject, ms.created_ammos);
    //                     Destroy(m.gameObject);
    //                     bulletCount += 1;
    //                     UpdateBulletText();
    //                     //ms.CreateBullet();
    //                     //eat_ammo = true;
    //                     //break;
    //                 }
    //             }

    //             else if (m.gameObject.CompareTag("Enemy"))
    //             {
    //                 // Compare sizes between player and enemy
    //                 if (transform.localScale.x > m.localScale.x)
    //                 {
    //                     RemoveObject(m.gameObject);
    //                     PlayerGrow();
    //                     ms.RemoveObject(m.gameObject, ms.created_enemies);
    //                     Destroy(m.gameObject);

    //                     //GameManager.instance.AddEXP(10);
    //                     //expText.text = "EXP: " + GameManager.instance.playerEXP.ToString();
    //                     // Log that this enemy was absorbed
    //                     analyticsManager.EnemyDefeated(); // Pass false to indicate the enemy was absorbed

    //                     GameManager.instance.AddCoins(Random.Range(20, 30));
    //                     coinText.text = "Coins: " + GameManager.instance.playerCoins.ToString();
    //                     GainExperience(10);
    //                     //continue;  
    //                     if (experience >= 100)
    //                     {
    //                         WinGame();
    //                     }
    //                     continue;
    //                 }
    //                 else
    //                 {
    //                     GameOver();
    //                 }
    //             }

    //         }
    //     }
    // }

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
                    Destroy(m.gameObject);
                    RemoveObject(m.gameObject);
                    PlayerGrow();

                    if (m.gameObject.CompareTag("Food"))
                    {
                        ms.RemoveObject(m.gameObject, ms.created_food);
                    }
                    else
                    {
                        ms.RemoveObject(m.gameObject, ms.created_ammos);
                        bulletCount += 1;
                        UpdateBulletText();
                    }
                }
                else if (m.gameObject.CompareTag("Enemy"))
                {
                    if (transform.localScale.x > m.localScale.x)
                    {
                        RemoveObject(m.gameObject);
                        PlayerGrow();
                        Destroy(m.gameObject); // Destroy immediately upon eating
                        ms.RemoveObject(m.gameObject, ms.created_enemies);

                        analyticsManager.EnemyDefeated();

                        GameManager.instance.AddCoins(2);
                        coinText.text = "Coins: " + GameManager.instance.playerCoins.ToString();
                        GainExperience(10);

                        if (experience >= 100)
                        {
                            WinGame();
                        }
                    }
                    else
                    {
                        GameOver();
                    }
                }
                else if (m.gameObject.CompareTag("Boss"))
                {
                    if (transform.localScale.x > m.localScale.x)
                    {
                        PlayerGrow();
                        Destroy(m.gameObject); // Destroy the boss immediately
                        Debug.Log("Boss defeated!");
                        WinGame();
                    }
                    else
                    {
                        GameOver();
                        Debug.Log("Player defeated by the boss!");
                    }
                }
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            Transform bossTransform = collision.gameObject.transform;

            // Compare sizes between player and boss
            if (transform.localScale.x > bossTransform.localScale.x)
            {
                PlayerGrow();               // Player grows by eating the boss
                Destroy(collision.gameObject); // Destroy the boss
                Debug.Log("Boss defeated!");
                WinGame();                  // End the game as the player wins
            }
            else
            {
                GameOver();                 // Player is defeated by the boss
                Debug.Log("Player defeated by the boss!");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "" + Mathf.RoundToInt(transform.localScale.x);
        }
    }


    public void GainExperience(int xp)
    {
        experience += xp;
        UpdateExperienceUI();
        // Check if experience is 100 or more and the boss hasn't been activated yet
        if (experience >= 10 && !bossActivated)
        {
            ActivateBoss();
        }
    }

    public void UpdateExperienceUI()
    {
        if (expText != null)
        {
            expText.text = "Exp : " + experience;
        }
    }

    // If the player eat the food or enemy, the player will grow the size
    void PlayerGrow()
    {

        transform.localScale += new Vector3(0.08f, 0.08f, 0.08f);
    }
    // Function to activate the boss
    public void ActivateBoss()
    {
        bossActivated = true; // Ensure the boss is only activated once
        boss.SetActive(true); // Make the boss visible in the game
        Debug.Log("Boss has appeared!");
    }

    //update the bullet text if get or use the bullet
    //public void AddBullet()
    //{
    //    has_bullet = true;
    //    UpdateBulletText();
    //}

    //public void RemoveBullet()
    //{
    //    has_bullet = false;
    //    UpdateBulletText();
    //}

    public void UpdateBulletText()
    {
        if (bullet_text != null)
        {
            bullet_text.text = "# of bullets: " + bulletCount;
            bullet_text.gameObject.SetActive(true);
        }
    }

    //if it is game over, destroy the player, freeze the time and update the text
    public void GameOver()
    {
        CancelInvoke("Check");
        CancelInvoke("CheckEnemy");

        ms.StopGenerating();
        //ms.DestroyPlayerBullet();

        result_text.text = "Game Over!";
        result_text.gameObject.SetActive(true);
        restart_button.gameObject.SetActive(true);

        healthText.gameObject.SetActive(false);
        gameObject.SetActive(false);

        Time.timeScale = 0f;
        EndRound();
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

        healthText.gameObject.SetActive(false);

        Time.timeScale = 0f;
        EndRound();
    }

    //GameManager.instance.ResetCoins();
    public void RestartGame()
    {
        Debug.Log("Before restarting, playerCoins: " + GameManager.instance.playerCoins);
        Time.timeScale = 1;

        // Reset the upgraded bullet properties upon restart of the game
        GameManager.instance.ResetBulletProperties();

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        GameManager.instance.UpdateReferences();
        Debug.Log("After scene reload, playerCoins: " + GameManager.instance.playerCoins);

    }

    ObjectGenerator ms;
    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;
        UpdateHealthUI();
        UpdateExperienceUI();

        UpdateFood();
        UpdateEnemy();
        UpdateAmmo();
        InvokeRepeating("Check", 0, 0.1f);
        InvokeRepeating("CheckEnemy", 0, 0.1f);
        InvokeRepeating("CheckAmmo", 0, 0.1f);
        ms = ObjectGenerator.ins;

        ms.players.Add(gameObject);
        analyticsManager = FindObjectOfType<AnalyticsManager>();

        // Set the boss to inactive at the start of the game
        if (boss != null)
        {
            boss.SetActive(false);
        }
    }


    public void EndRound()
    {
        analyticsManager.EndRound();
    }
}