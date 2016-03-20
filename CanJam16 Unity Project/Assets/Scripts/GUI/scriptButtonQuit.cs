using UnityEngine;
using System.Collections;
using System;

public class scriptButtonQuit : MonoBehaviour
{

    public void quitOnClick()
    {
        Debug.Log("Button clicked");
        Application.Quit();
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
