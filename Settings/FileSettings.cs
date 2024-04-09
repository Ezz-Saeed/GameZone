namespace GameZone.Settings
{
    public static class FileSettings
    {
        public const string ImagePath = "/assets/images/games";
        public const string AllowedExtensions = ".jpg,.jpeg,.png,.jfif";
        public const int MaxFileSizeInMb = 1;
        public const int MaxFileSizeInByte = MaxFileSizeInMb * 1024 *1024;
        
    }
}
