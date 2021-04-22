namespace AzureMapsControl.Components.Atlas
{
    public class Either<T1, T2>
    {
        internal bool HasFirstChoice => FirstChoice is not null;

        internal T1 FirstChoice { get; }
        internal T2 SecondChoice { get; }

        protected Either() { }

        public Either(T1 firstChoice) => FirstChoice = firstChoice;

        public Either(T2 secondChoice) => SecondChoice = secondChoice;
    }

    public class Either<T1, T2, T3> : Either<T1, T2>
    {
        internal bool HasSecondChoice { get; set; }

        internal T3 ThirdChoice { get; set; }

        public Either(T1 firstChoice) : base(firstChoice) { }

        public Either(T2 secondChoice) : base(secondChoice) { }

        public Either(T3 thirdChoice) => ThirdChoice = thirdChoice;
    }
}
