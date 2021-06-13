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

    void Update(){
        Collider2D[] hits;

        //hits = Physics2D.OverlapCircleAll(transform.position, 1.5f, targetMask);
        hits = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), gameObject.GetComponent<BoxCollider2D>().size, 0f, targetMask);
        
        for (int j = 0; j < hits.Length; j++)
        {
            Collider2D hit = hits[j];
            Slowed component = hit.transform.gameObject.GetComponent<Slowed>();
            if (component == null){
                hit.transform.gameObject.AddComponent<Slowed>();
            } else {
                component.refresh();
            }
        }
    }
}
