using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BackgroundColorChanger.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    public Renderer square_renderer;
    private Coroutine colorSwitchCoroutine;

    void Start()
    {
        // Set the background color to light blue
        square_renderer.material.color = new Color(0.678f, 0.847f, 0.902f); // Light blue

        // Start the color-switching coroutine if needed
        colorSwitchCoroutine = StartCoroutine(SwitchColor());
    }

    IEnumerator SwitchColor()
    {
        // Existing color-switching logic
        // ...

        yield return null;
    }

    // Method to change the background color to pink
    public void ChangeToPink()
    {
        // Stop the color-switching coroutine
        if (colorSwitchCoroutine != null)
        {
            StopCoroutine(colorSwitchCoroutine);
        }

        // Set the background color to pink
        square_renderer.material.color = new Color(1f, 0.75f, 0.8f); // Pink
    }
}


// public class BackgroundColorChanger : MonoBehaviour
// {
//     public Renderer square_renderer; 
//     private List<Color> colors;
//     private List<Color> initial_colors;
//     private Color current_color;
//     private float switch_time = 10f; 

//     void Start()
//     {
//         // Initialize the list of colors
//         // There are five different colors: Red, Blue, Green, White and Orange
//         colors = new List<Color>()
//         {
//             new Color(1f, 0.8f, 0.8f),  
//             new Color(0.8f, 0.8f, 1f),  
//             new Color(0.8f, 1f, 0.8f),
//             new Color(1f, 0.9f, 0.8f),  
//         };
//         // Initialize the list of initial colors (without white)
//         initial_colors = new List<Color>()
//         {
//             new Color(1f, 0.8f, 0.8f),  
//             new Color(0.8f, 0.8f, 1f),  
//             new Color(0.8f, 1f, 0.8f),  
//             new Color(1f, 0.9f, 0.8f),  
//         };
//         // Start to switch colors
//         StartCoroutine(SwitchColor());
//     }

//     // switch the background color
//     IEnumerator SwitchColor()
//     {
//         // First pick the color without white
//         // go to the loop to switch colors with white
//         // assign the color to the background and wait for next time changing the color
//         current_color = initial_colors[Random.Range(0, initial_colors.Count)];
//         square_renderer.material.color = current_color;
//         yield return new WaitForSeconds(switch_time);

//         while (true)
//         {
//             current_color = colors[Random.Range(0, colors.Count)];
//             square_renderer.material.color = current_color;
//             yield return new WaitForSeconds(switch_time);
//         }
//     }
// }

