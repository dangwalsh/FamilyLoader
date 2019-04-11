using Autodesk.Revit.DB;
namespace Gensler.Revit.FamilyLoader
{
    internal class Quad
    {
        private XYZ _center;

        public XYZ P1 { get; private set; }
        public XYZ P2 { get; private set; }
        public XYZ P3 { get; private set; }
        public XYZ P4 { get; private set; }

        public XYZ M1 => P1.Add(P2).Divide(2.0);
        public XYZ M2 => P2.Add(P3).Divide(2.0);
        public XYZ M3 => P3.Add(P4).Divide(2.0);
        public XYZ M4 => P4.Add(P1).Divide(2.0);

        public XYZ Center
        {
            get => _center;
            private set
            {
                _center = value;

                P1 = Center + new XYZ(-Width / 2.0, -Height / 2.0, 0);
                P2 = Center + new XYZ( Width / 2.0, -Height / 2.0, 0);
                P3 = Center + new XYZ( Width / 2.0,  Height / 2.0, 0);
                P4 = Center + new XYZ(-Width / 2.0,  Height / 2.0, 0);
            }
        }

        public static double Width { get; set; }
        public static double Height { get; set; }

        public Quad(int row, int column)
        {
            Center = new XYZ(column * Width, row * Height, 0);
        }

        public Quad(int row, int column, XYZ basis)
        {
            Center = new XYZ(column * Width + basis.X, row * Height + basis.Y, 0);
        }
    }
}
