using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossMessageController : MonoBehaviour
{
    public Text bossMessage; // Reference to the BossMessage Text element

    void Start()
    {
        bossMessage.gameObject.SetActive(false); // Ensure the message is hidden at the start
    }

    public void ShowBossMessage()
    {
        Debug.Log("ShowBossMessage called");
        StartCoroutine(DisplayBossMessage());
    }

    private IEnumerator DisplayBossMessage()
    {
        Debug.Log("DisplayBossMessage coroutine started");
        bossMessage.gameObject.SetActive(true); // Show the message
        yield return new WaitForSeconds(5); // Wait for 5 seconds
        bossMessage.gameObject.SetActive(false); // Hide the message
        Debug.Log("DisplayBossMessage coroutine ended");
    }
}