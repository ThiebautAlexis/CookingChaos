using System;
using UnityEngine;

namespace CookingChaos.InputEvents
{
    [CreateAssetMenu(fileName = "Movement Attribute", menuName = "Cooking Chaos/Attributes/MovementAttribute", order = 150)]

    public class MovementAttributes : ScriptableObject
    {
        #region Fields and Properties
        public float MovementSpeed = 1f;
        public DG.Tweening.Ease MovementEase = DG.Tweening.Ease.InOutBounce;
        #endregion
    }
}
