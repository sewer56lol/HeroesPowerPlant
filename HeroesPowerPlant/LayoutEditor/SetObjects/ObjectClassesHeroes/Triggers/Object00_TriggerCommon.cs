﻿using System;
using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_TriggerCommon : SetObjectManagerHeroes
    {
        private BoundingSphere sphereBound;

        public override bool TriangleIntersection(Ray r, string[] ModelNames)
        {
            if (TriggerShape == TriggerCommonShape.Sphere)
                return r.Intersects(sphereBound);
            else
                return base.TriangleIntersection(r, ModelNames);
        }

        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            switch (TriggerShape)
            {
                case TriggerCommonShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius_ScaleX);
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2);
                    break;
                case TriggerCommonShape.Cube:
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2, Height_ScaleY * 2, ScaleZ * 2);
                    break;
                case TriggerCommonShape.Cylinder:
                case TriggerCommonShape.CylinderXZ:
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2, Height_ScaleY * 2, Radius_ScaleX * 2);
                    break;
            }

            transformMatrix = transformMatrix
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            switch (TriggerShape)
            {
                case TriggerCommonShape.Sphere:
                    DrawSphereTrigger(transformMatrix, isSelected);
                    break;
                case TriggerCommonShape.Cube:
                    DrawCubeTrigger(transformMatrix, isSelected);
                    break;
                case TriggerCommonShape.Cylinder:
                case TriggerCommonShape.CylinderXZ:
                    DrawCylinderTrigger(transformMatrix, isSelected);
                    break;
            }
        }

        public enum TriggerCommonShape : Int32
        {
            Sphere = 0,
            Cylinder = 1,
            Cube = 2,
            CylinderXZ = 3,
        }

        public TriggerCommonShape TriggerShape
        {
            get { return (TriggerCommonShape)ReadLong(4); }
            set { Write(4, (int)value); CreateTransformMatrix(Position, Rotation); }
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