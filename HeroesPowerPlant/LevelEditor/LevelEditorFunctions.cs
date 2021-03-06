﻿using RenderWareFile;
using RenderWareFile.Sections;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HeroesPowerPlant.LevelEditor
{
    public partial class LevelEditorFunctions
    {
        public struct Vertex
        {
            public Vector3 Position;
            public SharpDX.Color Color;
            public Vector2 TexCoord;

            public bool HasUV;
            public bool HasColor;
        }

        public class Triangle
        {
            public int MaterialIndex;

            public int vertex1;
            public int vertex2;
            public int vertex3;

            public int UVCoord1;
            public int UVCoord2;
            public int UVCoord3;

            public int Color1;
            public int Color2;
            public int Color3;
        }

        public class TriangleExt : Triangle
        {
            public RenderWareFile.Color collisionFlag;
        }

        public struct ModelConverterData
        {
            public List<string> MaterialList;
            public List<Vertex> VertexList;
            public List<Vector2> UVList;
            public List<SharpDX.Color> ColorList;
            public List<Triangle> TriangleList;
            public string MTLLib;
        }

        public static ModelConverterData ReadOBJFile(string InputFile, bool isShadowCollision)
        {
            ModelConverterData objData = new ModelConverterData()
            {
                MaterialList = new List<string>(),
                VertexList = new List<Vertex>(),
                UVList = new List<Vector2>(),
                ColorList = new List<SharpDX.Color>(),
                TriangleList = new List<Triangle>(),
                MTLLib = null
            };

            string[] OBJFile = File.ReadAllLines(InputFile);

            int CurrentMaterial = -1;
            
            bool hasUVCoords = true;

            RenderWareFile.Color TempColFlags = new RenderWareFile.Color(0, 0, 0, 0);

            foreach (string j in OBJFile)
            {
                if (j.Length > 2)
                {
                    if (j.StartsWith("v "))
                    {
                        string a = Regex.Replace(j, @"\s+", " ");

                        string[] SubStrings = a.Split(' ');
                        Vertex TempVertex = new Vertex();
                        TempVertex.Position.X = Convert.ToSingle(SubStrings[1]);
                        TempVertex.Position.Y = Convert.ToSingle(SubStrings[2]);
                        TempVertex.Position.Z = Convert.ToSingle(SubStrings[3]);

                        TempVertex.Color = SharpDX.Color.White;

                        objData.VertexList.Add(TempVertex);
                    }
                    else if (j.Substring(0, 3) == "vt ")
                    {
                        string[] SubStrings = j.Split(' ');
                        Vector2 TempUV = new Vector2
                        {
                            X = Convert.ToSingle(SubStrings[1]),
                            Y = Convert.ToSingle(SubStrings[2])
                        };
                        objData.UVList.Add(TempUV);
                    }
                    else if (j.Substring(0, 3) == "vc ") // Special code
                    {
                        string[] SubStrings = j.Split(' ');
                        SharpDX.Color TempColor = new SharpDX.Color
                        {
                            R = Convert.ToByte(SubStrings[1]),
                            G = Convert.ToByte(SubStrings[2]),
                            B = Convert.ToByte(SubStrings[3]),
                            A = Convert.ToByte(SubStrings[4])
                        };

                        objData.ColorList.Add(TempColor);
                    }
                    else if (j.StartsWith("f "))
                    {
                        string[] SubStrings = j.Split(' ');

                        Triangle TempTriangle = new Triangle
                        {
                            MaterialIndex = CurrentMaterial,
                            vertex1 = Convert.ToInt32(SubStrings[1].Split('/')[0]) - 1,
                            vertex2 = Convert.ToInt32(SubStrings[2].Split('/')[0]) - 1,
                            vertex3 = Convert.ToInt32(SubStrings[3].Split('/')[0]) - 1
                        };

                        if (hasUVCoords & !isShadowCollision)
                        {
                            try
                            {
                                TempTriangle.UVCoord1 = Convert.ToInt32(SubStrings[1].Split('/')[1]) - 1;
                                TempTriangle.UVCoord2 = Convert.ToInt32(SubStrings[2].Split('/')[1]) - 1;
                                TempTriangle.UVCoord3 = Convert.ToInt32(SubStrings[3].Split('/')[1]) - 1;
                            }
                            catch
                            {
                                MessageBox.Show("Error parsing texture coordinates. The model will be imported without them.");
                                hasUVCoords = false;
                                objData.UVList = new List<Vector2>() { new Vector2() };
                            }
                        }

                        objData.TriangleList.Add(TempTriangle);
                    }
                    else if (j.StartsWith("usemtl "))
                    {
                        objData.MaterialList.Add(Regex.Replace(j.Substring(7), @"\s+", ""));
                        CurrentMaterial += 1;
                    }
                    else if (j.StartsWith("mtllib "))
                        objData.MTLLib = j.Substring(7).Split('\\').LastOrDefault();
                }
            }

            if (isShadowCollision)
                return objData;

            // Special code
            if (objData.ColorList.Count == objData.VertexList.Count)
                for (int i = 0; i < objData.VertexList.Count; i++)
                {
                    Vertex v = objData.VertexList[i];
                    v.Color = objData.ColorList[i];
                    objData.VertexList[i] = v;
                }

            try
            {
                objData.MaterialList = ReplaceMaterialNames(InputFile, objData.MTLLib, objData.MaterialList);
            }
            catch
            {
                MessageBox.Show("Unable to load material lib. Will use material names as texture names.");
            }

            if (hasUVCoords)
                return FixUVCoords(objData);
            else
                return objData;
        }

        public static List<string> ReplaceMaterialNames(string InputOBJFile, string MTLLib, List<string> MaterialList)
        {
            string MTLPath = Path.Combine(Path.GetDirectoryName(InputOBJFile), MTLLib);
            string[] MTLFile = File.ReadAllLines(MTLPath);

            Dictionary<string, string> MaterialLibrary = new Dictionary<string, string>();

            string MaterialName = "";

            foreach (string j in MTLFile)
            {
                string a = Regex.Replace(j, @"\s+", "");

                if (a.StartsWith("newmtl"))
                {
                    MaterialName = a.Substring(6);
                }
                else if (a.StartsWith("map_Kd"))
                {
                    if (!MaterialLibrary.ContainsKey(MaterialName))
                    MaterialLibrary.Add(MaterialName, Path.GetFileNameWithoutExtension(a.Substring(6)));
                }
            }

            for (int k = 0; k < MaterialList.Count; k++)
            {
                if (MaterialLibrary.ContainsKey(MaterialList[k]))
                    MaterialList[k] = MaterialLibrary[MaterialList[k]];
                else
                    MessageBox.Show("Texture name for material " + MaterialList[k] + " was not found in the " + MTLPath + " file.");
            }

            return MaterialList;
        }

        public static ModelConverterData FixUVCoords(ModelConverterData data)
        {
            for (int i = 0; i < data.TriangleList.Count; i++)
            {
                if (data.VertexList[data.TriangleList[i].vertex1].HasUV == false)
                {
                    Vertex TempVertex = data.VertexList[data.TriangleList[i].vertex1];

                    TempVertex.TexCoord.X = data.UVList[data.TriangleList[i].UVCoord1].X;
                    TempVertex.TexCoord.Y = data.UVList[data.TriangleList[i].UVCoord1].Y;
                    TempVertex.HasUV = true;
                    data.VertexList[data.TriangleList[i].vertex1] = TempVertex;
                }
                else
                {
                    Vertex TempVertex = data.VertexList[data.TriangleList[i].vertex1];

                    if ((TempVertex.TexCoord.X != data.UVList[data.TriangleList[i].UVCoord1].X) | (TempVertex.TexCoord.Y != data.UVList[data.TriangleList[i].UVCoord1].Y))
                    {
                        TempVertex.TexCoord.X = data.UVList[data.TriangleList[i].UVCoord1].X;
                        TempVertex.TexCoord.Y = data.UVList[data.TriangleList[i].UVCoord1].Y;

                        Triangle TempTriangle = data.TriangleList[i];
                        TempTriangle.vertex1 = data.VertexList.Count;
                        data.TriangleList[i] = TempTriangle;
                        data.VertexList.Add(TempVertex);
                    }
                }
                if (data.VertexList[data.TriangleList[i].vertex2].HasUV == false)
                {
                    Vertex TempVertex = data.VertexList[data.TriangleList[i].vertex2];

                    TempVertex.TexCoord.X = data.UVList[data.TriangleList[i].UVCoord2].X;
                    TempVertex.TexCoord.Y = data.UVList[data.TriangleList[i].UVCoord2].Y;
                    TempVertex.HasUV = true;
                    data.VertexList[data.TriangleList[i].vertex2] = TempVertex;
                }
                else
                {
                    Vertex TempVertex = data.VertexList[data.TriangleList[i].vertex2];

                    if ((TempVertex.TexCoord.X != data.UVList[data.TriangleList[i].UVCoord2].X) | (TempVertex.TexCoord.Y != data.UVList[data.TriangleList[i].UVCoord2].Y))
                    {
                        TempVertex.TexCoord.X = data.UVList[data.TriangleList[i].UVCoord2].X;
                        TempVertex.TexCoord.Y = data.UVList[data.TriangleList[i].UVCoord2].Y;

                        Triangle TempTriangle = data.TriangleList[i];
                        TempTriangle.vertex2 = data.VertexList.Count;
                        data.TriangleList[i] = TempTriangle;
                        data.VertexList.Add(TempVertex);
                    }
                }
                if (data.VertexList[data.TriangleList[i].vertex3].HasUV == false)
                {
                    Vertex TempVertex = data.VertexList[data.TriangleList[i].vertex3];

                    TempVertex.TexCoord.X = data.UVList[data.TriangleList[i].UVCoord3].X;
                    TempVertex.TexCoord.Y = data.UVList[data.TriangleList[i].UVCoord3].Y;
                    TempVertex.HasUV = true;
                    data.VertexList[data.TriangleList[i].vertex3] = TempVertex;
                }
                else
                {
                    Vertex TempVertex = data.VertexList[data.TriangleList[i].vertex3];

                    if ((TempVertex.TexCoord.X != data.UVList[data.TriangleList[i].UVCoord3].X) | (TempVertex.TexCoord.Y != data.UVList[data.TriangleList[i].UVCoord3].Y))
                    {
                        TempVertex.TexCoord.X = data.UVList[data.TriangleList[i].UVCoord3].X;
                        TempVertex.TexCoord.Y = data.UVList[data.TriangleList[i].UVCoord3].Y;

                        Triangle TempTriangle = data.TriangleList[i];
                        TempTriangle.vertex3 = data.VertexList.Count;
                        data.TriangleList[i] = TempTriangle;
                        data.VertexList.Add(TempVertex);
                    }
                }
            }

            return data;
        }

        public static int renderWareVersion = 0x1400FFFF;

        public static RWSection[] CreateBSPFile(string FileNameForBox, ModelConverterData data)
        {
            Vertex3 Max = new Vertex3(data.VertexList[0].Position.X, data.VertexList[0].Position.Y, data.VertexList[0].Position.Z);
            Vertex3 Min = new Vertex3(data.VertexList[0].Position.X, data.VertexList[0].Position.Y, data.VertexList[0].Position.Z);

            foreach (Vertex i in data.VertexList)
            {
                if (i.Position.X > Max.X)
                    Max.X = i.Position.X;
                if (i.Position.Y > Max.Y)
                    Max.Y = i.Position.Y;
                if (i.Position.Z > Max.Z)
                    Max.Z = i.Position.Z;
                if (i.Position.X < Min.X)
                    Min.X = i.Position.X;
                if (i.Position.Y < Min.Y)
                    Min.Y = i.Position.Y;
                if (i.Position.Z < Min.Z)
                    Min.Z = i.Position.Z;
            }

            List<Vertex3> vList = new List<Vertex3>(data.VertexList.Count);
            foreach (Vertex v in data.VertexList)
                vList.Add(new Vertex3(v.Position.X, v.Position.Y, v.Position.Z));

            List<RenderWareFile.Color> cList = new List<RenderWareFile.Color>(data.VertexList.Count);
            foreach (Vertex v in data.VertexList)
                cList.Add(new RenderWareFile.Color(v.Color.R, v.Color.G, v.Color.B, v.Color.A));

            List<TextCoord> uvList = new List<TextCoord>(data.VertexList.Count);
            if (Program.levelEditor.checkBoxFlipUVs.Checked)
                foreach (Vertex v in data.VertexList)
                    uvList.Add(new TextCoord(v.TexCoord.X, v.TexCoord.Y));
            else
                foreach (Vertex v in data.VertexList)
                    uvList.Add(new TextCoord(v.TexCoord.X, -v.TexCoord.Y));

            List<RenderWareFile.Triangle> tList = new List<RenderWareFile.Triangle>(data.TriangleList.Count);
            foreach (Triangle t in data.TriangleList)
                tList.Add(new RenderWareFile.Triangle((ushort)t.MaterialIndex, (ushort)t.vertex1, (ushort)t.vertex2, (ushort)t.vertex3));

            List<BinMesh> binMeshList = new List<BinMesh>();
            int TotalNumberOfTristripIndicies = 0;

            if (Program.levelEditor.checkBoxTristrip.Checked) // tristrip generator
            {
                for (int i = 0; i < data.MaterialList.Count; i++)
                {
                    List<Triangle> TriangleStream2 = new List<Triangle>();
                    foreach (Triangle f in data.TriangleList)
                        if (f.MaterialIndex == i)
                            TriangleStream2.Add(f);

                    List<List<int>> indexLists = GenerateTristrips(TriangleStream2);

                    foreach (List<int> indices in indexLists)
                    {
                        TotalNumberOfTristripIndicies += indices.Count();
                        binMeshList.Add(new BinMesh()
                        {
                            materialIndex = i,
                            indexCount = indices.Count(),
                            vertexIndices = indices.ToArray()
                        });
                    }
                }
            }
            else //trilist generator
            {
                for (int i = 0; i < data.MaterialList.Count; i++)
                {
                    List<int> indices = new List<int>();
                    foreach (Triangle f in data.TriangleList)
                    {
                        if (f.MaterialIndex == i)
                        {
                            indices.Add(f.vertex1);
                            indices.Add(f.vertex2);
                            indices.Add(f.vertex3);
                        }
                    }
                    TotalNumberOfTristripIndicies += indices.Count();

                    binMeshList.Add(new BinMesh
                    {
                        materialIndex = i,
                        indexCount = indices.Count(),
                        vertexIndices = indices.ToArray()
                    });
                }
            }

            WorldFlags worldFlags = WorldFlags.HasOneSetOfTextCoords | WorldFlags.HasVertexColors
                | WorldFlags.WorldSectorsOverlap | (WorldFlags)0x00010000;

            worldFlags = Program.levelEditor.checkBoxTristrip.Checked ? worldFlags | WorldFlags.UseTriangleStrips : worldFlags;

            World_000B world = new World_000B()
            {
                worldStruct = new WorldStruct_0001()
                {
                    rootIsWorldSector = 1,
                    inverseOrigin = new Vertex3(-0f, -0f, -0f),
                    numTriangles = (uint)data.TriangleList.Count(),
                    numVertices = (uint)data.VertexList.Count(),
                    numPlaneSectors = 0,
                    numAtomicSectors = 1,
                    colSectorSize = 0,
                    worldFlags = worldFlags,
                    boxMaximum = Max,
                    boxMinimum = Min,
                },

                materialList = new MaterialList_0008()
                {
                    materialListStruct = new MaterialListStruct_0001()
                    {
                        materialCount = data.MaterialList.Count()
                    },
                    materialList = new Material_0007[data.MaterialList.Count()]
                },

                firstWorldChunk = new AtomicSector_0009()
                {
                    atomicStruct = new AtomicSectorStruct_0001()
                    {
                        matListWindowBase = 0,
                        numTriangles = data.TriangleList.Count(),
                        numVertices = data.VertexList.Count(),
                        boxMaximum = Max,
                        boxMinimum = Min,
                        collSectorPresent = 0x2F50D984,
                        unused = 0,
                        vertexArray = vList.ToArray(),
                        colorArray = cList.ToArray(),
                        uvArray = uvList.ToArray(),
                        triangleArray = tList.ToArray()
                    },
                    atomicExtension = new Extension_0003()
                    {
                        extensionSectionList = new List<RWSection>() { new BinMeshPLG_050E()
                        {
                            binMeshHeaderFlags = Program.levelEditor.checkBoxTristrip.Checked ? BinMeshHeaderFlags.TriangleStrip : BinMeshHeaderFlags.TriangleList,
                            numMeshes = binMeshList.Count(),
                            totalIndexCount = TotalNumberOfTristripIndicies,
                            binMeshList = binMeshList.ToArray()
                        }
                        }
                    }
                },

                worldExtension = new Extension_0003()
            };

            for (int i = 0; i < data.MaterialList.Count; i++)
            {
                world.materialList.materialList[i] = new Material_0007()
                {
                    materialStruct = new MaterialStruct_0001()
                    {
                        unusedFlags = 0,
                        color = new RenderWareFile.Color() { R = 0xFF, G = 0xFF, B = 0xFF, A = 0xFF },
                        unusedInt2 = 0x2DF53E84,
                        isTextured = 1,
                        ambient = 1f,
                        specular = 1f,
                        diffuse = 1f
                    },
                    texture = new Texture_0006()
                    {
                        textureStruct = new TextureStruct_0001()
                        {
                            filterMode = FilterMode.FILTERLINEAR,
                            addressModeU = AddressMode.TEXTUREADDRESSWRAP,
                            addressModeV = AddressMode.TEXTUREADDRESSWRAP,
                            useMipLevels = 1
                        },
                        diffuseTextureName = new String_0002()
                        {
                            stringString = data.MaterialList[i]
                        },
                        alphaTextureName = new String_0002()
                        {
                            stringString = ""
                        },
                        textureExtension = new Extension_0003()
                    },
                    materialExtension = new Extension_0003(),
                };
            }

            return new RWSection[] { world };
        }

        private static List<List<int>> GenerateTristrips(List<Triangle> triangleStream)
        {
            List<List<int>> indexLists = new List<List<int>>
            {
                new List<int>()
            };
            indexLists.Last().Add(triangleStream[0].vertex1);
            indexLists.Last().Add(triangleStream[0].vertex2);
            indexLists.Last().Add(triangleStream[0].vertex3);
            triangleStream[0].MaterialIndex = -1;

            bool allAreDone = false;

            while (!allAreDone)
            {
                bool inverted = false;

                for (int i = 0; i < triangleStream.Count(); i++)
                {
                    if (triangleStream[i].MaterialIndex == -1) continue;

                    if (!inverted)
                    {
                        if (indexLists.Last()[indexLists.Last().Count - 2] == triangleStream[i].vertex1 &
                            indexLists.Last()[indexLists.Last().Count - 1] == triangleStream[i].vertex2)
                        {
                            indexLists.Last().Add(triangleStream[i].vertex3);
                            triangleStream[i].MaterialIndex = -1;
                            inverted = !inverted;
                            i = 0;
                            continue;
                        }
                        else if (indexLists.Last()[indexLists.Last().Count - 2] == triangleStream[i].vertex2 &
                            indexLists.Last()[indexLists.Last().Count - 1] == triangleStream[i].vertex3)
                        {
                            indexLists.Last().Add(triangleStream[i].vertex1);
                            triangleStream[i].MaterialIndex = -1;
                            inverted = !inverted;
                            i = 0;
                            continue;
                        }
                        else if (indexLists.Last()[indexLists.Last().Count - 2] == triangleStream[i].vertex3 &
                            indexLists.Last()[indexLists.Last().Count - 1] == triangleStream[i].vertex1)
                        {
                            indexLists.Last().Add(triangleStream[i].vertex2);
                            triangleStream[i].MaterialIndex = -1;
                            inverted = !inverted;
                            i = 0;
                            continue;
                        }
                    }
                    else
                    {
                        if (indexLists.Last()[indexLists.Last().Count - 2] == triangleStream[i].vertex2 &
                            indexLists.Last()[indexLists.Last().Count - 1] == triangleStream[i].vertex1)
                        {
                            indexLists.Last().Add(triangleStream[i].vertex3);
                            triangleStream[i].MaterialIndex = -1;
                            inverted = !inverted;
                            i = 0;
                            continue;
                        }
                        else if (indexLists.Last()[indexLists.Last().Count - 2] == triangleStream[i].vertex3 &
                            indexLists.Last()[indexLists.Last().Count - 1] == triangleStream[i].vertex2)
                        {
                            indexLists.Last().Add(triangleStream[i].vertex1);
                            triangleStream[i].MaterialIndex = -1;
                            inverted = !inverted;
                            i = 0;
                            continue;
                        }
                        else if (indexLists.Last()[indexLists.Last().Count - 2] == triangleStream[i].vertex1 &
                            indexLists.Last()[indexLists.Last().Count - 1] == triangleStream[i].vertex3)
                        {
                            indexLists.Last().Add(triangleStream[i].vertex2);
                            triangleStream[i].MaterialIndex = -1;
                            inverted = !inverted;
                            i = 0;
                            continue;
                        }
                    }
                }

                allAreDone = true;

                for (int i = 0; i < triangleStream.Count(); i++)
                {
                    if (triangleStream[i].MaterialIndex == -1)
                        continue;
                    else
                    {
                        indexLists.Add(new List<int>());
                        indexLists.Last().Add(triangleStream[i].vertex1);
                        indexLists.Last().Add(triangleStream[i].vertex2);
                        indexLists.Last().Add(triangleStream[i].vertex3);
                        triangleStream[i].MaterialIndex = -1;
                        allAreDone = false;

                    }
                }
            }

            return indexLists;
        }

        public static void ConvertBSPtoOBJ(string fileName, RenderWareModelFile bspFile)
        {
            int totalVertexIndices = 0;

            string materialLibrary = Path.ChangeExtension(fileName, "MTL");
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            StreamWriter OBJWriter = new StreamWriter((Path.ChangeExtension(fileName, "OBJ")), false);

            List<Triangle> triangleList = new List<Triangle>();

            foreach (RWSection rw in bspFile.GetAsRWSectionArray())
            {
                if (rw is World_000B w)
                {
                    OBJWriter.WriteLine("# Exported by Heroes Power Plant");
                    OBJWriter.WriteLine("mtllib " + Path.GetFileName(materialLibrary));
                    OBJWriter.WriteLine();
                    if (w.firstWorldChunk.sectionIdentifier == Section.AtomicSector)
                    {
                        GetAtomicTriangleList(OBJWriter, (AtomicSector_0009)w.firstWorldChunk, ref triangleList, ref totalVertexIndices, bspFile.isShadowCollision);
                    }
                    else if (w.firstWorldChunk.sectionIdentifier == Section.PlaneSector)
                    {
                        GetPlaneTriangleList(OBJWriter, (PlaneSector_000A)w.firstWorldChunk, ref triangleList, ref totalVertexIndices, bspFile.isShadowCollision);
                    }
                }
            }

            if (bspFile.isShadowCollision)
            {
                RenderWareFile.Color currentColFlag = new RenderWareFile.Color(3, 3, 3, 3);

                foreach (TriangleExt j in triangleList)
                {
                    if (j.collisionFlag != currentColFlag)
                    {
                        currentColFlag = j.collisionFlag;
                        OBJWriter.WriteLine();
                        OBJWriter.WriteLine("g " + fileNameWithoutExtension + "_" + currentColFlag.ToString());
                        OBJWriter.WriteLine("usemtl colmat_" + currentColFlag.ToString());
                    }

                    OBJWriter.WriteLine("f "
                        + (j.vertex1 + 1).ToString() + " "
                        + (j.vertex2 + 1).ToString() + " "
                        + (j.vertex3 + 1).ToString());
                }

                OBJWriter.Close();
                WriteCollisionMaterialLib(triangleList, bspFile.MaterialList.ToArray(), materialLibrary);
            }
            else
            {
                for (int i = 0; i < bspFile.MaterialList.Count; i++)
                {
                    OBJWriter.WriteLine("g " + fileNameWithoutExtension + "_" + bspFile.MaterialList[i]);
                    OBJWriter.WriteLine("usemtl " + bspFile.MaterialList[i] + "_m");

                    foreach (Triangle j in triangleList)
                        if (j.MaterialIndex == i)
                            OBJWriter.WriteLine("f "
                                + (j.vertex1 + 1).ToString() + "/" + (j.vertex1 + 1).ToString() + " "
                                + (j.vertex2 + 1).ToString() + "/" + (j.vertex2 + 1).ToString() + " "
                                + (j.vertex3 + 1).ToString() + "/" + (j.vertex3 + 1).ToString());

                    OBJWriter.WriteLine();
                }

                OBJWriter.Close();
                WriteMaterialLib(bspFile.MaterialList.ToArray(), materialLibrary);
            }
        }

        private static void GetPlaneTriangleList(StreamWriter OBJWriter, PlaneSector_000A planeSection, ref List<Triangle> triangleList, ref int totalVertexIndices, bool isCollision)
        {
            if (planeSection.leftSection is AtomicSector_0009 a1)
            {
                GetAtomicTriangleList(OBJWriter, a1, ref triangleList, ref totalVertexIndices, isCollision);
            }
            else if (planeSection.leftSection is PlaneSector_000A p1)
            {
                GetPlaneTriangleList(OBJWriter, p1, ref triangleList, ref totalVertexIndices, isCollision);
            }

            if (planeSection.rightSection is AtomicSector_0009 a2)
            {
                GetAtomicTriangleList(OBJWriter, a2, ref triangleList, ref totalVertexIndices, isCollision);
            }
            else if (planeSection.rightSection is PlaneSector_000A p2)
            {
                GetPlaneTriangleList(OBJWriter, p2, ref triangleList, ref totalVertexIndices, isCollision);
            }
        }

        private static void GetAtomicTriangleList(StreamWriter OBJWriter, AtomicSector_0009 AtomicSector, ref List<Triangle> triangleList, ref int totalVertexIndices, bool isCollision)
        {
            if (AtomicSector.atomicStruct.isNativeData)
            {
                GetNativeTriangleList(OBJWriter, AtomicSector.atomicExtension, ref triangleList, ref totalVertexIndices);
                return;
            }

            //Write vertex list to obj
            if (AtomicSector.atomicStruct.vertexArray != null)
                foreach (Vertex3 i in AtomicSector.atomicStruct.vertexArray)
                    OBJWriter.WriteLine("v " + i.X.ToString() + " " + i.Y.ToString() + " " + i.Z.ToString());

            OBJWriter.WriteLine();

            //Write uv list to obj
            if (AtomicSector.atomicStruct.uvArray != null)
            {
                if (Program.levelEditor.checkBoxFlipUVs.Checked)
                    foreach (TextCoord i in AtomicSector.atomicStruct.uvArray)
                        OBJWriter.WriteLine("vt " + i.X.ToString() + " " + (-i.Y).ToString());
                else
                    foreach (TextCoord i in AtomicSector.atomicStruct.uvArray)
                        OBJWriter.WriteLine("vt " + i.X.ToString() + " " + i.Y.ToString());
            }
            OBJWriter.WriteLine();

            // Write vcolors to obj
            if (AtomicSector.atomicStruct.colorArray != null)
                foreach (RenderWareFile.Color i in AtomicSector.atomicStruct.colorArray)
                {
                    //if (i.R < 255)
                    //{
                    //    OBJWriter.WriteLine("vc " + ((byte)Math.Min(255, i.R * 2.5f)).ToString() + " " + ((byte)(i.G * 0.6f)).ToString() + " " + ((byte)(i.B * 0.3f)).ToString() + " " + i.A.ToString());
                    //}
                    //else
                    //{
                        OBJWriter.WriteLine("vc " + i.R.ToString() + " " + i.G.ToString() + " " + i.B.ToString() + " " + i.A.ToString());
                    //}
                }

            OBJWriter.WriteLine();

            if (AtomicSector.atomicStruct.triangleArray != null)
            {
                if (isCollision)
                {
                    RenderWareFile.Color[] collisionFlagList = new RenderWareFile.Color[0];
                    foreach (RWSection r in AtomicSector.atomicExtension.extensionSectionList)
                        if (r is UserDataPLG_011F userdata)
                            collisionFlagList = userdata.collisionFlags;

                    for (int i = 0; i < AtomicSector.atomicStruct.triangleArray.Length; i++)
                    {
                        triangleList.Add(new TriangleExt
                        {
                            collisionFlag = collisionFlagList[i],
                            MaterialIndex = AtomicSector.atomicStruct.triangleArray[i].materialIndex,
                            vertex1 = AtomicSector.atomicStruct.triangleArray[i].vertex1 + totalVertexIndices,
                            vertex2 = AtomicSector.atomicStruct.triangleArray[i].vertex2 + totalVertexIndices,
                            vertex3 = AtomicSector.atomicStruct.triangleArray[i].vertex3 + totalVertexIndices,
                        });
                    }
                }
                else
                {
                    foreach (RenderWareFile.Triangle i in AtomicSector.atomicStruct.triangleArray)
                    {
                        triangleList.Add(new Triangle
                        {
                            MaterialIndex = i.materialIndex,
                            vertex1 = i.vertex1 + totalVertexIndices,
                            vertex2 = i.vertex2 + totalVertexIndices,
                            vertex3 = i.vertex3 + totalVertexIndices,
                        });
                    }
                }
            }

            if (AtomicSector.atomicStruct.vertexArray != null)
                totalVertexIndices += AtomicSector.atomicStruct.vertexArray.Count();
        }

        private static void GetNativeTriangleList(StreamWriter OBJWriter, Extension_0003 extension, ref List<Triangle> triangleList, ref int totalVertexIndices)
        {
            NativeDataGC n = null;

            foreach (RWSection rw in extension.extensionSectionList)
            {
                if (rw is BinMeshPLG_050E binmesh)
                {
                    if (binmesh.numMeshes == 0) return;
                }
                if (rw is NativeDataPLG_0510 native)
                {
                    n = native.nativeDataStruct.nativeData;
                }
            }

            if (n == null) throw new Exception();

            List<Vertex3> vertexList_init = new List<Vertex3>();
            List<RenderWareFile.Color> colorList_init = new List<RenderWareFile.Color>();
            List<TextCoord> textCoordList_init = new List<TextCoord>();

            foreach (Declaration d in n.declarations)
            {
                foreach (object o in d.entryList)
                {
                    if (o is Vertex3 v)
                        vertexList_init.Add(v);
                    else if (o is RenderWareFile.Color c)
                        colorList_init.Add(c);
                    else if (o is TextCoord t)
                        textCoordList_init.Add(t);
                    else throw new Exception();
                }
            }

            foreach (TriangleDeclaration td in n.triangleDeclarations)
            {
                foreach (TriangleList tl in td.TriangleListList)
                {
                    List<Vertex3> vertexList_final = new List<Vertex3>();
                    List<RenderWareFile.Color> colorList_final = new List<RenderWareFile.Color>();
                    List<TextCoord> textCoordList_final = new List<TextCoord>();

                    foreach (int[] objectList in tl.entries)
                    {
                        for (int j = 0; j < objectList.Count(); j++)
                        {
                            if (n.declarations[j].declarationType == Declarations.Vertex)
                            {
                                vertexList_final.Add(vertexList_init[objectList[j]]);
                            }
                            else if (n.declarations[j].declarationType == Declarations.Color)
                            {
                                colorList_final.Add(colorList_init[objectList[j]]);
                            }
                            else if (n.declarations[j].declarationType == Declarations.TextCoord)
                            {
                                textCoordList_final.Add(textCoordList_init[objectList[j]]);
                            }
                        }
                    }

                    bool control = true;

                    for (int i = 2; i < vertexList_final.Count(); i++)
                    {
                        if (control)
                        {
                            triangleList.Add(new Triangle
                            {
                                MaterialIndex = td.MaterialIndex,
                                vertex1 = i - 2 + totalVertexIndices,
                                vertex2 = i - 1 + totalVertexIndices,
                                vertex3 = i + totalVertexIndices
                            });

                            control = false;
                        }
                        else
                        {
                            triangleList.Add(new Triangle
                            {
                                MaterialIndex = td.MaterialIndex,
                                vertex1 = i - 2 + totalVertexIndices,
                                vertex2 = i + totalVertexIndices,
                                vertex3 = i - 1 + totalVertexIndices
                            });

                            control = true;
                        }
                    }

                    //Write vertex list to obj
                    foreach (Vertex3 i in vertexList_final)
                        OBJWriter.WriteLine("v " + i.X.ToString() + " " + i.Y.ToString() + " " + i.Z.ToString());

                    OBJWriter.WriteLine();

                    //Write uv list to obj
                    if (textCoordList_final.Count() > 0)
                    {
                        if (Program.levelEditor.checkBoxFlipUVs.Checked)
                            foreach (TextCoord i in textCoordList_final)
                                OBJWriter.WriteLine("vt " + i.X.ToString() + " " + (-i.Y).ToString());
                        else
                            foreach (TextCoord i in textCoordList_final)
                                OBJWriter.WriteLine("vt " + i.X.ToString() + " " + i.Y.ToString());
                    }
                    OBJWriter.WriteLine();

                    // Write vcolors to obj
                    if (colorList_final.Count() > 0)
                        foreach (RenderWareFile.Color i in colorList_final)
                            OBJWriter.WriteLine("vc " + i.R.ToString() + " " + i.G.ToString() + " " + i.B.ToString() + " " + i.A.ToString());

                    OBJWriter.WriteLine();

                    totalVertexIndices += vertexList_final.Count();
                }
            }
        }

        private static void WriteMaterialLib(string[] MaterialStream, string materialLibrary)
        {
            StreamWriter MTLWriter = new StreamWriter(materialLibrary, false);
            MTLWriter.WriteLine("# Exported by Heroes Power Plant");

            for (int i = 0; i < MaterialStream.Length; i++)
            {
                MTLWriter.WriteLine("newmtl " + MaterialStream[i] + "_m");
                MTLWriter.WriteLine("Ka 0.2 0.2 0.2");
                MTLWriter.WriteLine("Kd 0.8 0.8 0.8");
                MTLWriter.WriteLine("Ks 0 0 0");
                MTLWriter.WriteLine("Ns 10");
                MTLWriter.WriteLine("d 1.0");
                MTLWriter.WriteLine("illum 4");
                MTLWriter.WriteLine("map_Kd " + MaterialStream[i] + ".png");
                MTLWriter.WriteLine();
            }

            MTLWriter.Close();
        }

        private static void WriteCollisionMaterialLib(List<Triangle> triangleList, string[] MaterialStream, string materialLibrary)
        {
            StreamWriter MTLWriter = new StreamWriter(materialLibrary, false);
            MTLWriter.WriteLine("# Exported by Heroes Power Plant");
            MTLWriter.WriteLine();

            Dictionary<string, string[]> mtlFileStrings = new Dictionary<string, string[]>();

            foreach (TriangleExt j in triangleList)
            {
                if (!mtlFileStrings.ContainsKey(j.collisionFlag.ToString()))
                {

                    RenderWareFile.Color color = RenderWareFile.Color.FromString(MaterialStream[j.MaterialIndex]);

                    mtlFileStrings.Add(j.collisionFlag.ToString(), new string[] {
                        "newmtl colmat_" + j.collisionFlag.ToString(),
                        $"Kd {color.R / 255f} {color.G / 255f} {color.B / 255f}"
                    });
                }
            }

            foreach (string[] ss in mtlFileStrings.Values)
            {
                MTLWriter.WriteLine(ss[0]);
                MTLWriter.WriteLine(ss[1]);
                MTLWriter.WriteLine("d 1.0");
                MTLWriter.WriteLine();
            }

            MTLWriter.Close();
        }
    }
}