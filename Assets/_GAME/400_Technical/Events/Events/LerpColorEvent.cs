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
        protected override void CallEventStart(UnityEngine.InputSystem.InputAction.CallbackContext _context, SpriteRenderer _spriteRenderer)
        {
            base.CallEventStart(_context, _spriteRenderer);
            OnHoldStart(_spriteRenderer, (_context.interaction as UnityEngine.InputSystem.Interactions.HoldInteraction).duration);
        }
        #endregion

        #region IInputEventStart
        public override void OnInputCanceled(SpriteRenderer _renderer)
        {
            base.OnInputCanceled(_renderer);
            if(sequence.IsActive())
            {
                sequence.Pause();
                sequence.Rewind(true);
            }
        }
        #endregion

        #region IHoldEventInterface
        public void OnHoldStart(SpriteRenderer _renderer, float _holdingDuration)
        {
            if (sequence.IsActive())
                sequence.Kill(false);
            sequence = DOTween.Sequence();
            {
                _renderer.DOGradientColor(colorGradient, _holdingDuration);
            };
        }
        #endregion

        #endregion
    }
}
