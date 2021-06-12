using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCone : MonoBehaviour
{
    public GameObject Parent;
    public Transform TargetTransform;
    public Transform transform;
    public float updateSpeed = 0.2f;
    public float angleChange = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To-Do: Rotation towards player and shift towards player
        //transform.Rotate(0, 0, Time.deltaTime * 5);
        Vector3 TargetPosition = TargetTransform.position;
        Vector3 direction = TargetPosition - transform.position;

        // Todo: make it less complicated
        // updated y position
        // Vector3 newPosition = transform.position + Vector3.Scale(Vector3.Normalize(direction),new Vector3(0, updateSpeed, 0) );
        // if (newPosition.y < Parent.transform.position.y + 0.75 && newPosition.y > Parent.transform.position.y - 0.75){
        //     transform.position = newPosition;
        // }


        Vector3 LoS = Quaternion.AngleAxis(transform.eulerAngles.z, new Vector3(0, 0, 1)) * Vector3.down;

        float angle = Vector3.Angle(LoS, TargetPosition);

        // Debug.DrawRay(transform.position, LoS, Color.green);
        // Debug.DrawRay(transform.position, TargetPosition, Color.red);

        // Debug.Log(angle);

        if (angle > 3 && angle < 357){
            if (LoS.y < Vector3.Normalize(TargetPosition).y){
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - angleChange);
            } else {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + angleChange);
            }
        }        

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Boum! you're dead!");
    }
}
