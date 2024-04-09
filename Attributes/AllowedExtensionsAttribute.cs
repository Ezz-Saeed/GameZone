using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string _allowedExtensions;
        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                var isAllowedExtension = _allowedExtensions.Split(',').Contains(fileExtension);
                if (!isAllowedExtension)
                {
                    return new ValidationResult($"Only {_allowedExtensions} are allowed");
                }
            }
            return ValidationResult.Success;
        }
    }
}
