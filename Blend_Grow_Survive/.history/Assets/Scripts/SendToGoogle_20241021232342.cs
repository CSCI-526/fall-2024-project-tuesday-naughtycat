using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private string "https://docs.google.com/forms/u/0/d/e/1FAIpQLScmEefb4WP_tlhF4BV3FmBkw-o_JOC0ubvme369KOjwmviRvw/formResponse"

    private long _sessionID;
    private int _totalEnemiesDefeated;
    private int _enemiesShot;

    private void Awake()
    {
        _sessionID = System.DateTime.Now.Ticks;
    }
    public void SendMetrics(int totalEnemiesDefeated, int enemiesShot)
    {
        _totalEnemiesDefeated = totalEnemiesDefeated;
        _enemiesShot = enemiesShot;

        float shootingPercentage = 0f;
        if (_totalEnemiesDefeated > 0)
        {
            shootingPercentage = ((float)_enemiesShot / _totalEnemiesDefeated) * 100f;
        }
        StartCoroutine(PostMetrics(_sessionID.ToString(), _totalEnemiesDefeated.ToString(), _enemiesShot.ToString(), shootingPercentage.ToString("F2")));
    }

    private IEnumerator PostMetrics(string sessionID, string totalEnemiesDefeated, string enemiesShot, string shootingPercentage)
    {

        WWWForm form = new WWWForm();
        form.AddField("entry.1884265043", sessionID);
        form.AddField("entry.298833661", totalEnemiesDefeated);
        form.AddField("entry.1674722008", enemiesShot);
        form.AddField("entry.1281376075", shootingPercentage);
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
