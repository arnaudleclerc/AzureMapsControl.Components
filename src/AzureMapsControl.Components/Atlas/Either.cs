namespace AzureMapsControl.Components.Atlas
{
    public sealed class Either<TType, TOr>
    {
        internal bool HasFirstChoice => FirstChoice is not null;

        internal TType FirstChoice { get; }
        internal TOr SecondChoice { get; }

        public Either(TType firstChoice) => FirstChoice = firstChoice;

        public Either(TOr secondChoice) => SecondChoice = secondChoice;
    }
}
