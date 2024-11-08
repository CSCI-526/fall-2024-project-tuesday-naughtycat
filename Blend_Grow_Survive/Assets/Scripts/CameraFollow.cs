using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  
    public Vector3 offset;    

    public float zoom_factor = 2f;  
    public float min_zoom = 7f;
    public float max_zoom = 14f;

    private Camera cam;
    private static int instanceCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        instanceCount++;
        //Debug.Log("CameraFollow instance count: " + instanceCount);
        cam = Camera.main;
        //Debug.Log("Camera target: " + (target != null ? target.name : "null"));
        //AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        //if (listeners.Length > 1)
        //{
        //    for (int i = 1; i < listeners.Length; i++)
        //    {
        //        Destroy(listeners[i].gameObject);
        //    }
        //}
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