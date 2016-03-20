using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public Transform target;
    public int moveSpeed;
    public int maxDistance;

    private Transform myTransform;

    public void Awake()
    {
        myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {
        maxDistance = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, myTransform.position) > maxDistance)
        {
            // Get a direction vector from us to the target
            Vector3 enemyDirection = target.position - myTransform.position;

            // Normalize it so that it's a unit direction vector
            enemyDirection.Normalize();

            // Move ourselves in that direction
            myTransform.position += enemyDirection * moveSpeed * Time.deltaTime;
        }
    }
}
