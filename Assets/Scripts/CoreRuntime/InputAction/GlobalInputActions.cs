// GENERATED AUTOMATICALLY FROM 'Assets/GlobalInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GlobalInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GlobalInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GlobalInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""2d260d8b-1057-4814-bb46-fea0ba97bd7a"",
            ""actions"": [
                {
                    ""name"": ""Bubbleshoot"",
                    ""type"": ""Button"",
                    ""id"": ""2aeb5e9a-5133-4265-863b-0e1a99de056b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grapple"",
                    ""type"": ""Button"",
                    ""id"": ""b95b545b-ad78-40fe-8bcd-02e2210e6925"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""04090680-d4e1-46e8-90f8-008a94ade779"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""253e3979-a38f-4164-8cb0-bd61e95bc534"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Bubbleshoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8313655b-ec32-4829-89d9-89e2eb88890f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1e5e47b-dde9-44b5-80d6-386baae376a8"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Bubbleshoot = m_Player.FindAction("Bubbleshoot", throwIfNotFound: true);
        m_Player_Grapple = m_Player.FindAction("Grapple", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Bubbleshoot;
    private readonly InputAction m_Player_Grapple;
    private readonly InputAction m_Player_Move;
    public struct PlayerActions
    {
        private @GlobalInputActions m_Wrapper;
        public PlayerActions(@GlobalInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Bubbleshoot => m_Wrapper.m_Player_Bubbleshoot;
        public InputAction @Grapple => m_Wrapper.m_Player_Grapple;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Bubbleshoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBubbleshoot;
                @Bubbleshoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBubbleshoot;
                @Bubbleshoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBubbleshoot;
                @Grapple.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapple;
                @Grapple.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapple;
                @Grapple.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapple;
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Bubbleshoot.started += instance.OnBubbleshoot;
                @Bubbleshoot.performed += instance.OnBubbleshoot;
                @Bubbleshoot.canceled += instance.OnBubbleshoot;
                @Grapple.started += instance.OnGrapple;
                @Grapple.performed += instance.OnGrapple;
                @Grapple.canceled += instance.OnGrapple;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnBubbleshoot(InputAction.CallbackContext context);
        void OnGrapple(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}
