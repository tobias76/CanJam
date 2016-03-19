using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// Scrolling speed
    /// </summary>
    public Vector2 speed = new Vector2(2, 2);

    /// <summary>
    /// Moving direction
    /// </summary>
    private Vector2 direction = new Vector2(0,0);

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = new Vector2(-1, 0);
        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = new Vector2(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = new Vector2(0, 1);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = new Vector2(0, -1);
        }

        // Movement
        Vector3 movement = new Vector3(
          speed.x * direction.x,
          speed.y * direction.y,
          0);

        movement *= Time.deltaTime;
        transform.Translate(movement);
    }
}