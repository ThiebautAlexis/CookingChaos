using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CookingChaos
{
    public class RecipeEventsHandler : MonoBehaviour
    {
        #region Fields and Properties
        [SerializeField] private RecipeEventActivator<string>[] inputEvents;
        [SerializeField] private RecipeEventActivator<int>[] failedInstructionEvents;
        [SerializeField] private RecipeEventActivator<int>[] completedInstructionEvents;
        #endregion

        #region Methods 
        private void OnEnable()
        {
            RecipeInstruction.OnTriggerInstruction += OnCallEvent;
            RecipeAsset.OnInstructionFailed += OnFailInstructionEvent;
            RecipeAsset.OnInstructionCompleted += OnCompleteInstructionEvent;

            foreach (var recipeEvent in inputEvents)
                recipeEvent.ResetEvent();
            foreach (var recipeEvent in failedInstructionEvents)
                recipeEvent.ResetEvent();
            foreach (var recipeEvent in completedInstructionEvents)
                recipeEvent.ResetEvent();
        }

        private void OnDisable()
        {
            RecipeInstruction.OnTriggerInstruction -= OnCallEvent;
            RecipeAsset.OnInstructionFailed += OnFailInstructionEvent;
            RecipeAsset.OnInstructionCompleted += OnCompleteInstructionEvent;
        }

        private void OnCallEvent(InputAction.CallbackContext context)
        {
            for (int i = 0; i < inputEvents.Length; i++)
            {
                if(inputEvents[i].ActivationKey == context.action.name)
                    inputEvents[i].CallInputEvent(context);
            }
        }

        #region Failed / Success  Events
        private void OnFailInstructionEvent(int _index)
        {
            for (int i = 0; i < failedInstructionEvents.Length; i++)
            {
                if (failedInstructionEvents[i].ActivationKey == _index)
                    failedInstructionEvents[i].CallEvent();
            }
        }

        private void OnCompleteInstructionEvent(int _index)
        {
            for (int i = 0; i < completedInstructionEvents.Length; i++)
            {
                if (completedInstructionEvents[i].ActivationKey == _index)
                    completedInstructionEvents[i].CallEvent();
            }
        }
        #endregion

        #endregion
    }

    [Serializable]
    public class RecipeEventActivator<T>
    {
        [SerializeField] private InputEvents.RecipeEvent recipeEvent;
        [SerializeField] private GameObject targetObject;
        [SerializeField] private T activationKey;
        public T ActivationKey => activationKey;

        public void CallEvent() => recipeEvent.CallEvent(targetObject);

        public void CallInputEvent(InputAction.CallbackContext _context) => recipeEvent.CallEvent(_context, targetObject);

        public void ResetEvent() => recipeEvent.ResetEvent();
    }
}
