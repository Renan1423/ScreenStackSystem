using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenStack : MonoBehaviour
{
    public static ScreenStack instance;

    private List<ActivatableUI> stack;
    private ActivatableUI currentScreen;

    public delegate void OnScreensStackCleared();
    public OnScreensStackCleared OnScreenStackCleared;

    private void Awake()
    {
        int screenStackInstances = FindObjectsOfType<ScreenStack>().Length;

        if (instance != null && instance != this
            || screenStackInstances > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;

        if (!gameObject.TryGetComponent(out UIStateManager uiStateManager))
        {
            gameObject.AddComponent<UIStateManager>();
        }

        stack = new List<ActivatableUI>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (instance != this)
            return;

        ClearScreenStack();
    }

    public void AddScreenOntoStack(ActivatableUI screen) 
    {
        if (!stack.Contains(screen)) 
        {
            stack.Add(screen);
            screen.gameObject.SetActive(true);
            UpdateScreenStack();
        }
    }

    public void RemoveScreenFromStack(ActivatableUI screen)
    {
        screen.DeactivateScreen();
        stack.Remove(screen);
        UpdateScreenStack();
    }

    public void ClearScreenStack()
    {
        for (int i = 0; i < stack.Count; i++)
            RemoveScreenFromStack(stack[i]);
        stack.Clear();
    }

    public void UpdateScreenStack() 
    {
        if (stack.Count > 0)
        {
            currentScreen = stack[^1];

            GameState currentGameState = GameStateManager.instance.CurrentGameState;
            GameState newGameState = GameState.Paused;
            if (currentGameState == GameState.Gameplay)
                GameStateManager.instance.SetState(newGameState);
        }
        else
            OnStackCleared();
    }

    private void OnStackCleared()
    {
        currentScreen = null;
        GameState newGameState = GameState.Gameplay;
        GameStateManager.instance.SetState(newGameState);
        OnScreenStackCleared?.Invoke();
    }

    public bool ContainScreenOnStack(ActivatableUI screen) 
    {
        return stack.Contains(screen);
    }
}
