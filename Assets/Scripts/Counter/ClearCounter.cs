using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 柜台交互脚本
/// </summary>
public class ClearCounter : BaseCounter
{
    /// <summary>
    /// 交互函数
    /// </summary>
    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            //柜台没有食物
            if(player.HasKitchenObject())
            {
                //玩家手里面有食物
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else
            {
                Debug.Log("什么都没有发生");
            }
        }else
        {
            //柜台有食物
            if(!player.HasKitchenObject())
            {
                //玩家手里面没有食物
                GetKitchenObject().SetKitchenObjectParent(player);
            } else
            {
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //手里面有盘子
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }else
                {
                    //拿的不是盘子
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
    }
}

