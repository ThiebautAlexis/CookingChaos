using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
            inputActions.Enable();
        }

        public void Deactivate()
        {
            inputActions.Disable();
        }

        /// <summary>
        /// Return an int according to the state of the Instruction 
        /// -1 = Failed
        /// 0 = On Hold
        /// 1 = Right Input performed
        /// </summary>
        /// <returns>Return an int according to the state of the Instruction</returns>
        public int GetInstructionState()
        {
            for (int i = 0; i < inputActions.actions.Count; i++)
            {
                if (inputActions.actions[i].WasPerformedThisFrame())
                {
                    inputActions.actions[i].Disable();
                    progress += 1f / inputActions.actions.Count;
                    
                    if(progress == 1f)
                    {
                        // Success of all instructions - Callback here 
                        return 1;
                    }
                    Debug.Log("Input valid");
                    // Success of this instruction - Callback here 

                    return 0;
                }
                string _interaction = inputActions.actions[i].interactions.Split('(')[0];
                if (_interaction == interaction_Hold && inputActions.actions[i].IsPressed())
                {
                    Debug.Log("Holding");
                    return 0;
                }
                if (_interaction == interaction_MultiTap && inputActions.actions[i].WasPressedThisFrame())
                {
                    Debug.Log("Tapping");
                    return 0;
                }
                    
            }
            if (AnyKeyInput.WasPerformedThisFrame())
            {
                // Instructions Failed callback
                return -1;
            }
            
            return 0;
        }
        #endregion
    }
}
