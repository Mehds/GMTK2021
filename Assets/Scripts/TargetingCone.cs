using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCone : MonoBehaviour
{
    public Berzerker Parent;
    public Transform TargetTransform;
    public Transform transform;
    public float range = 10f;
    public float updateSpeed = 0.2f;
    public float angleChange = 1f;

    private Vector3 LoS = new Vector3(0, 0, 0);
    private Vector3 LoSL = new Vector3(0, 0, 0);
    private Vector3 LoSR = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Parent.isAttacking){
            handleMovement();
        }
        Debug.DrawRay(transform.position, LoS, Color.red);
        Debug.DrawRay(transform.position, LoSL, Color.red);
        Debug.DrawRay(transform.position, LoSR, Color.blue);

    }

    public void damageTargets(){
        // ToDo: Make sure to only be in the characters layer
        Vector3[] lines = {LoS, LoSL, LoSR};
        int targetMask = 1 << 8;
        for (int i = 0; i < 3; i++){
            RaycastHit[] hits;

            hits = Physics.RaycastAll(transform.position, lines[i], 150f, targetMask);

            Debug.Log(hits.Length);

            for (int j = 0; j < hits.Length; j++)
            {
                RaycastHit hit = hits[j];
                Character target = hit.transform.gameObject.GetComponent<Character>();
                target.applyDamage(5);
            }

        }
    }

    void handleMovement(){
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


        LoS = Quaternion.AngleAxis(transform.eulerAngles.z, new Vector3(0, 0, 1)) * Vector3.down;
        LoSL = Quaternion.AngleAxis(transform.eulerAngles.z + 30, new Vector3(0, 0, 1)) * Vector3.down;
        LoSR = Quaternion.AngleAxis(transform.eulerAngles.z - 30, new Vector3(0, 0, 1)) * Vector3.down;
        float angle = Vector3.Angle(LoS, TargetPosition);

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
}
