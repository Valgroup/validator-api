﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Validator.Domain.Core.Interfaces;

namespace Validator.API.Services
{
    public class TemplateRazorService : ITemplateRazorService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;

        public TemplateRazorService(IRazorViewEngine razorViewEngine, IServiceProvider serviceProvider, ITempDataProvider tempDataProvider)
        {
            _razorViewEngine = razorViewEngine;
            _serviceProvider = serviceProvider;
            _tempDataProvider = tempDataProvider;
        }


        public async Task<string> BuilderHtmlAsString<T>(string viewName, T model) where T : class, new()
        {
            var httpContext = new DefaultHttpContext() { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using var swHtml = new StringWriter();
            var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);
            if (viewResult.View == null)
                return string.Empty;

            var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };
            var tempDictionary = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);
            var viewContext = new ViewContext(actionContext, viewResult.View, viewDataDictionary, tempDictionary, swHtml, new HtmlHelperOptions());

            await viewResult.View.RenderAsync(viewContext);
            return swHtml.ToString();


        }
    }
}
