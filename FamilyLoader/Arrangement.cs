using Autodesk.Revit.DB;

namespace Gensler.Revit.FamilyLoader
{
    internal static class Arrangement
    {
        private static XYZ _basis;
        private static Quad _current;

        private static int Row { get; set; }

        private static int Column { get; set; }

        public static XYZ Basis
        {
            get => _basis ?? (_basis = new XYZ());
            set => _basis = value;
        }

        public static Quad Current
        {
            get => _current ?? (_current = new Quad(Row, Column, Basis));
            private set => _current = value;
        }

        public static void NextRow()
        {
            Current = new Quad(++Row, Column = 0, Basis);
        }

        public static void NextColumn()
        {
            Current = new Quad(Row, ++Column, Basis);
        }

        public static void Reset()
        {
            Row = 0;
            Column = 0;
        }
    }
}
