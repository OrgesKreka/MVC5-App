using MVCApplication.DataManagement;
using MVCApplication.DataManagement.Entities;
using MVCApplication.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MVCApplication.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            using (var dapperUnitOfWork = new DapperUnitOfWork())
            {

                var listOfProducts = dapperUnitOfWork.ProductRepository.GetAll();
                var listOfProductsDTO = listOfProducts.Select(x => new Product
                {
                    CategoryID = x.CategoryID,
                    Discounted = x.Discounted,
                    ProductID = x.ProductID,
                    ProductName = x.ProductName,
                    QuantityPerUnit = x.QuantityPerUnit,
                    ReorderLevel = x.ReorderLevel,
                    SupplierID = x.SupplierID,
                    UnitPrice = x.UnitPrice,
                    UnitsInStock = x.UnitsInStock,
                    UnitsOnOrder = x.UnitsOnOrder
                });

                return View(listOfProductsDTO);
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            using (var dapperUnitOfWork = new DapperUnitOfWork())
            {
                var product = dapperUnitOfWork.ProductRepository.Get(id.Value);

                if (product == null)
                    return RedirectToAction("Index");

                var productDTO = new Product
                {
                    CategoryID = product.CategoryID,
                    Discounted = product.Discounted,
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    QuantityPerUnit = product.QuantityPerUnit,
                    ReorderLevel = product.ReorderLevel,
                    SupplierID = product.SupplierID,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder

                };

                return View(productDTO);
            }
        }


        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var dapperUnitOfWork = new DapperUnitOfWork())
                    {

                        dapperUnitOfWork.ProductRepository.Add(new DataManagement.Entities.Products
                        {
                            CategoryID = model.CategoryID,
                            Discounted = model.Discounted,
                            ProductID = model.ProductID,
                            ProductName = model.ProductName,
                            QuantityPerUnit = model.QuantityPerUnit,
                            ReorderLevel = model.ReorderLevel,
                            SupplierID = model.SupplierID,
                            UnitPrice = model.UnitPrice,
                            UnitsInStock = model.UnitsInStock,
                            UnitsOnOrder = model.UnitsOnOrder
                        });

                        dapperUnitOfWork.Commit();
                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                // Log
            }

            return View();

        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            using (var unitOfWork = new DapperUnitOfWork())
            {
                var product = unitOfWork.ProductRepository.Get(id.Value);

                if (product == null)
                    return RedirectToAction("Index");

                var selectedproductDTO = new Product
                {
                    CategoryID = product.CategoryID,
                    Discounted = product.Discounted,
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    QuantityPerUnit = product.QuantityPerUnit,
                    ReorderLevel = product.ReorderLevel,
                    SupplierID = product.SupplierID,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder
                };

                return View(selectedproductDTO);
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (var unitOfWork = new DapperUnitOfWork())
                    {
                        var product = new Products
                        {
                            CategoryID = model.CategoryID,
                            Discounted = model.Discounted,
                            ProductID = model.ProductID,
                            ProductName = model.ProductName,
                            QuantityPerUnit = model.QuantityPerUnit,
                            ReorderLevel = model.ReorderLevel,
                            SupplierID = model.SupplierID,
                            UnitPrice = model.UnitPrice,
                            UnitsInStock = model.UnitsInStock,
                            UnitsOnOrder = model.UnitsOnOrder
                        };

                        unitOfWork.ProductRepository.Update(product);
                        unitOfWork.Commit();

                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                // TODO: Log
            }

            return View();
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            using (var unitOfWork = new DapperUnitOfWork())
            {
                var product = unitOfWork.ProductRepository.Get(id.Value);

                if (product == null)
                    return RedirectToAction("Index");

                var selectedProductDTO = new Product
                {
                    CategoryID = product.CategoryID,
                    Discounted = product.Discounted,
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    QuantityPerUnit = product.QuantityPerUnit,
                    ReorderLevel = product.ReorderLevel,
                    SupplierID = product.SupplierID,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder
                };

                return View(selectedProductDTO);
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            if (id == null)
                return RedirectToAction("Index");

            try
            {
                using (var unitOfWork = new DapperUnitOfWork())
                {

                    unitOfWork.ProductRepository.Remove(new Products { ProductID = id.Value });
                    unitOfWork.Commit();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception exc)
            {
                // Log Error
            }
            return View();
        }
    }
}
