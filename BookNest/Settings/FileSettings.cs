namespace BookNest.Settings
{
    public static class FileSettings
    {
        public const string ImagePath = "/assets/images/books";
        public const string AllowedExtentions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB = 3;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;

    }
}
