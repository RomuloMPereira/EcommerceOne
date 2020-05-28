using System;
using System.Linq;
using System.Web.Mvc;
using EcommerceOne.Core.Contracts;
using EcommerceOne.Core.Models;
using EcommerceOne.Core.ViewModels;
using EcommerceOne.WebUI.Controllers;
using EcommerceOne.WebUI.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EcommerceOne.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepository<Product> productContext = new MockContext<Product>();
            productContext.Insert(new Product());
            IRepository<ProductCategory> productCategoryContext = new MockContext<ProductCategory>();
            HomeController controller = new HomeController(productContext, productCategoryContext);

            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel) result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Products.Count());
        }
    }
}
