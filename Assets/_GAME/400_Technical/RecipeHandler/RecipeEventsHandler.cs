using System;
using UnityEngine;

namespace CookingChaos
{
    public class RecipeEventsHandler : MonoBehaviour
    {
        #region Fields and Properties
        [SerializeField] private RecipeEvent[] validInputEvents = new RecipeEvent[] { };
        [SerializeField] private RecipeEvent[] multiTapInputEvents = new RecipeEvent[] { };
        [SerializeField] private RecipeEvent[] holdingInputEvents = new RecipeEvent[] { };
        [SerializeField] private RecipeEvent[] failedInputEvents = new RecipeEvent[] { };
        #endregion

        #region Methods 

        internal void MultiTapInputEvent(string eventKey, int _tapCount)
        {
            for (int i = 0; i < multiTapInputEvents.Length; i++)
            {
                if (multiTapInputEvents[i].ActivationKey == eventKey)
                    multiTapInputEvents[i].CallEvent(_tapCount);
            }
        }
        internal void HoldingInputEvent(string eventKey, float _holdingProgress)
        {
            for (int i = 0; i < holdingInputEvents.Length; i++)
            {
                if (holdingInputEvents[i].ActivationKey == eventKey)
                    holdingInputEvents[i].CallEvent(_holdingProgress);
            }
        }
        internal void ValidInputEvent(string eventKey)
        {
            for (int i = 0; i < validInputEvents.Length; i++)
            {
                if (validInputEvents[i].ActivationKey == eventKey)
                    validInputEvents[i].CallEvent();
            }
        }
        internal void FailedInstructionEvent()
        {
            for (int i = 0; i < failedInputEvents.Length; i++)
            {
                failedInputEvents[i].CallEvent();
            }
        }
        #endregion
    }
}
