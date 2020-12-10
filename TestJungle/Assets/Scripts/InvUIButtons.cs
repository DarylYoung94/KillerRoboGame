using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvUIButtons : MonoBehaviour
{
    int clickedCount = 0;
    bool moving = false;
    private Button clicked1, clicked2;
    private Vector3 position1, position2;

    public Transform invParent;
    public Transform abilityParent;

    private void FixedUpdate() 
    {
        if(clickedCount == 2)
        {
            clicked1.transform.position = Vector3.Lerp(clicked1.transform.position, 
                                                       position2,
                                                       10f*Time.deltaTime); 

            clicked2.transform.position = Vector3.Lerp(clicked2.transform.position, 
                                                       position1,
                                                       10f*Time.deltaTime); 
            
            if (!moving)
            {
                if (clicked1.tag != clicked2.tag)
                {
                    string tempTag = clicked1.tag;
                    Transform tempTransform = clicked1.transform.parent;

                    clicked1.tag = clicked2.tag;
                    clicked1.transform.parent = clicked2.transform.parent;

                    clicked2.tag = tempTag;
                    clicked2.transform.parent = tempTransform;
                }

                moving = true;
                Invoke("ResetName", 1f);
            }
        }
    }

    private void ResetName()
    {
        clicked1.transform.localScale = new Vector2(1f,1f);
        clicked2.transform.localScale = new Vector2(1f,1f);

        clickedCount = 0;
        moving = false;


    }

   public void forBtn(Button btn)
   {
       if(clickedCount == 0)
       {
           clicked1 = btn;
           position1 = btn.transform.position;
           btn.transform.localScale = new Vector2(1.2f, 1.2f);
           clickedCount++;
       }
       else if( clickedCount == 1)
       {
           clicked2 = btn;
           position2 = btn.transform.position;
           btn.transform.localScale = new Vector2(1.2f, 1.2f);
           clickedCount++;
       }
   }
}
