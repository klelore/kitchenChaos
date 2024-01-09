using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ͨ�ýӿ�--
/// </summary>
public interface IKitchenObjectParent
{
    /// <summary>
    /// ��ȡ����ʳ���λ���ڳ��������ϵ�Ӧ���ڵ�λ��
    /// </summary>
    /// <returns>ʳ��λ��</returns>
    public Transform GetKitchenFollowTransform();
    /// <summary>
    /// ���õ�ǰ�����߳���ʳ���ʳ����Ϣ
    /// </summary>
    /// <param name="kitchenObject">ʳ����Ϣ</param>
    public void SetKitchenObject(KitchenObject kitchenObject);
    /// <summary>
    /// ��ȡ���������ڳ���ʳ���ʳ����Ϣ
    /// </summary>
    /// <returns>ʳ����Ϣ</returns>
    public KitchenObject GetKitchenObject();
    /// <summary>
    /// ʧȥʳ��ʱӦ�ý���ǰʳ����Ϣɾ���ĺ���
    /// </summary>
    public void ClearKitchenObject();
    /// <summary>
    /// �Ƿ����ʳ��
    /// </summary>
    /// <returns>boolֵ</returns>
    public bool HasKitchenObject();
}
