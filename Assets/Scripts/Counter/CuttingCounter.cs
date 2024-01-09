using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent,IHasProgress
{

    public static event EventHandler OnAnyCut;

    public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public event EventHandler OnCut;//�����¼�
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;//�������¼�

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;

    /// <summary>
    /// ��������
    /// </summary>
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //��̨û��ʳ��
            if (player.HasKitchenObject())
            {
                //�����������ʳ��
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // �����Ʒ����Ϊ�и���Ʒ���ܷ���
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / (float)cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
            else
            {
                Debug.Log("ʲô��û�з���");
            }
        }
        else
        {
            //��̨��ʳ��
            if (!player.HasKitchenObject())
            {
                //���������û��ʳ��
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //������������
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }
    /// <summary>
    /// ��������
    /// </summary>
    public override void InteractAlterate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //�˰���������ʳ��
            cuttingProgress++;//�и�����

            OnAnyCut?.Invoke(this, EventArgs.Empty);
            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / (float)cuttingRecipeSO.cuttingProgressMax
            });
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {

                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }
    //�ж��Ƿ����и��������
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }


    //ת��ʳ����̬
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var item in cuttingRecipeSOArray)
        {
            if (inputKitchenObjectSO == item.input)
            {
                return item;
            }
        }

        return null;
    }
}
