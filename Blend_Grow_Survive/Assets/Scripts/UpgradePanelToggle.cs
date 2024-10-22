using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanelToggle : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject upgradePanel;               // Reference to the Upgrade Panel
    public TextMeshProUGUI feedbackText;      // Reference to the Button's Text (optional for feedback)

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

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(isPanelOpen);
        }

        // Pause or resume the game
        if (isPanelOpen)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
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
        Time.timeScale = 1f;                      // Resume the game
        //Cursor.lockState = CursorLockMode.None;  // Lock the cursor
        ObjectGenerator.ins.StartGenerating();
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