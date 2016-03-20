using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

    public float zoomSpeed = 10;

    public float smoothSpeed = 2;

    public float targetOrtho;

	// Use this for initialization
	void Start ()
    {
        targetOrtho = Camera.main.orthographicSize;    
	}
	
	// Update is called once per frame
	void Update ()
    {
        targetOrtho = Mathf.Clamp(targetOrtho, 3, 5);

        scrollWheelControl();
	}

    void scrollWheelControl()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0.0f)
        { 
                targetOrtho -= scroll * zoomSpeed;
                targetOrtho = Mathf.Clamp(targetOrtho, 3, 5);
            
        }
        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
    }
}
