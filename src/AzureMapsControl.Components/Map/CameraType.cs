namespace AzureMapsControl.Components.Map
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class CameraType
    {
        private readonly string _type;

        public static readonly CameraType Ease = new CameraType("ease");
        public static readonly CameraType Fly = new CameraType("fly");
        public static readonly CameraType Jump = new CameraType("jump");

        private CameraType(string type) => _type = type;

        public override string ToString() => _type;
    }
}
