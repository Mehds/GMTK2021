using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCone : MonoBehaviour
{
    public GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To-Do: Rotation towards player and shift towards player
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Boum! you're dead!");
    }
}
