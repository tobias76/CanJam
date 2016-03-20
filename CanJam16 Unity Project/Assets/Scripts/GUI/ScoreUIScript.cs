using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUIScript : MonoBehaviour {

    //score var
    public static int score;

    //finds text box
    Text text;

    public static void storeHighScore()
    {
        int oldHighscore = PlayerPrefs.GetInt("highscore", 0);

        if(score > oldHighscore)
        {
            Debug.Log(score);
            PlayerPrefs.SetInt("highscore", score);
        }
    }

	//initialises score
	void Awake()
    {
        text = GetComponent<Text>();
        score = 0;
	}
	
    //updates score
	void Update () {
        text.text = "Score: " + score;

        if (Input.GetMouseButtonDown(0))
        {
            score++;
        }
	}
}
