using UnityEngine;
using DG.Tweening;

namespace CookingChaos.InputEvents
{
    [CreateAssetMenu(fileName = "Move Along Path Event", menuName = RECIPE_EVENT_MENU_NAME + "Move Along path", order = RECIPE_EVENT_MENU_ORDER)]
    public class FollowPathEvent : RecipeEvent
    {
        #region Fields and Properties
        [SerializeField] private Vector3[] path = new Vector3[] { };
        [SerializeField] private bool overrideSortingOrder = false;
        [SerializeField] private int targetSortingOrder = 0;
        [SerializeField] private float movementDelay = 0f;
        [SerializeField] private float movementDuration = 1f;
        [SerializeField] private Ease movementEase = Ease.Linear;
        #endregion

        #region Methods
        public override void CallEvent(GameObject _targetObject)
        {
            RecipeInstructionInfo _info = new RecipeInstructionInfo
            {
                Index = 0,
                BeforeStartDelay = movementDelay,
                BeforeActivationDelay = movementDuration
            };
            CallEvent(_targetObject, _info);
        }

        public override void CallEvent(GameObject _targetObject, RecipeInstructionInfo _info)
        {
            Sequence movementSequence = DOTween.Sequence();
            {
                movementSequence.AppendInterval(_info.BeforeStartDelay);
                movementSequence.Append(_targetObject.transform.DOPath(path, _info.BeforeActivationDelay, PathType.CatmullRom).SetEase(movementEase));
                movementSequence.onComplete += OnSequenceCompleted;
            };

            void OnSequenceCompleted()
            {
                if(overrideSortingOrder)
                {
                    if (_targetObject.TryGetComponent(out SpriteRenderer _rend))
                        _rend.sortingOrder = targetSortingOrder;
                }
                movementSequence.Kill();
            }
        }
        #endregion
    }
}
