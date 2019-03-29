namespace MGraphs
{
    partial class Form1
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
            Microsoft.Msagl.Core.Geometry.Curves.PlaneTransformation planeTransformation1 = new Microsoft.Msagl.Core.Geometry.Curves.PlaneTransformation();
            this.viewerB = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            this.SuspendLayout();
            // 
            // viewerB
            // 
            this.viewerB.ArrowheadLength = 10D;
            this.viewerB.AsyncLayout = false;
            this.viewerB.AutoScroll = true;
            this.viewerB.BackwardEnabled = false;
            this.viewerB.BuildHitTree = true;
            this.viewerB.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.UseSettingsOfTheGraph;
            this.viewerB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerB.EdgeInsertButtonVisible = true;
            this.viewerB.FileName = "";
            this.viewerB.ForwardEnabled = false;
            this.viewerB.Graph = null;
            this.viewerB.InsertingEdge = false;
            this.viewerB.LayoutAlgorithmSettingsButtonVisible = true;
            this.viewerB.LayoutEditingEnabled = true;
            this.viewerB.Location = new System.Drawing.Point(0, 0);
            this.viewerB.LooseOffsetForRouting = 0.25D;
            this.viewerB.MouseHitDistance = 0.05D;
            this.viewerB.Name = "viewerB";
            this.viewerB.NavigationVisible = true;
            this.viewerB.NeedToCalculateLayout = true;
            this.viewerB.OffsetForRelaxingInRouting = 0.6D;
            this.viewerB.PaddingForEdgeRouting = 8D;
            this.viewerB.PanButtonPressed = false;
            this.viewerB.SaveAsImageEnabled = true;
            this.viewerB.SaveAsMsaglEnabled = true;
            this.viewerB.SaveButtonVisible = true;
            this.viewerB.SaveGraphButtonVisible = true;
            this.viewerB.SaveInVectorFormatEnabled = true;
            this.viewerB.Size = new System.Drawing.Size(809, 450);
            this.viewerB.TabIndex = 3;
            this.viewerB.TightOffsetForRouting = 0.125D;
            this.viewerB.ToolBarIsVisible = false;
            this.viewerB.Transform = planeTransformation1;
            this.viewerB.UndoRedoButtonsVisible = true;
            this.viewerB.WindowZoomButtonPressed = false;
            this.viewerB.ZoomF = 1D;
            this.viewerB.ZoomWindowThreshold = 0.05D;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 450);
            this.Controls.Add(this.viewerB);
            this.Name = "Form1";
            this.Text = "TAiO";
            this.ResumeLayout(false);

        }

        #endregion

        public Microsoft.Msagl.GraphViewerGdi.GViewer viewerB;
    }
}

