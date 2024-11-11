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
    public int isTutorial;
    //public int leftEnemy = 10;
    //public int level = 1;

    //public TextMeshProUGUI hpText;
    //public TextMeshProUGUI expText;
    public TextMeshProUGUI coinText;
    //public TextMeshProUGUI leftEnemyText;
    //public TextMeshProUGUI levelText;

    public GameObject bullet;
    private BulletAttack bulletAttack;

    //storing initial values of bullets so it can be reset to them upon restarting the game.
    private float initialBulletSpeed = 15f;
    private float initialMaxDistance = 10f;
    //public List<string> availableGunUpgrades = new List<string> { "Bullet_Speed", "Range", "Damage" };
    //public List<string> availableStatUpgrades = new List<string> { "HP", "Speed", "AttackSpeed" };

    // capping upgradesystem
    [Header("Upgrade Levels")]
    public int bulletSpeedLevel = 0;
    public int bulletRangeLevel = 0;
    public int shrinkResistanceLevel = 0;
    public int movementSpeedLevel = 0;

    private const int MAX_UPGRADE_LEVEL = 5; // Maximum number of upgrades per stat

    public TextMeshProUGUI shinkProgressbar;
    public TextMeshProUGUI movementProgressbar;
    public TextMeshProUGUI rangeProgressbar;
    public TextMeshProUGUI speedProgressbar;

    private int shrinkCount = 0;
    private int movementCount = 0;
    private int rangeCount = 0;
    private int speedCount = 0;

    public int MaxUpgradeLevel
    {
        get { return MAX_UPGRADE_LEVEL; }
    }

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

        if (GameObject.Find("ShrinkProgressBar"))
        {
            shinkProgressbar = GameObject.Find("ShrinkProgressBar").GetComponent<TextMeshProUGUI>();
        }
        if (GameObject.Find("MovementProgressBar"))
        {
            movementProgressbar = GameObject.Find("MovementProgressBar").GetComponent<TextMeshProUGUI>();
        }
        if (GameObject.Find("RangeProgressBar"))
        {
            rangeProgressbar = GameObject.Find("RangeProgressBar").GetComponent<TextMeshProUGUI>();
        }
        if (GameObject.Find("SpeedProgressBar"))
        {
            speedProgressbar = GameObject.Find("SpeedProgressBar").GetComponent<TextMeshProUGUI>();
        }
        
        //leftEnemy = 10;
        //leftEnemyText = GameObject.Find("leftEnemy").GetComponent<TextMeshProUGUI>();

        //level = 1;
        //levelText = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();

        // Update the UI to show the current values immediately
        UpdateCoinText();
        
        //UpdateLeftEnemyText();
        //UpdateLevelText();
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
    /*
    private void UpdateLeftEnemyText()
    {
        if (level < 3)
        {
            leftEnemyText.text = "Left Enemies: " + leftEnemy.ToString() + "/ 10";
        }
        else
        {
            leftEnemyText.enabled = false;
        }
    }
    
    public void ReduceLeftEnemy()
    {
        leftEnemy -= 1;
        if (leftEnemy == 0)
        {
            leftEnemy = 10;
            level += 1;
            UpdateLevelText();
        }
        UpdateLeftEnemyText();
    }
    
    private void UpdateLevelText()
    {
        if (level == 3)
        {
            levelText.text = "boss!";
        }
        else
        {
            levelText.text = "Level " + level.ToString();
        }

    }
    */

    //WaveCHECKER
    public void CheckWaveCompletion()
    {
        objectGenerator = FindObjectOfType<ObjectGenerator>();
        if (objectGenerator != null && isTutorial == 0)
        {
            if (objectGenerator.created_enemies.Count == 0 && !objectGenerator.IsWaveActive)
            {
                objectGenerator.OnWaveCleared();
            }
        }
        else
        {
            //Debug.LogError("ObjectGenerator instance not found! it is here");
        }
    }

    public int getTheCount(string type)
    {
        if (type.CompareTo("shrink") == 0)
        {
            return shrinkCount;
        }
        else if (type.CompareTo("speed") == 0)
        {
            return speedCount;
        }
        else if (type.CompareTo("range") == 0)
        {
            return rangeCount;
        }
        else if (type.CompareTo("movement") == 0)
        {
            return movementCount;
        }

        return 0;
    }


    public void UpgradePanelProgressBar(TextMeshProUGUI progressBar, int count)
    {
        string barText = "";
        int maxUpgrade = 5;
        for (int i = 0; i < maxUpgrade; i++)
        {
            if (i < count)
            {
                barText += "█ ";
            }
            else
            {
                barText += "░ ";
            }
        }
        progressBar.SetText(barText);
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
                if (bulletSpeedLevel >= MaxUpgradeLevel)
                {
                    Debug.Log("Bullet Speed is already at maximum level.");
                    return;
                }
                Debug.Log("this is the current value of the bullet_speed" + bulletAttack.bullet_speed);
                bulletAttack.bullet_speed += 2f;
                bulletSpeedLevel++;
                Debug.Log($"Bullet Speed Upgraded to Level {bulletSpeedLevel}");
                Debug.Log("Gun Speed Upgraded");

                speedCount++;
                UpgradePanelProgressBar(speedProgressbar, speedCount);
                break;

            case "Range":
                if (bulletRangeLevel >= MaxUpgradeLevel)
                {
                    Debug.Log("Bullet Range is already at maximum level.");
                    return;
                }
                bulletAttack.max_distance += 2f;
                bulletRangeLevel++;
                Debug.Log($"Bullet Range Upgraded to Level {bulletRangeLevel}");

                rangeCount++;
                UpgradePanelProgressBar(rangeProgressbar, rangeCount);
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
                if (movementSpeedLevel >= MaxUpgradeLevel)
                {
                    Debug.Log("Movement Speed is already at maximum level.");
                    return;
                }
                PlayerMovements pm = FindObjectOfType<PlayerMovements>();
                if (pm != null)
                {
                    pm.speed += 0.5f;
                    movementSpeedLevel++;
                    Debug.Log($"Movement Speed Upgraded to Level {movementSpeedLevel}");
                    movementCount++;
                    UpgradePanelProgressBar(movementProgressbar, movementCount);

                }
                else
                {
                    Debug.LogWarning("PlayerMovements component not found.");
                }
                break;

            case "ShrinkResistance":
                if (shrinkResistanceLevel >= MaxUpgradeLevel)
                {
                    Debug.Log("Shrink Resistance is already at maximum level.");
                    return;
                }
                Actions pa = FindObjectOfType<Actions>();
                if (pa != null)
                {
                    pa.shrink_rate -= 0.01f;
                    shrinkResistanceLevel++;
                    Debug.Log($"Shrink Resistance Upgraded to Level {shrinkResistanceLevel}");
                    shrinkCount++;
                    UpgradePanelProgressBar(shinkProgressbar, shrinkCount);

                }
                else
                {
                    Debug.LogWarning("Actions component not found.");
                }
                break;

            // If you have other stat upgrades, handle them here
            default:
                Debug.LogWarning($"Stat type '{statType}' is not recognized.");
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

    public void ResetUpgrades()
    {
        bulletSpeedLevel = 0;
        bulletRangeLevel = 0;
        shrinkResistanceLevel = 0;
        movementSpeedLevel = 0;
        shrinkCount = 0;
        movementCount = 0;
        rangeCount = 0;
        speedCount = 0;
        Debug.Log("Upgrade levels have been reset to initial values.");
    }
}