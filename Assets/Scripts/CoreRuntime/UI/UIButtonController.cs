using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Sprite nextIcon;
    [SerializeField]
    private Sprite nextHoveredIcon;
    [SerializeField]
    private OnBoardingUIManager onBoardingManager;
    private bool mouse_over = false;
    private bool clicked = false;
    private Button actionButton;

    void Awake()
    {
        actionButton = GetComponent<Button>();
    }

    void Start()
    {
        actionButton.onClick.AddListener(HandleButtonClick);
}
    void Update()
    {
        if (mouse_over)
        {
            actionButton.GetComponent<Image>().sprite = nextHoveredIcon;
        } 
        else
        {
            actionButton.GetComponent<Image>().sprite = nextIcon;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        //Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        //Debug.Log("Mouse exit");
    }

    private void HandleButtonClick()
    {
        //clicked = true;
        onBoardingManager.GoToNextAvailablePage();
    }
}
