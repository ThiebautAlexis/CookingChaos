using System;
using UnityEngine;
using DG.Tweening;

namespace CookingChaos
{
    public class LerpColorEvent : RecipeEvent
    {
        #region Fields and Properties
        [SerializeField] private Gradient colorGradient = new Gradient();
        [SerializeField] private SpriteRenderer sprite = null;
        private Sequence sequence = null;

        #endregion

        #region Methods 
        internal override void CallEventStart(float holdingDuration)
        {
            if (sequence.IsActive())
                sequence.Kill(false);
            sequence = DOTween.Sequence();
            {
                sprite.DOGradientColor(colorGradient, holdingDuration);
            };
        }

        internal override void CallEventStop()
        {
            if (sequence.IsActive())
            {
                sequence.Pause();
                sequence.Rewind();
            }
        }
        #endregion
    }
}
