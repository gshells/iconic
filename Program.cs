using System;
using System.IO;
using System.Linq;
using ImageMagick;

namespace ImageToIcoConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ima computer";
            Console.WriteLine($"{GetGreeting()}, {Environment.GetEnvironmentVariable("UserName")},");
            Console.WriteLine("beep boop... ima computer");

            Console.Write("Enter the folder path containing image files (PNG, JPEG, BMP): ");
            string folderPath = Console.ReadLine();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"The folder '{folderPath}' does not exist.");
                return;
            }

            var imageFiles = Directory.GetFiles(folderPath)
                .Where(file => file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                               file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                               file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                               file.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (imageFiles.Length == 0)
            {
                Console.WriteLine($"No image files found in the folder '{folderPath}'.");
                return;
            }

            foreach (var imageFile in imageFiles)
            {
                string inputImagePath = imageFile;
                string outputIcoPath = Path.ChangeExtension(imageFile, ".ico");

                try
                {
                    using var inputStream = File.OpenRead(inputImagePath);
                    using var outputStream = File.OpenWrite(outputIcoPath);
                    CreateIco(inputStream, outputStream);
                    Console.WriteLine($"Successfully converted '{inputImagePath}' to '{outputIcoPath}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during the conversion of '{inputImagePath}': {ex.Message}");
                }
            }
            Console.WriteLine("Press Enter to exit the application...");
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(intercept: true).Key;
            } while (key != ConsoleKey.Enter);
        }
        private static string GetGreeting()
        {
            int hour = DateTime.Now.Hour;

            return (hour >= 5 && hour < 12) ? "Good morning" :
                   (hour >= 12 && hour < 18) ? "Good afternoon" :
                   "Good evening";
        }

        private static void CreateIco(Stream inputStream, Stream outputStream)
        {
            using var sourceImage = new MagickImage(inputStream);
            sourceImage.Settings.SetDefine(MagickFormat.Png, "bit-depth", "8");
            sourceImage.Settings.SetDefine(MagickFormat.Png, "format", "RGBA");
            sourceImage.Settings.SetDefine(MagickFormat.Png, "quality", "100");

            // Thicken lines by applying a dilation operation
            sourceImage.Morphology(MorphologyMethod.Dilate, Kernel.Diamond, 1);

            using var collection = new MagickImageCollection();
            int[] iconSizes = new[] { 256, 128, 96, 64, 48, 32, 16 };

            foreach (var size in iconSizes)
            {
                var resizedImage = sourceImage.Clone();
                resizedImage.Resize(size, size);
                resizedImage.Trim();
                resizedImage.BackgroundColor = MagickColors.Transparent;
                collection.Add(resizedImage);
            }

            collection.Write(outputStream, MagickFormat.Ico);
        }
    }
}
