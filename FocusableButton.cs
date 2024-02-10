using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class FocusableButton : MonoBehaviour, ISelectHandler
{
    public Animator anim;
    [Tooltip("The buttonName variable is completely optional. In general cases, it should be empty")]
    public string buttonName;
    [Tooltip("Text that is shown when the button is hovered")]
    public string buttonDescription;
    [HideInInspector]
    public bool isEnabled;

    private void OnEnable()
    {
        FocusableButtonsManager.Instance.OnFocusButtonChanged += OnFocusButtonChanged;
    }

    private void OnDisable()
    {
        FocusableButtonsManager.Instance.OnFocusButtonChanged -= OnFocusButtonChanged;
    }

    private void Awake()
    {
        Button btn = GetComponent<Button>();
        if (btn)
            SetupButton(btn);
    }

    private void SetupButton(Button btn)
    {
        btn.interactable = true;
        //Click event setup
        btn.onClick.AddListener(OnClicked);

        //Hover event setup
        EventTrigger eventListener = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry hoverEvent = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerEnter
        };
        hoverEvent.callback.AddListener(OnSelect);
        hoverEvent.callback.AddListener(EnableCursor);
        eventListener.triggers.Add(hoverEvent);
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        //Notify();
        FocusableButtonsManager.Instance.OnChangeFocusButton(this);
    }

    private void EnableCursor(BaseEventData eventData)
    {
        Cursor.visible = true;
    }

    public virtual void OnClicked()
    {
        if (HandleButtonAnimation(true))
        {
            //Notify();
            anim.SetTrigger("Clicked");
        }
    }

    public virtual void OnFocusButtonChanged(FocusableButton newFocusButton)
    {
        if (this.gameObject == newFocusButton.gameObject && !isEnabled)
        {
            HandleButtonAnimation(true);
        }
        else
        {
            HandleButtonAnimation(false);
        }

        //SoundManager.instance.PlayAudio(AudiosReference.buttonHover, AudioType.UI, null);
    }

    //Returns false if the Animator is null
    public bool HandleButtonAnimation(bool Enabled)
    {
        if (anim == null)
            return false;

        isEnabled = Enabled;
        anim.SetBool(buttonName + "Enabled", isEnabled);
        anim.SetBool(buttonName + "Disabled", !isEnabled);

        return true;
    }

    public bool GetIsEnabled()
    {
        return isEnabled;
    }
}
