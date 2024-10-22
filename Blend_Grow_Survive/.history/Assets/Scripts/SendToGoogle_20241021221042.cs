using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private string URL;

    private long _sessionID;
    private float _timeSurvived;
    private int _enemiesDefeated;

    private void Awake()
    {
        // Assign sessionID to identify playtests
        _sessionID = System.DateTime.Now.Ticks;
    }

    public void SendMetrics(float timeSurvived, int enemiesDefeated)
    {
        // Assign metrics to variables
        _timeSurvived = timeSurvived;
        _enemiesDefeated = enemiesDefeated;

        // Convert metrics to strings and send them
        StartCoroutine(PostMetrics(_sessionID.ToString(), _timeSurvived.ToString(), _enemiesDefeated.ToString()));
    }

    private IEnumerator PostMetrics(string sessionID, string timeSurvived, string enemiesDefeated)
    {
        // Create the form and add fields
        WWWForm form = new WWWForm();
        form.AddField("entry.1234567890", sessionID);  // Replace with actual Session ID entry
        form.AddField("entry.0987654321", timeSurvived);  // Replace with actual Time Survived entry
        form.AddField("entry.1122334455", enemiesDefeated);  // Replace with actual Enemies Defeated entry

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
