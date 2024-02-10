using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UI_State { KEY_OR_GAMEPAD, MOUSE }

public class UIStateManager : MonoBehaviour
{
    private UI_State currentState;

    private ScreenStack screenStack;

    private void Start()
    {
        screenStack = ScreenStack.instance;
    }

    private void Update()
    {
        bool newKeyInput = Input.anyKeyDown && currentState != UI_State.KEY_OR_GAMEPAD;
        bool newMouseInput = (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            && currentState != UI_State.MOUSE;

        SetState(newKeyInput, newMouseInput);
    }

    private void SetState(bool keyInput, bool mouseInput)
    {
        if (!keyInput && !mouseInput)
            return;

        if (keyInput)
            currentState = UI_State.KEY_OR_GAMEPAD;
        else if (mouseInput)
            currentState = UI_State.MOUSE;
    }
}
