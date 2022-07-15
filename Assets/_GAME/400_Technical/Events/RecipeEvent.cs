using System;
using UnityEngine;

namespace CookingChaos
{
    public abstract class RecipeEvent : MonoBehaviour
    {
        #region Fields and Properties
        [SerializeField] protected string activationKey = string.Empty;
        public string ActivationKey => activationKey;
        #endregion

        #region Methods 
        public abstract void CallEvent();

        public abstract void CallEvent(float _holdingProgress);

        public abstract void CallEvent(int _multitapCount);
        #endregion
    }
}
