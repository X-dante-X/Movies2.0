using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace AuthService.Services.RazorViewToStringRenderer
{

    public class RazorViewToStringRenderer : IRazorViewToStringRenderer
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public RazorViewToStringRenderer(
            IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider,
            IModelMetadataProvider modelMetadataProvider)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _modelMetadataProvider = modelMetadataProvider;
        }

        /// <summary>
        /// Returns the view filled with user data in a string form.
        /// </summary>

        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            var actionContext = GetActionContext();
            var view = FindView(actionContext, viewName);

            using (var output = new StringWriter())
            {
                var viewData = new ViewDataDictionary<TModel>(_modelMetadataProvider, actionContext.ModelState)
                {
                    Model = model
                };

                var tempData = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);

                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    viewData,
                    tempData,
                    output,
                    new HtmlHelperOptions());

                await view.RenderAsync(viewContext);

                return output.ToString();
            }
        }
        /// <summary>
        /// Finds the View of the email template in a form of .cshtml
        /// </summary>

        private IView FindView(ActionContext actionContext, string viewName)
        {
            var getViewResult = _razorViewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
            if (getViewResult.Success)
            {
                return getViewResult.View;
            }

            var findViewResult = _razorViewEngine.FindView(actionContext, viewName, isMainPage: true);
            if (findViewResult.Success)
            {
                return findViewResult.View;
            }

            var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
            var errorMessage = string.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{viewName}'." }.Concat(searchedLocations));

            throw new InvalidOperationException(errorMessage);
        }
        /// <summary>
        /// Helper function that enables to use the viewContext Built-in function
        /// </summary>

        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };

            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }
    }
}