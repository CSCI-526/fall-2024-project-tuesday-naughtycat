using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundColorChanger : MonoBehaviour
{
    public Renderer square_renderer;
    private Coroutine colorSwitchCoroutine;
    public GameObject minimap;

    void Start()
    {
        square_renderer.material.color = new Color(0.678f, 0.847f, 0.902f);
        colorSwitchCoroutine = StartCoroutine(SwitchColor());
    }

    IEnumerator SwitchColor()
    {
        yield return null;
    }

    // Method to change the background color to pink
    public void ChangeToPink()
    {
        if (colorSwitchCoroutine != null)
        {
            StopCoroutine(colorSwitchCoroutine);
        }
        square_renderer.material.color = new Color(1f, 0.75f, 0.8f); // Pink
        minimap.GetComponent<Image>().color = new Color(1f, 0.75f, 0.8f);
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

