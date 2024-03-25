using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCGToolkit.Sampling.Examples
{
    public class Noise1DSample : MonoBehaviour
    {
        [SerializeField] 
        private LayoutElement _layoutElement;

        [SerializeField] 
        private RectTransform _sampleIndicator;

        private RectTransform _rectTransform;
        private float _value;

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
        }

        public void Init(float width, float value)
        {
            _layoutElement.preferredWidth = width;
            _value = value;
        }

        private void Update()
        {
            float anchoredY = _value * _rectTransform.rect.height / 2;
            _sampleIndicator.anchoredPosition = new Vector2(_sampleIndicator.anchoredPosition.x, anchoredY);
        }
    }
}
