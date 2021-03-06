﻿using SharpDX;
using System.Windows.Forms;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System.Collections.Generic;
using HeroesPowerPlant.CollisionEditor;
using HeroesPowerPlant.LevelEditor;

namespace HeroesPowerPlant
{
    public class SharpRenderer
    {
        public static SharpDevice device;
        public static SharpCamera Camera = new SharpCamera();
        public static SharpFPS sharpFPS;

        public static float fovAngle;
        private static float aspectRatio;
        public static float near = 0.1f;
        public static float far;
        
        public SharpRenderer(Control control)
        {
            if (!SharpDevice.IsDirectX11Supported())
            {
                MessageBox.Show("DirectX11 Not Supported");
                return;
            }

            device = new SharpDevice(control);
            LoadModels();

            aspectRatio = (float)control.ClientSize.Width / control.ClientSize.Height;

            sharpFPS = new SharpFPS();
            sharpFPS.Reset();

            SetSharpShader(device);
        }
        
        public static SharpShader basicShader;
        public static SharpDX.Direct3D11.Buffer basicBuffer;

        public static SharpShader defaultShader;
        public static SharpDX.Direct3D11.Buffer defaultBuffer;

        public static SharpShader tintedShader;
        public static SharpDX.Direct3D11.Buffer tintedBuffer;

        public struct DefaultRenderData
        {
            public Matrix worldViewProjection;
            public Vector4 Color;
        }

        public static SharpShader collisionShader;
        public static SharpDX.Direct3D11.Buffer collisionBuffer;

        public struct CollisionRenderData
        {
            public Matrix viewProjection;
            public Vector4 ambientColor;
            public Vector4 lightDirection;
            public Vector4 lightDirection2;
        }

        public static void SetSharpShader(SharpDevice device)
        {
            basicShader = new SharpShader(device, "Resources/SharpDX/Shader_Basic.hlsl",
                new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS" },
                new InputElement[] {
                        new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0)
                });

            basicBuffer = basicShader.CreateBuffer<DefaultRenderData>();

            defaultShader = new SharpShader(device, "Resources/SharpDX/Shader_Default.hlsl",
                new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS" },
                new InputElement[] {
                        new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R8G8B8A8_UNorm, 12, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16, 0)
                });

            defaultBuffer = defaultShader.CreateBuffer<Matrix>();

