using System;

namespace PCGToolkit.Sampling
{
    public class Sampling2DHandle<TTile>
    {
        public Grid2D<TTile> Grid { get; }
        public bool IsFinished => !_hasNextFunction();

        private Func<bool> _hasNextFunction;
        private Func<SampleStep2D<TTile>> _sampleFunction;

        public Sampling2DHandle(
            Grid2D<TTile> grid,
            Func<bool> hasNextFunction,
            Func<SampleStep2D<TTile>> sampleFunction)
        {
            Grid = grid;
            _hasNextFunction = hasNextFunction;
            _sampleFunction = sampleFunction;
        }

        public Grid2D<TTile> Execute()
        {
            while (!IsFinished)
            {
                DoStep();
            }

            return Grid;
        }

        public SampleStep2D<TTile> ExecuteNextStep()
        {
            if (IsFinished)
            {
                throw new InvalidOperationException("There is no more tile to sample");
            }

            return DoStep();
        }

        private SampleStep2D<TTile> DoStep()
        {
            SampleStep2D<TTile> step = _sampleFunction();
            Grid[step.Coordinate.X, step.Coordinate.Y] = step.Item;
            return step;
        }
    }
}