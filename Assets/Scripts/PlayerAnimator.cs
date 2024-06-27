using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private string IsWalking = "IsWalking";
    [SerializeField] private Player player;
   void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool(IsWalking, player.IsWalking());
    }
}
