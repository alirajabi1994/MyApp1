namespace MyApp1.ViewModels.Image
{
    public class UpdateImageViewModel
    {
        // شناسه‌ی تصویر برای شناسایی رکورد در پایگاه داده
        public int Id { get; set; }

        // عنوان تصویر
        public string Title { get; set; }

        // توضیحات تصویر
        public string Description { get; set; }

        // مسیر تصویر فعلی در سرور
        public string ExistingImagePath { get; set; }

        // فایل جدید برای آپلود (در صورت وجود)
        public IFormFile? ImageFile { get; set; }
    }
}

