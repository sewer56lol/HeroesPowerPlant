﻿using System;
using System.Windows.Forms;

namespace HeroesPowerPlant.CollisionEditor
{
    public partial class CollisionEditor : Form
    {
        public CollisionEditor()
        {
            InitializeComponent();
            collisionSystem = new CollisionSystem();
        }

        private void CollisionEditor_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        CollisionSystem collisionSystem;

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenOBJFile = new OpenFileDialog
            {
                Filter = "OBJ Files|*.obj|All Files|*.*"
            };
            SaveFileDialog SaveCLFile = new SaveFileDialog
            {
                Filter = "CL Files|*.cl"
            };

            if (OpenOBJFile.ShowDialog() == DialogResult.OK)
                if (SaveCLFile.ShowDialog() == DialogResult.OK)
                {
                    collisionSystem.NewFile(OpenOBJFile.FileName, SaveCLFile.FileName, GetDepthLevel());
                    initFile();
                }
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenCLFile = new OpenFileDialog()
            {
                Filter = "CL Files|*.cl"
            };
            if (OpenCLFile.ShowDialog() == DialogResult.OK)
            {
                Open(OpenCLFile.FileName);
            }
        }

        private void exportOBJToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveOBJFile = new SaveFileDialog
            {
                Filter = "OBJ Files|*.obj|All Files|*.*"
            };
            if (SaveOBJFile.ShowDialog() == DialogResult.OK)
            {
                collisionSystem.ConvertCLtoOBJ(SaveOBJFile.FileName);
                initFile();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            collisionSystem.Close();
            labelFileLoaded.Text = "No CL loaded";
            labelVertexNum.Text = "Vertices: ";
            labelTriangles.Text = "Triangles: ";
            labelQuadnodes.Text = "QuadNodes: ";

            exportOBJToolStripMenuItem.Enabled = false;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (collisionSystem.HasOpenedFile())
            {
                OpenFileDialog OpenOBJFile = new OpenFileDialog();
                OpenOBJFile.Filter = "OBJ Files|*.obj|All Files|*.*";
                if (OpenOBJFile.ShowDialog() == DialogResult.OK)
                {
                    collisionSystem.Import(OpenOBJFile.FileName, GetDepthLevel());
                    initFile();
                }
            }
            else
                newToolStripMenuItem_Click(sender, e);
        }

        public void Open(string fileName)
        {
            collisionSystem.Open(fileName);
            initFile();
        }

        public string GetFileName()
        {
            return collisionSystem.CurrentCLfileName;
        }

        private byte GetDepthLevel()
        {
            return checkBox1.Checked ? (byte)0 : (byte)numericDepthLevel.Value;
        }

        public void initFile()
        {
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Step = 1;

            collisionSystem.LoadCLFile();

            labelFileLoaded.Text = "Loaded " + collisionSystem.CurrentCLfileName;
            labelVertexNum.Text = "Vertices: " + collisionSystem.NumVertices.ToString();
            labelTriangles.Text = "Triangles: " + collisionSystem.NumTriangles.ToString();
            labelQuadnodes.Text = "QuadNodes: " + collisionSystem.NumQuadNodes.ToString();
            if (collisionSystem.DepthLevel != 0)
                numericDepthLevel.Value = collisionSystem.DepthLevel;

            exportOBJToolStripMenuItem.Enabled = true;
            
            progressBar1.Value = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                numericDepthLevel.Enabled = false;
            else
                numericDepthLevel.Enabled = true;
        }

        private void buttonNote_Click(object sender, EventArgs e)
        {
            MessageBox.Show("It's recommended to set the depth level to Auto (so it will be as big as possible) and the power flag to D. However, custom collision files might make the game crash in random spots, specially if your depth level ends up at 8 or 9. If that's happening, set the depth level to 5.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DetermineRenderStuff2();
        }
    }
}