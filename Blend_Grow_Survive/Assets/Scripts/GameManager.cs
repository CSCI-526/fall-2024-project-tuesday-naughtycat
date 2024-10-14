using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerHP = 100;
    public int playerEXP = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to add HP
    public void AddHP(int hpAmount)
    {
        playerHP += hpAmount;
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
    public void AddEXP(int expAmount)
    {
        playerEXP += expAmount;
        playerEXP = Mathf.Clamp(playerEXP, 0, 100);
        Debug.Log("Current EXP: " + playerEXP);
    }

    // Reset EXP when restarting the game
    public void ResetEXP()
    {
        playerEXP = 0;
        Debug.Log("EXP reset to: " + playerEXP);
    }
}