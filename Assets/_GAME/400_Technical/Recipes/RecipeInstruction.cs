using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace CookingChaos
{
    public class RecipeInstruction : ScriptableObject
    {
        public static readonly InputAction AnyKeyInput = new InputAction("Any Key", binding: "<Keyboard>/anyKey");

        #region Events 
        public static event Action OnInstructionSucceded;
        public static event Action OnInstructionFailed;
        public static event Action<InputAction.CallbackContext> OnTriggerInstruction;
        #endregion

        #region Fields and Properties
        public int Index;
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
            OnTriggerInstruction?.Invoke(context);
            if(context.performed)
            {
                context.action.Disable();
                progress += (1f / inputActions.actions.Count);
                if (progress >= 1f)
                {
                    OnInstructionSucceded?.Invoke();
                    Deactivate();
                }
            }
        }

        public void Deactivate()
        {
            inputActions.actionTriggered -= TriggerAction;
            inputActions.Disable();
        }
        #endregion
    }
}
