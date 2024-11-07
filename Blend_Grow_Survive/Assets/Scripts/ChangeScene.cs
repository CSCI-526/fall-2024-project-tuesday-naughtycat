using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //public string sceneName;

    public static void switchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void ResetCoin()
    {
        GameManager.instance.playerCoins = 0;
        ObjectGenerator.ins.created_enemies = null;

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
