using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Transform player;
    public Text result_text;
    public Button restart_button;
    //public bool eat_ammo = false;
    public Text bullet_text;
    //private bool has_bullet = false;
    public int bulletCount = 0;

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
                    Destroy(m.gameObject);
                    RemoveObject(m.gameObject);
                    PlayerGrow();

                    if (m.gameObject.CompareTag("Food"))
                    {
                        ms.RemoveObject(m.gameObject, ms.created_food);
                        Destroy(m.gameObject);

                        //GainExperience(1); 
                        //break;  
                    }
                    else
                    {
                        ms.RemoveObject(m.gameObject, ms.created_ammos);
                        Destroy(m.gameObject);
                        bulletCount += 1;
                        UpdateBulletText();
                        //ms.CreateBullet();
                        //eat_ammo = true;
                        //break;
                    }
                }

                else if (m.gameObject.CompareTag("Enemy"))
                {
                    // Compare sizes between player and enemy
                    if (transform.localScale.x > m.localScale.x)
                    {
                        RemoveObject(m.gameObject);
                        PlayerGrow();
                        ms.RemoveObject(m.gameObject, ms.created_enemies);
                        Destroy(m.gameObject);
                        // Log that this enemy was absorbed
                        analyticsManager.EnemyDefeated(); // Pass false to indicate the enemy was absorbed

                        GainExperience(10);
                        //continue;  
                        if (experience >= 100)
                        {
                            WinGame();
                        }
                        continue;
                    }
                    else
                    {

                        //TakeDamage(1); 
                        //if (currentHealth <= 0)
                        //{
                        GameOver();
                        //}
                    }
                }
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


    public void GainExperience(int xp)
    {
        experience += xp;
        UpdateExperienceUI();
    }

    public void UpdateExperienceUI()
    {
        if (experienceText != null)
        {
            experienceText.text = "Exp : " + experience;
        }
    }

    // If the player eat the food or enemy, the player will grow the size
    void PlayerGrow()
    {

        transform.localScale += new Vector3(0.08f, 0.08f, 0.08f);
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

    //public void UpdateBulletText()
    //{
    //    if (bullet_text != null)
    //    {
    //        if (has_bullet)
    //        {
    //            bullet_text.text = "# of bullet: 1";
    //        }
    //        else
    //        {
    //            bullet_text.text = "# of bullet: 0";
    //        }
    //        bullet_text.gameObject.SetActive(true);
    //    }
    //}

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

    public void RestartGame()
    {
        Time.timeScale = 1;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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