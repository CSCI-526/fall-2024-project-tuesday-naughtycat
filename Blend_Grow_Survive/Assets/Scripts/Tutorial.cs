using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public Text hintText;
    public Image hintImage;
    public Image hintImageEscape1;
    public Image hintImageEscape2;
    public Image hintImageMouse;
    public Image hintImageB;
    public GameObject tutorialAmmo, upgradePanel, tutorialPlayer, hpText, wall1, wall2;

    private int status = 0;
    private GameObject firstEnemy, secondEnemy, thirdEnemy, fourthEnemy, fifthEnemy;
    private GameObject food1, food2, food3;
    private bool foodEnabled = false;

    private GameObject player;
    public TextMeshProUGUI arrow;

    void Start()
    {
        tutorialAmmo = GameObject.Find("newAmmo");
        tutorialAmmo.SetActive(false);
        hintImageEscape1.gameObject.SetActive(false);
        hintImageEscape2.gameObject.SetActive(false);
        hintImageMouse.gameObject.SetActive(false);
        hintImageB.gameObject.SetActive(false);
        upgradePanel = GameObject.Find("UpgradePanel");

        // Locate and disable preset food items
        food1 = GameObject.Find("Food");
        food2 = GameObject.Find("Food (1)");
        food3 = GameObject.Find("Food (2)");
        food1.SetActive(false);
        food2.SetActive(false);
        food3.SetActive(false);


        secondEnemy = GameObject.Find("Enemy");
        secondEnemy.SetActive(false);

        DisplayHintsAndEnableFood();
        arrow.gameObject.SetActive(false);
    }

    void Update()
    {
        switch (status)
        {
            case 0:
                CheckFoodConsumption();
                break;
            case 1:
                ActivateFirstEnemy();
                break;
            case 2:
                CheckFirstEnemyDefeat();
                break;
            case 3:
                ShowAmmoHint();
                break;
            case 4:
                ActivateSecondEnemy();
                break;
            case 5:
                CheckSecondEnemyDefeat();
                break;
            case 6:
                WaitAndDisplayEscapeMessage();
                break;
            case 7:
                ActivateOtherEnemies();
                break;
            case 8:
                ResumeAfterEscape();
                break;
            case 9:
                CheckAllEnemiesDefeated();
                break;
            case 10:
                PromptUpgrade();
                break;
            case 11:
                WaitForUpgrade();
                break;
            case 12:
                PromptCloseUpgrade();
                break;
            default:
                EndTutorial();
                break;
        }
    }

    private void DisplayHintsAndEnableFood()
    {
        hintImage.gameObject.SetActive(true);
        food1.SetActive(true);
        food2.SetActive(true);
        food3.SetActive(true);
        foodEnabled = true; // Set foodEnabled flag to true after enabling food
    }

    private void CheckFoodConsumption()
    {
        if (foodEnabled && !GameObject.Find("Food") && !GameObject.Find("Food (1)") && !GameObject.Find("Food (2)"))
        {
            hintImage.gameObject.SetActive(false);
            status++;
            DisplayHint("Incoming! Swallow it!");
        }
    }

    private void ActivateFirstEnemy()
    {
        firstEnemy = ObjectGenerator.ins.getEnemy()[0];
        firstEnemy.SetActive(true);
        status++;
    }

    private void CheckFirstEnemyDefeat()
    {
        if (!firstEnemy)
        {
            DisplayHint("Great! Now grab the ammo!");
            tutorialAmmo.SetActive(true);
            wall1.SetActive(false);
            wall2.SetActive(false);

            status++; // Move to the next state to wait for escape
        }
    }

    private void WaitAndDisplayEscapeMessage()
    {
        arrow.gameObject.SetActive(true);
        Time.timeScale = 0f;
        //yield return new WaitForSecondsRealtime(0.1f);
        hintImageEscape1.gameObject.SetActive(true);
        hintImageEscape2.gameObject.SetActive(true);
        DisplayHint("                                                     ESCAPE!");
        status++;
        // escapeArrow.SetActive(true); // Show escape arrow if needed
    }

    private void ResumeAfterEscape()
    {
        // Wait for the player to press Space to resume
        if (Input.GetKeyDown(KeyCode.Space))
        {
            arrow.gameObject.SetActive(false);
            hintImageEscape1.gameObject.SetActive(false);
            hintImageEscape2.gameObject.SetActive(false);
            DisplayHint("");
            Time.timeScale = 1f; // Resume game
            status++;
        }
    }

    private void ShowAmmoHint()
    {
        if (!GameObject.Find("ammo") && !GameObject.Find("newAmmo1") && !GameObject.Find("newAmmo2") && !GameObject.Find("newAmmo3"))
        {
            hintImageMouse.gameObject.SetActive(true);
            DisplayHint("            SHOOT!");
            status++;
        }
    }

    private void ActivateSecondEnemy()
    {
        secondEnemy.SetActive(true);
        status++;
    }

    private void CheckSecondEnemyDefeat()
    {
        if (!secondEnemy)
        {
            hintImageMouse.gameObject.SetActive(false);
            DisplayHint("");
            status++;
        }
    }

    private void ActivateOtherEnemies()
    {
        var enemies = ObjectGenerator.ins.getEnemy();
        if (enemies.Count >= 3)
        {
            Vector2 playerPosition = GameObject.FindWithTag("Player").transform.position;
            Vector2 offset1 = new Vector2(-2, -2);
            Vector2 offset2 = new Vector2(2, -2);
            Vector2 offset3 = new Vector2(-2, 2);

            // Calculate the positions based on player's position plus offsets
            Vector2 Position3 = playerPosition + offset1;
            Vector2 Position4 = playerPosition + offset2;
            Vector2 Position5 = playerPosition + offset3;
            thirdEnemy = enemies[0];
            fourthEnemy = enemies[1];
            fifthEnemy = enemies[2];
            thirdEnemy.transform.position = Position3;
            fourthEnemy.transform.position = Position4;
            fifthEnemy.transform.position = Position5;
            thirdEnemy.SetActive(true);
            fourthEnemy.SetActive(true);
            fifthEnemy.SetActive(true);
            status++;
        }
    }

    private void CheckAllEnemiesDefeated()
    {
        if (!thirdEnemy && !fourthEnemy && !fifthEnemy)
        {
            hintImageB.gameObject.SetActive(true);
            DisplayHint("                          Open the upgrade system");
            status++;
        }
    }

    private void PromptUpgrade()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            hintImageB.gameObject.SetActive(false);
            DisplayHint("Upgrade one skill - 10 coins");
            status++;
        }
    }

    private void WaitForUpgrade()
    {
        if (GameManager.instance.playerCoins == 0)
        {
            GameManager.instance.ShowDeductedCoins(10);
            hintImageB.gameObject.SetActive(true);
            DisplayHint("Close it");
            status++;
        }
    }

    private void PromptCloseUpgrade()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            status++;
        }
    }

    private void EndTutorial()
    {
        /*
        tutorialPlayer = GameObject.Find("Player");
        tutorialPlayer.SetActive(false);
        hpText = GameObject.Find("hpText");
        hpText.SetActive(false);
        DisplayHint("Tutorial complete! Click 'Back' to return to main menu.");
        */
        GameManager.instance.ResetUpgrades();
        GameManager.instance.ResetHP();
        GameManager.instance.ResetBulletProperties();
        ChangeScene.switchScene("MainScene");
    }

    private void DisplayHint(string message)
    {
        hintText.text = message;
    }
}