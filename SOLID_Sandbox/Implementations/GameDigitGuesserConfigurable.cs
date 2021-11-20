using SOLID_Sandbox.Abstractions;

namespace SOLID_Sandbox
{
    public class GameDigitGuesserConfigurable : GameDigitGuesser
    {
        private readonly IConfigProvider mConfigurator;

        public GameDigitGuesserConfigurable(
            IMessageWriter writer, 
            IMessageReader reader, 
            IRandomGenerator random,
            IConfigProvider configurator)
            : base (writer, reader, random)
        {
            mConfigurator = configurator;
            Setup();
        }

        protected void Setup()
        {
            mMin = mConfigurator.GetMin();
            mMax = mConfigurator.GetMax();

            if (mMin > mMax)
                mMin = mMax;

            mMaxGuessCount = mConfigurator.GetGuessCount();

            if (mMaxGuessCount <= 0)
                mMaxGuessCount = 1;
        }
    }
}
