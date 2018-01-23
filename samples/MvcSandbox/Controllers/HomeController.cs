// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace MvcSandbox.Controllers
{
    public class HomeController : Controller
    {
        [ModelBinder]
        public string Id { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test(
            [FromHeader(Name = "carType")] CarType carType,
            [FromHeader(Name = "carTypes")]CarType[] carTypes,
            Customer customer)
        {
            return Content($"foo:blah, carType:{carType}, carTypes:{carTypes}, customer:{customer}");
        }
    }

    public enum CarType
    {
        Coupe,
        Sedan
    }

    public class Customer
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public override string ToString()
        {
            return $"Name:{Name}, Address: {Address}";
        }
    }

    public class Address
    {
        [FromHeader]
        public int Zipcode { get; set; }

        public override string ToString()
        {
            return $"Zipcode:{Zipcode}";
        }
    }
}
