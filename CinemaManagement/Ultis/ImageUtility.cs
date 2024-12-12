using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Ultis
{
    public class ImageUtility
    {
        public static string ImagePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;
            return null;
        }
        public static byte[] ConvertImageToByteArray(string imagePath) 
        { 
            byte[] imageData = null; 
            FileInfo fileInfo = new FileInfo(imagePath); 
            long imageFileLength = fileInfo.Length; 
            FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read); 
            BinaryReader br = new BinaryReader(fs); 
            imageData = br.ReadBytes((int)imageFileLength); 
            return imageData; 
        }
    }
}
