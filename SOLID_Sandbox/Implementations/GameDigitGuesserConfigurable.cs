using SOLID_Sandbox.Abstractions;

namespace SOLID_Sandbox
{
    public class GameDigitGuesserConfigurable : GameDigitGuesser
    {
        private readonly IConfigProvider _configurator;

        public GameDigitGuesserConfigurable(
            IMessageWriter writer, 
            IMessageReader reader, 
            IRandomGenerator random,
            IConfigProvider configurator)
            : base (writer, reader, random)
        {
            _configurator = configurator;
            Setup();
        }

        protected void Setup()
        {
            _min = _configurator.GetMin();
            _max = _configurator.GetMax();

            if (_min > _max)
                _min = _max;

            _maxGuessCount = _configurator.GetGuessCount();

            if (_maxGuessCount <= 0)
                _maxGuessCount = 1;
        }
    }
}
