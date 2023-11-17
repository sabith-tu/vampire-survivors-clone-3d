using SABI.SOA;
using UnityEngine;

[CreateAssetMenu(fileName = "QuickActionLog", menuName = "SO/QuickActions/Log")]
public class QuickActionBaseLog : QuickActionBaseSO
{
    [SerializeField] private StringReference message;

    public override void InvokeQuickAction()
    {
        Debug.Log(message.Value);
    }
}