using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject food;
    public Text hintText;
    public GameObject ammo;

    private int status = 0;
    private GameObject firstEnemy, secondEnemy, thirdEnemy;
    private GameObject tutorialAmmo;

    // Start is called before the first frame update
    void Start()
    {
        /*
        tutorialAmmo = GameObject.FindGameObjectsWithTag("tutorialAmmo");
        for (int i = 0; i < tutorialAmmo.Length; i++)
        {
            tutorialAmmo[i].gameObject.SetActive(false);
        }
        */

        //Debug.Log("Type is: " + GameObject.Find("newAmmo").GetType());
        tutorialAmmo = GameObject.Find("newAmmo");
        tutorialAmmo.SetActive(false);
        //Debug.Log("ammo: " + GameObject.FindGameObjectsWithTag("Ammo").Length);

    }

    // Update is called once per frame
    void Update()
    {
        if (status == 0)
        {
            if (GameObject.Find("Food") == null || GameObject.Find("Food (1)") == null || GameObject.Find("Food (2)") == null)
            {
                hintText.text = "Your size has been grown.";
            }

            if (GameObject.Find("Food") == null && GameObject.Find("Food (1)") == null && GameObject.Find("Food (2)") == null)
            {

                hintText.text = "Go to the ammo. ->\nUse the spacebar to speedup.";

                if (GameObject.Find("ammo") == null)
                {
                    //ObjectGenerator.ins.changeMaxEnemy(1);
                    //Debug.Log("max enemy is: " + ObjectGenerator.ins.getMaxEnemy());
                    hintText.text = "Now there is an enemy towards you.\nLeft click to shot ammo or swallow others when your size is larger.";

                    List<GameObject> created_enemies = ObjectGenerator.ins.getEnemy();
                    firstEnemy = created_enemies[0];
                    firstEnemy.gameObject.SetActive(true);
                    //Debug.Log(created_enemies.Count + "-- created_enemies.Count");
                    status = 1;
                }
            }

        }

        if (status == 1)
        {
            if (!firstEnemy)
            {
                status = 2;
                hintText.text = "You have destroyed an enemy.\nAdded 10 EXP. More enemies are coming...";
                /*
                for (int i = 0; i < tutorialAmmo.Length; i++)
                {
                    tutorialAmmo[i].gameObject.SetActive(true);
                }
                //PlayerEat.ins.UpdateTutorialAmmo();
                */
                tutorialAmmo.SetActive(true);
            }
        }

        //first enemy is over
        if (status == 2)
        {
            
            List<GameObject> created_enemies = ObjectGenerator.ins.getEnemy();
            secondEnemy = created_enemies[0];
            secondEnemy.gameObject.SetActive(true);
            thirdEnemy = created_enemies[1];
            thirdEnemy.gameObject.SetActive(true);
            Debug.Log("third enemy: " + thirdEnemy);
            status = 3;
        }

        if (status == 3)
        {
            if (!secondEnemy && !thirdEnemy)
            {
                hintText.text = "Tutorial ends here! Click 'back' to main page.";
                status = 4;
            }
        }



    }
}
