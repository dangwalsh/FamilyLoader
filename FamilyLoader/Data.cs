namespace Gensler.Revit.FamilyLoader
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.DB;

    internal class Data
    {
        private readonly Family _family;
        private List<FamilySymbol> _familySymbols;

        public Family Family => _family;

        public List<FamilySymbol> FamilySymbols
        {
            get
            {
                if (_familySymbols != null) return _familySymbols;

                _familySymbols = new List<FamilySymbol>();

                foreach (var elementId in this.Family.GetFamilySymbolIds())
                {
                    if (!(Loader.Document.GetElement(elementId) is FamilySymbol familySymbol)) continue;
                    if (!familySymbol.IsActive)
                    {
                        using (var transaction = new Transaction(Loader.Document, "Activate Type"))
                        {
                            transaction.Start();

                            try
                            {
                                familySymbol.Activate();
                            }
                            catch (Exception e)
                            {
                                TaskDialog.Show("Error", e.Message);
                                throw;
                            }

                            transaction.Commit();
                        }                      
                    }    

                    _familySymbols.Add(familySymbol);
                }

                return _familySymbols;
            }
        }

        public Data(string filename)
        {
            using (var transaction = new Transaction(Loader.Document, "Load Family"))
            {
                transaction.Start();

                try
                {
                    if (!Loader.Document.LoadFamily(filename, new Options(), out _family))
                        RetrieveLoadedFamily(RemovePath(filename), out _family);
                }
                catch (Exception e)
                {
                    TaskDialog.Show("Error",e.Message);
                    throw;
                }
                
                transaction.Commit();
            }
                
        }

        private static void RetrieveLoadedFamily(string name, out Family family)
        {
            family = new FilteredElementCollector(Loader.Document)
                .OfClass(typeof(Family))
                .OfType<Family>()
                .FirstOrDefault(x => x.Name == name);
        }

        private static string RemovePath(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
    }
}