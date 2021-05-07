namespace AzureMapsControl.Components.Atlas
{
    public class Either<T1, T2>
    {
        internal bool HasFirstChoice => FirstChoice is not null && FirstChoice.ToString() != default(T1)?.ToString();

        internal T1 FirstChoice { get; }
        internal T2 SecondChoice { get; }

        protected Either() { }

        public Either(T1 firstChoice) => FirstChoice = firstChoice;

        public Either(T2 secondChoice) => SecondChoice = secondChoice;
    }

    public class Either<T1, T2, T3> : Either<T1, T2>
    {
        internal bool HasSecondChoice => SecondChoice is not null && SecondChoice.ToString() != default(T2)?.ToString();

        internal T3 ThirdChoice { get; set; }

        public Either(T1 firstChoice) : base(firstChoice) { }

        public Either(T2 secondChoice) : base(secondChoice) { }

        public Either(T3 thirdChoice) => ThirdChoice = thirdChoice;
    }
}
