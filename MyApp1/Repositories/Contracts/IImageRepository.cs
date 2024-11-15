using MyApp1.Models;
using MyApp1.ViewModels.Image;
namespace MyApp1.Repositories.Contracts

{
    public interface IImageRepository
    {
        List<ImageViewModel> GetAll();
        void Add(Image image);
        void Save();
        UpdateImageViewModel GetByIdForEdit(int id);
        Image GetById(int id);
        void Update(Image image);
        void Delete(Image  image);
    }
}
