using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter 
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;


   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    
    
    public override void  Interact(Player player)
    {   
        
       if(!HasKitchenObject())
        {
            if(player.HasKitchenObject() )
            {
                player.GetKitchenObject().SetkitchenObjectParent(this);
            }
        }
       else
        {
            if(!player.HasKitchenObject() )
            {
                this.GetKitchenObject().SetkitchenObjectParent(player);
            }
        }



    }
  
}
