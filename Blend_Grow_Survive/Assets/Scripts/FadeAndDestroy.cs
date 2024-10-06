using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndDestroy : MonoBehaviour
{
    public float fade_duration = 3f; 
    private float fade_speed;
    private Material object_material;
    private Color object_color;

    void Start()
    {
        // Get the material of the object
        // Calculate the fade speed based on the fade duration
        // Start the fade process
        object_material = GetComponent<Renderer>().material;
        object_color = object_material.color;
        fade_speed = 1f / fade_duration;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float alpha_value = object_color.a;

        // Gradually reduce the alpha value over time
        // Destroy the object after it becomes fully transparent
        while (alpha_value > 0f)
        {
            alpha_value -= fade_speed * Time.deltaTime;
            object_color.a = Mathf.Clamp(alpha_value, 0f, 1f); 
            object_material.color = object_color;
            yield return null; 
        }
        Destroy(gameObject);
    }
}