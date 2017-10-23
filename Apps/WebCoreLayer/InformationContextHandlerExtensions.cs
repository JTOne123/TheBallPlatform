﻿using Microsoft.AspNetCore.Builder;

namespace WebCoreLayer
{
    public static class InformationContextHandlerExtensions
    {
        public static IApplicationBuilder UseInformationContext(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InformationContextMiddleware>();
        }
    }
}