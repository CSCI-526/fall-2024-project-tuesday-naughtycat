using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject food;
    public Text hintText;
    public GameObject enemy;
    public GameObject ammo;
    // Start is called before the first frame update
    void Start()
    {
        //ammo.gameObject.SetActive(false);
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
            hintText.text = "Go to the ammo. ->";
            //ammo.gameObject.SetActive(true);
            if (GameObject.Find("ammo") == null)
            {
                hintText.text = "Now there is an enemy.";
                enemy.gameObject.SetActive(true);
            }
        }

        
    }
}
