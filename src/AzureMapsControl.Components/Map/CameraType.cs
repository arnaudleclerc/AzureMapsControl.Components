namespace AzureMapsControl.Components.Map
{
    public sealed class CameraType
    {
        private readonly string _type;

        public static CameraType Ease = new CameraType("ease");
        public static CameraType Fly = new CameraType("fly");
        public static CameraType Jump = new CameraType("jump");

        private CameraType(string type) => _type = type;

        public override string ToString() => _type;
    }
}
