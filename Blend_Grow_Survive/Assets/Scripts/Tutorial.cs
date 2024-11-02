using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text hintText;
    public GameObject ammo, tutorialAmmo;

    private int status = 0;
    private GameObject firstEnemy, secondEnemy, thirdEnemy, fourthEnemy, fifthEnemy;

    void Start()
    {
        ammo = GameObject.Find("ammo");
        ammo.SetActive(false);
        tutorialAmmo = GameObject.Find("newAmmo");
        tutorialAmmo.SetActive(false); // Initially hide tutorial ammo
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
                ShowAmmoHint();
                break;
            case 4:
                ActivateSecondEnemy();
                break;
            case 5:
                CheckSecondEnemyDefeat();
                break;
            case 6:
                ActivateOtherEnemies();
                break;
            case 7:
                CheckAllEnemiesDefeated();
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

    private void ShowAmmoHint()
    {
        if (!GameObject.Find("ammo"))
        {
            DisplayHint("Enemy ahead! Left-click to shoot!");
            status++;
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
            DisplayHint("Great! You gained 10 EXP. Now grab the ammo!");
            ammo.SetActive(true);
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
            tutorialAmmo.SetActive(true); // Show extra ammo after first enemy defeat
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
