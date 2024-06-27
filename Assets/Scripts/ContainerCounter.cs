using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KicthenObject>().SetkitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);

        }
        



    }
}
   

    
