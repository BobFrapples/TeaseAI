﻿using System;
using TeaseAI.Common.Constants;

namespace TeaseAI.Common.Data
{
    [Obsolete("Switch to MediaContainer")]
    public class ImageContainer
    {
        public ImageGenre Genre { get; set; }
        public string Path { get; set; }
        public bool UseSubFolders { get; set; }
        public ImageSource Source { get; set; }
        public bool IsEnabled { get; set; }
    }
}
