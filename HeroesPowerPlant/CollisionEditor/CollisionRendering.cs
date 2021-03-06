﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharpDX;
using static HeroesPowerPlant.ReadWriteCommon;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.CollisionEditor
{
    public class CollisionRendering
    {
        private static SharpMesh quadTreeMesh;
        private static DefaultRenderData mainQuadtreeRenderData = new DefaultRenderData();

        public static void SetQuadHeight(float value)
        {
            quadTreeTranslation = Matrix.Translation(0, value, 0);
        }

        private static Matrix quadTreeTranslation = Matrix.Identity;

        public static void RenderQuadTree()
        {
            if (quadTreeMesh == null)
                return;

            mainQuadtreeRenderData.worldViewProjection = quadTreeTranslation * viewProjection;

            device.SetFillModeSolid();
            device.SetCullModeNone();
            device.SetDefaultBlendState();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(basicBuffer, mainQuadtreeRenderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            quadTreeMesh.Draw();
        }

        private static SharpMesh secondQuadTreeMesh;
        private static DefaultRenderData secondQuadtreeRenderData = new DefaultRenderData();

        public static void RenderQuadTree2(SharpDevice device, SharpShader shader, SharpDX.Direct3D11.Buffer buffer, Matrix viewProjection)
        {
            if (secondQuadTreeMesh == null)
                return;

            secondQuadtreeRenderData.worldViewProjection = quadTreeTranslation * viewProjection;

            device.SetFillModeSolid();
            device.SetCullModeNone();
            device.SetDefaultBlendState();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(buffer, secondQuadtreeRenderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, buffer);
            shader.Apply();

            secondQuadTreeMesh.Draw();
        }

        private static SharpMesh collisionMesh;
        private static CollisionRenderData sceneInformation;

        public static void RenderCollisionModel(Matrix viewProjection, Vector3 lightDirection, Vector3 lightDirection2)
        {
            if (collisionMesh == null) return;

            sceneInformation = new CollisionRenderData()
            {
                viewProjection = viewProjection,
                ambientColor = new Vector4(0.1f, 0.1f, 0.3f, 1f),
                lightDirection = new Vector4(lightDirection, 1f),
                lightDirection2 = new Vector4(lightDirection2, 1f)
            };

            device.SetDefaultBlendState();
            device.SetCullModeDefault();
            device.SetFillModeDefault();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(collisionBuffer, sceneInformation);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, collisionBuffer);
            collisionShader.Apply();

            collisionMesh.Draw();
        }

        public static void Dispose()
        {
            if (collisionMesh != null)
                collisionMesh.Dispose();
            if (quadTreeMesh != null)
                quadTreeMesh.Dispose();
            if (secondQuadTreeMesh != null)
                secondQuadTreeMesh.Dispose();
        }
        
        public static CLFile LoadCLFile(string FileName)
        {
            CLFile data = new CLFile(0);
            
            BinaryReader CLReader = new BinaryReader(new FileStream(FileName, FileMode.Open));

            CLReader.BaseStream.Position = 0;

            data.numBytes = Switch(CLReader.ReadUInt32());
            //Get total number of bytes in file

            if (data.numBytes != CLReader.BaseStream.Length)
                throw new Exception("Not a valid CL file.");

            //Get offset of structs
            data.pointQuadtree = Switch(CLReader.ReadUInt32());
            data.pointTriangle = Switch(CLReader.ReadUInt32());
            data.pointVertex = Switch(CLReader.ReadUInt32());

            //Get quadtree center and radius
            data.quadCenterX = Switch(CLReader.ReadSingle());
            data.quadCenterY = Switch(CLReader.ReadSingle());
            data.quadCenterZ = Switch(CLReader.ReadSingle());
            data.quadLenght = Switch(CLReader.ReadSingle());

            //Get amount of stuff
            data.PowerFlag = Switch(CLReader.ReadUInt16());
            data.numTriangles = Switch(CLReader.ReadUInt16());
            data.numVertices = Switch(CLReader.ReadUInt16());
            data.numQuadnodes = Switch(CLReader.ReadUInt16());

            Program.collisionEditor.progressBar1.Maximum = data.numTriangles + data.numVertices + 3 * data.numQuadnodes;

            List<CLTriangle> CLTriangleList = new List<CLTriangle>(data.numTriangles);
            List<CLVertex> CLVertexList = new List<CLVertex>(data.numVertices);
            data.MeshTypeList = new List<UInt64>();

            Program.collisionEditor.labelVertexNum.Text = "Number of Vertices: " + data.numVertices.ToString();
            Program.collisionEditor.labelTriangles.Text = "Number of Triangles: " + data.numTriangles.ToString();
            Program.collisionEditor.labelQuadnodes.Text = "Number of QuadNodes: " + data.numQuadnodes.ToString();

            for (int i = 0; i < data.numVertices; i++)
            {
                CLReader.BaseStream.Position = data.pointVertex + i * 0xC;
                CLVertexList.Add(new CLVertex(Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle())));
                Program.collisionEditor.progressBar1.PerformStep();
            }
            
            for (int i = 0; i < data.numTriangles; i++)
            {
                CLReader.BaseStream.Position = data.pointTriangle + i * 0x20;
                CLTriangleList.Add(new CLTriangle(Switch(CLReader.ReadUInt16()), Switch(CLReader.ReadUInt16()), Switch(CLReader.ReadUInt16())));
                CLReader.BaseStream.Position += 6;

                Vector3 Normals = new Vector3(Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle()), Switch(CLReader.ReadSingle()));
                CLVertexList[CLTriangleList[i].Vertices[0]].NormalList.Add(Normals);
                CLVertexList[CLTriangleList[i].Vertices[1]].NormalList.Add(Normals);
                CLVertexList[CLTriangleList[i].Vertices[2]].NormalList.Add(Normals);

                UInt64 FlagsAsUint64 = CLReader.ReadUInt64();
                CLTriangleList[i].ColFlags = BitConverter.GetBytes(FlagsAsUint64);
                if (!data.MeshTypeList.Contains(FlagsAsUint64))
                    data.MeshTypeList.Add(FlagsAsUint64);

                Color TempColor = new Color(CLTriangleList[i].ColFlags[1], CLTriangleList[i].ColFlags[2], CLTriangleList[i].ColFlags[3]);

                if (TempColor.R == 0) TempColor.R = 255;
                else
                    TempColor.R = (byte)(256 - (Math.Log(TempColor.R, 2) + 1) * 32);
                if (TempColor.G == 0) TempColor.G = 255;
                else
                    TempColor.G = (byte)(256 - (Math.Log(TempColor.G, 2) + 1) * 32);
                if (TempColor.B == 0) TempColor.B = 255;
                else
                    TempColor.B = (byte)(256 - (Math.Log(TempColor.B, 2) + 1) * 32);
                
                CLVertexList[CLTriangleList[i].Vertices[0]].Color = TempColor;
                CLVertexList[CLTriangleList[i].Vertices[1]].Color = TempColor;
                CLVertexList[CLTriangleList[i].Vertices[2]].Color = TempColor;

                Program.collisionEditor.progressBar1.PerformStep();
            }

            data.CLTriangleArray = CLTriangleList.ToArray();

            for (int i = 0; i < data.numQuadnodes; i++)
            {
                CLReader.BaseStream.Position = data.pointQuadtree + i * 0x20;
                CLQuadNode TempNode = new CLQuadNode
                {
                    Index = Switch(CLReader.ReadUInt16()),
                    Parent = Switch(CLReader.ReadUInt16()),
                    Child = Switch(CLReader.ReadUInt16())
                };
                CLReader.BaseStream.Position += 8;
                TempNode.NodeTriangleAmount = Switch(CLReader.ReadUInt16());
                TempNode.TriListOffset = Switch(CLReader.ReadUInt32());
                TempNode.PosValueX = Switch(CLReader.ReadUInt16());
                TempNode.PosValueZ = Switch(CLReader.ReadUInt16());
                TempNode.Depth = CLReader.ReadByte();

                data.CLQuadNodeList.Add(TempNode);
                Program.collisionEditor.progressBar1.PerformStep();
            }

            ReBuildQuadtree(ref data);
            DetermineQuadtreeRenderStuff(ref data);

            CLReader.Close();

            data.CLVertexArray = new VertexColoredNormalized[CLVertexList.Count()];
            for (int i = 0; i < CLVertexList.Count; i++)
            {
                data.CLVertexArray[i] = new VertexColoredNormalized(CLVertexList[i].Position,
                    CLVertexList[i].CalculateNormals(),
                    CLVertexList[i].Color);
            }

            int[] IndexArray = new int[data.CLTriangleArray.Count() * 3];

            for (int i = 0; i < data.CLTriangleArray.Count() * 3; i++)
                IndexArray[i] = data.CLTriangleArray[i / 3].Vertices[i % 3];

            collisionMesh = SharpMesh.Create(device, data.CLVertexArray, IndexArray, new List<SharpSubSet>() { new SharpSubSet(0, IndexArray.Count(), null) }, SharpDX.Direct3D.PrimitiveTopology.TriangleList);

            return data;
        }

        public static bool ReBuildQuadtree(ref CLFile data)
        {
            data.CLQuadNodeList[0].NodeSquare.X = data.quadCenterX - (data.quadLenght / 2);
            data.CLQuadNodeList[0].NodeSquare.Y = data.quadCenterZ - (data.quadLenght / 2);

            data.CLQuadNodeList[0].NodeSquare.Height = data.quadLenght;
            data.CLQuadNodeList[0].NodeSquare.Width = data.quadLenght;

            for (int i = 0; i < data.CLQuadNodeList.Count; i++)
            {
                if (data.CLQuadNodeList[i].Child != 0)
                {
                    GiveSquareToChild(ref data, data.CLQuadNodeList[i].NodeSquare, data.CLQuadNodeList[i].Child, 0);
                    GiveSquareToChild(ref data, data.CLQuadNodeList[i].NodeSquare, data.CLQuadNodeList[i].Child, 1);
                    GiveSquareToChild(ref data, data.CLQuadNodeList[i].NodeSquare, data.CLQuadNodeList[i].Child, 2);
                    GiveSquareToChild(ref data, data.CLQuadNodeList[i].NodeSquare, data.CLQuadNodeList[i].Child, 3);
                }
                Program.collisionEditor.progressBar1.PerformStep();
            }

            //sewer's code
            Program.collisionEditor.checkedListBox1.Items.Clear();
            foreach (CLQuadNode i in data.CLQuadNodeList)
                Program.collisionEditor.checkedListBox1.Items.Add(i);
            // end

            return true;
        }

        public static void GiveSquareToChild(ref CLFile data, RectangleF r, int Child, int Offset)
        {
            data.CLQuadNodeList[Child + Offset].NodeSquare = new RectangleF(r.X, r.Y, r.Width / 2, r.Height / 2);

            if (Offset == 1)
                data.CLQuadNodeList[Child + Offset].NodeSquare.X += data.CLQuadNodeList[Child + Offset].NodeSquare.Width;
            else if (Offset == 2)
                data.CLQuadNodeList[Child + Offset].NodeSquare.Y += data.CLQuadNodeList[Child + Offset].NodeSquare.Height;
            else if (Offset == 3)
            {
                data.CLQuadNodeList[Child + Offset].NodeSquare.X += data.CLQuadNodeList[Child + Offset].NodeSquare.Width;
                data.CLQuadNodeList[Child + Offset].NodeSquare.Y += data.CLQuadNodeList[Child + Offset].NodeSquare.Height;
            }
        }
        
        public static void DetermineQuadtreeRenderStuff(ref CLFile data)
        {
            List<Vertex> QuadNodeVertexList = new List<Vertex>();
            List<Int32> QuadNodeIndexList = new List<Int32>();

            Int32 k = 0;
            foreach (CLQuadNode i in data.CLQuadNodeList)
            {
                Program.collisionEditor.progressBar1.PerformStep();
                if (i.Child == 0 | i.Index == 0)
                {
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X, 0, i.NodeSquare.Y)));
                    QuadNodeIndexList.Add(k);
                    QuadNodeIndexList.Add(k + 1);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X + i.NodeSquare.Width, 0, i.NodeSquare.Y)));
                    QuadNodeIndexList.Add(k + 1);
                    QuadNodeIndexList.Add(k + 2);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X + i.NodeSquare.Width, 0, i.NodeSquare.Y + i.NodeSquare.Height)));
                    QuadNodeIndexList.Add(k + 2);
                    QuadNodeIndexList.Add(k + 3);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X, 0, i.NodeSquare.Y + i.NodeSquare.Height)));
                    QuadNodeIndexList.Add(k + 3);
                    QuadNodeIndexList.Add(k);
                    k += 4;
                }
            }

            quadTreeMesh = SharpMesh.Create(device, QuadNodeVertexList.ToArray(), QuadNodeIndexList.ToArray(), new List<SharpSubSet>(){
                new SharpSubSet(0, QuadNodeIndexList.Count, null) }, SharpDX.Direct3D.PrimitiveTopology.LineList);
            mainQuadtreeRenderData.Color = new Vector4(0.9f, 0.3f, 0.6f, 1f);
        }
        
        //sewer's code
        public static void DetermineRenderStuff2(CLFile data)
        {
            List<Vertex> QuadNodeVertexList = new List<Vertex>();
            List<Int32> QuadNodeIndexList = new List<Int32>();

            Int32 k = 0;
            foreach (CLQuadNode i in data.CLQuadNodeList)
            {
                if (Program.collisionEditor.checkedListBox1.CheckedIndices.Contains(i.Index))
                {
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X, 100 / (i.Depth + 1), i.NodeSquare.Y)));
                    QuadNodeIndexList.Add(k);
                    QuadNodeIndexList.Add(k + 1);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X + i.NodeSquare.Width, 100 / (i.Depth + 1), i.NodeSquare.Y)));
                    QuadNodeIndexList.Add(k + 1);
                    QuadNodeIndexList.Add(k + 2);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X + i.NodeSquare.Width, 100 / (i.Depth + 1), i.NodeSquare.Y + i.NodeSquare.Height)));
                    QuadNodeIndexList.Add(k + 2);
                    QuadNodeIndexList.Add(k + 3);
                    QuadNodeVertexList.Add(new Vertex(new Vector3(i.NodeSquare.X, 100 / (i.Depth + 1), i.NodeSquare.Y + i.NodeSquare.Height)));
                    QuadNodeIndexList.Add(k + 3);
                    QuadNodeIndexList.Add(k);
                    k += 4;
                }
            }
            
            secondQuadTreeMesh = SharpMesh.Create(SharpRenderer.device, QuadNodeVertexList.ToArray(), QuadNodeIndexList.ToArray(), new List<SharpSubSet>(){
                new SharpSubSet(0, QuadNodeIndexList.Count, null) }, SharpDX.Direct3D.PrimitiveTopology.LineList);
            secondQuadtreeRenderData.Color = new Vector4(0.3f, 0.9f, 0.6f, 1f);
        }
    }
}
