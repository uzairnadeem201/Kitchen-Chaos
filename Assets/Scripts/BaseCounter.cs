using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour , IKitchenObjectParent
{
    
    [SerializeField] Transform counterTopPoint;
    [SerializeField] private bool testing;
    private KicthenObject kitchenObject;
    public virtual void Interact(Player player)
    {

    }
    public virtual void InteractAlternate(Player player)
    {

    }
    public Transform GetKitchenObjectFollowTranform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KicthenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KicthenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return (kitchenObject != null);
    }
}

