using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public Transform target;
    public Transform entity;
    public int moveSpeed;
    public int rotation;
    

    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            // Only needed if objects don't share 'z' value.
            dir.z = 0.0f;

            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(target.transform.position.y - entity.transform.position.y, target.transform.position.x - entity.transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (270 / Mathf.PI) * AngleRad;
            // Rotate Object
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

            //Move Towards Target
            transform.position += (target.position - transform.position).normalized
                * moveSpeed * Time.deltaTime;
        }
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
