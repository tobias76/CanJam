using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUIScript : MonoBehaviour
{
    //finds text box
    Text text;

    //initialises score
    void Awake()
    {
        text = GetComponent<Text>();
    }

    //updates score
    void Update()
    {
        text.text = "Health: " + PlayerScript.playerHealth;
    }
}
