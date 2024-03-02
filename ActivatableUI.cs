using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActivatableUI : MonoBehaviour
{
    private FocusableButton buttonToFocus;
    [SerializeField]
    private bool isBackHandler;

    [Space(10)]
    [Header("Navigation")]
    [SerializeField]
    private Transform buttonsParent;
    [SerializeField]
    private bool horizontalNavigation;

    [Space(10)]
    [Header("Optional parameters - Parent")]
    [SerializeField]
    private ActivatableUI parentUI;
    [SerializeField]
    private bool removeParentOnClose;

    [Space(10)]
    [Header("Optional parameters - Animations")]
    [SerializeField]
    private bool containOpenCloseAnimation;
    [SerializeField]
    private float delayToOpenClose;
    [SerializeField]
    [Tooltip("If the animator parameter is null, the Activatable UI is going to get the " +
        "animator component from the game object")]
    private Animator anim;

    protected virtual void Awake()
    {
        if (buttonsParent == null)
            buttonsParent = transform;

        SetupChildButtons();
    }

    private void OnEnable()
    {
        ActivateScreen();
    }

    public void ActivateScreen()
    {
        if (!anim)
            gameObject.TryGetComponent(out anim);

        StartCoroutine(StartOpenCloseAnimation(true));

        buttonToFocus = GetDesiredButtonToFocus();
        if (buttonToFocus)
            EventSystem.current.SetSelectedGameObject(buttonToFocus.gameObject);
    }

    public void DeactivateScreen() 
    {
        StartCoroutine(StartOpenCloseAnimation(false));
    }
	
	public void FocusOnButton() 
    {
        buttonToFocus = GetDesiredButtonToFocus();
        if (buttonToFocus)
            EventSystem.current.SetSelectedGameObject(buttonToFocus.gameObject);
    }

    private IEnumerator StartOpenCloseAnimation(bool active) 
    {
        if (containOpenCloseAnimation && anim != null)
            anim.SetTrigger("OpenClose");

        ToggleButtons(active);

        yield return new WaitForSeconds(delayToOpenClose);

        if(active == false)
            gameObject.SetActive(false);
    }

    public void HandleBackInput(InputAction.CallbackContext value) 
    {
        if (!isBackHandler || value.canceled)
            return;

        ScreenStack.instance.RemoveScreenFromStack(this);
    }

    public FocusableButton GetDesiredButtonToFocus()
    {
        if (buttonToFocus != null)
            return buttonToFocus;

        for (int i = 0; i < buttonsParent.childCount; i++)
        {
            FocusableButton btn = buttonsParent.GetChild(i).GetComponent<FocusableButton>();
            if (btn)
            {
                buttonToFocus = btn;
                break;
            }
            else
            {
                continue;
            }
        }

        return buttonToFocus;
    }

    protected void SetupChildButtons()
    {
        List<Button> childButtons = new List<Button>();

        for (int i = 0; i < buttonsParent.childCount; i++)
        {
            Button btn = buttonsParent.GetChild(i).GetComponent<Button>();
            if (btn)
                childButtons.Add(btn);
        }

        for (int i = 0; i < childButtons.Count; i++)
        {
            Button btn = childButtons[i];

            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Explicit;
            Button downButton = btn;
            Button upButton = btn;
            if (i < childButtons.Count - 1)
            {
                if (i + 1 < childButtons.Count)
                {
                    downButton = childButtons[i + 1];
                }
            }
            else
            {
                downButton = childButtons[0];
            }

            if (i > 0)
            {
                upButton = childButtons[i - 1];
            }
            else
            {
                if (childButtons.Count > 1)
                    upButton = childButtons[childButtons.Count - 1];
            }

            if (!horizontalNavigation)
            {
                if (downButton != btn)
                    nav.selectOnDown = downButton;
                if (upButton != btn)
                    nav.selectOnUp = upButton;
            }
            else
            {
                if (downButton != btn)
                    nav.selectOnRight = downButton;
                if (upButton != btn)
                    nav.selectOnLeft = upButton;
            }

            btn.navigation = nav;
        }
    }

    public void ToggleButtons(bool enabled)
    {
        for (int i = 0; i < buttonsParent.childCount; i++)
        {
            Button btn = buttonsParent.GetChild(i).GetComponent<Button>();
            if (btn)
                btn.interactable = enabled;
        }
    }
}
