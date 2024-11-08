using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text hintText;
    public GameObject ammo, tutorialAmmo, upgradePanel, tutorialPlayer, hpText;

    private int status = 0;
    private GameObject firstEnemy, secondEnemy, thirdEnemy, fourthEnemy, fifthEnemy;
    private GameObject food1, food2, food3;
    private bool hasUsedEscape = false;
    private bool foodEnabled = false;

    void Start()
    {
        ammo = GameObject.Find("ammo");
        ammo.SetActive(false);
        tutorialAmmo = GameObject.Find("newAmmo");
        tutorialAmmo.SetActive(false);
        upgradePanel = GameObject.Find("UpgradePanel");

        // Locate and disable preset food items
        food1 = GameObject.Find("Food");
        food2 = GameObject.Find("Food (1)");
        food3 = GameObject.Find("Food (2)");
        food1.SetActive(false);
        food2.SetActive(false);
        food3.SetActive(false);

        StartCoroutine(DisplayHintsAndEnableFood());
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
                CheckEscapePress();
                break;
            case 4:
                RequireEscapeFunction();
                break;
            case 5:
                ShowAmmoHint();
                break;
            case 6:
                ActivateSecondEnemy();
                break;
            case 7:
                CheckSecondEnemyDefeat();
                break;
            case 8:
                ActivateOtherEnemies();
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

    private IEnumerator DisplayHintsAndEnableFood()
    {
        // Display "W S A D" hint for 3 seconds
        DisplayHint("W  S  A  D");
        yield return new WaitForSeconds(3f);

        // Display "You are hungry!" hint and enable food
        DisplayHint("You are hungry!");
        food1.SetActive(true);
        food2.SetActive(true);
        food3.SetActive(true);
        foodEnabled = true; // Set foodEnabled flag to true after enabling food
    }

    private void CheckFoodConsumption()
    {
        if (foodEnabled && !GameObject.Find("Food") && !GameObject.Find("Food (1)") && !GameObject.Find("Food (2)"))
        {
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
            DisplayHint("+10 EXP");
            status++; // Move to the next state to wait for escape
        }
    }

    private void CheckEscapePress()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasUsedEscape = true;
            DisplayHint("Great! Now grab the ammo!");
            status++;
        }
    }

    private void RequireEscapeFunction()
    {
        if (hasUsedEscape)
        {
            ammo.SetActive(true);
            status++;
        }
    }

    private void ShowAmmoHint()
    {
        if (!GameObject.Find("ammo"))
        {
            DisplayHint("Enemy ahead! Left-click to shoot!");
            status++;
        }
    }

    private void ActivateSecondEnemy()
    {
        secondEnemy = ObjectGenerator.ins.getEnemy()[1];
        secondEnemy.SetActive(true);
        status++;
    }

    private void CheckSecondEnemyDefeat()
    {
        if (!secondEnemy)
        {
            DisplayHint("Perfect! Now use the combo of swallow and shoot!");
            tutorialAmmo.SetActive(true);
            status++;
        }
    }

    private void ActivateOtherEnemies()
    {
        var enemies = ObjectGenerator.ins.getEnemy();
        if (enemies.Count >= 3)
        {
            thirdEnemy = enemies[0];
            fourthEnemy = enemies[1];
            fifthEnemy = enemies[2];
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
            DisplayHint("Press 'B' to open the upgrade system.");
            status++;
        }
    }

    private void PromptUpgrade()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            DisplayHint("Upgrade one skill for 10 points.");
            status++;
        }
    }

    private void WaitForUpgrade()
    {
        if (GameManager.instance.playerCoins == 0)
        {
            DisplayHint("Press 'B' to close the upgrade system.");
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