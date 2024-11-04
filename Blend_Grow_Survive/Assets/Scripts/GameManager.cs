using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private ObjectGenerator objectGenerator;
    public int playerHP = 100;
    //public int playerEXP = 0;
    public int playerCoins = 0;

    //public TextMeshProUGUI hpText;
    //public TextMeshProUGUI expText;
    public TextMeshProUGUI coinText;

    public GameObject bullet;
    private BulletAttack bulletAttack;

    //storing initial values of bullets so it can be reset to them upon restarting the game.
    private float initialBulletSpeed = 15f;
    private float initialMaxDistance = 10f;
    //public List<string> availableGunUpgrades = new List<string> { "Bullet_Speed", "Range", "Damage" };
    //public List<string> availableStatUpgrades = new List<string> { "HP", "Speed", "AttackSpeed" };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("GameManager: New instance created.");
        }
        else
        {
            Debug.Log("GameManager: Instance already exists, destroying this one.");
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateReferences();
    }

    private void Start()
    {
        objectGenerator = FindObjectOfType<ObjectGenerator>();
        Debug.Log("GameManager Start: playerCoins = " + playerCoins);
        UpdateCoinText();
        Debug.Log("GameManager: Start called. Current coins: " + playerCoins);
        if (bullet != null)
        {
            bulletAttack = bullet.GetComponent<BulletAttack>();
            if (bulletAttack == null)
            {
                Debug.LogWarning("BulletAttack component not found on bulletPrefab.");
            }
        }
    }

    void Update()
    {
        //AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        //if (listeners.Length > 1)
        //{
        //    Debug.Log("Number of AudioListeners: " + listeners.Length);
        //    for (int i = 0; i < listeners.Length; i++)
        //    {
        //        Debug.Log("AudioListener found on: " + listeners[i].gameObject.name);
        //    }
        //}
    }

    public void UpdateReferences()
    {
        Debug.Log("Update references is being called");
        // Find the UI elements in the scene and update the references
        //hpText = GameObject.Find("hpText").GetComponent<TextMeshProUGUI>();
        //expText = GameObject.Find("expText").GetComponent<TextMeshProUGUI>();
        coinText = GameObject.Find("coinText").GetComponent<TextMeshProUGUI>();

        // Update the UI to show the current values immediately
        UpdateCoinText();
        //UpdateEXPText();
        //UpdateHpText();
    }

    // Method to add HP
    public void AddHP(int hpAmount)
    {
        playerHP += hpAmount;
        // clamp or no clamp? -> upgrade of HP
        playerHP = Mathf.Clamp(playerHP, 0, 100);
        Debug.Log("Current HP: " + playerHP);
    }

    // Method to deduct HP
    public void DeductHP(int hpAmount)
    {
        playerHP -= hpAmount;
        Debug.Log("Current HP: " + playerHP);
        
        // If HP falls to 0 or below, end the game
        if (playerHP <= 0)
        {
            playerHP = 0;
            Debug.Log("Player HP is 0, Game Over!");
            FindObjectOfType<PlayerEat>().GameOver();
        }
    }

    // Reset HP to 100% when restarting the game
    public void ResetHP()
    {
        playerHP = 100;
        Debug.Log("HP reset to: " + playerHP);
        
    }

    // Method to add EXP when an enemy is defeated
    //public void AddEXP(int expAmount)
    //{
    //    playerEXP += expAmount;
    //    playerEXP = Mathf.Clamp(playerEXP, 0, 100);
    //    Debug.Log("Current EXP: " + playerEXP);
    //    UpdateEXPText();
    //}

    // Reset EXP when restarting the game
    //public void ResetEXP()
    //{
    //    playerEXP = 0;
    //    Debug.Log("EXP reset to: " + playerEXP);
        
    //}
    // COINS------
    // Method to add coins upon defeating an enemy
    public void AddCoins(int coinAmount)
    {
        playerCoins += coinAmount;
        Debug.Log("Current Coins: " + playerCoins);
        UpdateCoinText();
    }

    // Method to spend coins once the player buys an upgrade
    public bool SpendCoins(int coinAmount)
    {
        if (playerCoins >= coinAmount)
        {
            playerCoins -= coinAmount;   
            Debug.Log("Coins spent: " + coinAmount + ". Remaining Coins: " + playerCoins);
            UpdateCoinText();   
            return true;   
        }
        Debug.Log("Not enough coins to Buy! :(, Come back soon Champ!");   
        return false;   
    }

    //Reset coins upon newgame
    //public void ResetCoins()
    //{
    //    playerCoins = 0;
    //    UpdateCoinText();
    //    Debug.Log("Coins reset to: " + playerCoins);
    //}
    //private void UpdateHpText()
    //{
    //    if (hpText != null)
    //    {
    //        hpText.text = "HP: " + playerHP.ToString();
    //    }
    //}

    //private void UpdateEXPText()
    //{
    //    if (expText != null)
    //    {
    //        expText.text = "EXP: " + playerEXP.ToString();
    //    }
    //}

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            Debug.Log("These are the curent amount of playercoins" + playerCoins);
            coinText.text = "Coins: " + playerCoins.ToString();
        }
    }

    //WaveCHECKER
    public void CheckWaveCompletion()
    {
        objectGenerator = FindObjectOfType<ObjectGenerator>();
        if (objectGenerator != null)
        {
            if (objectGenerator.created_enemies.Count == 0 && !objectGenerator.IsWaveActive)
            {
                objectGenerator.OnWaveCleared();
            }
        }
        else
        {
            Debug.LogError("ObjectGenerator instance not found! it is here");
        }
    }

    // UPGRADES-------
    // Methods for GUN UPGRADES   
    public void UpgradeGun(string upgradeType)
    {
        if (bulletAttack == null)
        {
            Debug.LogWarning("BulletAttack component is not initialized.");
            return;
        }

        switch (upgradeType)
        {
            case "Speed":
                Debug.Log("this is the current value of the bullet_speed" + bulletAttack.bullet_speed);
                bulletAttack.bullet_speed += 2f;
                Debug.Log("UPDATED bullet_speed" + bulletAttack.bullet_speed);
                Debug.Log("Gun Speed Upgraded");
                break;

            case "Range": 
                Debug.Log("Bullet max distance CURRENTLY: " + bulletAttack.max_distance);
                bulletAttack.max_distance += 2f;
                Debug.Log("Bullet max distance upgraded to: " + bulletAttack.max_distance);

                break;

            case "Damage":
                break;
            case "Penetration":
                break;
            case "Reload":
                break;
        }
    }

    // Method for STAT UPGRADES
    public void UpgradeStat(string statType)
    {
        switch (statType)
        {
            case "MovementSpeed":
                // Implement speed upgrade
                PlayerMovements pm = FindObjectOfType<PlayerMovements>();
                if (pm != null)
                {
                    Debug.Log("Speed currently" + pm.speed);
                    pm.speed += 0.5f;
                    Debug.Log("Speed Upgraded");
                }
                break;
            case "ShrinkResistance":
                Actions pa = FindObjectOfType<Actions>();
                if (pa != null)
                {
                    Debug.Log("Shrink Rate currently" + pa.shrink_rate);
                    pa.shrink_rate -= 0.01f;
                    Debug.Log("Shrink Rate Upgraded");
                }
                break;
            case "MaxHealth":
                AddHP(5);
                Debug.Log("HP Upgraded");
                break;
            case "BodyDamage":
                break;
        }
    }

    public void ResetBulletProperties()
    {
        if (bulletAttack != null)
        {
            bulletAttack.bullet_speed = initialBulletSpeed;
            bulletAttack.max_distance = initialMaxDistance;
            Debug.Log("Bullet properties reset to initial values.");
        }
        else
        {
            Debug.LogWarning("BulletAttack component not found.");
        }
    }
}