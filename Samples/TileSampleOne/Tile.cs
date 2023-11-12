using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCGToolkit.Sampling.Examples.TileSampleOne
{
    public class Tile : MonoBehaviour
    {
        [field: SerializeField] private Image _image;
        public RectTransform RectTransform => (RectTransform)transform;
        
        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}
