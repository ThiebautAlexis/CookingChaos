using System;
using UnityEngine;

namespace CookingChaos.InputEvents
{
    public interface IInputEventInterface
    {
        public void OnInputStart(GameObject _targetObject);
        public void OnInputCanceled(GameObject _targetObject);
        public void OnInputPerformed(GameObject _targetObject);
    }

    public interface IHoldEventInterface
    {
        public void OnHoldStart(GameObject _targetObject, float _holdingDuration);
    }

    public interface IMultiTapEventInterface
    {
        public void OnMultiTapStart(GameObject _targetObject, int _multitapCount);
    }
}
