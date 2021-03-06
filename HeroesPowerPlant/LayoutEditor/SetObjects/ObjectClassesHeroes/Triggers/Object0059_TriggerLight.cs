﻿using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0059_TriggerLight : SetObjectManagerHeroes
    {
        private BoundingSphere sphereBound;

        public override bool TriangleIntersection(Ray r, string[] ModelNames)
        {
            if (TriggerShape == TriggerLightShape.Sphere)
                return r.Intersects(sphereBound);
            else
                return base.TriangleIntersection(r, ModelNames);
        }

        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (TriggerShape == TriggerLightShape.NotInUse)
                return base.CreateBoundingBox(modelNames);
            else
                return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;
            
            switch (TriggerShape)
            {
                case TriggerLightShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius_ScaleX);
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2);
                    break;
                case TriggerLightShape.Cube:
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2, Height_ScaleY * 2, ScaleZ * 2);
                    break;
                case TriggerLightShape.Cylinder:
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2, Height_ScaleY * 2, Radius_ScaleX * 2);
                    break;
                case TriggerLightShape.NotInUse:
                    base.CreateTransformMatrix(Position, Rotation);
                    return;
            }

            transformMatrix = transformMatrix
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (TriggerShape == TriggerLightShape.Cube)
                DrawCubeTrigger(transformMatrix, isSelected);
            
            else if (TriggerShape == TriggerLightShape.Sphere)
                DrawSphereTrigger(transformMatrix, isSelected);
            
            else if (TriggerShape == TriggerLightShape.Cylinder)
                DrawCylinderTrigger(transformMatrix, isSelected);
            
            else if (TriggerShape ==  TriggerLightShape.NotInUse)
                base.Draw(modelNames, isSelected);
        }

        public enum NumberEnum
        {
            Player0 = 0,
            Player1 = 1,
            Player2 = 2,
            Player3 = 3,
            Object0 = 4,
            Object1 = 5,
            Object2 = 6,
            Object3 = 7,
            Enemy0 = 8,
            Enemy1 = 9,
            Enemy2 = 10,
            Enemy3 = 11,
            NotInUnse = 12,
            Other0 = 13,
            Other1 = 14,
            Other2 = 15,
            Ignore = 16
        }

        public NumberEnum Number
        {
            get { return (NumberEnum)ReadByte(4); }
            set { Write(4, (byte)value); }
        }

        public enum TypeEnum
        {
            Area = 0,
            Switch = 1,
            AreaDef = 2,
            SwitchOff = 3
        }
        public TypeEnum Type
        {
            get { return (TypeEnum)ReadByte(5); }
            set { Write(5, (byte)value); }
        }

        public enum TriggerLightShape
        {
            Cube = 0,
            Sphere = 1,
            Cylinder = 2,
            NotInUse = 3
        }

        public TriggerLightShape TriggerShape
        {
            get { return (TriggerLightShape)ReadByte(6); }
            set { Write(6, (byte)value); CreateTransformMatrix(Position, Rotation); }
        }

        public float Radius_ScaleX
        {
            get { return ReadFloat(8); }
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float Height_ScaleY
        {
            get { return ReadFloat(12); }
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleZ
        {
            get { return ReadFloat(16); }
            set { Write(16, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}