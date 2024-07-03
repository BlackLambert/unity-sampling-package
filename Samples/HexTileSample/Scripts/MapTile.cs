using UnityEngine;

namespace SBaier.Sampling.Samples.HexTileSample
{
    public class MapTile : MonoBehaviour
    {
        [field: SerializeField] private SpriteRenderer _renderer;
        
        public void SetSprite(Sprite sprite)
        {
            _renderer.sprite = sprite;
        }
    }
}
