using Microsoft.AspNetCore.Http;
using SchoolSaas.Domain.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.OpenAPIs.EdulinkService
{
    public partial class ReferentialClient
    {
        public ReferentialClient(string baseUrl, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
            : this(baseUrl, httpClient)
        {
            // inject Bearer token.
            string? authorizationHeaderValue = httpContextAccessor.HttpContext?.Request.Headers[WebConstants.HttpHeaderAuthorizationKey];
            if (!httpClient.DefaultRequestHeaders.Contains(WebConstants.HttpHeaderAuthorizationKey))
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderAuthorizationKey, authorizationHeaderValue);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Remove(WebConstants.HttpHeaderAuthorizationKey);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderAuthorizationKey, authorizationHeaderValue);
            }

            // inject TenantId.
            string? tenantHeaderValue = httpContextAccessor.HttpContext?.Request.Headers[WebConstants.HttpHeaderTenantKey];
            if (!httpClient.DefaultRequestHeaders.Contains(WebConstants.HttpHeaderTenantKey))
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderTenantKey, tenantHeaderValue);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Remove(WebConstants.HttpHeaderTenantKey);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderTenantKey, tenantHeaderValue);
            }
        }
    }
    public partial class SchoolClient
    {
        public SchoolClient(string baseUrl, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
            : this(baseUrl, httpClient)
        {
            // inject Bearer token.
            string? authorizationHeaderValue = httpContextAccessor.HttpContext?.Request.Headers[WebConstants.HttpHeaderAuthorizationKey];
            if (!httpClient.DefaultRequestHeaders.Contains(WebConstants.HttpHeaderAuthorizationKey))
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderAuthorizationKey, authorizationHeaderValue);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Remove(WebConstants.HttpHeaderAuthorizationKey);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderAuthorizationKey, authorizationHeaderValue);
            }

            // inject TenantId.
            string? tenantHeaderValue = httpContextAccessor.HttpContext?.Request.Headers[WebConstants.HttpHeaderTenantKey];
            if (!httpClient.DefaultRequestHeaders.Contains(WebConstants.HttpHeaderTenantKey))
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderTenantKey, tenantHeaderValue);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Remove(WebConstants.HttpHeaderTenantKey);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderTenantKey, tenantHeaderValue);
            }
        }
    }
    //public partial class AttendanceClient
    //{
    //    public AttendanceClient(string baseUrl, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    //        : this(baseUrl, httpClient)
    //    {
    //        // inject Bearer token.
    //        string? authorizationHeaderValue = httpContextAccessor.HttpContext?.Request.Headers[WebConstants.HttpHeaderAuthorizationKey];
    //        if (!httpClient.DefaultRequestHeaders.Contains(WebConstants.HttpHeaderAuthorizationKey))
    //        {
    //            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderAuthorizationKey, authorizationHeaderValue);
    //        }
    //        else
    //        {
    //            httpClient.DefaultRequestHeaders.Remove(WebConstants.HttpHeaderAuthorizationKey);
    //            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderAuthorizationKey, authorizationHeaderValue);
    //        }

    //        // inject TenantId.
    //        string? tenantHeaderValue = httpContextAccessor.HttpContext?.Request.Headers[WebConstants.HttpHeaderTenantKey];
    //        if (!httpClient.DefaultRequestHeaders.Contains(WebConstants.HttpHeaderTenantKey))
    //        {
    //            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderTenantKey, tenantHeaderValue);
    //        }
    //        else
    //        {
    //            httpClient.DefaultRequestHeaders.Remove(WebConstants.HttpHeaderTenantKey);
    //            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderTenantKey, tenantHeaderValue);
    //        }
    //    }
    //}
    //public partial class CoursesClient
    //{
    //    public CoursesClient(string baseUrl, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    //        : this(baseUrl, httpClient)
    //    {
    //        // inject Bearer token.
    //        string? authorizationHeaderValue = httpContextAccessor.HttpContext?.Request.Headers[WebConstants.HttpHeaderAuthorizationKey];
    //        if (!httpClient.DefaultRequestHeaders.Contains(WebConstants.HttpHeaderAuthorizationKey))
    //        {
    //            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderAuthorizationKey, authorizationHeaderValue);
    //        }
    //        else
    //        {
    //            httpClient.DefaultRequestHeaders.Remove(WebConstants.HttpHeaderAuthorizationKey);
    //            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderAuthorizationKey, authorizationHeaderValue);
    //        }

    //        // inject TenantId.
    //        string? tenantHeaderValue = httpContextAccessor.HttpContext?.Request.Headers[WebConstants.HttpHeaderTenantKey];
    //        if (!httpClient.DefaultRequestHeaders.Contains(WebConstants.HttpHeaderTenantKey))
    //        {
    //            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderTenantKey, tenantHeaderValue);
    //        }
    //        else
    //        {
    //            httpClient.DefaultRequestHeaders.Remove(WebConstants.HttpHeaderTenantKey);
    //            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(WebConstants.HttpHeaderTenantKey, tenantHeaderValue);
    //        }
    //    }
    //}

}
