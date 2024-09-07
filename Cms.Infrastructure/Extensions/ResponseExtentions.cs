using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Cms.Infrastructure.Extensions
{
    public interface IResponse
    {
        string Message { get; set; }

        bool DidError { get; set; }

        string ErrorMessage { get; set; }
        HttpStatusCode Status { get; set; }
    }

    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }

    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }

    public interface IPagedResponse<TModel> : IListResponse<TModel>
    {
        int ItemsCount { get; set; }

        double PageCount { get; }
    }

    public class Response : IResponse
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public HttpStatusCode Status { get; set; }
    }

    public class SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public HttpStatusCode Status { get; set; }

        public TModel Model { get; set; }
    }

    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<TModel> Model { get; set; }

        public HttpStatusCode Status { get; set; }
    }

    public class PagedResponse<TModel> : IPagedResponse<TModel>
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<TModel> Model { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int ItemsCount { get; set; }

        public double PageCount
            => ItemsCount < PageSize ? 1 : (int)(((double)ItemsCount / PageSize) + 1);

        public HttpStatusCode Status { get; set; }
    }

    public static class ResponseExtensions
    {
        public static IActionResult ToHttpResponse(this IResponse response)
        {
            var status = (int)response.Status == 0 ? (int)HttpStatusCode.OK : (int)response.Status;
            if (!string.IsNullOrEmpty(response.Message))
                status = (int)HttpStatusCode.OK;
            else if ((!string.IsNullOrEmpty(response.ErrorMessage) && (int)response.Status == 0) || response.DidError)
                status = (int)HttpStatusCode.PreconditionFailed;
            return new ObjectResult(response)
            {
                StatusCode = status
            };
        }

        public static IActionResult ToHttpResponse<TModel>(this ISingleResponse<TModel> response)
        {
            var status = (int)response.Status == 0 ? (int)HttpStatusCode.OK : (int)response.Status;
            if (!string.IsNullOrEmpty(response.Message) || response.Model != null)
                status = (int)HttpStatusCode.OK;
            else if ((!string.IsNullOrEmpty(response.ErrorMessage) && (int)response.Status == 0) || response.DidError)
                status = (int)HttpStatusCode.PreconditionFailed;
            else if (response.Model == null && (int)response.Status == 0)
                status = (int)HttpStatusCode.NotFound;
            return new ObjectResult(response)
            {
                StatusCode = status
            };
        }

        public static IActionResult ToHttpCreatedResponse<TModel>(this ISingleResponse<TModel> response)
        {
            var status = (int)response.Status == 0 ? (int)HttpStatusCode.OK : (int)response.Status;
            if (!string.IsNullOrEmpty(response.Message) || response.Model != null)
                status = (int)HttpStatusCode.OK;
            else if ((!string.IsNullOrEmpty(response.ErrorMessage) && (int)response.Status == 0) || response.DidError)
                status = (int)HttpStatusCode.PreconditionFailed;
            else if (response.Model == null && (int)response.Status == 0)
                status = (int)HttpStatusCode.NotFound;
            return new ObjectResult(response)
            {
                StatusCode = status
            };
        }

        public static IActionResult ToHttpResponse<TModel>(this IListResponse<TModel> response)
        {
            var status = (int)response.Status == 0 ? (int)HttpStatusCode.OK : (int)response.Status;
            if (!string.IsNullOrEmpty(response.Message) || response.Model != null)
                status = (int)HttpStatusCode.OK;
            else if ((!string.IsNullOrEmpty(response.ErrorMessage) && (int)response.Status == 0) || response.DidError)
                status = (int)HttpStatusCode.PreconditionFailed;
            else if (response.Model == null && (int)response.Status == 0)
                status = (int)HttpStatusCode.NotFound;
            return new ObjectResult(response)
            {
                StatusCode = status
            };
        }

        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("access-control-expose-headers", "Application-Error");
        }
    }
}
