using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradePanelToggle : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject upgradePanel;               // Reference to the Upgrade Panel
    public TextMeshProUGUI feedbackText;      // Reference to the Button's Text (optional for feedback)
    public UpgradeShop upgradeShop; // Assign this in the inspector or find it in code

    //[Header("Audio Feedback")]
    //public AudioSource audioSource;               // AudioSource component for sound effects
    //public AudioClip openSound;                   // Sound when opening the panel
    //public AudioClip closeSound;                  // Sound when closing the panel

    //[Header("Animation Settings")]
    public float animationDuration = 0.3f;        // Duration of the panel's open/close animation

    private bool isPanelOpen = false;             // Tracks the current state of the panel

    public bool IsPanelOpen() // Create a public method to access isPanelOpen
    {
        return isPanelOpen;
    }

    void Start()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);        // Ensures that the panel is initially hidden
        }
        if (upgradeShop == null)
        {
            upgradeShop = FindObjectOfType<UpgradeShop>();
        }
    }

    void Update()
    {
        // Listen for the 'B' key press
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleUpgradePanel();
        }
    }

    /// <summary>
    /// Toggles the visibility of the Upgrade Panel and pauses/resumes the game.
    /// </summary>
    public void ToggleUpgradePanel()
    {
        isPanelOpen = !isPanelOpen;
        GameManager.instance.isUpgradePanelOpen = isPanelOpen;
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(isPanelOpen);
            GameManager.instance.UpgradePanelProgressBar(GameManager.instance.speedProgressbar, GameManager.instance.getTheCount("speed"));
            GameManager.instance.UpgradePanelProgressBar(GameManager.instance.shinkProgressbar, GameManager.instance.getTheCount("shrink"));
            GameManager.instance.UpgradePanelProgressBar(GameManager.instance.movementProgressbar, GameManager.instance.getTheCount("movement"));
            GameManager.instance.UpgradePanelProgressBar(GameManager.instance.rangeProgressbar, GameManager.instance.getTheCount("range"));
            GameManager.instance.UpgradePanelProgressBar(GameManager.instance.damageProgressbar, GameManager.instance.getTheCount("damage"));
            //Debug.Log("speed Count: -----  " + GameManager.instance.getTheCount("speed"));
        }

        // Pause or resume the game
        if (isPanelOpen)
        {
            if (FindObjectOfType<ObjectGenerator>().levelText)
            {
                FindObjectOfType<ObjectGenerator>().levelText.enabled = false;
            }
            GameManager.instance.HideBButtonReminder();
            GameManager.instance.ToggleBlur(true);
            PauseGame();
        }
        else
        {
            GameManager.instance.ToggleBlur(false);
            ResumeGame();
            GameManager.instance.CheckAndShowBButtonReminder();

            if (upgradeShop != null)
            {
                upgradeShop.HideOutOfCoinsMessageImmediately();
            }
        }
    }

    /// <summary>
    /// Pauses the game by setting Time.timeScale to 0 and managing the cursor.
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0f;
        ObjectGenerator.ins.StopGenerating();
    }

    /// <summary>
    /// Resumes the game by setting Time.timeScale to 1 and managing the cursor.
    /// </summary>
    private void ResumeGame()
    {
        //Time.timeScale = 1f;
        //ObjectGenerator.ins.StartGenerating();


        Time.timeScale = 1f;

        // Resume enemy generation only if not in the tutorial
        if (ObjectGenerator.ins != null && ObjectGenerator.ins.isTutorial == 0)
        {
            ObjectGenerator.ins.StartGenerating();
        }
        // Else, do not start generating in the tutorial scene


        //Cursor.lockState = CursorLockMode.None;  // Lock the cursor
        //if (SceneManager.GetActiveScene().name.CompareTo("TutorialScene") != 0)
        //{
        //    Time.timeScale = 1f;
        //}
        //else
        //{
        //    Time.timeScale = 1f;                      // Resume the game
        //    ObjectGenerator.ins.StartGenerating();
        //}
    }

    /// <summary>
    /// Displays a temporary feedback message to the player.
    /// </summary>
    /// <param name="message">Message to display.</param>
//    /// <param name="color">Color of the message text.</param>
//    private void ShowFeedback(string message, Color color)
//    {
//        if (feedbackText != null)
//        {
//            feedbackText.text = message;
//            feedbackText.color = color;
//            feedbackText.gameObject.SetActive(true);
//            StartCoroutine(HideFeedbackAfterDelay(2f)); // Hide after 2 seconds
//        }
//    }

//    /// <summary>
//    /// Coroutine to hide the feedback message after a delay.
//    /// </summary>
//    /// <param name="delay">Delay in seconds before hiding the message.</param>
//    /// <returns></returns>
//    IEnumerator HideFeedbackAfterDelay(float delay)
//    {
//        yield return new WaitForSecondsRealtime(delay); // Use WaitForSecondsRealtime since Time.timeScale = 0
//        if (feedbackText != null)
//        {
//            feedbackText.gameObject.SetActive(false);
//        }
//    }
//}
}