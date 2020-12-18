using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBotAnim : MonoBehaviour
{
    bool moving ;
    Animator anim;
    void Start() 
    {
        anim = GetComponent<Animator>();    
    }
    void Update() 
    {
        moving = this.gameObject.transform.parent.gameObject.GetComponent<EnemyAI>().moving;
        if (moving)
        {
         anim.SetBool ("moving" , true);    
        }
        else
        {
            anim.SetBool("moving" , false);
        }
        
           
    }
}
