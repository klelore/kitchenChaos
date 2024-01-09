using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;

    public static void ResetStaticDataManager()
    {
        OnAnyObjectPlacedHere = null;
    }

    [SerializeField] private Transform counterTop;//��̨����

    protected KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter Interact");
    }
    public virtual void InteractAlterate(Player player)
    {
        Debug.Log("BaseCounter InteractAlterate");
    }
    #region ��̨��Ϣ����
    public Transform GetKitchenFollowTransform()
    {
        return counterTop;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject!=null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
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
