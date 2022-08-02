using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CookingChaos
{
    public class RecipeEventsHandler : MonoBehaviour
    {
        #region Fields and Properties
        [Header("Events")]
        [SerializeField] private RecipeEventActivator<int>[] startInstructionEvents;
        [SerializeField] private RecipeEventActivator<string>[] inputEvents;
        [SerializeField] private RecipeEventActivator<int>[] failedInstructionEvents;
        [SerializeField] private RecipeEventActivator<int>[] completedInstructionEvents;
        #endregion

        #region Methods 
        private void OnEnable()
        {
            RecipeInstruction.OnTriggerInstruction += OnCallEvent;
            RecipeAsset.OnInstructionStarted += OnStartInstructionEvents;
            RecipeAsset.OnInstructionFailed += OnFailInstructionEvents;
            RecipeAsset.OnInstructionCompleted += OnCompleteInstructionEvents;

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
            RecipeAsset.OnInstructionStarted -= OnStartInstructionEvents;
            RecipeAsset.OnInstructionFailed -= OnFailInstructionEvents;
            RecipeAsset.OnInstructionCompleted -= OnCompleteInstructionEvents;
        }

        private void OnCallEvent(InputAction.CallbackContext context)
        {
            for (int i = 0; i < inputEvents.Length; i++)
            {
                if(inputEvents[i].ActivationKey == context.action.name)
                    inputEvents[i].CallInputEvent(context);
            }
        }

        #region Start / Failed / Success  Events
        private void OnStartInstructionEvents(RecipeInstructionInfo _info)
        {
            int _index = _info.Index;
            for (int i = 0; i < startInstructionEvents.Length; i++)
            {
                if (startInstructionEvents[i].ActivationKey == _index)
                    startInstructionEvents[i].CallEvent(_info);
            }
        }

        private void OnFailInstructionEvents(int _index)
        {
            for (int i = 0; i < failedInstructionEvents.Length; i++)
            {
                if (failedInstructionEvents[i].ActivationKey == _index)
                    failedInstructionEvents[i].CallEvent();
            }
        }

        private void OnCompleteInstructionEvents(int _index)
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
        public void CallEvent(RecipeInstructionInfo _info) => recipeEvent.CallEvent(targetObject, _info);
        public void CallInputEvent(InputAction.CallbackContext _context) => recipeEvent.CallEvent(_context, targetObject);

        public void ResetEvent() => recipeEvent.ResetEvent();
    }
}
