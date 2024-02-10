using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusableButtonsManager : MonoBehaviour
{
    public delegate void FocusButtonChangeHandler(FocusableButton newFocusButton);
    public event FocusButtonChangeHandler OnFocusButtonChanged;

    private static FocusableButtonsManager _instance;

    [HideInInspector] public FocusableButton buttonOnFocus;

    public static FocusableButtonsManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new FocusableButtonsManager();

            return _instance;
        }
    }

    public void OnChangeFocusButton(FocusableButton newFocusButton)
    {
        OnFocusButtonChanged?.Invoke(newFocusButton);
        buttonOnFocus = newFocusButton;
    }

    public void ResetButtonOnFocus()
    {
        buttonOnFocus.isEnabled = false;
        if (buttonOnFocus.anim != null)
        {
            buttonOnFocus.anim.SetBool(buttonOnFocus.buttonName + "Enabled", false);
            buttonOnFocus.anim.SetBool(buttonOnFocus.buttonName + "Disabled", true);
        }
    }
}
