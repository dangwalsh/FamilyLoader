

namespace Gensler.Revit.FamilyLoader.Views
{
    using System;
    using Autodesk.Revit.UI;

    public class RequestHandler : IExternalEventHandler
    {
        public Request _request = new Request();

        public Request Request
        {
            get { return _request; }
        }

        public void Execute(UIApplication app)
        {
            try
            {
                var request = Request.Take();

                switch (request.Id)
                {
                    case RequestId.None:
                        return;
                    case RequestId.GetPoint:
                        // call pickpoint
                        PickPoints(app);
                        break;
                    case RequestId.Load:
                        // instantiate loader
                        LoadFamilies(app, request.Text);
                        break;
                    default:
                        throw new Exception("Invalid Request Id.");
                }
            }
            catch (Exception e)
            {
                TaskDialog.Show("Error", e.Message);
            }
        }

        private static void PickPoints(UIApplication app)
        {
            throw new NotImplementedException();
        }

        private static void LoadFamilies(UIApplication app, string path)
        {
            var loader = new Loader(RevitCommand.Document, path);
        }

        public string GetName()
        {
            return "Family Loader";
        }
    }
}
