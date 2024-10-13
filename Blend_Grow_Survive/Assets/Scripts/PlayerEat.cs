using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    /// 新增
    public int maxHealth = 10; // 玩家最大血量
    public int currentHealth;   // 玩家當前血量
    public int experience = 0;  // 玩家經驗值

    public Text healthText;     // 用來顯示血量的 UI
    public Text experienceText; // 用來顯示經驗值的 UI 

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
                    RemoveObject(m.gameObject);
                    PlayerGrow();
                    if (m.gameObject.CompareTag("Food"))
                    {
                        ms.RemoveObject(m.gameObject, ms.created_food);
                        Destroy(m.gameObject);
                           Debug.Log("吃掉食物");
                        GainExperience(1); // 吃掉食物獲得 1 點經驗值
                        break;  // 避免重複處理
                    }
                    else
                    {
                        ms.RemoveObject(m.gameObject, ms.created_ammos);
                        Destroy(m.gameObject);
                    Debug.Log("補充子彈");
                        ms.CreateBullet();
                        eat_ammo = true;
                         break;  // 避免重複處理
                    }
                }
                 // 碰撞到敵人
                else if (m.gameObject.CompareTag("Enemy"))
                {
                    // Compare sizes between player and enemy
                    if (transform.localScale.x > m.localScale.x)
                    {
                        RemoveObject(m.gameObject);
                        PlayerGrow();
                        ms.RemoveObject(m.gameObject, ms.created_enemies);
                        Destroy(m.gameObject);
                        Debug.Log("碰到敵人");
                        GainExperience(2); // 吃掉籃球獲得經驗值
                        continue;  // 避免重複處理
                        if (ms.created_enemies.Count == 0)
                        {
                            WinGame();
                        }
                    }
                    else
                    {
                       TakeDamage(1); // 當玩家比敵人小，扣 1 點血量
                        if (currentHealth <= 0)
                        {
                            GameOver(); // 血量為 0 時，遊戲結束
                        }
                    }
                }
            }
        }
    }
     // 受到傷害時
public void TakeDamage(int damage)
{
    currentHealth -= damage;
    UpdateHealthUI();//更新UI

    if (currentHealth <= 0)
    {
        GameOver(); // 血量降為 0 時，結束遊戲
    }
}

public void UpdateHealthUI()
{
    if (healthText != null)
    {
        healthText.text = "Health : " + currentHealth + "/" + maxHealth;
    }
}

// 經驗值
public void GainExperience(int xp)
{
    experience += xp;
    UpdateExperienceUI();
}

public void UpdateExperienceUI()
{
    if (experienceText != null)
    {
        experienceText.text = "Exp : " + experience ;
    }
}

    // If the player eat the food or enemy, the player will grow the size
    void PlayerGrow()
    {
         // 增加玩家大小
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

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    ObjectGenerator ms;
    // Start is called before the first frame update
    void Start()
    {
        // 血量跟經驗值ui
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
    }


}