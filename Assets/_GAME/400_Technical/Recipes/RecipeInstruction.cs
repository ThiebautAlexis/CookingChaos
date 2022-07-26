using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace CookingChaos
{
    public class RecipeInstruction : ScriptableObject
    {
        public static readonly string IntervalPropertyName = "validInterval";
        public static readonly string DurationPropertyName = "minBaseDuration";

        #region Events 
        public static event Action OnInstructionSucceded;
        public static event Action OnInstructionFailed;
        public static event Action<InputAction.CallbackContext> OnTriggerInstruction;
        #endregion

        #region Fields and Properties
        public int Index;
        [SerializeField] private InputActionMap inputActions;
        [SerializeField] private Vector2 minBaseDuration = new Vector2(.1f, 5f);
        [SerializeField] private Vector2 validInterval = new Vector2(0f, 1f);
        private float progress = 0f;
        private Sequence instructionSequence;

        public float Duration
        {
            get {
                return Mathf.Max(minBaseDuration.x, minBaseDuration.y); // Multiply by speed here
            }
        }
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

            
            if (instructionSequence.IsActive())
                instructionSequence.Kill();
            instructionSequence = DOTween.Sequence();
            instructionSequence.AppendInterval(Duration);
            instructionSequence.AppendCallback(FailAction);
            

            inputActions.actionTriggered += TriggerAction;
            inputActions.Enable();
        }

        private void TriggerAction(InputAction.CallbackContext context)
        {
            if(IsIntervalValid(instructionSequence.fullPosition))
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
            else
            {
                FailAction();
            }
        }

        private bool IsIntervalValid(float _time) => (_time >= validInterval.x * Duration && _time <= validInterval.y * Duration);

        private void FailAction()
        {
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
