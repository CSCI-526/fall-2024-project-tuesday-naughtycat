using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossUIController : MonoBehaviour
{
    public Text bossMessage; // Reference to the BossMessage Text element

    void Start()
    {
        bossMessage.gameObject.SetActive(false); // Ensure the message is hidden at the start
    }

    public void ShowBossMessage()
    {
        StartCoroutine(DisplayBossMessage());
    }

    private IEnumerator DisplayBossMessage()
    {
        bossMessage.gameObject.SetActive(true); // Show the message
        yield return new WaitForSeconds(5); // Wait for 5 seconds
        bossMessage.gameObject.SetActive(false); // Hide the message
    }
}