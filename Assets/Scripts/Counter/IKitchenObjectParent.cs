using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定义通用接口--
/// </summary>
public interface IKitchenObjectParent
{
    /// <summary>
    /// 获取持有食物的位置在持有者身上的应该在的位置
    /// </summary>
    /// <returns>食物位置</returns>
    public Transform GetKitchenFollowTransform();
    /// <summary>
    /// 设置当前持有者持有食物的食物信息
    /// </summary>
    /// <param name="kitchenObject">食物信息</param>
    public void SetKitchenObject(KitchenObject kitchenObject);
    /// <summary>
    /// 获取持有者现在持有食物的食物信息
    /// </summary>
    /// <returns>食物信息</returns>
    public KitchenObject GetKitchenObject();
    /// <summary>
    /// 失去食物时应该将当前食物信息删除的函数
    /// </summary>
    public void ClearKitchenObject();
    /// <summary>
    /// 是否持有食物
    /// </summary>
    /// <returns>bool值</returns>
    public bool HasKitchenObject();
}
