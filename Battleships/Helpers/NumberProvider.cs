namespace Battleships.Helpers
{
    public class NumberProvider : INumberProvider
    {
        private readonly Random numberGenerator;

        public NumberProvider()
        {
            numberGenerator = new Random();
        }

        public int GetNumber(int minValue, int maxValue)
        {
            return numberGenerator.Next(minValue, maxValue + 1);
        }
    }
}
