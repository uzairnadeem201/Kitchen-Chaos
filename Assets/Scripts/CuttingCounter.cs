using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter 
{
    [SerializeField] CuttingRecipeSo[] cuttingRecipeSoArray ;
    int cuttingProgress;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {  if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetkitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs{ progressNormalized = (float)cuttingProgress / cuttingRecipeSo.cuttingProgressMax });
                    
                }
                
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                this.GetKitchenObject().SetkitchenObjectParent(player);
            }
        }

    }
    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            OnCut?.Invoke(this,EventArgs.Empty);
            CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingRecipeSo.cuttingProgressMax });
            if (cuttingProgress >= cuttingRecipeSo.cuttingProgressMax) 
            {
                KitchenObjectSO outputKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                Transform kitchenObjectTransform = Instantiate(outputKitchenObjectSo.prefab);
                kitchenObjectTransform.GetComponent<KicthenObject>().SetkitchenObjectParent(this);
            }
            
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeSoWithInput(inputKitchenObjectSO);
        return cuttingRecipeSo != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    { CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeSoWithInput(inputKitchenObjectSO);
        if(cuttingRecipeSo != null)
        {
            return cuttingRecipeSo.output;
        }
        return null;

    }
    private CuttingRecipeSo GetCuttingRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo) {
        foreach (CuttingRecipeSo cuttingRecipeSo in cuttingRecipeSoArray)
        {
            if (cuttingRecipeSo.input == inputKitchenObjectSo)
            {
                return cuttingRecipeSo;
            }

        }
        return null;
    }
}
