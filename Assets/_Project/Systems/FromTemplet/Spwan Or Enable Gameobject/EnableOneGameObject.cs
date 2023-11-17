using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

[AddComponentMenu("_SABI/Gameobject/EnableOneGameObject")]
public class EnableOneGameObject : MonoBehaviour
{
    [TabGroup("Settings")] [SerializeField]
    private bool disableEverythingElse = true;

    [TabGroup("Settings")] [SerializeField]
    private bool activateOnStartWithIndex = false;

    [TabGroup("Settings")] [SerializeField]
    private int activateOnStartIndex = 0;

    [TabGroup("Settings")] [SerializeField]
    private bool activateOnStartRandom = false;

    [TabGroup("Settings")] [SerializeField]
    private bool useModulusOfIndex = false;

    [TabGroup("References")] [SerializeField]
    private GameObject[] allItems;

    private int _currentEnabledItem = 0;

    private void Start()
    {
        if (activateOnStartWithIndex)
        {
            EnableOneWithIndex(activateOnStartIndex);
        }
        else if (activateOnStartRandom)
        {
            EnableOneRandomly();
        }
    }

    [ContextMenu("FillAllItemsWithChild")]
    void FillAllItemsWithChild()
    {
        foreach (var VARIABLE in GetComponentsInChildren<Transform>())
        {
            allItems.Append(VARIABLE.gameObject);
        }
    }

    void DisableAll()
    {
        foreach (var VARIABLE in allItems)
        {
            VARIABLE.SetActive(false);
        }
    }

    public void EnableOneWithIndex(int index)
    {
        if (disableEverythingElse) DisableAll();

        if ((index < allItems.Length) && (index >= 0))
        {
            allItems[index].SetActive(true);
            _currentEnabledItem = index;
        }
        else
        {
            if (useModulusOfIndex)
            {
                int newIndex = index % DoGetMaximumIndex() + 1;
                allItems[newIndex].SetActive(true);
                _currentEnabledItem = newIndex;
            }
            else
            {
                Debug.LogWarning("EnableOneWithIndex Out of range index = " + index +
                                 " Possible values = 0 - " + allItems.Length);
            }
        }
    }

    public int DoGetMaximumIndex()
    {
        return allItems.Length;
    }

    public void EnableOneRandomly()
    {
        if (disableEverythingElse) DisableAll();
        allItems[Random.Range(0, allItems.Length)].SetActive(true);
    }

    public void EnableNextItemWithRepeat()
    {
        if (_currentEnabledItem + 1 == DoGetMaximumIndex()) EnableOneWithIndex(0);
        else
        {
            EnableOneWithIndex(_currentEnabledItem + 1);
        }
    }

    public void EnablePreviewsItemWithRepeat()
    {
        if (_currentEnabledItem - 1 == -1) EnableOneWithIndex(DoGetMaximumIndex() - 1);
        else
        {
            EnableOneWithIndex(_currentEnabledItem - 1);
        }
    }

    public void EnableNextItem()
    {
        EnableOneWithIndex(Mathf.Clamp(_currentEnabledItem + 1, 0, DoGetMaximumIndex()));
    }

    public void EnablePreviewsItem()
    {
        EnableOneWithIndex(Mathf.Clamp(_currentEnabledItem - 1, 0, DoGetMaximumIndex()));
    }
}