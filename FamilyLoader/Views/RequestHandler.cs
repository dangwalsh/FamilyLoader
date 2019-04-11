

namespace Gensler.Revit.FamilyLoader.Views
{
    using System;
    using Autodesk.Revit.UI;

    public class RequestHandler : IExternalEventHandler
    {
        private Request _request = new Request();

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
                        break;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static void PickPoints(UIApplication app)
        {
            throw new NotImplementedException();
        }

        private static void LoadFamilies(UIApplication app, string path)
        {
            var loader = new Loader(app.ActiveUIDocument.Document, path);
        }

        public string GetName()
        {
            return "Family Loader";
        }
    }
}
