using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 厨房信息脚本化
/// </summary>
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
    
}
