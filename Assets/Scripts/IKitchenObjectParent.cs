using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTranform();


    public void SetKitchenObject(KicthenObject kitchenObject);


    public KicthenObject GetKitchenObject();


    public void ClearKitchenObject();


    public bool HasKitchenObject();
   
}
