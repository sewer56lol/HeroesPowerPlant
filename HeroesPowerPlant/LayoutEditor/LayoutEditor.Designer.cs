﻿namespace HeroesPowerPlant.LayoutEditor
{
    partial class LayoutEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heroesLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shadowLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byDistanceFromOriginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportINIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importINIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importLayoutFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importOBJToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.objectAmountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.listBoxObjects = new System.Windows.Forms.ListBox();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.ButtonRemove = new System.Windows.Forms.Button();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ButtonFindNextLink = new System.Windows.Forms.Button();
            this.ComboBoxObject = new System.Windows.Forms.ComboBox();
            this.LabelRend = new System.Windows.Forms.Label();
            this.NumericObjRend = new System.Windows.Forms.NumericUpDown();
            this.LabelLinkId = new System.Windows.Forms.Label();
            this.NumericObjLink = new System.Windows.Forms.NumericUpDown();
            this.LabelObject = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.NumericRotZ = new System.Windows.Forms.NumericUpDown();
            this.NumericRotY = new System.Windows.Forms.NumericUpDown();
            this.NumericRotX = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.NumericPosZ = new System.Windows.Forms.NumericUpDown();
            this.NumericPosY = new System.Windows.Forms.NumericUpDown();
            this.NumericPosX = new System.Windows.Forms.NumericUpDown();
            this.buttonViewHere = new System.Windows.Forms.Button();
            this.GroupBoxGameStuff = new System.Windows.Forms.GroupBox();
            this.ButtonTeleport = new System.Windows.Forms.Button();
            this.GroupBoxGetRot = new System.Windows.Forms.GroupBox();
            this.ButtonPowRot = new System.Windows.Forms.Button();
            this.ButtonSpeedRot = new System.Windows.Forms.Button();
            this.ButtonFlyRot = new System.Windows.Forms.Button();
            this.GroupBoxGetPos = new System.Windows.Forms.GroupBox();
            this.ButtonGetPow = new System.Windows.Forms.Button();
            this.ButtonGetFly = new System.Windows.Forms.Button();
            this.ButtonGetSpeed = new System.Windows.Forms.Button();
            this.RichTextBoxDescription = new System.Windows.Forms.RichTextBox();
            this.PropertyGridMisc = new System.Windows.Forms.PropertyGrid();
            this.buttonDrop = new System.Windows.Forms.Button();
            this.buttonForceReload = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericObjRend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericObjLink)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericRotZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericRotY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericRotX)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericPosX)).BeginInit();
            this.GroupBoxGameStuff.SuspendLayout();
            this.GroupBoxGetRot.SuspendLayout();
            this.GroupBoxGetPos.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(671, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.heroesLayoutToolStripMenuItem,
            this.shadowLayoutToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // heroesLayoutToolStripMenuItem
            // 
            this.heroesLayoutToolStripMenuItem.Name = "heroesLayoutToolStripMenuItem";
            this.heroesLayoutToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.heroesLayoutToolStripMenuItem.Text = "Heroes Layout";
            this.heroesLayoutToolStripMenuItem.Click += new System.EventHandler(this.heroesLayoutToolStripMenuItem_Click);
            // 
            // shadowLayoutToolStripMenuItem
            // 
            this.shadowLayoutToolStripMenuItem.Name = "shadowLayoutToolStripMenuItem";
            this.shadowLayoutToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.shadowLayoutToolStripMenuItem.Text = "Shadow Layout";
            this.shadowLayoutToolStripMenuItem.Click += new System.EventHandler(this.shadowLayoutToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportINIToolStripMenuItem,
            this.importINIToolStripMenuItem,
            this.importLayoutFileToolStripMenuItem,
            this.importOBJToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byIDToolStripMenuItem,
            this.byDistanceFromOriginToolStripMenuItem});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.sortToolStripMenuItem.Text = "Sort";
            // 
            // byIDToolStripMenuItem
            // 
            this.byIDToolStripMenuItem.Name = "byIDToolStripMenuItem";
            this.byIDToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.byIDToolStripMenuItem.Text = "by ID";
            this.byIDToolStripMenuItem.Click += new System.EventHandler(this.byIDToolStripMenuItem_Click);
            // 
            // byDistanceFromOriginToolStripMenuItem
            // 
            this.byDistanceFromOriginToolStripMenuItem.Name = "byDistanceFromOriginToolStripMenuItem";
            this.byDistanceFromOriginToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.byDistanceFromOriginToolStripMenuItem.Text = "by distance from origin";
            this.byDistanceFromOriginToolStripMenuItem.Click += new System.EventHandler(this.byDistanceFromOriginToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(167, 6);
            // 
            // exportINIToolStripMenuItem
            // 
            this.exportINIToolStripMenuItem.Name = "exportINIToolStripMenuItem";
            this.exportINIToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exportINIToolStripMenuItem.Text = "Export INI";
            this.exportINIToolStripMenuItem.Click += new System.EventHandler(this.exportINIToolStripMenuItem_Click);
            // 
            // importINIToolStripMenuItem
            // 
            this.importINIToolStripMenuItem.Name = "importINIToolStripMenuItem";
            this.importINIToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.importINIToolStripMenuItem.Text = "Import INI";
            this.importINIToolStripMenuItem.Click += new System.EventHandler(this.importINIToolStripMenuItem_Click);
            // 
            // importLayoutFileToolStripMenuItem
            // 
            this.importLayoutFileToolStripMenuItem.Name = "importLayoutFileToolStripMenuItem";
            this.importLayoutFileToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.importLayoutFileToolStripMenuItem.Text = "Import Layout File";
            this.importLayoutFileToolStripMenuItem.Click += new System.EventHandler(this.importLayoutFileToolStripMenuItem_Click);
            // 
            // importOBJToolStripMenuItem
            // 
            this.importOBJToolStripMenuItem.Name = "importOBJToolStripMenuItem";
            this.importOBJToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.importOBJToolStripMenuItem.Text = "Import OBJ";
            this.importOBJToolStripMenuItem.Click += new System.EventHandler(this.importOBJToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectAmountLabel,
            this.toolStripStatusLabel1,
            this.openFileLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(671, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // objectAmountLabel
            // 
            this.objectAmountLabel.Name = "objectAmountLabel";
            this.objectAmountLabel.Size = new System.Drawing.Size(54, 17);
            this.objectAmountLabel.Text = "0 objects";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // openFileLabel
            // 
            this.openFileLabel.Name = "openFileLabel";
            this.openFileLabel.Size = new System.Drawing.Size(81, 17);
            this.openFileLabel.Text = "No file loaded";
            // 
            // listBoxObjects
            // 
            this.listBoxObjects.FormattingEnabled = true;
            this.listBoxObjects.Location = new System.Drawing.Point(12, 27);
            this.listBoxObjects.Name = "listBoxObjects";
            this.listBoxObjects.Size = new System.Drawing.Size(182, 342);
            this.listBoxObjects.TabIndex = 2;
            this.listBoxObjects.SelectedIndexChanged += new System.EventHandler(this.listBoxObjects_SelectedIndexChanged);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(12, 404);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(88, 23);
            this.buttonCopy.TabIndex = 61;
            this.buttonCopy.Text = "Copy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(106, 404);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(88, 23);
            this.buttonClear.TabIndex = 64;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // ButtonRemove
            // 
            this.ButtonRemove.Location = new System.Drawing.Point(106, 375);
            this.ButtonRemove.Name = "ButtonRemove";
            this.ButtonRemove.Size = new System.Drawing.Size(88, 23);
            this.ButtonRemove.TabIndex = 63;
            this.ButtonRemove.Text = "Remove";
            this.ButtonRemove.UseVisualStyleBackColor = true;
            this.ButtonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(12, 375);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(88, 23);
            this.ButtonAdd.TabIndex = 62;
            this.ButtonAdd.Text = "Add";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // ButtonFindNextLink
            // 
            this.ButtonFindNextLink.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonFindNextLink.Location = new System.Drawing.Point(256, 169);
            this.ButtonFindNextLink.Name = "ButtonFindNextLink";
            this.ButtonFindNextLink.Size = new System.Drawing.Size(42, 18);
            this.ButtonFindNextLink.TabIndex = 73;
            this.ButtonFindNextLink.Text = "Find";
            this.ButtonFindNextLink.UseVisualStyleBackColor = true;
            this.ButtonFindNextLink.Click += new System.EventHandler(this.ButtonFindNextLink_Click);
            // 
            // ComboBoxObject
            // 
            this.ComboBoxObject.FormattingEnabled = true;
            this.ComboBoxObject.Location = new System.Drawing.Point(206, 42);
            this.ComboBoxObject.Name = "ComboBoxObject";
            this.ComboBoxObject.Size = new System.Drawing.Size(281, 21);
            this.ComboBoxObject.TabIndex = 70;
            this.ComboBoxObject.SelectedIndexChanged += new System.EventHandler(this.ComboBoxObject_SelectedIndexChanged);
            // 
            // LabelRend
            // 
            this.LabelRend.AutoSize = true;
            this.LabelRend.Location = new System.Drawing.Point(304, 172);
            this.LabelRend.Name = "LabelRend";
            this.LabelRend.Size = new System.Drawing.Size(90, 13);
            this.LabelRend.TabIndex = 69;
            this.LabelRend.Text = "Render Distance:";
            // 
            // NumericObjRend
            // 
            this.NumericObjRend.Location = new System.Drawing.Point(304, 188);
            this.NumericObjRend.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NumericObjRend.Name = "NumericObjRend";
            this.NumericObjRend.Size = new System.Drawing.Size(92, 20);
            this.NumericObjRend.TabIndex = 72;
            this.NumericObjRend.ValueChanged += new System.EventHandler(this.NumericObjRend_ValueChanged);
            // 
            // LabelLinkId
            // 
            this.LabelLinkId.AutoSize = true;
            this.LabelLinkId.Location = new System.Drawing.Point(206, 172);
            this.LabelLinkId.Name = "LabelLinkId";
            this.LabelLinkId.Size = new System.Drawing.Size(44, 13);
            this.LabelLinkId.TabIndex = 68;
            this.LabelLinkId.Text = "Link ID:";
            // 
            // NumericObjLink
            // 
            this.NumericObjLink.Location = new System.Drawing.Point(206, 188);
            this.NumericObjLink.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NumericObjLink.Name = "NumericObjLink";
            this.NumericObjLink.Size = new System.Drawing.Size(92, 20);
            this.NumericObjLink.TabIndex = 71;
            this.NumericObjLink.ValueChanged += new System.EventHandler(this.NumericObjLink_ValueChanged);
            // 
            // LabelObject
            // 
            this.LabelObject.AutoSize = true;
            this.LabelObject.Location = new System.Drawing.Point(206, 27);
            this.LabelObject.Name = "LabelObject";
            this.LabelObject.Size = new System.Drawing.Size(41, 13);
            this.LabelObject.TabIndex = 67;
            this.LabelObject.Text = "Object:";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.NumericRotZ);
            this.GroupBox2.Controls.Add(this.NumericRotY);
            this.GroupBox2.Controls.Add(this.NumericRotX);
            this.GroupBox2.Location = new System.Drawing.Point(200, 122);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(385, 47);
            this.GroupBox2.TabIndex = 66;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Rotation (X, Y, Z)";
            // 
            // NumericRotZ
            // 
            this.NumericRotZ.DecimalPlaces = 4;
            this.NumericRotZ.Location = new System.Drawing.Point(256, 19);
            this.NumericRotZ.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NumericRotZ.Minimum = new decimal(new int[] {
            65535,
            0,
            0,
            -2147483648});
            this.NumericRotZ.Name = "NumericRotZ";
            this.NumericRotZ.Size = new System.Drawing.Size(119, 20);
            this.NumericRotZ.TabIndex = 29;
            this.NumericRotZ.ValueChanged += new System.EventHandler(this.NumericRot_ValueChanged);
            // 
            // NumericRotY
            // 
            this.NumericRotY.DecimalPlaces = 4;
            this.NumericRotY.Location = new System.Drawing.Point(131, 19);
            this.NumericRotY.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NumericRotY.Minimum = new decimal(new int[] {
            65535,
            0,
            0,
            -2147483648});
            this.NumericRotY.Name = "NumericRotY";
            this.NumericRotY.Size = new System.Drawing.Size(119, 20);
            this.NumericRotY.TabIndex = 28;
            this.NumericRotY.ValueChanged += new System.EventHandler(this.NumericRot_ValueChanged);
            // 
            // NumericRotX
            // 
            this.NumericRotX.DecimalPlaces = 4;
            this.NumericRotX.Location = new System.Drawing.Point(6, 19);
            this.NumericRotX.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NumericRotX.Minimum = new decimal(new int[] {
            65535,
            0,
            0,
            -2147483648});
            this.NumericRotX.Name = "NumericRotX";
            this.NumericRotX.Size = new System.Drawing.Size(119, 20);
            this.NumericRotX.TabIndex = 27;
            this.NumericRotX.ValueChanged += new System.EventHandler(this.NumericRot_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.NumericPosZ);
            this.groupBox3.Controls.Add(this.NumericPosY);
            this.groupBox3.Controls.Add(this.NumericPosX);
            this.groupBox3.Location = new System.Drawing.Point(200, 69);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(385, 47);
            this.groupBox3.TabIndex = 65;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Position (X, Y, Z)";
            // 
            // NumericPosZ
            // 
            this.NumericPosZ.DecimalPlaces = 4;
            this.NumericPosZ.Location = new System.Drawing.Point(256, 19);
            this.NumericPosZ.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericPosZ.Name = "NumericPosZ";
            this.NumericPosZ.Size = new System.Drawing.Size(119, 20);
            this.NumericPosZ.TabIndex = 26;
            this.NumericPosZ.ValueChanged += new System.EventHandler(this.NumericPos_ValueChanged);
            // 
            // NumericPosY
            // 
            this.NumericPosY.DecimalPlaces = 4;
            this.NumericPosY.Location = new System.Drawing.Point(131, 19);
            this.NumericPosY.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericPosY.Name = "NumericPosY";
            this.NumericPosY.Size = new System.Drawing.Size(119, 20);
            this.NumericPosY.TabIndex = 25;
            this.NumericPosY.ValueChanged += new System.EventHandler(this.NumericPos_ValueChanged);
            // 
            // NumericPosX
            // 
            this.NumericPosX.DecimalPlaces = 4;
            this.NumericPosX.Location = new System.Drawing.Point(6, 19);
            this.NumericPosX.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericPosX.Name = "NumericPosX";
            this.NumericPosX.Size = new System.Drawing.Size(119, 20);
            this.NumericPosX.TabIndex = 24;
            this.NumericPosX.ValueChanged += new System.EventHandler(this.NumericPos_ValueChanged);
            // 
            // buttonViewHere
            // 
            this.buttonViewHere.Location = new System.Drawing.Point(493, 40);
            this.buttonViewHere.Name = "buttonViewHere";
            this.buttonViewHere.Size = new System.Drawing.Size(82, 23);
            this.buttonViewHere.TabIndex = 74;
            this.buttonViewHere.Text = "View Here";
            this.buttonViewHere.UseVisualStyleBackColor = true;
            this.buttonViewHere.Click += new System.EventHandler(this.buttonViewHere_Click);
            // 
            // GroupBoxGameStuff
            // 
            this.GroupBoxGameStuff.Controls.Add(this.buttonForceReload);
            this.GroupBoxGameStuff.Controls.Add(this.ButtonTeleport);
            this.GroupBoxGameStuff.Controls.Add(this.GroupBoxGetRot);
            this.GroupBoxGameStuff.Controls.Add(this.GroupBoxGetPos);
            this.GroupBoxGameStuff.Location = new System.Drawing.Point(485, 175);
            this.GroupBoxGameStuff.Name = "GroupBoxGameStuff";
            this.GroupBoxGameStuff.Size = new System.Drawing.Size(181, 196);
            this.GroupBoxGameStuff.TabIndex = 75;
            this.GroupBoxGameStuff.TabStop = false;
            this.GroupBoxGameStuff.Text = "Sonic Heroes Stuff";
            // 
            // ButtonTeleport
            // 
            this.ButtonTeleport.Location = new System.Drawing.Point(7, 140);
            this.ButtonTeleport.Name = "ButtonTeleport";
            this.ButtonTeleport.Size = new System.Drawing.Size(168, 23);
            this.ButtonTeleport.TabIndex = 3;
            this.ButtonTeleport.Text = "Teleport";
            this.ButtonTeleport.UseVisualStyleBackColor = true;
            this.ButtonTeleport.Click += new System.EventHandler(this.ButtonTeleport_Click);
            // 
            // GroupBoxGetRot
            // 
            this.GroupBoxGetRot.Controls.Add(this.ButtonPowRot);
            this.GroupBoxGetRot.Controls.Add(this.ButtonSpeedRot);
            this.GroupBoxGetRot.Controls.Add(this.ButtonFlyRot);
            this.GroupBoxGetRot.Location = new System.Drawing.Point(93, 19);
            this.GroupBoxGetRot.Name = "GroupBoxGetRot";
            this.GroupBoxGetRot.Size = new System.Drawing.Size(81, 115);
            this.GroupBoxGetRot.TabIndex = 5;
            this.GroupBoxGetRot.TabStop = false;
            this.GroupBoxGetRot.Text = "Get Rotation";
            // 
            // ButtonPowRot
            // 
            this.ButtonPowRot.Location = new System.Drawing.Point(6, 80);
            this.ButtonPowRot.Name = "ButtonPowRot";
            this.ButtonPowRot.Size = new System.Drawing.Size(69, 23);
            this.ButtonPowRot.TabIndex = 2;
            this.ButtonPowRot.Text = "Other2";
            this.ButtonPowRot.UseVisualStyleBackColor = true;
            this.ButtonPowRot.Click += new System.EventHandler(this.ButtonPowRot_Click);
            // 
            // ButtonSpeedRot
            // 
            this.ButtonSpeedRot.Location = new System.Drawing.Point(6, 22);
            this.ButtonSpeedRot.Name = "ButtonSpeedRot";
            this.ButtonSpeedRot.Size = new System.Drawing.Size(69, 23);
            this.ButtonSpeedRot.TabIndex = 0;
            this.ButtonSpeedRot.Text = "Leader";
            this.ButtonSpeedRot.UseVisualStyleBackColor = true;
            this.ButtonSpeedRot.Click += new System.EventHandler(this.ButtonSpeedRot_Click);
            // 
            // ButtonFlyRot
            // 
            this.ButtonFlyRot.Location = new System.Drawing.Point(6, 51);
            this.ButtonFlyRot.Name = "ButtonFlyRot";
            this.ButtonFlyRot.Size = new System.Drawing.Size(69, 23);
            this.ButtonFlyRot.TabIndex = 1;
            this.ButtonFlyRot.Text = "Other";
            this.ButtonFlyRot.UseVisualStyleBackColor = true;
            this.ButtonFlyRot.Click += new System.EventHandler(this.ButtonFlyRot_Click);
            // 
            // GroupBoxGetPos
            // 
            this.GroupBoxGetPos.Controls.Add(this.ButtonGetPow);
            this.GroupBoxGetPos.Controls.Add(this.ButtonGetFly);
            this.GroupBoxGetPos.Controls.Add(this.ButtonGetSpeed);
            this.GroupBoxGetPos.Location = new System.Drawing.Point(6, 19);
            this.GroupBoxGetPos.Name = "GroupBoxGetPos";
            this.GroupBoxGetPos.Size = new System.Drawing.Size(81, 115);
            this.GroupBoxGetPos.TabIndex = 4;
            this.GroupBoxGetPos.TabStop = false;
            this.GroupBoxGetPos.Text = "Get Position";
            // 
            // ButtonGetPow
            // 
            this.ButtonGetPow.Location = new System.Drawing.Point(6, 80);
            this.ButtonGetPow.Name = "ButtonGetPow";
            this.ButtonGetPow.Size = new System.Drawing.Size(69, 23);
            this.ButtonGetPow.TabIndex = 2;
            this.ButtonGetPow.Text = "Other2";
            this.ButtonGetPow.UseVisualStyleBackColor = true;
            this.ButtonGetPow.Click += new System.EventHandler(this.ButtonGetPow_Click);
            // 
            // ButtonGetFly
            // 
            this.ButtonGetFly.Location = new System.Drawing.Point(6, 51);
            this.ButtonGetFly.Name = "ButtonGetFly";
            this.ButtonGetFly.Size = new System.Drawing.Size(69, 23);
            this.ButtonGetFly.TabIndex = 1;
            this.ButtonGetFly.Text = "Other";
            this.ButtonGetFly.UseVisualStyleBackColor = true;
            this.ButtonGetFly.Click += new System.EventHandler(this.ButtonGetFly_Click);
            // 
            // ButtonGetSpeed
            // 
            this.ButtonGetSpeed.Location = new System.Drawing.Point(6, 22);
            this.ButtonGetSpeed.Name = "ButtonGetSpeed";
            this.ButtonGetSpeed.Size = new System.Drawing.Size(69, 23);
            this.ButtonGetSpeed.TabIndex = 0;
            this.ButtonGetSpeed.Text = "Leader";
            this.ButtonGetSpeed.UseVisualStyleBackColor = true;
            this.ButtonGetSpeed.Click += new System.EventHandler(this.ButtonGetSpeed_Click);
            // 
            // RichTextBoxDescription
            // 
            this.RichTextBoxDescription.BackColor = System.Drawing.SystemColors.Control;
            this.RichTextBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBoxDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.RichTextBoxDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichTextBoxDescription.Location = new System.Drawing.Point(485, 377);
            this.RichTextBoxDescription.Name = "RichTextBoxDescription";
            this.RichTextBoxDescription.ReadOnly = true;
            this.RichTextBoxDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.RichTextBoxDescription.Size = new System.Drawing.Size(181, 50);
            this.RichTextBoxDescription.TabIndex = 77;
            this.RichTextBoxDescription.Text = "";
            // 
            // PropertyGridMisc
            // 
            this.PropertyGridMisc.HelpVisible = false;
            this.PropertyGridMisc.LineColor = System.Drawing.SystemColors.ControlDark;
            this.PropertyGridMisc.Location = new System.Drawing.Point(200, 214);
            this.PropertyGridMisc.Name = "PropertyGridMisc";
            this.PropertyGridMisc.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.PropertyGridMisc.Size = new System.Drawing.Size(279, 213);
            this.PropertyGridMisc.TabIndex = 78;
            this.PropertyGridMisc.ToolbarVisible = false;
            // 
            // buttonDrop
            // 
            this.buttonDrop.Location = new System.Drawing.Point(591, 88);
            this.buttonDrop.Name = "buttonDrop";
            this.buttonDrop.Size = new System.Drawing.Size(69, 20);
            this.buttonDrop.TabIndex = 79;
            this.buttonDrop.Text = "Drop";
            this.buttonDrop.UseVisualStyleBackColor = true;
            this.buttonDrop.Click += new System.EventHandler(this.buttonDrop_Click);
            // 
            // buttonForceReload
            // 
            this.buttonForceReload.Enabled = false;
            this.buttonForceReload.Location = new System.Drawing.Point(7, 169);
            this.buttonForceReload.Name = "buttonForceReload";
            this.buttonForceReload.Size = new System.Drawing.Size(168, 23);
            this.buttonForceReload.TabIndex = 6;
            this.buttonForceReload.Text = "Force Reload";
            this.buttonForceReload.UseVisualStyleBackColor = true;
            this.buttonForceReload.Click += new System.EventHandler(this.buttonForceReload_Click);
            // 
            // LayoutEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 454);
            this.Controls.Add(this.buttonDrop);
            this.Controls.Add(this.PropertyGridMisc);
            this.Controls.Add(this.RichTextBoxDescription);
            this.Controls.Add(this.GroupBoxGameStuff);
            this.Controls.Add(this.buttonViewHere);
            this.Controls.Add(this.ButtonFindNextLink);
            this.Controls.Add(this.ComboBoxObject);
            this.Controls.Add(this.LabelRend);
            this.Controls.Add(this.NumericObjRend);
            this.Controls.Add(this.LabelLinkId);
            this.Controls.Add(this.NumericObjLink);
            this.Controls.Add(this.LabelObject);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.ButtonRemove);
            this.Controls.Add(this.ButtonAdd);
            this.Controls.Add(this.listBoxObjects);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "LayoutEditor";
            this.ShowIcon = false;
            this.Text = "Layout Editor";
            this.TopMost = true;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericObjRend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericObjLink)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericRotZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericRotY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericRotX)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericPosX)).EndInit();
            this.GroupBoxGameStuff.ResumeLayout(false);
            this.GroupBoxGetRot.ResumeLayout(false);
            this.GroupBoxGetPos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heroesLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shadowLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exportINIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importINIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importLayoutFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importOBJToolStripMenuItem;
        private System.Windows.Forms.ListBox listBoxObjects;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonClear;
        internal System.Windows.Forms.Button ButtonRemove;
        internal System.Windows.Forms.Button ButtonAdd;
        internal System.Windows.Forms.Button ButtonFindNextLink;
        public System.Windows.Forms.ComboBox ComboBoxObject;
        internal System.Windows.Forms.Label LabelRend;
        internal System.Windows.Forms.NumericUpDown NumericObjRend;
        internal System.Windows.Forms.Label LabelLinkId;
        internal System.Windows.Forms.NumericUpDown NumericObjLink;
        internal System.Windows.Forms.Label LabelObject;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.NumericUpDown NumericRotZ;
        internal System.Windows.Forms.NumericUpDown NumericRotY;
        internal System.Windows.Forms.NumericUpDown NumericRotX;
        internal System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.NumericUpDown NumericPosZ;
        internal System.Windows.Forms.NumericUpDown NumericPosY;
        internal System.Windows.Forms.NumericUpDown NumericPosX;
        private System.Windows.Forms.Button buttonViewHere;
        internal System.Windows.Forms.GroupBox GroupBoxGameStuff;
        internal System.Windows.Forms.Button ButtonTeleport;
        internal System.Windows.Forms.GroupBox GroupBoxGetRot;
        internal System.Windows.Forms.Button ButtonPowRot;
        internal System.Windows.Forms.Button ButtonFlyRot;
        internal System.Windows.Forms.Button ButtonSpeedRot;
        internal System.Windows.Forms.GroupBox GroupBoxGetPos;
        internal System.Windows.Forms.Button ButtonGetPow;
        internal System.Windows.Forms.Button ButtonGetFly;
        internal System.Windows.Forms.Button ButtonGetSpeed;
        internal System.Windows.Forms.RichTextBox RichTextBoxDescription;
        private System.Windows.Forms.ToolStripStatusLabel objectAmountLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel openFileLabel;
        internal System.Windows.Forms.PropertyGrid PropertyGridMisc;
        private System.Windows.Forms.ToolStripMenuItem byIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byDistanceFromOriginToolStripMenuItem;
        private System.Windows.Forms.Button buttonDrop;
        internal System.Windows.Forms.Button buttonForceReload;
    }
}