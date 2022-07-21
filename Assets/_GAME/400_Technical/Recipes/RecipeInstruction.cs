using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace CookingChaos
{
    public class RecipeInstruction : ScriptableObject
    {

        #region Events 
        public static event Action OnInstructionSucceded;
        public static event Action OnInstructionFailed;
        public static event Action<InputAction.CallbackContext> OnTriggerInstruction;
        #endregion

        #region Fields and Properties
        public int Index;
        [SerializeField] private InputActionMap inputActions;
        [SerializeField] private Vector2 minBaseDuration = new Vector2(.1f, 5f);
        private float progress = 0f;
        private Sequence instructionSequence;
        #endregion

        #region Methods 
        public void Activate()
        {
            progress = 0f;
            /*
            var _rebindAction = RecipeAsset.AnyKeyInput.PerformInteractiveRebinding().WithTargetBinding(0);
            for (int i = 0; i < inputActions.bindings.Count; i++)
            {
                _rebindAction.WithControlsExcluding(inputActions.bindings[i].path);
            }
            _rebindAction.OnComplete(x => Debug.Log("Rebind completed")) ;
            _rebindAction.Start();
            */

            float _duration = Mathf.Max(minBaseDuration.x, minBaseDuration.y); // Multiply by speed here
            if (instructionSequence.IsActive())
                instructionSequence.Kill();
            instructionSequence = DOTween.Sequence();
            instructionSequence.AppendInterval(_duration);
            instructionSequence.AppendCallback(FailAction);

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

        private void FailAction()
        {
            Debug.Log("Fail Instruction");
            OnInstructionFailed?.Invoke();
            Deactivate();
        }

        public void Deactivate()
        {
            instructionSequence.Kill(false);
            inputActions.actionTriggered -= TriggerAction;
            inputActions.Disable();
        }
        #endregion
    }
}
