﻿namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0200_CrumbleStonePillar : SetObjectManagerHeroes
    {
        public enum RuinType : byte
        {
            Left = 0,
            Right = 1,
            Center = 2
        }
        public RuinType Type
        {
            get { return (RuinType)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }
    }
}
