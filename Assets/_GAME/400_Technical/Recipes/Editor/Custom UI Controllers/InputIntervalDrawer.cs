using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using System;

namespace CookingChaos.Recipe.Editor
{
    public class InputIntervalDrawer : BindableElement, INotifyValueChanged<Vector2>
    {
        #region Fields and Properties
        public static string InputIntervalDrawer_UXML_Path = "Assets/_GAME/400_Technical/Recipes/Editor/Custom UI Controllers/InputIntervalDrawer_UXML.uxml";

        private static readonly string backgroundName = "background";
        private static readonly string progressName = "progress";
        private static readonly string maxValueName = "max-value";
        private static readonly string minIntervalName = "min-interval";
        private static readonly string maxIntervalName = "max-interval";

        private float maxValue = 1f;
        public float MaxValue
        {
            get { return maxValue; }
            set 
            {
                maxValue = value;
                if (maxIntervalLabel != null)
                    maxIntervalLabel.text = maxValue.ToString("0.00");
                SetProgress(m_value);
            }
        }

        public Vector2 m_value { get; set; }
        public virtual Vector2 value 
        {
            get { return m_value; }
            set
            {
                value.x = Mathf.Max(value.x, 0);
                value.y = Mathf.Min(value.y, 1);
                value.x = Mathf.Min(value.x, value.y);
                value.y = Mathf.Max(value.x, value.y);
                if(!EqualityComparer<Vector2>.Default.Equals(m_value, value))
                {
                    if(panel != null)
                    {
                        using (ChangeEvent<Vector2> evt = ChangeEvent<Vector2>.GetPooled(m_value, value))
                        {
                            evt.target = this;
                            SetValueWithoutNotify(value);
                            SendEvent(evt);
                        }
                    }
                    else
                    {
                        SetValueWithoutNotify(value);
                    }
                }
            }
        }
        #endregion

        #region UXML
        public new class UxmlFactory : UxmlFactory<InputIntervalDrawer, UxmlTraits>
        {
            public UxmlFactory() { }
        }
        public new class UxmlTraits : BindableElement.UxmlTraits
        {
            public UxmlTraits() { }
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) 
            {
            }
        }
        #endregion
        
        #region Constructor
        private readonly VisualElement background;
        private readonly VisualElement progress;
        private readonly Label maxValueLabel;
        private readonly Label minIntervalLabel;
        private readonly Label maxIntervalLabel;

        public InputIntervalDrawer()
        {
            VisualTreeAsset _asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(InputIntervalDrawer_UXML_Path);

            VisualElement _container = new VisualElement();
            _asset.CloneTree(_container);
            hierarchy.Add(_container);
 
            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
            background = _container.Q<VisualElement>(backgroundName);
            progress = _container.Q<VisualElement>(progressName);
            maxValueLabel = _container.Q<Label>(maxValueName);
            minIntervalLabel = _container.Q<Label>(minIntervalName);
            maxIntervalLabel = _container.Q<Label>(maxIntervalName);
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            SetProgress(value);
        }
        #endregion

        #region Methods
        public void SetValueWithoutNotify(Vector2 newValue)
        {
            m_value = newValue;
            SetProgress(value);
        }

        private void SetProgress(Vector2 _progress)
        {
            float _left = _progress.x, _right = _progress.y;
            _left = Mathf.Max(_left, 0);
            _left = Mathf.Min(_left, _right, 1);

            _right = Mathf.Max(_right, _left, 0);
            _right = Mathf.Min(_right, 1);

            if (background == null || progress == null)
            {
                return;
            }

            float _maxWidth = background.layout.width - 2;
            progress.style.left = _left * _maxWidth;
            if(minIntervalLabel!=null)
            {
                if (_left > .1f)
                {
                    minIntervalLabel.style.left = progress.style.left;
                    minIntervalLabel.text = (_left * maxValue).ToString("0.00");
                }
                else
                    minIntervalLabel.text = string.Empty;
            }

            progress.style.right = _maxWidth - (_right * _maxWidth);
            if(maxIntervalLabel != null)
            {
                if (_right < .9f)
                {
                    maxIntervalLabel.style.right = progress.style.right;
                    maxIntervalLabel.text = (_right * maxValue).ToString("0.00");
                }
                else
                    maxIntervalLabel.text = string.Empty;
            }
        }
        #endregion
    }
}
