using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;


[AddComponentMenu("_SABI/SimpleStateMachine/SabiSimpleStateMachineSOs")]
public class SabiSimpleStateMachineSO : SerializedMonoBehaviour
{
    [TabGroup("Settings")] [Space(5)] [SerializeField]
    private bool isActive = true;

    [TabGroup("Settings")] [Space(5)] [DisplayAsString] [SerializeField]
    private SimpleStateSO currentState = null;

    [TabGroup("Settings")] [Space(5)] [SerializeField]
    protected bool allowSameStateToSetAgain = false;

    [TabGroup("States")] [SerializeField] private Dictionary<SimpleStateSO, UnityEvent> states = new();

    public void SetAndRunState(SimpleStateSO newState)
    {
        if (isActive == false)
        {
            Debug.LogError(
                $"Simple statemachine at {gameObject.name} is not active. But you are trying to Set and Run State ");
            return;
        }

        if (currentState == newState) return;
        SetState(newState);
        RunCurrentState();
    }

    private void SetState(SimpleStateSO newState)
    {
        if (isActive == false)
        {
            Debug.LogError($"Simple statemachine at {gameObject.name} is not active. But you are trying to Set State ");
            return;
        }

        if (currentState == newState && !allowSameStateToSetAgain) return;

        currentState = newState;
    }


    public void RunCurrentState()
    {
        if (isActive == false)
        {
            Debug.LogError(
                $"Simple statemachine at {gameObject.name} is not active. But you are trying to  Run State ");
            return;
        }

        if (states.ContainsKey(currentState)) states[currentState].Invoke();
    }
}