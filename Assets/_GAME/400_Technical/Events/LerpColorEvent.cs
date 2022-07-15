using System;
using UnityEngine;

namespace CookingChaos
{
    public class LerpColorEvent : RecipeEvent
    {
        #region Fields and Properties
        [SerializeField] private Gradient colorGradient = new Gradient();
        [SerializeField] private SpriteRenderer sprite = null;
        #endregion

        #region Methods 

        public override void CallEvent()
        {
        }

        public override void CallEvent(float _holdingProgress)
        {
            sprite.color = colorGradient.Evaluate(_holdingProgress);
        }

        public override void CallEvent(int _multitapCount)
        {
        }
        #endregion
    }
}
