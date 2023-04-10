# PngToIcoConverter

PngToIcoConverter is a C# Console application that automates the process of converting high-quality PNG images to ICO files. The application scans a specified folder for image files (PNG, JPEG, BMP) and converts them into ICO files with multiple resolutions, preserving the original file names. This utility is designed to create high-quality ICO files for various applications, such as application icons, favicons, or any other use cases where an ICO format is required.

## Requirements

- .NET 5.0 or higher
- ImageMagick.NET library

## Main Class - Program

The `Program` class contains the `Main` method which is the entry point of the application. In this method, it first prompts the user for the folder path containing the image files. Then, it checks if the folder exists and retrieves the list of image files (PNG, JPEG, BMP). Next, the method iterates through each image file, converts it to an ICO file, and saves the result with the same name but with the ".ico" extension. Finally, it informs the user about the conversion status for each image.

## Methods

- `CreateIco(Stream inputStream, Stream outputStream)`: Converts the input image stream to an ICO format using the ImageMagick.NET library and writes the result to the output stream.

## Usage

1. Ensure you have installed .NET 5.0 or higher, and added ImageMagick.NET library to the project.
2. Run the console application. The program will prompt you to enter the folder path containing the image files (PNG, JPEG, BMP).
3. The application will then iterate through each image file in the specified folder, converting it to an ICO file with multiple resolutions and preserving the original file name.

The conversion process utilizes the ImageMagick.NET library, which is responsible for reading the input image files, resizing them to the specified dimensions (256x256, 128x128, 64x64, 32x32, and 16x16 pixels), and generating an ICO file containing multiple resolutions of the original image. The library is designed to handle a wide range of image formats, enabling high-quality image processing and ensuring that the resulting ICO files maintain the original image's detail and appearance.

4. The resulting ICO files will be saved in the same folder as the source image files.
