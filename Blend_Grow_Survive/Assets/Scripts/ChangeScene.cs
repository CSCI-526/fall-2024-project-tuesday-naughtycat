using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //public string sceneName;
    public GameObject startGame;
    public static void switchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void ResetCoin()
    {
        GameManager.instance.playerCoins = 0;
        ObjectGenerator.ins.created_enemies = null;

    }

    public static void addScene(string sceneName)
    {
        Time.timeScale = 0f;
        /*
        Debug.Log("PlayerEat.ins.bulletCount is : ++++ " + PlayerEat.ins.bulletCount);
        Destroy(GameObject.Find("Bullet(Clone)")); //delete the bullet generated by user clicking the help button
        if (PlayerEat.ins.bulletCount != 0)
        {
            PlayerEat.ins.bulletCount += 1;
            PlayerEat.ins.UpdateBulletText();
        }
        */
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        //Time.timeScale = 0f;
        ObjectGenerator.ins.StopGenerating();
    }
    public static void removeScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        Time.timeScale = 1f;
        if (ObjectGenerator.ins != null && ObjectGenerator.ins.isTutorial == 0)
        {
            ObjectGenerator.ins.StartGenerating();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
