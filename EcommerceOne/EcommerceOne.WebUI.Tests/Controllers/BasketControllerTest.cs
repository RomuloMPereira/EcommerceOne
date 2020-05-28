﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceOne.Core.Contracts;
using EcommerceOne.Core.Models;
using EcommerceOne.Core.ViewModels;
using EcommerceOne.Services;
using EcommerceOne.WebUI.Controllers;
using EcommerceOne.WebUI.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EcommerceOne.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            //Arrange
            IRepository<Basket> basketRepository = new MockContext<Basket>();
            IRepository<Product> productRepository = new MockContext<Product>();
            IBasketService basketService = new BasketService(productRepository, basketRepository);
            HttpContextBase httpContext = new MockHttpContext();
            BasketController controller = new BasketController(basketService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            //basketService.AddToBasket(httpContext, "1");
            controller.AddToBasket("1");
            
            Basket basket = basketRepository.Collection().FirstOrDefault();

            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count());
            Assert.AreEqual("1", basket.BasketItems.FirstOrDefault().ProductId);
        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Basket> basketRepository = new MockContext<Basket>();
            IRepository<Product> productRepository = new MockContext<Product>();

            productRepository.Insert(new Product() { Id = "1", Price = 10.00m });
            productRepository.Insert(new Product() { Id = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1 });
            basketRepository.Insert(basket);

            IBasketService basketService = new BasketService(productRepository, basketRepository);

            var controller = new BasketController(basketService);
            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new HttpCookie("eCommercebasket") { Value=basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(25.00m, basketSummary.BasketTotal);

        }
    }
}
