using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    public Renderer squareRenderer; // Reference to the square's Renderer

    private List<Color> colors;
    private Color currentColor;

    private float switchTime = 20f; // Default time to switch colors

    void Start()
    {
        // Initialize the list of colors
        colors = new List<Color>()
        {
            new Color(1f, 0.8f, 0.8f),  // Light Red
            new Color(0.8f, 0.8f, 1f),  // Light Blue
            new Color(0.8f, 1f, 0.8f),  // Light Green
            Color.white,                // White
            new Color(1f, 0.9f, 0.8f),  // Light Orange
        };

        // Start the coroutine to switch colors
        StartCoroutine(SwitchColor());
    }

    // Coroutine to switch the background color
    IEnumerator SwitchColor()
    {
        while (true)
        {
            // Pick a random color from the list
            currentColor = colors[Random.Range(0, colors.Count)];

            // Set the square's material color to the selected color
            squareRenderer.material.color = currentColor;

            // If the color is white, only hold for 5 seconds, otherwise hold for 30 seconds
            if (currentColor == Color.white)
            {
                switchTime = 5f;
            }
            else
            {
                switchTime = 20f;
            }

            // Wait for the defined switch time before changing the color again
            yield return new WaitForSeconds(switchTime);
        }
    }
}
