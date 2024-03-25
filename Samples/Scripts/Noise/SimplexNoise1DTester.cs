using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

namespace PCGToolkit.Sampling.Examples
{
    public class SimplexNoise1DTester : MonoBehaviour
    {
        [SerializeField] private int _seed = 1000;

        [SerializeField] private Vector2 _range = new Vector2(0, 10);

        [FormerlySerializedAs("_samples")] [SerializeField]
        private int _samplesAmount = 100;

        [SerializeField] private RectTransform _hook;

        [SerializeField] private Noise1DSample _samplePrefab;

        [SerializeField] private Button _generateButton;

        [SerializeField] private List<Noise1DStep> _steps = new List<Noise1DStep>() { new Noise1DStep() };

        private List<Noise1DSample> _samples = new List<Noise1DSample>();
        private SimplexNoise1D _noise;

        private void Start()
        {
            _generateButton.onClick.AddListener(OnGenerateButtonClick);
        }

        private void OnDestroy()
        {
            _generateButton.onClick.RemoveListener(OnGenerateButtonClick);
            Clear();
        }

        private void OnGenerateButtonClick()
        {
            _noise = SimplexNoise1D.Create(new Random(_seed));
            Clear();
            GenerateSamples();
        }

        private void GenerateSamples()
        {
            float delta = (_range.y - _range.x) / _samplesAmount;
            float containerWidth = _hook.rect.width;
            float widthDelta = containerWidth / _samplesAmount;

            for (int i = 0; i < _samplesAmount; i++)
            {
                float sample = 0;

                foreach (Noise1DStep step in _steps)
                {
                    sample += _noise.Evaluate((_range.x + i * delta) * step.IntervalScale + step.Offset) *
                              step.ValueScale;
                }

                sample = _steps.Count > 0 ? sample / _steps.Count : sample;
                Noise1DSample sampleElement = Instantiate(_samplePrefab, _hook, false);
                sampleElement.Init(widthDelta, sample);
                _samples.Add(sampleElement);
            }
        }

        private void Clear()
        {
            foreach (Noise1DSample sample in _samples)
            {
                Destroy(sample.gameObject);
            }

            _samples.Clear();
        }
    }
}