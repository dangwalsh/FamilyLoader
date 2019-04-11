using System;
using Autodesk.Revit.UI;

namespace Gensler.Revit.FamilyLoader
{
    using System.IO;
    using Autodesk.Revit.DB;

    internal class Loader
    {
        private const string Failure =   "The add-in has run into an issue."
                                       + " It is most likely due to an unsupported"
                                       + " family type being loaded.  If you are"
                                       + " interested here is the detailed message:\n\n";

        public static Document Document { get; private set; }
        public static View View { get; private set; }
        public static Level Level { get; private set; }
        public int Count { get; }
        public TimeSpan Time { get; }

        public Loader(Document document, string path)
        {
            Arrangement.Reset();

            Document = document;
            View = Document.ActiveView;
            Level = Document.GetElement(View.GenLevel.Id) as Level;
            Count = 0;

            var startTime = DateTime.Now;

            foreach (var file in Directory.EnumerateFiles(path, "*.rfa",SearchOption.AllDirectories))
            {
                var data = new Data(file);
                try
                {
                    Placer.Place(data);
                    Count++;
                }
                catch (Exception e)
                {                  
                    TaskDialog.Show("Error", Failure + e.Message);
                }
            }

            Time = DateTime.Now - startTime;
        }
    }
}
