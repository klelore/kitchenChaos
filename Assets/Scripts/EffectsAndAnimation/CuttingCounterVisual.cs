using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CuttingCounter cuttingCounter;

    private string CUT = "Cut";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnPlayerGrabedOgject;
    }

    private void CuttingCounter_OnPlayerGrabedOgject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