            tintedShader = new SharpShader(device, "Resources/SharpDX/Shader_Tinted.hlsl",
                new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS" },
                new InputElement[] {
                        new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R8G8B8A8_UNorm, 12, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16, 0)
                });

            tintedBuffer = defaultShader.CreateBuffer<DefaultRenderData>();

            collisionShader = new SharpShader(device, "Resources/SharpDX/Shader_Collision.hlsl",
                new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS" },
                new InputElement[] {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("NORMAL", 0, Format.R32G32B32_Float, 16, 0),
                        new InputElement("COLOR", 0, Format.R8G8B8A8_UNorm, 28, 0)
                });

            collisionBuffer = collisionShader.CreateBuffer<CollisionRenderData>();
        }

        private static DefaultRenderData cubeRenderData;

        public static Vector4 normalColor = new Vector4(0.2f, 0.6f, 0.8f, 0.8f);
        public static Vector4 selectedColor = new Vector4(1f, 0.5f, 0.1f, 0.8f);
        public static Vector4 selectedObjectColor = new Vector4(1f, 0f, 0f, 1f);

        public static void DrawCubeTrigger(Matrix world, bool isSelected)
        {
            cubeRenderData.worldViewProjection = world * viewProjection;

            if (isSelected)
                cubeRenderData.Color = selectedColor;
            else
                cubeRenderData.Color = normalColor;

            device.SetFillModeDefault();
            device.SetCullModeNone();
            device.SetBlendStateAlphaBlend();
            device.ApplyRasterState();
            device.UpdateAllStates();
            
            device.UpdateData(basicBuffer, cubeRenderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Cube.Draw();
        }

        private static DefaultRenderData cylinderRenderData;

        public static void DrawCylinderTrigger(Matrix world, bool isSelected)// = false)
        {
            cylinderRenderData.worldViewProjection = world * viewProjection;

            if (isSelected)
                cylinderRenderData.Color = selectedColor;
            else
                cylinderRenderData.Color = normalColor;

            device.SetFillModeDefault();
            device.SetCullModeNone();
            device.SetBlendStateAlphaBlend();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(basicBuffer, cylinderRenderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Cylinder.Draw();
        }

        private static DefaultRenderData sphereRenderData;

        public static void DrawSphereTrigger(Matrix world, bool isSelected)
        {
            sphereRenderData.worldViewProjection = world * viewProjection;

            if (isSelected)
                sphereRenderData.Color = selectedColor;
            else
                sphereRenderData.Color = normalColor;

            device.SetFillModeDefault();
            device.SetCullModeNone();
            device.SetBlendStateAlphaBlend();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(basicBuffer, sphereRenderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Sphere.Draw();
        }

        private static bool showStartPositions = true;
        private static bool showSplines = true;
        private static bool showChunkBoxes = false;
        private static bool showCollision = false;
        private static bool showQuadtree = false;
        private static CheckState showObjects = CheckState.Indeterminate;
        private static bool showCameras = true;
        private static bool mouseModeObjects = true;

        public static void SetShowStartPos(bool value)
        {
            showStartPositions = value;
        }

        public static void SetSplines(bool value)
        {
            showSplines = value;
        }

        public static void SetChunkBoxes(bool value)
        {
            showChunkBoxes = value;
        }

        public static void SetShowCollision(bool value)
        {
            showCollision = value;
        }

        public static void SetShowQuadtree(bool value)
        {
            showQuadtree = value;
        }

        public static void SetShowObjects(CheckState checkState)
        {
            showObjects = checkState;
        }

        public static void SetShowCameras(bool value)
        {
            showCameras = value;
        }

        public static void SetMouseModeObjects(bool value)
        {
            mouseModeObjects = value;
        }

        public static SharpMesh Cube;
        public static SharpMesh Cylinder;
        public static SharpMesh Pyramid;
        public static SharpMesh Sphere;

        public static List<Vector3> cubeVertices;

        public static void LoadModels()
        {
            cubeVertices = new List<Vector3>();

            for (int i = 0; i < 4; i++)// 3; i++)
            {
                LevelEditorFunctions.ModelConverterData objData;

                if (i == 0) objData = LevelEditorFunctions.ReadOBJFile("Resources/Models/Box.obj", true);
                else if (i == 1) objData = LevelEditorFunctions.ReadOBJFile("Resources/Models/Cylinder.obj", true);
                else if (i == 2) objData = LevelEditorFunctions.ReadOBJFile("Resources/Models/Pyramid.obj", true);
                else objData = LevelEditorFunctions.ReadOBJFile("Resources/Models/Sphere.obj", true);

                List<Vertex> vertexList = new List<Vertex>();
                foreach (LevelEditorFunctions.Vertex v in objData.VertexList)
                {
                    vertexList.Add(new Vertex(v.Position));
                    if (i == 0) cubeVertices.Add(new Vector3(v.Position.X, v.Position.Y, v.Position.Z) * 5);
                }

                List<int> indexList = new List<int>();
                foreach (LevelEditorFunctions.Triangle t in objData.TriangleList)
                {
                    indexList.Add(t.vertex1);
                    indexList.Add(t.vertex2);
                    indexList.Add(t.vertex3);
                }

                if (i == 0) Cube = SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
                else if (i == 1) Cylinder = SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
                else if (i == 2) Pyramid = SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
                else Sphere = SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
            }
        }

        public static void ScreenClicked(Rectangle viewRectangle, int X, int Y)
        {
            Ray ray = Ray.GetPickRay(X, Y, new Viewport(viewRectangle), viewProjection);
            if (mouseModeObjects)
                Program.layoutEditor.ScreenClicked(ray);
            else
                Program.cameraEditor.ScreenClicked(ray);
        }

        public static Matrix viewProjection;
        public static Color4 backgroundColor = new Color4(0.05f, 0.05f, 0.15f, 1f);
        public static bool dontRender = false;
        public static BoundingFrustum frustum;

        public static void RunMainLoop(Panel Panel)
        {
            RenderLoop.Run(Panel, () =>
            {
                if (dontRender) return;

                //Resizing
                if (device.MustResize)
                {
                    device.Resize();
                    aspectRatio = (float)Panel.Width / Panel.Height;
                }

                Program.mainForm.KeyboardController();

                sharpFPS.Update();

                Program.mainForm.SetToolStripStatusLabel(Camera.GetInformation() + " FPS: " + sharpFPS.FPS.ToString());

                //clear color
                device.Clear(backgroundColor);

                //Set matrices
                viewProjection = Camera.GenerateLookAtRH() * Matrix.PerspectiveFovRH(fovAngle, aspectRatio, near, far);
                frustum = new BoundingFrustum(viewProjection);

                if (showCollision)
                {
                    CollisionRendering.RenderCollisionModel(viewProjection, -Camera.GetForward(), Camera.GetUp());
                    BSPRenderer.RenderShadowCollisionModel(viewProjection);
                }
                else
                {
                    BSPRenderer.RenderLevelModel(viewProjection);
                }

                if (showChunkBoxes)
                    VisibilityFunctions.RenderChunkModels(viewProjection);

                if (showObjects == CheckState.Checked)
                    Program.layoutEditor.layoutSystem.RenderAllSetObjects(true);
                else if (showObjects == CheckState.Indeterminate)
                    Program.layoutEditor.layoutSystem.RenderAllSetObjects(false);

                if (showCameras)
                    Program.cameraEditor.RenderAllCameras();

                if (showStartPositions)
                    Program.configEditor.RenderStartPositions();

                if (showSplines)
                    Program.splineEditor.RenderSplines();

                if (showQuadtree)
                    CollisionRendering.RenderQuadTree();
                                
                //present
                device.Present();
            });

            //release resources
            foreach (RenderWareModelFile r in BSPRenderer.BSPStream)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            foreach (RenderWareModelFile r in BSPRenderer.ShadowCollisionBSPStream)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            foreach (RenderWareModelFile r in DFFRenderer.DFFStream.Values)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            CollisionRendering.Dispose();

            if (BSPRenderer.whiteDefault != null)
                BSPRenderer.whiteDefault.Dispose();

            foreach (ShaderResourceView texture in BSPRenderer.TextureStream.Values)
                texture.Dispose();

            Cube.Dispose();
            Pyramid.Dispose();
            Cylinder.Dispose();

            basicBuffer.Dispose();
            basicShader.Dispose();

            defaultBuffer.Dispose();
            defaultShader.Dispose();

            collisionBuffer.Dispose();
            collisionShader.Dispose();

            device.Dispose();
        }
    }
}
