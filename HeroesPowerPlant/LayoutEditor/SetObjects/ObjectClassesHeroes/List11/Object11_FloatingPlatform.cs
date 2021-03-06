﻿using System.Linq;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object11_FloatingPlatform : SetObjectManagerHeroes
    {
        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (AlternateType)
                base.Draw(modelNames.Skip(2).ToArray(), isSelected);
            else
                base.Draw(modelNames.Take(2).ToArray(), isSelected);
        }
        public enum PlatformTypeEnum
        {
            Fixed = 0,
            Moving = 1,
            Alternate = 2,
            Disappear = 3
        }
        public PlatformTypeEnum PlatformType
        {
            get { return (PlatformTypeEnum)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }

        public bool AlternateType
        {
            get { return ReadByte(5) != 0; }
            set { Write(5, (byte)(value ? 1 : 0)); }
        }

        public short UnknownAlternateRange0
        {
            get { return ReadShort(6); }
            set { Write(8, value); }
        }

        public short UnknownAlternateRange1
        {
            get { return ReadShort(8); }
            set { Write(8, value); }
        }

        public short XOffset
        {
            get { return ReadShort(10); }
            set { Write(10, value); }
        }

        public short YOffset
        {
            get { return ReadShort(12); }
            set { Write(12, value); }
        }

        public short ZOffset
        {
            get { return ReadShort(14); }
            set { Write(14, value); }
        }

        public short TimeCycleFrame
        {
            get { return ReadShort(16); }
            set { Write(16, value); }
        }

        public byte DisappearLinkID
        {
            get { return ReadByte(18); }
            set { Write(18, value); }
        }
    }
}
