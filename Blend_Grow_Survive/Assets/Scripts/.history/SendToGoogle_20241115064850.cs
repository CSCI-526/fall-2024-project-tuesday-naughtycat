using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField]
    private string URL = "https://docs.google.com/forms/d/e/1FAIpQLScmEefb4WP_tlhF4BV3FmBkw-o_JOC0ubvme369KOjwmviRvw/viewform";

    private long _sessionID;
    private int _totalEnemiesDefeated;
    private int _enemiesShot;
    private int _enemiesSwallow;
    private int _escape;
    float _survivalTime;
    bool _isWin;

    private void Awake()
    {
        _sessionID = System.DateTime.Now.Ticks;
    }
    public void SendMetrics(float survivalTime, bool isWin, int totalEnemiesDefeated, int enemiesShot, int enemiesSwallow, int escape, GameManager gameManager)
    {
        _totalEnemiesDefeated = totalEnemiesDefeated;
        _enemiesShot = enemiesShot;

        _enemiesSwallow = enemiesSwallow;
        _escape = escape;
        // _survivalTime = survivalTime;
        // _isWin = isWin;
        float shootingPercentage = 0f;
        float ratio = 0f;
        if (_totalEnemiesDefeated > 0)
        {
            shootingPercentage = ((float)_enemiesShot / _totalEnemiesDefeated) * 100f;
            ratio = (float)(_enemiesShot + 0.1f) / (float)(_enemiesSwallow + 0.1f);
        }
        StartCoroutine(PostMetrics(survivalTime, isWin, _sessionID.ToString(), _totalEnemiesDefeated.ToString(), _enemiesShot.ToString(), _enemiesSwallow.ToString(), _escape.ToString(), shootingPercentage.ToString("F2"), ratio.ToString("F2"), gameManager));
        //StartCoroutine(PostMetrics(survivalTime, isWin));
    }

    private IEnumerator PostMetrics(float survivalTime, bool isWin, string sessionID, string totalEnemiesDefeated, string enemiesShot, string enemiesSwallow, string escape, string shootingPercentage, string ratio, GameManager gameManager)
    {
        //calculating average win probability per upgrade type across all levels
        float winProbBS = gameManager.CalculateWinProbability("BulletSpeed");
        float winProbBR = gameManager.CalculateWinProbability("BulletRange");
        float winProbMS = gameManager.CalculateWinProbability("MovementSpeed");
        float winProbSR = gameManager.CalculateWinProbability("ShrinkResistance");

        // calculating win probability per level of each type of upgrade
        float winProbBS_Level0 = gameManager.Total_sessions_BS_level_0 > 0 ? (gameManager.Wins_BS_level_0 * 100f) / gameManager.Total_sessions_BS_level_0 : 0f;
        float winProbBS_Level1 = gameManager.Total_sessions_BS_level_1 > 0 ? (gameManager.Wins_BS_level_1 * 100f) / gameManager.Total_sessions_BS_level_1 : 0f;
        float winProbBS_Level2 = gameManager.Total_sessions_BS_level_2 > 0 ? (gameManager.Wins_BS_level_2 * 100f) / gameManager.Total_sessions_BS_level_2 : 0f;
        float winProbBS_Level3 = gameManager.Total_sessions_BS_level_3 > 0 ? (gameManager.Wins_BS_level_3 * 100f) / gameManager.Total_sessions_BS_level_3 : 0f;
        float winProbBS_Level4 = gameManager.Total_sessions_BS_level_4 > 0 ? (gameManager.Wins_BS_level_4 * 100f) / gameManager.Total_sessions_BS_level_4 : 0f;
        float winProbBS_Level5 = gameManager.Total_sessions_BS_level_5 > 0 ? (gameManager.Wins_BS_level_5 * 100f) / gameManager.Total_sessions_BS_level_5 : 0f;

        float winProbBR_Level0 = gameManager.Total_sessions_BR_level_0 > 0 ? (gameManager.Wins_BR_level_0 * 100f) / gameManager.Total_sessions_BR_level_0 : 0f;
        float winProbBR_Level1 = gameManager.Total_sessions_BR_level_1 > 0 ? (gameManager.Wins_BR_level_1 * 100f) / gameManager.Total_sessions_BR_level_1 : 0f;
        float winProbBR_Level2 = gameManager.Total_sessions_BR_level_2 > 0 ? (gameManager.Wins_BR_level_2 * 100f) / gameManager.Total_sessions_BR_level_2 : 0f;
        float winProbBR_Level3 = gameManager.Total_sessions_BR_level_3 > 0 ? (gameManager.Wins_BR_level_3 * 100f) / gameManager.Total_sessions_BR_level_3 : 0f;
        float winProbBR_Level4 = gameManager.Total_sessions_BR_level_4 > 0 ? (gameManager.Wins_BR_level_4 * 100f) / gameManager.Total_sessions_BR_level_4 : 0f;
        float winProbBR_Level5 = gameManager.Total_sessions_BR_level_5 > 0 ? (gameManager.Wins_BR_level_5 * 100f) / gameManager.Total_sessions_BR_level_5 : 0f;

        float winProbMS_Level0 = gameManager.Total_sessions_MS_level_0 > 0 ? (gameManager.Wins_MS_level_0 * 100f) / gameManager.Total_sessions_MS_level_0 : 0f;
        float winProbMS_Level1 = gameManager.Total_sessions_MS_level_1 > 0 ? (gameManager.Wins_MS_level_1 * 100f) / gameManager.Total_sessions_MS_level_1 : 0f;
        float winProbMS_Level2 = gameManager.Total_sessions_MS_level_2 > 0 ? (gameManager.Wins_MS_level_2 * 100f) / gameManager.Total_sessions_MS_level_2 : 0f;
        float winProbMS_Level3 = gameManager.Total_sessions_MS_level_3 > 0 ? (gameManager.Wins_MS_level_3 * 100f) / gameManager.Total_sessions_MS_level_3 : 0f;
        float winProbMS_Level4 = gameManager.Total_sessions_MS_level_4 > 0 ? (gameManager.Wins_MS_level_4 * 100f) / gameManager.Total_sessions_MS_level_4 : 0f;
        float winProbMS_Level5 = gameManager.Total_sessions_MS_level_5 > 0 ? (gameManager.Wins_MS_level_5 * 100f) / gameManager.Total_sessions_MS_level_5 : 0f;

        float winProbSR_Level0 = gameManager.Total_sessions_SR_level_0 > 0 ? (gameManager.Wins_SR_level_0 * 100f) / gameManager.Total_sessions_SR_level_0 : 0f;
        float winProbSR_Level1 = gameManager.Total_sessions_SR_level_1 > 0 ? (gameManager.Wins_SR_level_1 * 100f) / gameManager.Total_sessions_SR_level_1 : 0f;
        float winProbSR_Level2 = gameManager.Total_sessions_SR_level_2 > 0 ? (gameManager.Wins_SR_level_2 * 100f) / gameManager.Total_sessions_SR_level_2 : 0f;
        float winProbSR_Level3 = gameManager.Total_sessions_SR_level_3 > 0 ? (gameManager.Wins_SR_level_3 * 100f) / gameManager.Total_sessions_SR_level_3 : 0f;
        float winProbSR_Level4 = gameManager.Total_sessions_SR_level_4 > 0 ? (gameManager.Wins_SR_level_4 * 100f) / gameManager.Total_sessions_SR_level_4 : 0f;
        float winProbSR_Level5 = gameManager.Total_sessions_SR_level_5 > 0 ? (gameManager.Wins_SR_level_5 * 100f) / gameManager.Total_sessions_SR_level_5 : 0f;


        Debug.Log("this is the winProbBS_Level0" + winProbBS_Level0);
        Debug.Log("this is the winProbBR_Level0" + winProbBR_Level0);
        Debug.Log("this is the winProbMS_Level0" + winProbMS_Level0);
        Debug.Log("this is the winProbSR_Level0" + winProbSR_Level0);

        WWWForm form = new WWWForm();
        form.AddField("entry.1884265043", sessionID);
        form.AddField("entry.298833661", totalEnemiesDefeated);
        form.AddField("entry.1674722008", enemiesShot);
        form.AddField("entry.1281376075", shootingPercentage);
        form.AddField("entry.591181317", enemiesSwallow);
        form.AddField("entry.1675395676", ratio);
        form.AddField("entry.1893958430", escape);

        form.AddField("entry.85732061", gameManager.Win_number.ToString());
        form.AddField("entry.1488563677", gameManager.Lose_number.ToString());

        form.AddField("entry.557217919", gameManager.Total_sessions_BS_level_0.ToString());
        form.AddField("entry.1342663829", gameManager.Wins_BS_level_0.ToString());
        form.AddField("entry.1216866866", gameManager.Total_sessions_BS_level_1.ToString());
        form.AddField("entry.1694317104", gameManager.Wins_BS_level_1.ToString());
        form.AddField("entry.2003226039", gameManager.Total_sessions_BS_level_2.ToString());
        form.AddField("entry.1606912677", gameManager.Wins_BS_level_2.ToString());
        form.AddField("entry.349401136", gameManager.Total_sessions_BS_level_3.ToString());
        form.AddField("entry.1217989302", gameManager.Wins_BS_level_3.ToString());
        form.AddField("entry.1504563720", gameManager.Total_sessions_BS_level_4.ToString());
        form.AddField("entry.45344403", gameManager.Wins_BS_level_4.ToString());
        form.AddField("entry.1871984959", gameManager.Total_sessions_BS_level_5.ToString());
        form.AddField("entry.413984651", gameManager.Wins_BS_level_5.ToString());

        form.AddField("entry.2012468691", gameManager.Total_sessions_BR_level_0.ToString());
        form.AddField("entry.2011324107", gameManager.Wins_BR_level_0.ToString());
        form.AddField("entry.2066949797", gameManager.Total_sessions_BR_level_1.ToString());
        form.AddField("entry.1129305520", gameManager.Wins_BR_level_1.ToString());
        form.AddField("entry.881587315", gameManager.Total_sessions_BR_level_2.ToString());
        form.AddField("entry.1611779881", gameManager.Wins_BR_level_2.ToString());
        form.AddField("entry.772899570", gameManager.Total_sessions_BR_level_3.ToString());
        form.AddField("entry.443259054", gameManager.Wins_BR_level_3.ToString());
        form.AddField("entry.470892174", gameManager.Total_sessions_BR_level_4.ToString());
        form.AddField("entry.89784221", gameManager.Wins_BR_level_4.ToString());
        form.AddField("entry.959153928", gameManager.Total_sessions_BR_level_5.ToString());
        form.AddField("entry.2118746552", gameManager.Wins_BR_level_5.ToString());

        form.AddField("entry.22355104", gameManager.Total_sessions_MS_level_0.ToString());
        form.AddField("entry.375827971", gameManager.Wins_MS_level_0.ToString());
        form.AddField("entry.1188133047", gameManager.Total_sessions_MS_level_1.ToString());
        form.AddField("entry.640272403", gameManager.Wins_MS_level_1.ToString());
        form.AddField("entry.1623281794", gameManager.Total_sessions_MS_level_2.ToString());
        form.AddField("entry.1438769334", gameManager.Wins_MS_level_2.ToString());
        form.AddField("entry.1050698498", gameManager.Total_sessions_MS_level_3.ToString());
        form.AddField("entry.1991540730", gameManager.Wins_MS_level_3.ToString());
        form.AddField("entry.1240870643", gameManager.Total_sessions_MS_level_4.ToString());
        form.AddField("entry.6956729", gameManager.Wins_MS_level_4.ToString());
        form.AddField("entry.822388275", gameManager.Total_sessions_MS_level_5.ToString());
        form.AddField("entry.725028977", gameManager.Wins_MS_level_5.ToString());

        form.AddField("entry.322429898", gameManager.Total_sessions_SR_level_0.ToString());
        form.AddField("entry.1370134867", gameManager.Wins_SR_level_0.ToString());
        form.AddField("entry.1112930783", gameManager.Total_sessions_SR_level_1.ToString());
        form.AddField("entry.432346338", gameManager.Wins_SR_level_1.ToString());
        form.AddField("entry.446405374", gameManager.Total_sessions_SR_level_2.ToString());
        form.AddField("entry.149810283", gameManager.Wins_SR_level_2.ToString());
        form.AddField("entry.1322549632", gameManager.Total_sessions_SR_level_3.ToString());
        form.AddField("entry.552395275", gameManager.Wins_SR_level_3.ToString());
        form.AddField("entry.328265566", gameManager.Total_sessions_SR_level_4.ToString());
        form.AddField("entry.660867920", gameManager.Wins_SR_level_4.ToString());
        form.AddField("entry.1737689388", gameManager.Total_sessions_SR_level_5.ToString());
        form.AddField("entry.258674584", gameManager.Wins_SR_level_5.ToString());

        // average win probability per upgrade type across all levels
        form.AddField("entry.920775684", winProbBS.ToString("F2"));
        form.AddField("entry.397920053", winProbBR.ToString("F2"));
        form.AddField("entry.172393391", winProbMS.ToString("F2"));
        form.AddField("entry.1762443621", winProbSR.ToString("F2"));

        // win probability per level of one type of upgrade
        form.AddField("entry.1708779149", winProbBS_Level0.ToString("F2"));
        form.AddField("entry.1300144322", winProbBS_Level1.ToString("F2"));
        form.AddField("entry.824982932", winProbBS_Level2.ToString("F2"));
        form.AddField("entry.969123627", winProbBS_Level3.ToString("F2"));
        form.AddField("entry.1686677321", winProbBS_Level4.ToString("F2"));
        form.AddField("entry.1865476006", winProbBS_Level5.ToString("F2"));

        form.AddField("entry.264827039", winProbBR_Level0.ToString("F2"));
        form.AddField("entry.981031203", winProbBR_Level1.ToString("F2"));
        form.AddField("entry.1821153898", winProbBR_Level2.ToString("F2"));
        form.AddField("entry.2081795298", winProbBR_Level3.ToString("F2"));
        form.AddField("entry.16190821", winProbBR_Level4.ToString("F2"));
        form.AddField("entry.1342111069", winProbBR_Level5.ToString("F2"));

        form.AddField("entry.1513219487", winProbMS_Level0.ToString("F2"));
        form.AddField("entry.369999648", winProbMS_Level1.ToString("F2"));
        form.AddField("entry.1779907303", winProbMS_Level2.ToString("F2"));
        form.AddField("entry.679457700", winProbMS_Level3.ToString("F2"));
        form.AddField("entry.1497329997", winProbMS_Level4.ToString("F2"));
        form.AddField("entry.1166658289", winProbMS_Level5.ToString("F2"));

        form.AddField("entry.357403862", winProbSR_Level0.ToString("F2"));
        form.AddField("entry.1182268923", winProbSR_Level1.ToString("F2"));
        form.AddField("entry.1660312273", winProbSR_Level2.ToString("F2"));
        form.AddField("entry.1044719861", winProbSR_Level3.ToString("F2"));
        form.AddField("entry.767772997", winProbSR_Level4.ToString("F2"));
        form.AddField("entry.833627712", winProbSR_Level5.ToString("F2"));

        // Track how long the player survived each round, and whether they won or lost
        form.AddField("entry.2043309709", survivalTime);
        form.AddField("entry.749494884", isWin ? "Win" : "Lose");

        // Send the form
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Form submission error: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Form submission complete!");
                Debug.Log("Response: " + www.downloadHandler.text);
            }
        }
    }
}
