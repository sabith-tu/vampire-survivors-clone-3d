using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private GameObject uiMainMenu;
    [SerializeField] private GameObject uiGamePlay;
    [SerializeField] private GameObject uiPauseMenu;
    [SerializeField] private GameObject uiSettings;
    [SerializeField] private GameObject uiFailed;
    [SerializeField] private GameObject uiWon;


    private AllUiStates _curentUiState = AllUiStates.MainMenu;

    public AllUiStates DoGetCurentUiState()
    {
        return _curentUiState;
    }


    public void DoSetCurentUiStateToMainMenu() => DoSetCurentUiState(AllUiStates.MainMenu);
    public void DoSetCurentUiStateToGamePlay() => DoSetCurentUiState(AllUiStates.GamePlay);
    public void DoSetCurentUiStateToPauseMenu() => DoSetCurentUiState(AllUiStates.PauseMenu);
    public void DoSetCurentUiStateToWon() => DoSetCurentUiState(AllUiStates.Won);
    public void DoSetCurentUiStateToFailed() => DoSetCurentUiState(AllUiStates.Failed);

    private void Start()
    {
        DisableAllUiState();
        DoSetCurentUiStateToMainMenu();
    }

    public void FaileScreenWithPauseInDelay()
    {
        Invoke(nameof(DoSetCurentUiStateToFailed), 1);
    }

    public void WinScreenWithPauseInDelay()
    {
        Invoke(nameof(DoSetCurentUiStateToWon), 1);
    }


    public void DoSetCurentUiState(AllUiStates newUiState)
    {
        DisableOldUiState(_curentUiState);
        _curentUiState = newUiState;
        EnableNewUiState(_curentUiState);
    }


    private void DisableOldUiState(AllUiStates uiStateToDisable)
    {
        switch (uiStateToDisable)
        {
            case AllUiStates.MainMenu:
                if (uiMainMenu != null) uiMainMenu.SetActive(false);
                break;
            case AllUiStates.GamePlay:
                if (uiGamePlay != null) uiGamePlay.SetActive(false);
                break;
            case AllUiStates.PauseMenu:
                if (uiPauseMenu != null) uiPauseMenu.SetActive(false);
                break;
            case AllUiStates.Settings:
                if (uiSettings != null) uiSettings.SetActive(false);
                break;
            case AllUiStates.Failed:
                if (uiFailed != null) uiFailed.SetActive(false);
                break;
            case AllUiStates.Won:
                if (uiWon != null) uiWon.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DisableAllUiState()
    {
        if (uiMainMenu != null) uiMainMenu.SetActive(false);
        if (uiGamePlay != null) uiGamePlay.SetActive(false);
        if (uiPauseMenu != null) uiPauseMenu.SetActive(false);
        if (uiSettings != null) uiSettings.SetActive(false);
        if (uiFailed != null) uiFailed.SetActive(false);
        if (uiWon != null) uiWon.SetActive(false);
    }


    private void EnableNewUiState(AllUiStates uiStateToEnable)
    {
        switch (uiStateToEnable)
        {
            case AllUiStates.MainMenu:
                if (uiMainMenu != null) uiMainMenu.SetActive(true);
                break;
            case AllUiStates.GamePlay:
                if (uiGamePlay != null) uiGamePlay.SetActive(true);
                break;
            case AllUiStates.PauseMenu:
                if (uiPauseMenu != null) uiPauseMenu.SetActive(true);
                break;
            case AllUiStates.Settings:
                if (uiSettings != null) uiSettings.SetActive(true);
                break;
            case AllUiStates.Failed:
                if (uiFailed != null) uiFailed.SetActive(true);
                break;
            case AllUiStates.Won:
                if (uiWon != null) uiWon.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}


public enum AllUiStates
{
    MainMenu,
    GamePlay,
    PauseMenu,
    Settings,
    Failed,
    Won
}