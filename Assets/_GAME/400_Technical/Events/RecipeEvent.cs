using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace CookingChaos.InputEvents
{
    public abstract class RecipeEvent : ScriptableObject, IInputEventInterface
    {
        #region Fields and Properties
        protected const string RECIPE_EVENT_MENU_NAME = "Cooking Chaos/Recipe Events/";
        protected const int RECIPE_EVENT_MENU_ORDER = 150;
        #endregion

        #region Methods 
        public abstract void CallEvent(GameObject _targetObject);

        public virtual void CallEvent(InputAction.CallbackContext context, GameObject _targetObject)
        {
            if (context.started)
                CallEventStart(context, _targetObject);
            else if (context.performed)
                CallEventPerformed(_targetObject);
            else if (context.canceled)
                CallEventCanceled(_targetObject);
        }

        protected virtual void CallEventStart(InputAction.CallbackContext context, GameObject _targetObject) => OnInputStart(_targetObject);
        protected void CallEventCanceled(GameObject _targetObject) => OnInputCanceled(_targetObject);
        protected void CallEventPerformed(GameObject _targetObject) => OnInputPerformed(_targetObject);

        public virtual void ResetEvent() { }

        #region IInputEvent
        public virtual void OnInputStart(GameObject _renderer) { }

        public virtual void OnInputCanceled(GameObject _renderer) { }

        public virtual void OnInputPerformed(GameObject _renderer) { }
        #endregion
        #endregion

        protected enum EventActivation
        {
            None,
            OnStarted,
            OnPerformed,
            OnCanceled
        }
    }
}
