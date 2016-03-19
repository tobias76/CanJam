using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // This instance allows it to be accessed by the other scripts
    public static GameManager instance = null;

    private BoardManager boardManager;

    private int level = 3;

    void Awake()
    {
        // Check if an instance already exists
        if(instance == null)
        {
            // If not, create this.
            instance = this;
        }
        // If an imposter exists EXTERMINATE.
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        // This object isn't destroyed when a scene is loaded
        DontDestroyOnLoad(gameObject);

        boardManager = GetComponent<BoardManager>();

        initGame();
    }

    void initGame()
    {
        boardManager.SetupScene(level);
    }
}
