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

        #region Multi Tap Events
         internal void MultiTapInputEventStart(string eventKey, int _multitapLength)
        {
            for (int i = 0; i < multiTapInputEvents.Length; i++)
            {
                if (multiTapInputEvents[i].ActivationKey == eventKey)
                    multiTapInputEvents[i].CallEventStart(_multitapLength);
            }
        }

        internal void MultiTapInputEventStop(string eventKey)
        {
            for (int i = 0; i < multiTapInputEvents.Length; i++)
            {
                if (multiTapInputEvents[i].ActivationKey == eventKey)
                    multiTapInputEvents[i].CallEventStop();
            }
        }
        #endregion

        #region Hold Events
        internal void HoldingInputEventStart(string eventKey, float _holdingDuration)
        {
            for (int i = 0; i < holdingInputEvents.Length; i++)
            {
                if (holdingInputEvents[i].ActivationKey == eventKey)
                    holdingInputEvents[i].CallEventStart(_holdingDuration);
            }
        }

        internal void HoldingInputEventStop(string eventKey)
        {
            for (int i = 0; i < holdingInputEvents.Length; i++)
            {
                if (holdingInputEvents[i].ActivationKey == eventKey)
                    holdingInputEvents[i].CallEventStop();
            }
        }
        #endregion

        #region Input Events
        internal void InputEventStart(string eventKey)
        {
            for (int i = 0; i < validInputEvents.Length; i++)
            {
                if (validInputEvents[i].ActivationKey == eventKey)
                    validInputEvents[i].CallEventStart();
            }
        }
        internal void InputEventPerformed(string eventKey)
        {
            for (int i = 0; i < validInputEvents.Length; i++)
            {
                if (validInputEvents[i].ActivationKey == eventKey)
                    validInputEvents[i].CallEventPerformed();
            }
        }

        internal void InputEventStop(string eventKey)
        {
            for (int i = 0; i < validInputEvents.Length; i++)
            {
                if (validInputEvents[i].ActivationKey == eventKey)
                    validInputEvents[i].CallEventStop();
            }
        }
        #endregion

        #region Failed Events
        internal void FailedInstructionEvent(string eventKey)
        {
            for (int i = 0; i < failedInputEvents.Length; i++)
            {
                failedInputEvents[i].CallEventFailed(eventKey);
            }
        }
        #endregion

        #endregion
    }
}
