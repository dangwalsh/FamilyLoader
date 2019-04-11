using Autodesk.Revit.Creation;

namespace Gensler.Revit.FamilyLoader
{
    using System;
    using System.Linq;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.DB.Structure;

    /// <summary>
    /// Places the imported families and their types into the active Revit document.
    /// </summary>
    internal class Placer
    {
        private const double Height = 10;

        public static void Place(Data data)
        {
            var create = Loader.Document.Create;
            var familySymbols = data.FamilySymbols;

            foreach (var familySymbol in familySymbols)
            {
                using (var transaction = new Transaction(Loader.Document, "Place Instance"))
                {
                    transaction.Start();

                    try
                    {
                        switch (data.Family.FamilyPlacementType)
                        {
                            case FamilyPlacementType.OneLevelBased:
                                PlaceLevelBased(create, familySymbol);
                                break;
                            case FamilyPlacementType.OneLevelBasedHosted:
                                switch (data.Family.get_Parameter(BuiltInParameter.FAMILY_HOSTING_BEHAVIOR).AsInteger())
                                {
                                    case 5: // face
                                        PlaceLevelBasedHosted(create, CreateFace(), familySymbol);
                                        break;
                                    case 4: // roof
                                        PlaceLevelBasedHosted(create, CreateRoof(), familySymbol);
                                        break;
                                    case 3: // ceiling
                                        PlaceLevelBasedHosted(create, CreateCeiling(), familySymbol);
                                        break;
                                    case 2: // floor
                                        PlaceLevelBasedHosted(create, CreateFloor(), familySymbol);
                                        break;
                                    case 1: // wall
                                        PlaceLevelBasedHosted(create, CreateWall(), familySymbol);
                                        break;
                                    default: // not hosted
                                        PlaceLevelBased(create, familySymbol);
                                        break;
                                }
                                break;
                            case FamilyPlacementType.ViewBased:
                                PlaceViewBased(create, familySymbol);
                                break;
                            case FamilyPlacementType.TwoLevelsBased:
                                throw new NotSupportedException("Two Level Based families are currently not supported.");
                            case FamilyPlacementType.WorkPlaneBased:
                                PlaceLevelBasedHosted(create, CreateFace(), familySymbol);
                                break;
                            case FamilyPlacementType.CurveBased:
                                throw new NotSupportedException("Curve Based families are currently not supported.");
                            case FamilyPlacementType.CurveBasedDetail:
                                throw new NotSupportedException("Curve Based Detail families are not supported.");
                            case FamilyPlacementType.CurveDrivenStructural:
                                throw new NotSupportedException("Curve Driven Structural families are currently not supported.");
                            case FamilyPlacementType.Adaptive:
                                throw new NotSupportedException("Adaptive families are currently not supported.");
                            case FamilyPlacementType.Invalid:
                                throw new NotSupportedException("Family type is not supported.");
                            default:
                                throw new Exception("PlacementType does not exist.");
                        }
                    }
                    catch (Exception e)
                    {
                        TaskDialog.Show("Error", e.Message);
                        throw;
                    }

                    transaction.Commit();
                }

                Arrangement.NextColumn();
            }

            Arrangement.NextRow();
        }

        private static void PlaceViewBased(ItemFactoryBase create, FamilySymbol familySymbol)
        {
            create.NewFamilyInstance(Arrangement.Current.Center, familySymbol, Loader.View);
        }

        private static void PlaceLevelBased(Autodesk.Revit.Creation.Document create, FamilySymbol familySymbol)
        {
            create.NewFamilyInstance(Arrangement.Current.Center, familySymbol, Loader.Level, StructuralType.NonStructural);
        }

        private static void PlaceLevelBasedHosted(Autodesk.Revit.Creation.Document create, Element host, FamilySymbol familySymbol)
        {
            create.NewFamilyInstance(Arrangement.Current.Center, familySymbol, host, Loader.Level, StructuralType.NonStructural);
        }

        private static Element CreateFace()
        {
            return Loader.View.SketchPlane;
        }

        private static Element CreateCeiling()
        {
            return Loader.View.SketchPlane;
        }

        private static Element CreateRoof()
        {
            var curveArray = CreateCurveArray();
            var modelCurveArray = new ModelCurveArray();
            var roofType = new FilteredElementCollector(Loader.Document).OfClass(typeof(RoofType)).FirstOrDefault() as RoofType;
            var roof = Loader.Document.Create.NewFootPrintRoof(curveArray, Loader.Level, roofType, out modelCurveArray);

            return roof;
        }

        private static Element CreateFloor()
        {
            var curveArray = CreateCurveArray();
            var floor = Loader.Document.Create.NewFloor(curveArray, false);

            return floor;
        }

        private static Element CreateWall()
        {
            var line = CreateLine(Arrangement.Current.M4, Arrangement.Current.M2);
            var wall = Wall.Create(Loader.Document, line, Loader.Level.Id, false);
            wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).Set(10.0);

            return wall;
        }

        private static Curve CreateLine(XYZ start, XYZ end)
        {
            return Line.CreateBound(start, end);
        }

        private static CurveArray CreateCurveArray()
        {
            var curveArray = new CurveArray();
            curveArray.Append(CreateLine(Arrangement.Current.P1, Arrangement.Current.P2));
            curveArray.Append(CreateLine(Arrangement.Current.P2, Arrangement.Current.P3));
            curveArray.Append(CreateLine(Arrangement.Current.P3, Arrangement.Current.P4));
            curveArray.Append(CreateLine(Arrangement.Current.P4, Arrangement.Current.P1));

            return curveArray;
        }
    }
}
