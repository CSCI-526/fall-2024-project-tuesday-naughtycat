using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text hintText;
    public GameObject ammo, tutorialAmmo, upgradePanel;

    private int status = 0;
    private GameObject firstEnemy, secondEnemy, thirdEnemy, fourthEnemy, fifthEnemy;
    private bool hasUsedEscape = false;

    void Start()
    {
        ammo = GameObject.Find("ammo");
        ammo.SetActive(false);
        tutorialAmmo = GameObject.Find("newAmmo");
        tutorialAmmo.SetActive(false);
        upgradePanel = GameObject.Find("UpgradePanel"); // Assuming you have an upgrade panel in the scene
        upgradePanel.SetActive(false); // Initially hide the upgrade panel
        DisplayHint("Use WSAD to find food!");
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
            default:
                EndTutorial();
                break;
        }
    }

    private void CheckFoodConsumption()
    {
        if (!GameObject.Find("Food") && !GameObject.Find("Food (1)") && !GameObject.Find("Food (2)"))
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
            DisplayHint("You gained 10 EXP. Press spacebar to escape!");
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
            upgradePanel.SetActive(true);
            DisplayHint("Upgrade one skill for 10 points.");
            status++;
        }
    }

    private void WaitForUpgrade()
    {
        DisplayHint("Upgrade one skill for 10 points.");

        if (GameManager.instance.playerCoins == 0)
        {
            upgradePanel.SetActive(false);
            status++;
        }
    }

    private void EndTutorial()
    {
        DisplayHint("Tutorial complete! Click 'Back' to return to main menu.");
    }

    private void DisplayHint(string message)
    {
        hintText.text = message;
    }
}