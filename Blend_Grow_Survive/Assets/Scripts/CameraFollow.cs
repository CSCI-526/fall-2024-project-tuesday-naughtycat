using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  
    public Vector3 offset;    

    public float zoom_factor = 2f;  
    public float min_zoom = 3f;     
    public float max_zoom = 7f;    

    private Camera cam; 
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {
        // Follow the player's position with  offset
        // Adjust the camera's orthographic size between min_zoom and max_zoom
        if (target != null)
        {
            transform.position = target.position + offset;
            float targetZoom = target.localScale.x * zoom_factor;
            cam.orthographicSize = Mathf.Clamp(targetZoom, min_zoom, max_zoom);
        }
    }
}