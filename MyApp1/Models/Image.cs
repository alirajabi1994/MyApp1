using System.ComponentModel.DataAnnotations;

namespace MyApp1.Models
{
    public class Image
    
    {
        [Key]
        public int ImageID { get; set; }
        [Display(Name = " عنوان تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر وارد شده برای {0} حداقل باید {1} تا باشد.")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر وارد شده برای {0} حداکثر باید {1} تا باشد.")]
        public string Title { get; set; }
        [Display(Name = " توضیحات تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(500)]
        public string Description { get; set; }
        [Display(Name = "مسیر ذخیره فایل تصویر")]
        [MaxLength(255)]
        public string FilePath { get; set; }
        [Display(Name = "تاریخ آپلود تصویر")]
        public DateTime UploadedDate { get; set; }
    }

}
