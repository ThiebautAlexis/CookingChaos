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
        public void CallEvent(InputAction.CallbackContext context, SpriteRenderer _renderer)
        {
            if (context.started)
                CallEventStart(context, _renderer);
            else if (context.performed)
                CallEventPerformed(_renderer);
            else if (context.canceled)
                CallEventCanceled(_renderer);
        }

        protected virtual void CallEventStart(InputAction.CallbackContext context, SpriteRenderer _spriteRenderer) => OnInputStart(_spriteRenderer);   
        protected void CallEventCanceled(SpriteRenderer _spriteRenderer) => OnInputCanceled( _spriteRenderer);
        protected void CallEventPerformed(SpriteRenderer _spriteRenderer) => OnInputPerformed( _spriteRenderer);


        #region IInputEvent
        public virtual void OnInputStart(SpriteRenderer _renderer){}

        public virtual void OnInputCanceled(SpriteRenderer _renderer){}

        public virtual void OnInputPerformed(SpriteRenderer _renderer){}
        #endregion 

        #endregion

        protected enum EventActivation
        {
            OnStarted, 
            OnPerformed,
            OnCanceled
        }
    }
}
