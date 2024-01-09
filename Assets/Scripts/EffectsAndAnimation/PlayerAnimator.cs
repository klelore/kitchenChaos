using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画控制器脚本
/// </summary>
public class PlayerAnimator : MonoBehaviour
{

    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update() {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
