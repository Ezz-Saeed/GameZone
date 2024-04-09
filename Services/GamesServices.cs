using GameZone.Data;
using GameZone.Models;
using GameZone.Settings;
using GameZone.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class GamesServices : IGamesServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string imagePath;
        public GamesServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        public  IEnumerable<Game> GetAll()
        {
            return  _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetById(int id)
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(g => g.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }

        public async Task  Create(CreateGameFormViewModel model)
        {
            var fileName = await SaveFile(model.Cover);
            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = fileName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId=d}).ToList(),
            };
            _context.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game?> Update(EditFormViewModel model)
        {
            var game = _context.Games
                .Include(g => g.Devices).SingleOrDefault(g => g.Id == model.Id);
            if (game is null)
                return null;
            var oldCover = game.Cover;
            var hasNewCover = model.Cover is not null;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();
            if (hasNewCover)
            {
                game.Cover = await SaveFile(model.Cover!);
            }
            var affectedRows = _context.SaveChanges();
            if(affectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(imagePath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(imagePath,game.Cover);
                File.Delete(cover);
                return null;
            }
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            var game = _context.Games.Find(id);
            if (game is null)
                return isDeleted;
            _context.Games.Remove(game);
            var affectedRows = _context.SaveChanges();
            if (affectedRows > 0)
            {
                var cover = Path.Combine(imagePath, game.Cover);
                File.Delete(cover);
                isDeleted = true;
            }
            return isDeleted;
        }
        private  async Task<string> SaveFile(IFormFile file)
        {
            string fileName = $"{Guid.NewGuid}{file.FileName}";
            string filePath = Path.Combine(imagePath, fileName);

            using(var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

       
    }
}
