using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler OnPlayerGrabedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;//�ű������

    /// <summary>
    /// ��������
    /// </summary>
    public override void Interact(Player player)
    {
        //��һ��ʳ�����ɽ�ʳ������������
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO,player);

            OnPlayerGrabedObject?.Invoke(this,EventArgs.Empty);
        } else
        {
            Debug.Log("����Ѿ�����ʳ��");
        }
    }
}
