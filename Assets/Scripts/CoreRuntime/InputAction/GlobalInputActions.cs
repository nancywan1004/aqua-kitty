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
                },
                {
                    ""name"": ""SwitchWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""b00dde79-1611-4e76-b816-4e0ff3a3f326"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""482ec551-b21e-4c32-b11f-45499bc54dfe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""1db4d624-aff0-4d84-a41d-d0a57d631700"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""aaacfebe-cac1-4be2-8d79-db1d8db2ecba"",
                    ""path"": ""<Keyboard>/#(a)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6679dbd0-0b5a-49a0-b360-6b6b85086216"",
                    ""path"": ""<Keyboard>/#(d)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""3c511996-a5d5-4650-a1c0-df5415770b8d"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeapon"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""8fec6ad6-d2bc-42de-9005-948a5fb5df74"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""37f1c6c9-01ba-457c-9d0c-faf6c87d1037"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
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
        m_Player_SwitchWeapon = m_Player.FindAction("SwitchWeapon", throwIfNotFound: true);
        m_Player_MoveLeft = m_Player.FindAction("MoveLeft", throwIfNotFound: true);
        m_Player_MoveRight = m_Player.FindAction("MoveRight", throwIfNotFound: true);
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
    private readonly InputAction m_Player_SwitchWeapon;
    private readonly InputAction m_Player_MoveLeft;
    private readonly InputAction m_Player_MoveRight;
    public struct PlayerActions
    {
        private @GlobalInputActions m_Wrapper;
        public PlayerActions(@GlobalInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Bubbleshoot => m_Wrapper.m_Player_Bubbleshoot;
        public InputAction @Grapple => m_Wrapper.m_Player_Grapple;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @SwitchWeapon => m_Wrapper.m_Player_SwitchWeapon;
        public InputAction @MoveLeft => m_Wrapper.m_Player_MoveLeft;
        public InputAction @MoveRight => m_Wrapper.m_Player_MoveRight;
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
                @SwitchWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeapon;
                @MoveLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveLeft;
                @MoveRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveRight;
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
                @SwitchWeapon.started += instance.OnSwitchWeapon;
                @SwitchWeapon.performed += instance.OnSwitchWeapon;
                @SwitchWeapon.canceled += instance.OnSwitchWeapon;
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnBubbleshoot(InputAction.CallbackContext context);
        void OnGrapple(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSwitchWeapon(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
    }
}
