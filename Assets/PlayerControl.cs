// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""f3b47256-a75b-4d24-8228-51ce06c39f7d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""611777c9-7dd4-4b31-9e2b-5e763624e9b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""89114862-89bc-4ef6-a01b-4f96b2fc3851"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""432b33a2-0e4e-4d45-900e-4eb672ab6480"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""KeyBoard"",
                    ""id"": ""5e61e907-0bd2-4b19-87a6-816e1a7aafe6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a836ee03-8f78-44a6-aa39-aa6792e1638e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e0a882cd-dc45-4027-9c24-c6311ef88816"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9f815ec5-dad4-459f-94e5-2f318124272f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bc54d3f1-d4fe-4184-aec6-0f67ee0282ca"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller"",
                    ""id"": ""43a30ed1-d271-47ca-aba4-585c9dd3201a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""98b173b7-fdf3-4504-9ffc-7b0ea5e8f629"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""54327e59-ff26-488b-88f4-63ca13a265de"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c87ff7be-8ff7-4673-98e6-8506d078a0d9"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""af90bffc-90cf-4fc4-917c-5b30c9de7fdc"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b4c8639c-3f25-4372-ad4a-fc23c8516e53"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""89d86eae-d763-412f-9df4-9889a5c8f584"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""427d4ec8-aaed-4326-820e-6b2c76881cee"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a422dd60-1a1a-4862-bd19-db018b537514"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8e6f3d83-b52d-481f-89c8-d95746c41f91"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ff38f51-d264-45e5-abf9-8d8d41686d55"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a0b9ddb-0da2-4eb2-a4bf-44bc8d0ea3b2"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a29fcffb-dc3a-4a96-98ff-b5f817132aaa"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MapBuilding"",
            ""id"": ""7ac39bbc-65cf-496d-a16a-0b04a73bd0c9"",
            ""actions"": [
                {
                    ""name"": ""GenerateNode"",
                    ""type"": ""Button"",
                    ""id"": ""c0bb12cd-824e-4b54-b2c7-3b632d3cc87f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GeneratePath"",
                    ""type"": ""Button"",
                    ""id"": ""c9736da5-7706-4cdc-acf0-44282c5dc78a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShowShortestPath"",
                    ""type"": ""Button"",
                    ""id"": ""d74bcd7d-066e-402c-bc51-85e35a4895c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""c0ecfe10-bf92-4ccb-81c8-2a841f6b8d5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""SelectObstacle"",
                    ""type"": ""Button"",
                    ""id"": ""9529e2ee-8a15-432e-b264-164fbad3ac53"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""PlaceObstacle"",
                    ""type"": ""Button"",
                    ""id"": ""7d771419-8abf-4c59-8474-9203252dba60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5d91bb47-0515-42bd-a432-611508a855bc"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GenerateNode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5064564e-6662-4d88-9b4c-d9f0412ab94b"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GeneratePath"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0ac6337-96bf-4b60-b3f6-8682559b7e08"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowShortestPath"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""KeyBoard"",
                    ""id"": ""9d97fd34-586e-4f00-b812-8139051410f0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a214d871-e154-4049-b63a-ea88d2d04246"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e543f81d-b8c8-4aae-b540-1fc4d871b15c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c5f1c344-4fd9-411d-ba6a-eae3d2aaa797"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e04dd872-0c65-4e5b-b4ec-5b335536f14d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller"",
                    ""id"": ""149f1e74-7627-4df5-be1e-f73934becfb3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d60f42a7-d481-450c-8067-a06b3e1e9f39"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""af65f1c8-7b5f-4c43-b008-99ebbfc91a2c"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7b335f95-bbac-4c37-a071-f5d5673f2ee6"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5ac78453-2100-4523-b722-0d399199a284"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3026f7da-fc20-4937-92f2-5dbe3c91a7bf"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7864dfcf-deb9-4a66-b13a-3b2b73df4435"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""146f0309-1f85-474e-a63e-4ce63860e96a"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a57f3a6a-f51a-472c-ba30-3d4133589098"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""fed0c554-f7b7-4406-aeab-16b5249e8054"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectObstacle"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""75cdf5fd-d0b4-4123-81fa-b8bc7be299df"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectObstacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8a22e8e0-0545-49ec-a3ef-a09b24efe529"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectObstacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d542aeee-dc7a-4c2d-93b1-740cc22b5254"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectObstacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9eb512db-7802-43b0-9c43-d12c42ae2edc"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectObstacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3db3d9f3-a843-4038-9732-302c8ea8add0"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceObstacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77303249-fe20-490f-b14a-7234f4a563b9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceObstacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Move = m_UI.FindAction("Move", throwIfNotFound: true);
        m_UI_Interact = m_UI.FindAction("Interact", throwIfNotFound: true);
        m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
        // MapBuilding
        m_MapBuilding = asset.FindActionMap("MapBuilding", throwIfNotFound: true);
        m_MapBuilding_GenerateNode = m_MapBuilding.FindAction("GenerateNode", throwIfNotFound: true);
        m_MapBuilding_GeneratePath = m_MapBuilding.FindAction("GeneratePath", throwIfNotFound: true);
        m_MapBuilding_ShowShortestPath = m_MapBuilding.FindAction("ShowShortestPath", throwIfNotFound: true);
        m_MapBuilding_Movement = m_MapBuilding.FindAction("Movement", throwIfNotFound: true);
        m_MapBuilding_SelectObstacle = m_MapBuilding.FindAction("SelectObstacle", throwIfNotFound: true);
        m_MapBuilding_PlaceObstacle = m_MapBuilding.FindAction("PlaceObstacle", throwIfNotFound: true);
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

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Move;
    private readonly InputAction m_UI_Interact;
    private readonly InputAction m_UI_Cancel;
    public struct UIActions
    {
        private @PlayerControl m_Wrapper;
        public UIActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_UI_Move;
        public InputAction @Interact => m_Wrapper.m_UI_Interact;
        public InputAction @Cancel => m_Wrapper.m_UI_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_UIActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnInteract;
                @Cancel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // MapBuilding
    private readonly InputActionMap m_MapBuilding;
    private IMapBuildingActions m_MapBuildingActionsCallbackInterface;
    private readonly InputAction m_MapBuilding_GenerateNode;
    private readonly InputAction m_MapBuilding_GeneratePath;
    private readonly InputAction m_MapBuilding_ShowShortestPath;
    private readonly InputAction m_MapBuilding_Movement;
    private readonly InputAction m_MapBuilding_SelectObstacle;
    private readonly InputAction m_MapBuilding_PlaceObstacle;
    public struct MapBuildingActions
    {
        private @PlayerControl m_Wrapper;
        public MapBuildingActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @GenerateNode => m_Wrapper.m_MapBuilding_GenerateNode;
        public InputAction @GeneratePath => m_Wrapper.m_MapBuilding_GeneratePath;
        public InputAction @ShowShortestPath => m_Wrapper.m_MapBuilding_ShowShortestPath;
        public InputAction @Movement => m_Wrapper.m_MapBuilding_Movement;
        public InputAction @SelectObstacle => m_Wrapper.m_MapBuilding_SelectObstacle;
        public InputAction @PlaceObstacle => m_Wrapper.m_MapBuilding_PlaceObstacle;
        public InputActionMap Get() { return m_Wrapper.m_MapBuilding; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapBuildingActions set) { return set.Get(); }
        public void SetCallbacks(IMapBuildingActions instance)
        {
            if (m_Wrapper.m_MapBuildingActionsCallbackInterface != null)
            {
                @GenerateNode.started -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnGenerateNode;
                @GenerateNode.performed -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnGenerateNode;
                @GenerateNode.canceled -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnGenerateNode;
                @GeneratePath.started -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnGeneratePath;
                @GeneratePath.performed -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnGeneratePath;
                @GeneratePath.canceled -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnGeneratePath;
                @ShowShortestPath.started -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnShowShortestPath;
                @ShowShortestPath.performed -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnShowShortestPath;
                @ShowShortestPath.canceled -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnShowShortestPath;
                @Movement.started -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnMovement;
                @SelectObstacle.started -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnSelectObstacle;
                @SelectObstacle.performed -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnSelectObstacle;
                @SelectObstacle.canceled -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnSelectObstacle;
                @PlaceObstacle.started -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnPlaceObstacle;
                @PlaceObstacle.performed -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnPlaceObstacle;
                @PlaceObstacle.canceled -= m_Wrapper.m_MapBuildingActionsCallbackInterface.OnPlaceObstacle;
            }
            m_Wrapper.m_MapBuildingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @GenerateNode.started += instance.OnGenerateNode;
                @GenerateNode.performed += instance.OnGenerateNode;
                @GenerateNode.canceled += instance.OnGenerateNode;
                @GeneratePath.started += instance.OnGeneratePath;
                @GeneratePath.performed += instance.OnGeneratePath;
                @GeneratePath.canceled += instance.OnGeneratePath;
                @ShowShortestPath.started += instance.OnShowShortestPath;
                @ShowShortestPath.performed += instance.OnShowShortestPath;
                @ShowShortestPath.canceled += instance.OnShowShortestPath;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @SelectObstacle.started += instance.OnSelectObstacle;
                @SelectObstacle.performed += instance.OnSelectObstacle;
                @SelectObstacle.canceled += instance.OnSelectObstacle;
                @PlaceObstacle.started += instance.OnPlaceObstacle;
                @PlaceObstacle.performed += instance.OnPlaceObstacle;
                @PlaceObstacle.canceled += instance.OnPlaceObstacle;
            }
        }
    }
    public MapBuildingActions @MapBuilding => new MapBuildingActions(this);
    public interface IUIActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IMapBuildingActions
    {
        void OnGenerateNode(InputAction.CallbackContext context);
        void OnGeneratePath(InputAction.CallbackContext context);
        void OnShowShortestPath(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnSelectObstacle(InputAction.CallbackContext context);
        void OnPlaceObstacle(InputAction.CallbackContext context);
    }
}
