using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyApp1.Repositories.Contracts;
using MyApp1.Repositories.Implementation;
using MyApp1.ViewModels.Image;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using System;
using Image = MyApp1.Models.Image;
using MyApp1.Models;

namespace MyApp1.Controllers
{
    public class ImageController : Controller
    {
        private IImageRepository imageRepository = new ImageRepository();

        public IActionResult List()
        {
            List<ImageViewModel> images = imageRepository.GetAll();
            return View(images);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateImageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var filePath = Path.Combine("wwwroot/images", model.ImageFile.FileName);

            // ذخیره‌سازی فایل در مسیر مشخص‌شده
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.ImageFile.CopyTo(stream);
            }

            var image = new Image
            {
                Title = model.Title,
                Description = model.Description,
                FilePath = "/images/" + model.ImageFile.FileName,
                UploadedDate = model.UploadedDate
            };
            imageRepository.Add(image);
            imageRepository.Save();
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            UpdateImageViewModel model = imageRepository.GetByIdForEdit(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UpdateImageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // بازگشت به صفحه ویرایش با خطاهای اعتبارسنجی
            }

            var image = imageRepository.GetById(model.Id);
            if (image == null)
            {
                return NotFound(); // اگر تصویر با این شناسه وجود نداشت
            }

            // به‌روزرسانی عنوان و توضیحات تصویر
            image.Title = model.Title;
            image.Description = model.Description;

            if (model.ImageFile != null)
            {
                // حذف تصویر قبلی از سرور (در صورت وجود)
                if (!string.IsNullOrEmpty(model.ExistingImagePath))
                {
                    var oldImagePath = Path.Combine("wwwroot", model.ExistingImagePath);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        try
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                        catch (Exception ex)
                        {
                            // خطا در حذف فایل قبلی
                            ModelState.AddModelError("", "خطا در حذف فایل قبلی.");
                            return View(model);
                        }
                    }
                }

                // اطمینان از وجود پوشه‌ی بارگذاری
                var uploadsFolder = Path.Combine("wwwroot/images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // ذخیره تصویر جدید
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                var newImagePath = Path.Combine(uploadsFolder, uniqueFileName);

                try
                {
                    using (var fileStream = new FileStream(newImagePath, FileMode.Create))
                    {
                        model.ImageFile.CopyTo(fileStream); // اینجا await را حذف کنید چون متد به صورت همزمان نیست
                    }
                    image.FilePath = "/images/" + uniqueFileName;
                }
                catch (Exception ex)
                {
                    // خطا در ذخیره فایل جدید
                    ModelState.AddModelError("", "خطا در ذخیره فایل جدید.");
                    return View(model);
                }
            }

            // به‌روزرسانی داده‌ها در پایگاه داده
            imageRepository.Update(image);
            imageRepository.Save();
            return RedirectToAction(nameof(List)); // بازگشت به صفحه اصلی یا فهرست تصاویر
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var image = imageRepository.GetById(id);
            if (image == null)
            {
                return NotFound(); // If the image with this ID doesn't exist
            }

            // Delete the image file from the server if it exists
            if (!string.IsNullOrEmpty(image.FilePath))
            {
                var imagePath = Path.Combine("wwwroot", image.FilePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // Remove the image record from the database
            imageRepository.Delete(image);
            imageRepository.Save();

            return RedirectToAction(nameof(List)); // Redirect to the image list after deletion
        }


    }
}
    
