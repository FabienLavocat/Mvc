// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Moq;
using Xunit;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Binders
{
    public class HeaderModelBinderProviderTest
    {
        public static TheoryData<BindingSource> NonHeaderBindingSources
        {
            get
            {
                return new TheoryData<BindingSource>()
                {
                    BindingSource.Body,
                    BindingSource.Form,
                    null,
                };
            }
        }

        [Theory]
        [MemberData(nameof(NonHeaderBindingSources))]
        public void Create_WhenBindingSourceIsNotFromHeader_ReturnsNull(BindingSource source)
        {
            // Arrange
            var provider = new HeaderModelBinderProvider();
            var testBinder = Mock.Of<IModelBinder>();
            var context = new TestModelBinderProviderContext(typeof(string));
            context.OnCreatingBinder(modelMetadata => testBinder);
            context.BindingInfo.BindingSource = source;

            // Act
            var result = provider.GetBinder(context);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Create_WhenBindingSourceIsFromHeader_ReturnsBinder()
        {
            // Arrange
            var provider = new HeaderModelBinderProvider();
            var testBinder = Mock.Of<IModelBinder>();
            var context = new TestModelBinderProviderContext(typeof(string));
            context.OnCreatingBinder(modelMetadata => testBinder);
            context.BindingInfo.BindingSource = BindingSource.Header;

            // Act
            var result = provider.GetBinder(context);

            // Assert
            var headerModelBinder = Assert.IsType<HeaderModelBinder>(result);
            Assert.Same(testBinder, headerModelBinder.InnerModelBinder);
        }

        [Fact]
        public void Create_WhenBindingSourceIsFromHeader_NoInnerBinderAvailable_ReturnsNull()
        {
            // Arrange
            var provider = new HeaderModelBinderProvider();
            var context = new TestModelBinderProviderContext(typeof(string));
            context.OnCreatingBinder(modelMetadata => null);
            context.BindingInfo.BindingSource = BindingSource.Header;

            // Act
            var result = provider.GetBinder(context);

            // Assert
            Assert.Null(result);
        }
    }
}
