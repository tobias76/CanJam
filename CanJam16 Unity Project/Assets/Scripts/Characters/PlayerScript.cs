using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //speed and direction vars
    public Vector2 speed = new Vector2(2, 2);
    private Vector2 direction = new Vector2(0, 0);

    //health and score vars
    public int startingHealth = 100;
    public static int playerHealth;

    //damage vars
    public int scoreDamage = 100;
    public int healthDamage = 5;

    // Directions
    private Vector2 Left = new Vector2(-1, 0);
    private Vector2 Right = new Vector2(1, 0);
    private Vector2 Up = new Vector2(0, 1);
    private Vector2 Down = new Vector2(0, -1);

    //level var
    public int level = 0;

    //resets health
    void Awake()
    {
        playerHealth = startingHealth;
    }

    //damage on collision
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            direction = new Vector2(0, 0);

            if (ScoreUIScript.score > 0)
            {
                ScoreUIScript.score -= scoreDamage;
                if (ScoreUIScript.score < 0)
                {
                    ScoreUIScript.score = 0;
                }
            }
            else if (ScoreUIScript.score == 0)
            {
                playerHealth -= healthDamage;
            }
        }
    }

    void Update()
    {
        //input controls
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Left;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Right;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Up;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Down;
        }

        //movement
        Vector3 movement = new Vector3(
          speed.x * direction.x,
          speed.y * direction.y,
          0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        //standstill damage
        if (level > 0)
        {
            if (ScoreUIScript.score > 0)
            {
                ScoreUIScript.score -= scoreDamage;
                if (ScoreUIScript.score < 0)
                {
                    ScoreUIScript.score = 0;
                }
            }
            else if (ScoreUIScript.score == 0)
            {
                playerHealth -= healthDamage;
            }
        }

        //death
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}