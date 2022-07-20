using System;
using UnityEngine;
using DG.Tweening;

namespace CookingChaos.InputEvents
{
    [CreateAssetMenu(fileName = "LerpColorEvent", menuName = RECIPE_EVENT_MENU_NAME + "Lerp Color Event", order = RECIPE_EVENT_MENU_ORDER)]
    public class LerpColorEvent : RecipeEvent, IHoldEventInterface
    {
        #region Fields and Properties
        [SerializeField] private Gradient colorGradient = new Gradient();
        private Sequence sequence = null;
        #endregion

        #region Methods 

        #region Recipe Event
        public override void CallEvent(GameObject _targetObject) {}

        protected override void CallEventStart(UnityEngine.InputSystem.InputAction.CallbackContext _context, GameObject _targetObject)
        {
            base.CallEventStart(_context, _targetObject);
            OnHoldStart(_targetObject, (_context.interaction as UnityEngine.InputSystem.Interactions.HoldInteraction).duration);
        }
        #endregion

        #region IInputEventStart
        public override void OnInputCanceled(GameObject _targetObject)
        {
            base.OnInputCanceled(_targetObject);
            if(sequence.IsActive())
            {
                sequence.Pause();
                sequence.Rewind(true);
            }
        }
        #endregion

        #region IHoldEventInterface
        public void OnHoldStart(GameObject _targetObject, float _holdingDuration)
        {
            if(_targetObject.TryGetComponent(out SpriteRenderer _renderer))
            {
                if (sequence.IsActive())
                    sequence.Kill(false);
                sequence = DOTween.Sequence();
                {
                    _renderer.DOGradientColor(colorGradient, _holdingDuration);
                };
            }
        }
        #endregion

        #endregion
    }
}
