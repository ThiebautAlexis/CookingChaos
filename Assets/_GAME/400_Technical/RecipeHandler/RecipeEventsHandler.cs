using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CookingChaos
{
    public class RecipeEventsHandler : MonoBehaviour
    {
        #region Fields and Properties
        [SerializeField] private RecipeEventRendererPair[] events; 
        #endregion

        #region Methods 
        public void CallSuccessInstructionEvent(int _index)
        {

        }

        void OnCallEvent(InputAction.CallbackContext context)
        {
            for (int i = 0; i < events.Length; i++)
            {
                if(events[i].ActivationKey == context.action.name)
                    events[i].CallEvent(context);
            }
        }

        #region Failed Events

        #endregion

        #endregion
    }

    [Serializable]
    public struct RecipeEventRendererPair
    {
        [SerializeField] private InputEvents.RecipeEvent Event;
        [SerializeField] private SpriteRenderer renderer;
        [SerializeField] private string activationKey;
        public string ActivationKey => activationKey;

        public void CallEvent(InputAction.CallbackContext _context) => Event.CallEvent(_context, renderer);
    }
}
