using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace CookingChaos
{
    public class RecipeInstruction : ScriptableObject
    {
        public static readonly InputAction AnyKeyInput = new InputAction("Any Key", binding: "<Keyboard>/anyKey");

        private static readonly string interaction_Hold = "Hold"; 
        private static readonly string interaction_MultiTap = "MultiTap"; 

        #region Fields and Properties
        public int Index { get; set; }
        [SerializeField] private InputActionMap inputActions;
        private float progress = 0f;
        #endregion

        #region Methods 
        public void Activate()
        {
            progress = 0f;
            
            inputActions.actionTriggered += TriggerAction;
            inputActions.Enable();
        }

        private void TriggerAction(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log($"Start {context.action.name}");
                RecipeHandler.EventHandler.InputEventStart(context.action.name);
                if (context.interaction is MultiTapInteraction)
                {
                    Debug.Log($"{context.action.name} is multitap");
                    RecipeHandler.EventHandler.MultiTapInputEventStart(context.action.name, (context.interaction as MultiTapInteraction).tapCount);
                }
                if (context.interaction is HoldInteraction)
                {
                    Debug.Log($"{context.action.name} is hold");
                    RecipeHandler.EventHandler.HoldingInputEventStart(context.action.name, (context.interaction as HoldInteraction).duration);
                }
            }
            if(context.performed)
            {
                RecipeHandler.EventHandler.InputEventPerformed(context.action.name);
                Debug.Log($"Performed {context.action.name}");
                context.action.Disable();
            }
            if (context.canceled)
            {
                Debug.Log($"Canceled {context.action.name}");
                RecipeHandler.EventHandler.InputEventStop(context.action.name);
            }
        }

        public void Deactivate()
        {
            inputActions.Disable();
        }
        #endregion
    }
}
