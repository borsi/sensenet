﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SenseNet.ApplicationModel;
using SenseNet.Configuration;
using SenseNet.ContentRepository;
using SenseNet.ContentRepository.Storage;
using SenseNet.Portal.Virtualization;

namespace SenseNet.Portal.OData
{
    public class ODataTools
    {
        /// <summary>
        /// Returns the OData service url for the specified content. Eg.: /OData/Root/Sites/Default_Site('test.txt')"
        /// </summary>
        /// <param name="content">Requested content</param>
        /// <param name="escapeApostrophes">Replaces ' with escaped form so it can be used in javascript in form '(url)'</param>
        /// <returns></returns>
        public static string GetODataUrl(Content content, bool escapeApostrophes = false)
        {
            var url = string.Concat("/" + Configuration.Services.ODataServiceToken, content.ContentHandler.ParentPath, "('", content.Name, "')");
            return escapeApostrophes ? url.Replace("'", "\\'") : url;
        }
        /// <summary>
        /// Returns the OData service url for the specified content. Eg.: /OData/Root/Sites/Default_Site('test.txt')"
        /// </summary>
        /// <param name="content">Path of requested content</param>
        /// <param name="escapeApostrophes">Replaces ' with escaped form so it can be used in javascript in form '(url)'</param>
        /// <returns></returns>
        public static string GetODataUrl(string path, bool escapeApostrophes = false)
        {
            var url = string.Concat("/" + Configuration.Services.ODataServiceToken, RepositoryPath.GetParentPath(path), "('", RepositoryPath.GetFileName(path), "')");
            return escapeApostrophes ? url.Replace("'", "\\'") : url;
        }
        /// <summary>
        /// Returns the OData service operation url for the specified content and operation name. Eg.: /OData/Root/Sites/Default_Site('test.txt')/MoveTo"
        /// </summary>
        /// <param name="content">Requested content</param>
        /// <param name="operationName">Name of requested operation</param>
        /// <param name="escapeApostrophes">Replaces ' with escaped form so it can be used in javascript in form '(url)'</param>
        /// <returns></returns>
        public static string GetODataOperationUrl(Content content, string operationName, bool escapeApostrophes = false)
        {
            return string.Concat(GetODataUrl(content, escapeApostrophes), "/", operationName);
        }
        /// <summary>
        /// Returns the OData service operation url for the specified content and operation name. Eg.: /OData/Root/Sites/Default_Site('test.txt')/MoveTo"
        /// </summary>
        /// <param name="content">Path of requested content</param>
        /// <param name="operationName">Name of requested operation</param>
        /// <param name="escapeApostrophes">Replaces ' with escaped form so it can be used in javascript in form '(url)'</param>
        /// <returns></returns>
        public static string GetODataOperationUrl(string path, string operationName, bool escapeApostrophes = false)
        {
            return string.Concat(GetODataUrl(path, escapeApostrophes), "/", operationName);
        }

        internal static IEnumerable<ActionBase> GetActions(Content content, ODataRequest request)
        {
            // Use the back url provided by the client. If it is empty, use
            // the url of the caller page (the referrer provided by ASP.NET).
            // The back url can be omitted (switched off) by the client if it provides the
            // appropriate request parameter (includebackurl false).
            var backUrl = PortalContext.Current != null && (request == null || request.IncludeBackUrl)
                ? PortalContext.Current.BackUrl
                : null;

            if (string.IsNullOrEmpty(backUrl) &&
                (request == null || request.IncludeBackUrl) &&
                HttpContext.Current != null &&
                HttpContext.Current.Request.UrlReferrer != null)
                backUrl = HttpContext.Current.Request.UrlReferrer.ToString();

            return ODataHandler.ActionResolver.GetActions(content,
                request != null ? request.Scenario : null,
                string.IsNullOrEmpty(backUrl) ? null : backUrl);
        }
        internal static IEnumerable<ODataActionItem> GetHtmlActionItems(Content content, ODataRequest request)
        {
            return GetActions(content, request).Where(a => a.IsHtmlOperation).Select(a => new ODataActionItem
            {
                Name = a.Name,
                DisplayName = SNSR.GetString(a.Text),
                Icon = a.Icon,
                Index = a.Index,
                Url = a.Uri,
                IncludeBackUrl = a.GetApplication() == null ? 0 : (int)a.GetApplication().IncludeBackUrl,
                ClientAction = a is ClientAction && !string.IsNullOrEmpty(((ClientAction)a).Callback),
                Forbidden = a.Forbidden
            });
        }
    }
}
