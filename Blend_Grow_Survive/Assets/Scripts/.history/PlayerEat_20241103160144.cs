using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerEat : MonoBehaviour
{

    #region instance 
    public static PlayerEat ins;

    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
        }
    }
    #endregion


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
    public BackgroundColorChanger backgroundColorChanger;
    // boss inactive
    public bool bossActivated = false;
    //public BossMessageController bossMessageController;

    //public TextMeshProUGUI hpText;

    public TextMeshProUGUI coinText;

    public int maxHealth = 10;
    public int currentHealth;
    public int experience = 0;

    public Text healthText;
    public Text experienceText;

    AnalyticsManager analyticsManager;

    public void UpdateFood()
    {
        food = GameObject.FindGameObjectsWithTag("Food");
    }

    public void UpdateEnemy()
    {
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // find object with tag cannot find disabled objects, use it carefully
        // when dealing with tutorial scene because we disabled the enemy once
        // the scene starts 
        enemies = ObjectGenerator.ins.getEnemyArr();
    }

    public void UpdateAmmo()
    {
        ammos = GameObject.FindGameObjectsWithTag("Ammo");
        // ammos = ObjectGenerator.ins.getAmmoArr();
        // don't use this one! Ammo is created manually, not by Object generator
        // getting it from object generator will prevent payer from eating it
    }

    public void UpdateTutorialAmmo()
    {
        int oldAmmoNum = ammos.Length;
        GameObject[] tutorialAmmo = GameObject.FindGameObjectsWithTag("tutorialAmmo");
        int newAmmoNum = oldAmmoNum + tutorialAmmo.Length;

        GameObject[] newAmmo = new GameObject[newAmmoNum];

        for (int i = 0; i < oldAmmoNum; i++)
        {
            newAmmo[i] = ammos[i];
        }
        int j = 0;
        for (int i = oldAmmoNum; i < newAmmoNum; i++)
        {
            newAmmo[i] = tutorialAmmo[j];
            j++;
        }

        ammos = newAmmo;
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


                    if (m.gameObject.CompareTag("Food"))
                    {
                        PlayerGrow();
                        ms.RemoveObject(m.gameObject, ms.created_food);
                        Destroy(m.gameObject);


                        //GainExperience(1); 
                        //break;  
                    }
                    if (m.gameObject.CompareTag("Ammo"))
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
                        PlayerGrow();

                        Destroy(m.gameObject); // Destroy immediately upon eating
                        ms.RemoveObject(m.gameObject, ms.created_enemies);

                        analyticsManager.EnemyDefeated();

                        GameManager.instance.AddCoins(2);
                        coinText.text = "Coins: " + GameManager.instance.playerCoins.ToString();

                        GainExperience(10);

                        // if (experience >= 100)
                        // {
                        //     WinGame();
                        // }
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
            if (transform.localScale.x > bossTransform.localScale.x)
            {
                PlayerGrow();
                Destroy(collision.gameObject);
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
            //healthText.text = "" + Mathf.RoundToInt(transform.localScale.x);
            healthText.text = "" + Mathf.RoundToInt((transform.localScale.x * 10)).ToString();
        }
    }

    public string GenProgressBar(int score)
    {

        string barText = "EXP: ";
        for (int i = 0; i < score; i++)
        {
            barText += "█";
        }
        for (int i = 0; i < 10 - score; i++)
        {
            barText += "░";
        }
        //Debug.Log("score: " + score + "      text:" + barText);
        return barText;
    }


    public void GainExperience(int xp)
    {
        experience += xp;
        UpdateExperienceUI();
        // Check if experience is 100 or more and the boss hasn't been activated yet
        if (experience >= 100)
        {
            ActivateBoss();
        }
    }

    public void UpdateExperienceUI()
    {
        if (experienceText != null)
        {

            //experienceText.text = "Exp : " + experience;
            experienceText.text = GenProgressBar(experience / 10);
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
        Debug.Log("Activating Boss...");
        bossActivated = true; // Ensure the boss is only activated once
        boss.SetActive(true); // Make the boss visible in the game
        Debug.Log("Boss has appeared!");


        if (boss != null)
        {
            boss.SetActive(true); // Make the boss visible in the game
            Debug.Log("Boss has appeared!");
        }
        else
        {
            Debug.LogError("Boss GameObject is not assigned in PlayerEat script.");
        }
        backgroundColorChanger = FindObjectOfType<BackgroundColorChanger>();

        if (backgroundColorChanger != null)
        {
            backgroundColorChanger.ChangeToPink();
        }
        else
        {
            Debug.LogWarning("BackgroundColorChanger is not assigned in PlayerEat script.");
        }

        // // Display the boss message
        // if (bossMessageController != null)
        // {
        //     bossMessageController.ShowBossMessage();
        // }
        // else
        // {
        //     Debug.LogError("BossMessageController reference is missing in PlayerEat script.");
        // }
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

        //Debug.Log("Before restarting, playerCoins: " + GameManager.instance.playerCoins);
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().name.CompareTo("TutorialScene") == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // Reset the upgraded bullet properties upon restart of the game
            GameManager.instance.ResetBulletProperties();

            // Reload the current scene

            GameManager.instance.UpdateReferences();
            Debug.Log("After scene reload, playerCoins: " + GameManager.instance.playerCoins);
        }



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
        //UpdateTutorialAmmo();
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

    void Update()
    {
        UpdateAmmo();
    }
}