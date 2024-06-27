using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private Animator animator;
    private const string Cut = "Cut";
    [SerializeField] private CuttingCounter cuttingCounter;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {

        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(Cut);
    }
}

    
