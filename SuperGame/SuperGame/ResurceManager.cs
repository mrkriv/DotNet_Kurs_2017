using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SuperGame
{
    public class ResurceManager
    {
        private readonly Dictionary<string, ImageSource> textures = new Dictionary<string, ImageSource>();

        public ImageSource LoadTextute(string imageName)
        {
            if (textures.ContainsKey(imageName))
            {
                return textures[imageName];
            }

            var img = new BitmapImage(new Uri("gamedata\\texture\\" + imageName + ".png", UriKind.Relative));
            textures.Add(imageName, img);

            return img;
        }
    }
}