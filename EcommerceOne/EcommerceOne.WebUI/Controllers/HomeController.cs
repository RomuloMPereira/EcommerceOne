﻿using EcommerceOne.Core.Contracts;
using EcommerceOne.Core.Models;
using EcommerceOne.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceOne.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            this.context = productContext;
            this.productCategories = productCategoryContext;
        }

        public ActionResult Index(string category = null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();

            if(category == null)
            {
                products = context.Collection().ToList();
            } else
            {
                products = context.Collection().Where(p => p.Category == category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;

            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);

            if(product == null)
            {
                return HttpNotFound();
            } else
            {
                return View(product);
            }
        }

        
    }
}