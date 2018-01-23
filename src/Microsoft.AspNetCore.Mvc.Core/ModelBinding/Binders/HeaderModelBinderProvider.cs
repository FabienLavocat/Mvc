// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Binders
{
    /// <summary>
    /// An <see cref="IModelBinderProvider"/> for binding header values.
    /// </summary>
    public class HeaderModelBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc />
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.BindingInfo.BindingSource != null &&
                context.BindingInfo.BindingSource.CanAcceptDataFrom(BindingSource.Header))
            {
                var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();

                var metadata = context.Metadata.GetMetadataForType(context.Metadata.ModelType);
                var innerModelBinder = context.CreateBinder(metadata);
                if (innerModelBinder == null)
                {
                    return null;
                }

                return new HeaderModelBinder(loggerFactory, innerModelBinder);
            }

            return null;
        }
    }
}
