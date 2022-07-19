using System;
using UnityEngine;

namespace CookingChaos.InputEvents
{
    public interface IInputEventInterface
    {
        public void OnInputStart(SpriteRenderer _renderer);
        public void OnInputCanceled(SpriteRenderer _renderer);
        public void OnInputPerformed(SpriteRenderer _renderer);
    }

    public interface IHoldEventInterface
    {
        public void OnHoldStart(SpriteRenderer _renderer, float _holdingDuration);
    }

    public interface IMultiTapEventInterface
    {
        public void OnMultiTapStart(SpriteRenderer _renderer, int _multitapCount);
    }
}
