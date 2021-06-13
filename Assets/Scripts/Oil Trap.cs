using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTrap : MonoBehaviour
{
    int targetMask = 1 << 8 | 1 << 7;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 8 || other.transform.gameObject.layer == 7){
            // Check if the 
            Debug.Log(other.transform.gameObject);
            
            Slowed component = other.transform.gameObject.GetComponent<Slowed>();
            if (component == null){
                other.transform.gameObject.AddComponent<Slowed>();
            } else {
                component.refresh();
            }
        }
    }
}
