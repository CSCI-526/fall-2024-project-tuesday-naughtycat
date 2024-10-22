using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private string URL;

    private long _sessionID;
    private int _totalEnemiesDefeated;
    private int _enemiesShot;

    private void Awake()
    {
        // Assign sessionID to identify playtests
        _sessionID = System.DateTime.Now.Ticks;
    }

    // This method will be called when the round ends or at appropriate points
    public void SendMetrics(int totalEnemiesDefeated, int enemiesShot)
    {
        _totalEnemiesDefeated = totalEnemiesDefeated;
        _enemiesShot = enemiesShot;

        // Calculate the shooting percentage
        float shootingPercentage = 0f;
        if (_totalEnemiesDefeated > 0)
        {
            shootingPercentage = ((float)_enemiesShot / _totalEnemiesDefeated) * 100f;
        }

        // Convert metrics to strings and send them
        StartCoroutine(PostMetrics(_sessionID.ToString(), _totalEnemiesDefeated.ToString(), _enemiesShot.ToString(), shootingPercentage.ToString("F2")));
    }

    private IEnumerator PostMetrics(string sessionID, string totalEnemiesDefeated, string enemiesShot, string shootingPercentage)
    {
        // Create the form and add fields
        WWWForm form = new WWWForm();
        form.AddField("entry.1234567890", sessionID);  // Replace with actual Session ID entry
        form.AddField("entry.0987654321", totalEnemiesDefeated);  // Replace with actual Total Enemies Defeated entry
        form.AddField("entry.1122334455", enemiesShot);  // Replace with actual Enemies Shot entry
        form.AddField("entry.9876543210", shootingPercentage);  // Replace with actual Shooting Percentage entry

        // Send the form
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Form submission complete!");
            }
        }
    }
}
