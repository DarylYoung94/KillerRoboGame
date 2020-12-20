using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBotAnim : MonoBehaviour
{
    GameObject player;
    bool moving ;
    Animator anim;
    
    public float rotSpeed = 20f;


    void Start() 
    {
        player = GameManager.instance.player;
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
