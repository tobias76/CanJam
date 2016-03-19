﻿using System;

[Serializable]
public class IntRange
{

    public int minimumValue;
    public int maximumValue;

    public IntRange(int min, int max)
    {
        minimumValue = min;
        maximumValue = max;
    }

    public int randomNumber
    {
        get
        {
            return UnityEngine.Random.Range(minimumValue, maximumValue);
        }
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
