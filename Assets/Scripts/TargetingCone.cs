using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCone : MonoBehaviour
{
    public GameObject Parent;
    public GameObject Target;
    public Transform transform;
    public float updateSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To-Do: Rotation towards player and shift towards player
        //transform.Rotate(0, 0, Time.deltaTime * 5);
        Vector3 direction = Target.transform.position - transform.position;
        //float singleStep = updateSpeed * Time.deltaTime;

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, direction, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        //transform.rotation = Quaternion.LookRotation(TargetPos);

        // Debug.DrawRay(transform.position, TargetPos, Color.red);

        Vector3 newPosition = transform.position + Vector3.Scale(Vector3.Normalize(direction),new Vector3(0, updateSpeed, 0) );
        if (newPosition.y < Parent.transform.position.y + 0.75 && newPosition.y > Parent.transform.position.y - 0.75){
            transform.position = newPosition;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Boum! you're dead!");
    }
}
