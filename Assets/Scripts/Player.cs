using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour , IKitchenObjectParent
{   public static Player Instance { get; private set; }
    [SerializeField] float speed = 10f;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] GameInput gameInput;
    [SerializeField] LayerMask countersLayerMask;
    private bool isWalking = false;
    private Vector3 lastInteraction;
    private BaseCounter selectedCounter;
    private KicthenObject kitchenObject;
    [SerializeField] Transform kitchenObjectHoldPoint;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs:EventArgs
    {
        public BaseCounter selectedCounter;
    }
    private void Awake()
    { if(Instance == null)
        {
            Instance = this;
        }
        
    }
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(Instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
        float interactDistance = 2f;
        if(moveDir != Vector3.zero)
        {
            lastInteraction = moveDir;
        }
        if(Physics.Raycast(transform.position, lastInteraction,out RaycastHit rayCastHit , interactDistance,countersLayerMask))
        {
            if(rayCastHit.transform.TryGetComponent(out BaseCounter baseCounter) )
            {
                if(baseCounter != selectedCounter)
                {
                    selectedCounter = baseCounter;
                    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });

                   
                }
            }
            else
            {
                selectedCounter = null;
                OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });

            }
        }
        else
        {
            selectedCounter = null;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
        }
        Debug.Log(selectedCounter);
        
    }

    public bool IsWalking()
    { 
        
        return isWalking; 
    }
    void HandleMovement()
    {
        float moveDistance = speed * Time.deltaTime;
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
        float playerRadius = .7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //cant move
                }
            }


        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }


        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    public Transform GetKitchenObjectFollowTranform()
    {
        return kitchenObjectHoldPoint;
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
