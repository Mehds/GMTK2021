using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCircle : MonoBehaviour
{
    private GameObject Parent = null;

    // Update is called once per frame

    public void Start(){
        GetComponent<SpriteRenderer>().enabled = false;
        Parent = transform.parent.gameObject;
    }
    // public void damageTargets()
    // {
    //     RaycastHit2D[] hits;

    //     hits = Physics2D.CircleCastAll(transform.position, 0.7f, new Vector3(0,0,0));

    //     for (int j = 0; j < hits.Length; j++)
    //     {
    //         RaycastHit2D hit = hits[j];
    //         Character target = hit.transform.gameObject.GetComponent<Character>();
    //         if (target != null && target != Parent){
    //             target.applyDamage(1);
    //         }

    //         Berzerker b = hit.transform.gameObject.GetComponent<Berzerker>();
    //         if (b != null){
    //             Parent.GetComponent<Character>().applyDamage(1);
    //             Parent.GetComponent<Character>().pushBack(hit.transform);
    //         }
    //     }
    // }
}
