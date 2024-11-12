using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField]
    private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScmEefb4WP_tlhF4BV3FmBkw-o_JOC0ubvme369KOjwmviRvw/formResponse";

    private long _sessionID;
    private int _totalEnemiesDefeated;
    private int _enemiesShot;
    private int _enemiesSwallow;
    private int _escape;

    private void Awake()
    {
        _sessionID = System.DateTime.Now.Ticks;
    }
    public void SendMetrics(int totalEnemiesDefeated, int enemiesShot, int enemiesSwallow, int escape)
    {
        _totalEnemiesDefeated = totalEnemiesDefeated;
        _enemiesShot = enemiesShot;

        _enemiesSwallow = enemiesSwallow;
        _escape = escape;
        float shootingPercentage = 0f;
        float ratio = 0f;
        if (_totalEnemiesDefeated > 0)
        {
            shootingPercentage = ((float)_enemiesShot / _totalEnemiesDefeated) * 100f;
            ratio = (float)(_enemiesShot + 0.1f) / (float)(_enemiesSwallow + 0.1f);
        }
        StartCoroutine(PostMetrics(_sessionID.ToString(), _totalEnemiesDefeated.ToString(), _enemiesShot.ToString(), _enemiesSwallow.ToString(), _escape.ToString(), shootingPercentage.ToString("F2"), ratio.ToString("F2")));
    }

    private IEnumerator PostMetrics(string sessionID, string totalEnemiesDefeated, string enemiesShot, string enemiesSwallow, string escape, string shootingPercentage, string ratio)
    {

        WWWForm form = new WWWForm();
        form.AddField("entry.1884265043", sessionID);
        form.AddField("entry.298833661", totalEnemiesDefeated);
        form.AddField("entry.1674722008", enemiesShot);
        form.AddField("entry.1281376075", shootingPercentage);
        form.AddField("entry.591181317", enemiesSwallow);
        form.AddField("entry.1675395676", ratio);
        form.AddField("entry.1893958430", escape);

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
