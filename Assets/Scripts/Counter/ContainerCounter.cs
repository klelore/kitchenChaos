using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler OnPlayerGrabedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;//脚本化组件

    /// <summary>
    /// 交互函数
    /// </summary>
    public override void Interact(Player player)
    {
        //将一个食物生成将食物放在玩家身上
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO,player);

            OnPlayerGrabedObject?.Invoke(this,EventArgs.Empty);
        } else
        {
            Debug.Log("玩家已经持有食物");
        }
    }
}
