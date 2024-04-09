using GameZone.Attributes;
using GameZone.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
        //[Extension]
        [AllowedExtensions(FileSettings.AllowedExtensions), AllowedSize(FileSettings.MaxFileSizeInByte)]
        public IFormFile Cover { get; set; } = default!;
        
    }
}
