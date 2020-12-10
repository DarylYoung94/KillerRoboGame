using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvUIButtons : MonoBehaviour
{
    int clickedCount = 0;
    private Button clicked1, clicked2;
    private Vector3 position1, position2;

    public Transform invParent;
    public Transform abilityParent;

    private void FixedUpdate() 
    {
        if(clickedCount == 2)
        {
            if(clicked1.tag == "AbilityItem"  && clicked2.tag == "InventoryItem")
            {
                Debug.Log("Tagged");
                clicked2.tag = "AbilityItem";
                clicked2.transform.parent = abilityParent;
               
                clicked1.tag = "InventoryItem";
                clicked1.transform.parent = invParent;
                
               
            } 

            if(clicked1.tag == "InventoryItem" && clicked2.tag == "AbilityItem"  )
            {
                Debug.Log("Tagged");
                clicked2.tag = "InventoryItem";
                clicked2.transform.parent = invParent;

                clicked1.tag = "AbilityItem";
                clicked1.transform.parent = abilityParent;
            }


            clicked1.transform.position = Vector3.Lerp(clicked1.transform.position, 
            position2, 10f*Time.deltaTime); 

            clicked2.transform.position = Vector3.Lerp(clicked2.transform.position, 
            position1, 10f*Time.deltaTime); 
           
            
            Invoke("ResetName", 1f);
        }
    }

    private void ResetName()
    {
        clicked1.transform.localScale = new Vector2(1f,1f);
        clicked2.transform.localScale = new Vector2(1f,1f);
        clickedCount =0; 
    }



   public void forBtn(Button btn)
   {
       if(clickedCount ==0)
       {
           clicked1 = btn;
           position1 = btn.transform.position;
           //btn.transform.name = "clicked";
           btn.transform.localScale = new Vector2(1.2f, 1.2f);
            clickedCount++;
       }
       
       else if( clickedCount ==1)
       {
           clicked2 = btn;
           position2 = btn.transform.position;
           //btn.transform.name = "clicked";
           btn.transform.localScale = new Vector2(1.2f, 1.2f);
            clickedCount++;
       }
   }
}
