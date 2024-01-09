using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 食物柜台动画类
/// </summary>
public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    [SerializeField] private Animator animator;

    private const string OPEN_CLOSE = "OpenClose";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabedObject += ContainerCounter_OnPlayerGrabedOgject;
    }
    private void ContainerCounter_OnPlayerGrabedOgject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
