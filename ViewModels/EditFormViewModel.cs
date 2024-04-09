using GameZone.Attributes;
using GameZone.Settings;

namespace GameZone.ViewModels
{
    public class EditFormViewModel : GameFormViewModel
    {
        public int Id { get; set; }

        public string? CurrentCover { get; set; }
        [AllowedExtensions(FileSettings.AllowedExtensions), AllowedSize(FileSettings.MaxFileSizeInByte)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
