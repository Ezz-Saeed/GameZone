using GameZone.Data;
using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        
        private readonly ICategoriesServices _categoriesServices;
        private readonly IDevicesServices _devicesServices;
        private readonly IGamesServices _gamesServices;
        public GamesController(IDevicesServices devicesServices, 
            ICategoriesServices categoriesServices, 
            IGamesServices gamesServices)
        {
            _categoriesServices = categoriesServices;
            _devicesServices = devicesServices;
            _gamesServices = gamesServices;
        }
        public IActionResult Index()
        {
            var games = _gamesServices.GetAll();
            return View(games);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var game = _gamesServices.GetById(id);
            if(game is null)
                return NotFound();
            return View(game);
        }
        [HttpGet]
    
        public IActionResult Edit(int id)
        {
            var game = _gamesServices.GetById(id);
            if (game is null)
                return NotFound();
            EditFormViewModel viewModel = new()
            {
                Id= game.Id,
                Name= game.Name,
                Description= game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoriesServices.GetSelectListItem(),
                Devices = _devicesServices.GetSelectListItems(),
                CurrentCover = game.Cover,
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectListItem();
                model.Devices = _devicesServices.GetSelectListItems();
                return View(model);
            }
            var game = await _gamesServices.Update(model);
            if (game is null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {

            CreateGameFormViewModel createGameFormViewModel = new()
            {
                Categories = _categoriesServices.GetSelectListItem(),
                Devices = _devicesServices.GetSelectListItems()
            };
            return View(createGameFormViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectListItem();
                model.Devices = _devicesServices.GetSelectListItems();
                return View(model);
            }

            await _gamesServices.Create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return BadRequest();
            var isDeleted = _gamesServices.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
