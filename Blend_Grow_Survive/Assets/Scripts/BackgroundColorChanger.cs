using System.Collections;
using System.Collections.Generic;
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