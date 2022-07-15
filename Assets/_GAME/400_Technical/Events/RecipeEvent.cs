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
        internal virtual void CallEventStart(int multitapLength) { }
        internal virtual void CallEventStart(float holdingDuration) { }
        internal virtual void CallEventStart() { }

        internal virtual void CallEventStop() { }
        internal virtual void CallEventPerformed() { }
        internal virtual void CallEventFailed(string eventKey) { }


        #endregion
    }
}
