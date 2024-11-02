using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text hintText;
    public GameObject tutorialAmmo;

    private int status = 0;
    private GameObject firstEnemy, secondEnemy, thirdEnemy;

    void Start()
    {
        tutorialAmmo = GameObject.Find("newAmmo");
        tutorialAmmo.SetActive(false); // Initially hide tutorial ammo
        DisplayHint("Eat food to grow!");
    }

    void Update()
    {
        switch (status)
        {
            case 0:
                CheckFoodConsumption();
                break;
            case 1:
                ShowAmmoHint();
                break;
            case 2:
                ActivateFirstEnemy();
                break;
            case 3:
                CheckFirstEnemyDefeat();
                break;
            case 4:
                ActivateSecondAndThirdEnemies();
                break;
            case 5:
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
            DisplayHint("Grab the ammo ahead!");
        }
    }

    private void ShowAmmoHint()
    {
        if (!GameObject.Find("ammo"))
        {
            DisplayHint("Enemy ahead! Left-click to shoot or swallow if you're larger.");
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
            DisplayHint("Great! You gained 10 EXP. More enemies ahead...");
            tutorialAmmo.SetActive(true); // Show extra ammo after first enemy defeat
            status++;
        }
    }

    private void ActivateSecondAndThirdEnemies()
    {
        var enemies = ObjectGenerator.ins.getEnemy();
        if (enemies.Count >= 2)
        {
            secondEnemy = enemies[0];
            thirdEnemy = enemies[1];
            secondEnemy.SetActive(true);
            thirdEnemy.SetActive(true);
            status++;
        }
    }

    private void CheckAllEnemiesDefeated()
    {
        if (!secondEnemy && !thirdEnemy)
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
