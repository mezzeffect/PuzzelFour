using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace Workbench3.Web.Tests
{
    public static class MvcMockHelpers
    {
        public static HttpContextBase FakeHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);
            context.Setup(ctx => ctx.Items).Returns(new Hashtable());            


            return context.Object;
        }

        public static HttpContextBase FakeHttpContext(string url)
        {
            HttpContextBase context = FakeHttpContext();
            context.Request.SetupRequestUrl(url);
            return context;
        }

        public static void SetFakeControllerContext(this System.Web.Mvc.Controller controller)
        {
            var httpContext = FakeHttpContext();
            //var httpContext = MvcContrib.TestHelper.Fakes.FakeHttpContext.Root();
            ControllerContext context = new ControllerContext(new RequestContext(httpContext, new RouteData()), controller);
            
            Mock.Get(httpContext.Request).Setup(r => r.RequestContext).Returns(context.RequestContext);
            controller.ControllerContext = context;
        }

        static string GetUrlFileName(string url)
        {
            if (url.Contains("?"))
                return url.Substring(0, url.IndexOf("?"));
            else
                return url;
        }

        static NameValueCollection GetQueryStringParameters(string url)
        {
            if (url.Contains("?"))
            {
                NameValueCollection parameters = new NameValueCollection();

                string[] parts = url.Split("?".ToCharArray());
                string[] keys = parts[1].Split("&".ToCharArray());

                foreach (string key in keys)
                {
                    string[] part = key.Split("=".ToCharArray());
                    parameters.Add(part[0], part[1]);
                }

                return parameters;
            }
            else
            {
                return null;
            }
        }

        public static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
        {
            Mock.Get(request)
                .Expect(req => req.HttpMethod)
                .Returns(httpMethod);
        }

        public static void SetupRequestUrl(this HttpRequestBase request, string url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            //if (!url.StartsWith("~/"))
            //    throw new ArgumentException("Sorry, we expect a virtual url starting with \"~/\".");

            var mock = Mock.Get(request);
            mock.Setup(r => r.Url).Returns(new Uri(url));

            mock.Expect(req => req.QueryString)
                .Returns(GetQueryStringParameters(url));
            mock.Expect(req => req.AppRelativeCurrentExecutionFilePath)
                .Returns(GetUrlFileName(url));
            mock.Expect(req => req.PathInfo)
                .Returns(string.Empty);
        }
    }

    public static class ModelStateExtensions
    {
        //returns true if an error message exists in the modelstate
        public static bool ContainsErrorMessage(this ModelStateDictionary modelState, string errorMessage)
        {
            return modelState.Any(ms => ms.Value.Errors.Count(e => e.ErrorMessage == errorMessage) > 0);
        }

        public static string GetErrorMessage(this ModelStateDictionary modelState, string key)
        {
            var state = modelState[key];
            if(state==null) return string.Empty;

            var firstError = state.Errors.FirstOrDefault();
            return firstError == null ? string.Empty : firstError.ErrorMessage;
        }

        public static bool ContainsErrorFor(this ModelStateDictionary modelState,string key)
        {
            var state = modelState[key];
            if (state == null)
                throw new ArgumentNullException(key);
            return state.Errors.Count != 0;
        }
    }
}
