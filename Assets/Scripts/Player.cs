using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.Burst.Intrinsics;
using UnityEngine;
/// <summary>
/// 角色控制脚本
/// </summary>
public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldObject;

    private Vector3 lastInteraction;//最后方向变量
    private bool isWalking;//是否在运动?
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    private void Awake()
    {
        //单例模式
        if (Instance != null)
        {
            Debug.LogError("出现了不止一个玩家实例");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;//添加用户交互事件为E键
        gameInput.OnInteractAlterateAction += GameInput_OnInteractAlterateAction;//添加切菜事件为f键
    }

    #region 订阅事件
    private void GameInput_OnInteractAlterateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlterate(this);
        }
    }
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        //通过检测是否有selected来判断能否触发对应Counter
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }
    #endregion

    private void Update()
    {
        Vector2 inputVector;

        inputVector = gameInput.GetMovementVectorNormalized();
        HandleMovement(inputVector);
        HandleInteractions(inputVector);
    }
    

    /// <summary>
    /// 柜台交互
    /// </summary>
    private void HandleInteractions(Vector2 inputVector)
    {
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float interactDistance = 2f;
        //用于让其持续监测碰撞
        if (moveDir != Vector3.zero)
        {
            lastInteraction = moveDir;//记录暂停时最后的一个方向变量
        }
        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit hitInfo, interactDistance, countersLayerMask))
        {
            if (hitInfo.transform.TryGetComponent(out BaseCounter BaseCounter))
            {
                if (BaseCounter != selectedCounter)//通过碰撞检测让selected赋值为对应Counter
                {
                    SetSelectedCounter(BaseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    /// <summary>
    /// 碰撞检测
    /// </summary>
    /// <param name="inputVector">键盘输入vector2</param>
    /// <returns>vector是否归零</returns>
    private void HandleMovement(Vector2 inputVector)
    {
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = 0.7f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDirX.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Can move only on the X
                moveDir = moveDirX;
            } else
            {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDirZ.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // Can move only on the Z
                    moveDir = moveDirZ;
                } else
                {
                    // Cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;//位置变化
        }

        isWalking = moveDir != Vector3.zero;//判断是否允许运动

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);//随位置转动
    }
    /// <summary>
    /// 获取运动状值
    /// </summary>
    /// <returns>运动bool值?</returns>
    public bool IsWalking()
    {
        return isWalking;
    }

    /// <summary>
    /// 对selected进行赋值,并启动一个对selected的事件
    /// </summary>
    /// <param name="selectedCounter"></param>
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }


    #region 柜台信息设置
    public Transform GetKitchenFollowTransform()
    {
        return kitchenObjectHoldObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject!=null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    #endregion
}
