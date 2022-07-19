using System;
using UnityEngine;
using DG.Tweening;

namespace CookingChaos.InputEvents
{
    [CreateAssetMenu(fileName = "Move To Position", menuName = RECIPE_EVENT_MENU_NAME + "Move to Position Event", order = RECIPE_EVENT_MENU_ORDER)]

    public class MoveToPositionEvent : RecipeEvent
    {
        #region Fields and Properties
        [SerializeField] private Vector2[] targetPositions = new Vector2[] { };
        [SerializeField] private MovementAttributes attributes;
        [SerializeField] private EventActivation eventActivation = EventActivation.OnPerformed;
        [SerializeField] private bool overrideSortingOrder = false;
        private int currentIndex = 0;
        #endregion

        #region Methods 
        public override void OnInputPerformed(SpriteRenderer _renderer)
        {
            if (eventActivation == EventActivation.OnPerformed)
                MoveToPosition(_renderer);
        }

        public override void OnInputCanceled(SpriteRenderer _renderer)
        {
            if (eventActivation == EventActivation.OnCanceled)
                MoveToPosition(_renderer);
        }

        public override void OnInputStart(SpriteRenderer _renderer)
        {
            if (eventActivation == EventActivation.OnStarted)
                MoveToPosition(_renderer);
        }


        public void MoveToPosition(SpriteRenderer _renderer)
        {
            if (currentIndex >= targetPositions.Length) currentIndex = 0;
            if (overrideSortingOrder)
                _renderer.sortingOrder = currentIndex;

            float _time = Vector2.Distance(_renderer.transform.position, targetPositions[currentIndex]) / attributes.MovementSpeed;
            Sequence movementSequence = DOTween.Sequence();
            {
                movementSequence.AppendInterval(.75f);
                movementSequence.Append(_renderer.transform.DOLocalMove(targetPositions[currentIndex], _time).SetEase(attributes.MovementEase));
            };
            currentIndex++;
        }
        #endregion
    }
}
