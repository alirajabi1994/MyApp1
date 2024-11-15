using System.ComponentModel.DataAnnotations;

namespace MyApp1.ViewModels.Image
{
    public class CreateImageViewModel
    {
        [Display(Name = "عنوان تصویر")]
        [Required] [StringLength(100)]
            public string Title { get; set; } // عنوان تصویر
        [Display(Name = "توضیحات تصویر")]
        [StringLength(500)]
        public string Description { get; set; }    // توضیحات تصویر
        [Display(Name = " اپلود ")]
        [Required]
        public IFormFile ImageFile { get; set; }   // فایل تصویر که از کاربر آپلود می‌شود
        [Display(Name = "تاریخ  اپلود")]
        public DateTime UploadedDate { get; set; } = DateTime.Now;  // تاریخ آپلود
    }
}

