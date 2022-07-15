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
            progress = holdingDurationMax = holdingDuration = 0f;
            multitapCount = 0;
            // inputActions.actionTriggered += TriggerAction;
            inputActions.Enable();
        }

        private void TriggerAction(InputAction.CallbackContext context)
        {
            Debug.Log("Call => " + context.action.name);
            if (context.started)
                Debug.Log($"Start {context.action.name}");
            if(context.performed)
            {
                Debug.Log($"Performed {context.action.name}");
                context.action.Disable();
            }
            if (context.canceled)
                Debug.Log($"Canceled {context.action.name}");

            if(context.interaction is MultiTapInteraction)
            {
                Debug.Log($"{context.action.name} is multitap");
            }
            if(context.interaction is HoldInteraction)
            {
                Debug.Log($"{context.action.name} is hold");
            }

        }

        public void Deactivate()
        {
            inputActions.Disable();
        }

        private float holdingDurationMax = 0f, holdingDuration = 0f;
        private int multitapCount = 0;
        /// <summary>
        /// Return an int according to the state of the Instruction 
        /// -1 = Failed
        /// 0 = On Hold
        /// 1 = Right Input performed
        /// </summary>
        /// <returns>Return an int according to the state of the Instruction</returns>
        public InstructionState GetInstructionState(out string _eventKey, out float _holdingProgress, out int _multitapCount)
        {

            _eventKey = string.Empty;
            _holdingProgress = 0f;
            _multitapCount = 0;

            for (int i = 0; i < inputActions.actions.Count; i++)
            {
                if (inputActions.actions[i].WasPerformedThisFrame())
                {
                    inputActions.actions[i].Disable();
                    progress += 1f / inputActions.actions.Count;
                    _eventKey = inputActions.actions[i].name;
                    
                    if(progress == 1f)
                    {
                        // Success of all instructions - Callback here
                        return InstructionState.Success;
                    }
                    Debug.Log("Input valid");
                    // Success of this instruction - Callback here 
                    return InstructionState.Input;
                }
                string _interaction = inputActions.actions[i].interactions.Split('(')[0];
                if (_interaction == interaction_Hold )
                {
                    if (inputActions.actions[i].IsPressed())
                    {
                        if(holdingDurationMax == 0f)
                        {
                            if(!float.TryParse(inputActions.actions[i].interactions.Split('=')[1].Split(')')[0].Replace('.',','), out holdingDurationMax))
                            {
                                holdingDurationMax = -1f;
                            }
                        }

                        holdingDuration += Time.deltaTime;
                        _holdingProgress = (float)holdingDuration / (float)holdingDurationMax;
                        _eventKey = inputActions.actions[i].name;
                        return InstructionState.Hold;
                    }
                    else if (inputActions.actions[i].WasReleasedThisFrame())
                        holdingDurationMax = 0f;
                }
                if (_interaction == interaction_MultiTap && inputActions.actions[i].WasPressedThisFrame())
                {
                    _multitapCount = multitapCount++;
                    _eventKey = inputActions.actions[i].name;
                    return InstructionState.MultiTap;
                }
                    
            }
            if (AnyKeyInput.WasPerformedThisFrame())
            {
                // Instructions Failed callback
                return InstructionState.Failed;
            }
            
            return InstructionState.None;
        }
        #endregion
    }
}
