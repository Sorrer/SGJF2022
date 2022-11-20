using System.Collections;
using System.Collections.Generic;
using Game.Conditions;
using UnityEngine;

/// <summary>
/// Disables on start
/// </summary>
public class DisableOnCondition : MonoBehaviour
{
    public List<ConditionBase> conditions = new List<ConditionBase>();
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(!ConditionBase.IsTrueAll(conditions));
    }
}
