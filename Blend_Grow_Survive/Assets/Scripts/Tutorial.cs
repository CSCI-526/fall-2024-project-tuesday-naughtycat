using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject food;
    public Text hintText;
    public GameObject ammo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
                hintText.text = "Now there is an enemy towards you.\nLeft click to shot ammo or swallow others when your size is larger.";

                List<GameObject> created_enemies = ObjectGenerator.ins.getEnemy();
                GameObject firstEnemy = created_enemies[0];
                firstEnemy.gameObject.SetActive(true);
                //Debug.Log("enemy tag: " + created_enemies[0].gameObject.tag);
                if (!firstEnemy)
                {
                    hintText.text = "You have destroyed an enemy.\nAdded 10 EXP. More enemies are coming...";
                }
            }
        }

        
    }
}
