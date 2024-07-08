using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DnsForItLearningLabs.Functions
{
    public class StaticFilesFunction
    {
        // Hard to find the docs on this but the * before "path" indicates that there may be any number of slashes in the path
        const string c_route = "c/{*path}";
        const string c_routePrefix = "/c/";
        const string c_physicalPrefix = "c";
        const string c_defaultFilename = "index.html";

        static string[] s_defaultExtensions = new string[] { ".html", ".htm" };

        static string s_physicalBasePath = null;

        ILogger<StaticFilesFunction> m_logger;

        public StaticFilesFunction(ILogger<StaticFilesFunction> logger) {
            m_logger = logger;
        }

        [Function("StaticFiles")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = c_route)]
            HttpRequest req)
        {
            // Load the configuration if this is the first time in.
            if (s_physicalBasePath is null)
            {
                var root = Environment.GetEnvironmentVariable("static_root");
                if (string.IsNullOrEmpty(root))
                {
                    root = Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot")
                        ?? Environment.GetEnvironmentVariable("HOME") + "/site/wwwroot";  
                }
                s_physicalBasePath = Path.Combine(root, c_physicalPrefix);
                m_logger.LogInformation($"StaticFilesFunction: root={s_physicalBasePath}");
            }

            //log.LogInformation($"StaticFilesFunction Path: Path={req.Path} PathBase={req.PathBase}");

            string path = req.Path;
            if (path.StartsWith(c_routePrefix))
                path = path.Substring(c_routePrefix.Length);
            if (path.Length == 0 || string.Equals(path, c_routePrefix.Substring(0, c_routePrefix.Length - 1), StringComparison.Ordinal))
                path = c_defaultFilename;

            var physicalPath = Path.GetFullPath(Path.Combine(s_physicalBasePath, path));

            // Security check = doesn't use /, \, or .. to get to stuff it shouldn't reach
            if (!physicalPath.StartsWith(s_physicalBasePath))
            {
                m_logger.LogWarning("Access denied, attempt to violate path constraint.");
                return new BadRequestResult();
            }

            // If no extension, add .htm default
            if (!HasExtension(physicalPath))
            {
                bool found = false;
                foreach (var extension in s_defaultExtensions)
                {
                    var tryPath = physicalPath + extension;
                    if (File.Exists(tryPath))
                    {
                        physicalPath = tryPath;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    m_logger.LogWarning($"File not found: {physicalPath + s_defaultExtensions[0]}");
                    return new NotFoundResult();
                }
            }
            else if (!File.Exists(physicalPath))
            {
                m_logger.LogWarning($"File not found: {physicalPath}");
                return new NotFoundResult();
            }

#if DEBUG
            // This code supports dynamically updating the static content locally without
            // having to restart the application.
            {
                int b = physicalPath.IndexOf(@"\bin\");
                int c = physicalPath.IndexOf(@"\c\");
                if (b > 0 && c > b)
                {
                    var altPath = physicalPath.Substring(0, b) + physicalPath.Substring(c);
                    if (File.Exists(altPath)
                        && File.GetLastWriteTimeUtc(altPath) > File.GetLastWriteTimeUtc(physicalPath))
                    {
                        m_logger.LogInformation($"Substituting updated file: {altPath}");
                        physicalPath = altPath;
                    }
                }
            }
#endif

            return new PhysicalFileResult(physicalPath, GetContentType(physicalPath));
        }

        static readonly char[] s_slashes = new char[] { '/', '\\' };

        static bool HasExtension(string path)
        {
            int fn = path.LastIndexOfAny(s_slashes);
            fn = (fn < 0) ? 0 : fn + 1;
            int dot = path.IndexOf('.', fn);
            return (dot > 0);
        }

        public static string GetContentType(string path)
        {
            switch (Path.GetExtension(path).ToLowerInvariant())
            {
                case ".css":
                    return "text/css; charset=utf-8";

                case ".htm":
                case ".html":
                    return "text/html; charset=utf-8";

                case ".ico":
                    return "image/vnd.microsoft.icon";

                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";

                case ".js":
                case ".mjs":
                    return "application/javascript";

                case ".json":
                    return "application/json";

                case ".mp3":
                    return "audio/mpeg";

                case ".mpeg":
                    return "video/mpeg";

                case ".png":
                    return "image/png";

                case ".svg":
                    return "image/svg+xml; charset=utf-8";

                case ".xml":
                    return "application/xml; charset=utf-8";

                default:
                    return "application/octet-stream";
            }
        }
    }
}
