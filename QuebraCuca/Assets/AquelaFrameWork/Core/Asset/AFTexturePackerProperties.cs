using System;
using System.Collections.Generic;

using AquelaFrameWork.Utils;

namespace AquelaFrameWork.Core.Asset
{
    public class AFTexturePackerProperties
    {
        public static readonly int FILE_NAME = Utility.Hash("filename");
        public static readonly int FRAME = Utility.Hash("frame");
        public static readonly int ROTATED = Utility.Hash("rotated");
        public static readonly int TRIMED = Utility.Hash("trimmed");
        public static readonly int SPRITE_SOURCE_SIZE = Utility.Hash("spriteSourceSize");
        public static readonly int SOURCE_SIZE = Utility.Hash("sourceSize");
        public static readonly int PIVOT = Utility.Hash("pivot");
    }
}
