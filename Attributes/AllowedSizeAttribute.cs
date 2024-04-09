using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class AllowedSizeAttribute : ValidationAttribute
    {
        private readonly  int MaxFileSizeInByte ;
        public AllowedSizeAttribute(int maxFileSizeInByte)
        {
            
            MaxFileSizeInByte = maxFileSizeInByte;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
              
                if (!(file.Length <= MaxFileSizeInByte))
                {
                    return new ValidationResult($"File Size must not exceed {MaxFileSizeInByte} Bytes");
                }
            }
            return ValidationResult.Success;
        }
    }
}
