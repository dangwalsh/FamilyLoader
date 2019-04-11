﻿namespace Gensler.Revit.FamilyLoader
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.Attributes;
    using Views;

    [Transaction(TransactionMode.Manual)]
    public class RevitCommand : IExternalCommand
    {
        public static UIDocument UiDocument { get; private set; }
        public static Document Document { get; private set; }
        public static RequestHandler RequestHandler { get; private set; }
        public static ExternalEvent ExternalEvent { get; private set; }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UiDocument = commandData.Application.ActiveUIDocument;
            Document = commandData.Application.ActiveUIDocument.Document;

            //var handler = new RequestHandler();
            //var exEvent = ExternalEvent.Create(handler);
            //var window = new MainWindow(exEvent, handler);

            RequestHandler = new RequestHandler();
            ExternalEvent = ExternalEvent.Create(RequestHandler);
            var window = new MainWindow();

            window.Show();

            return Result.Succeeded;
        }
    }
}
