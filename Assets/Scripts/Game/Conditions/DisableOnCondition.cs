using System.Collections;
using System.Collections.Generic;
using Game.Conditions;
using UnityEngine;

/// <summary>
/// Disables on start
/// </summary>
public class DisableOnCondition : MonoBehaviour
{
    public bool Opposite = false;
    public List<ConditionBase> conditions = new List<ConditionBase>();

    // Start is called before the first frame update
    void Start()
    {
        if (Opposite)
        {
            this.gameObject.SetActive(ConditionBase.IsTrueAll(conditions));
        }
        else
        {
            this.gameObject.SetActive(!ConditionBase.IsTrueAll(conditions));
        }
    }
}
