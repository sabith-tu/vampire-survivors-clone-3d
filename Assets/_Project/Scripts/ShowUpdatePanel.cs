using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ShowUpdatePanel : MonoBehaviour
{
    [SerializeField] private GameObject updatePanel;
    [SerializeField] private GameObject[] abilitys;
    [SerializeField] private UnityEvent OnShow;
    [SerializeField] private UnityEvent OnHide;
    private int elementsLeftToShow = 3;

    [Button]
    public void Show()
    {
        updatePanel.SetActive(true);

        while (elementsLeftToShow > 0)
        {
            int abilityToShow = Random.Range(0, abilitys.Length);
            if (!abilitys[abilityToShow].activeInHierarchy)
            {
                abilitys[abilityToShow].SetActive(true);
                elementsLeftToShow--;
            }
        }

        OnShow.Invoke();
    }

    [Button]
    public void Hide()
    {
        elementsLeftToShow = 3;
        foreach (var VARIABLE in abilitys)
        {
            VARIABLE.SetActive(false);
        }

        updatePanel.SetActive(false);
        OnHide.Invoke();
    }
}