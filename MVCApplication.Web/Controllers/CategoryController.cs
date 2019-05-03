using MVCApplication.DataManagement;
using MVCApplication.DataManagement.Entities;
using MVCApplication.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MVCApplication.Web.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            using (var dapperUnitOfWork = new DapperUnitOfWork())
            {

                var listOfCategories = dapperUnitOfWork.CategoryRepository.GetAll();
                var listOfCategoriesDTO = listOfCategories.Select(x => new Category
                {
                    CategoryID = x.CategoryID,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    Picture = x.Picture
                });

                return View(listOfCategoriesDTO);
            }
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            using (var dapperUnitOfWork = new DapperUnitOfWork())
            {
                var category = dapperUnitOfWork.CategoryRepository.Get(id.Value);

                if (category == null)
                    return RedirectToAction("Index");

                var categoryDTO = new Category
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    Picture = category.Picture

                };

                return View(categoryDTO);
            }
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category categoryDTOModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var dapperUnitOfWork = new DapperUnitOfWork())
                    {

                        dapperUnitOfWork.CategoryRepository.Add(new DataManagement.Entities.Categories
                        {
                            CategoryID = categoryDTOModel.CategoryID,
                            CategoryName = categoryDTOModel.CategoryName,
                            Description = categoryDTOModel.Description,
                            Picture = categoryDTOModel.Picture  ?? new byte[] {}
                        });

                        dapperUnitOfWork.Commit();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                // Log error
            }
            return View();
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            using (var unitOfWork = new DapperUnitOfWork())
            {
                var category = unitOfWork.CategoryRepository.Get(id.Value);

                if (category == null)
                    return RedirectToAction("Index");

                var selectedCategoryDTO = new Category
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    Picture = category.Picture
                };

                return View(selectedCategoryDTO);
            }
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category categoryDTOModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (var unitOfWork = new DapperUnitOfWork())
                    {
                        var category = new Categories
                        {
                            CategoryID = categoryDTOModel.CategoryID,
                            CategoryName = categoryDTOModel.CategoryName,
                            Description = categoryDTOModel.Description,
                            Picture = categoryDTOModel.Picture  ?? new byte[] {}
                        };

                        unitOfWork.CategoryRepository.Update(category);
                        unitOfWork.Commit();

                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                // Log Error
            }

            return View();
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            using (var unitOfWork = new DapperUnitOfWork())
            {
                var category = unitOfWork.CategoryRepository.Get(id.Value);

                if (category == null)
                    return RedirectToAction("Index");

                var selectedCategoryDTO = new Category
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    Picture = category.Picture
                };

                return View(selectedCategoryDTO);
            }
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Category categoryDTOModel)
        {
            if (id == null)
                return RedirectToAction("Index");

            try
            {
                using (var unitOfWork = new DapperUnitOfWork())
                {

                    unitOfWork.CategoryRepository.RemoveById(id.Value);
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
