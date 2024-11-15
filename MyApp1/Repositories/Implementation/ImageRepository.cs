using MyApp1.Context;
using MyApp1.Models;
using MyApp1.Repositories.Contracts;
using MyApp1.ViewModels.Image;

namespace MyApp1.Repositories.Implementation
{
    public class ImageRepository: IImageRepository
    {
        private ShopDbContext context = new ShopDbContext();

        public void Add(Image image)
        {
            context.Images.Add(image);
        }

        public List<ImageViewModel> GetAll()
        {
            List<ImageViewModel> images= context.Images.Select(i=>new ImageViewModel()
                {
                    Description = i.Description,
                    FilePath = i.FilePath,
                    Title = i.Title,
                    ImageID = i.ImageID,
                    UploadedDate = i.UploadedDate
                }).ToList();
            return images;
        }

        public UpdateImageViewModel GetByIdForEdit(int id)
        {
            return context.Images.Select(i => new UpdateImageViewModel()
            {
                Description = i.Description,
                ExistingImagePath = i.FilePath,
                Title = i.Title,
                Id = i.ImageID
            }).FirstOrDefault(i => i.Id == id);
        }

        public Image GetById(int id)
        {
            return context.Images.FirstOrDefault(i => i.ImageID == id);
        }

        public void Update(Image image)
        {
            context.Images.Update(image);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Delete(Image image)
        {
            context.Images.Remove(image);
        }
    }
}
