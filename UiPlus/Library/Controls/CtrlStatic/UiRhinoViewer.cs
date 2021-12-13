using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rg = Rhino.Geometry;
using Rc = Rhino.UI.Controls;
using Eto.Forms;
using Eto.Drawing;

using Sw = System.Windows;
using Wm = System.Windows.Media;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;

using Swf = System.Windows.Forms;
using Swi = System.Windows.Forms.Integration;

namespace UiPlus.Elements
{
    public class UiRhinoViewer : UiElement
    {

        #region Members

        protected Rc.ViewportControl vport = new Rc.ViewportControl();

        public enum DisplayModes { Wireframe, Xray, Ghosted, Shaded, Rendered, AmbientOcclusion, Raytraced, Artistic, Pen, Tech};
        public enum ProjectionModes { Default,Perpsective,TwoPointPerspective,Parallel};

        protected DisplayModes displayMode = DisplayModes.Shaded;
        protected ProjectionModes projectionMode = ProjectionModes.Default;

        protected bool hasGrid = false;
        protected bool hasWorldAxes = false;
        protected bool hasConstructionAxes = false;

        protected string viewport = "Perspective";

        #endregion

        #region Constructors

        public UiRhinoViewer() : base()
        {
            SetInputs();
        }

        public UiRhinoViewer(UiRhinoViewer uiControl) : base(uiControl)
        {
            this.vport = uiControl.vport;

            this.displayMode = uiControl.displayMode;
            this.projectionMode = uiControl.projectionMode;

            this.hasGrid = uiControl.hasGrid;
            this.hasWorldAxes = uiControl.hasWorldAxes;
            this.hasConstructionAxes = uiControl.hasConstructionAxes;

            this.viewport = uiControl.viewport;
            this.border = uiControl.border;

            SetInputs();
        }

        #endregion

        #region Properties

        public virtual string Viewport
        {
            get { return viewport; }
            set 
            { 
                this.viewport = value;
                SetViewport();
            }
        }

        public virtual DisplayModes DisplayMode
        {
            get { return displayMode; }
            set
            {
                this.displayMode = value;
                SetDisplayMode();
            }
        }

        public virtual ProjectionModes ProjectionMode
        {
            get { return projectionMode; }
            set
            {
                this.projectionMode = value;
                SetProjectionMode();
            }
        }

        public virtual bool HasGrid
        {
            get { return hasGrid; }
            set
            {
                hasGrid = value;
                SetConstructionGrid();
            }
        }

        public virtual bool HasConstructionAxis
        {
            get { return hasConstructionAxes; }
            set
            {
                hasConstructionAxes = value;
                SetConstructionAxes();
            }
        }

        public virtual bool HasWorldAxis
        {
            get { return hasWorldAxes; }
            set
            {
                hasWorldAxes = value;
                SetWorldAxes();
            }
        }

        #endregion

        #region Methods

        public void RefreshView()
        {
            vport.Enabled = false;
            vport.Enabled = true;
        }

        public void SetViewport()
        {

            int index = Rhino.RhinoDoc.ActiveDoc.NamedViews.FindByName(viewport);
            Rhino.Display.RhinoView[] views = Rhino.RhinoDoc.ActiveDoc.Views.GetStandardRhinoViews();
            Rhino.DocObjects.ViewInfo viewInfo = Rhino.RhinoDoc.ActiveDoc.NamedViews[index];

            if (viewInfo != null)
            {
                vport.Viewport.PushViewInfo(viewInfo, true);
            }
            else
            {
                vport.Viewport.DisplayMode = Rhino.Display.DisplayModeDescription.GetDisplayMode(Rhino.Display.DisplayModeDescription.ShadedId);
                vport.Viewport.ChangeToPerspectiveProjection(true, 50);
                vport.Viewport.ZoomExtents();
            }

            RefreshView();
        }

        public void SetDisplayMode()
        {

            Guid displayModeId = Rhino.Display.DisplayModeDescription.WireframeId;

            switch (displayMode)
            {
                case DisplayModes.Wireframe:
                    displayModeId = Rhino.Display.DisplayModeDescription.WireframeId;
                    break;
                case DisplayModes.AmbientOcclusion:
                    displayModeId = Rhino.Display.DisplayModeDescription.AmbientOcclusionId;
                    break;
                case DisplayModes.Artistic:
                    displayModeId = Rhino.Display.DisplayModeDescription.ArtisticId;
                    break;
                case DisplayModes.Ghosted:
                    displayModeId = Rhino.Display.DisplayModeDescription.GhostedId;
                    break;
                case DisplayModes.Pen:
                    displayModeId = Rhino.Display.DisplayModeDescription.PenId;
                    break;
                case DisplayModes.Raytraced:
                    displayModeId = Rhino.Display.DisplayModeDescription.RaytracedId;
                    break;
                case DisplayModes.Rendered:
                    displayModeId = Rhino.Display.DisplayModeDescription.RenderedId;
                    break;
                case DisplayModes.Shaded:
                    displayModeId = Rhino.Display.DisplayModeDescription.ShadedId;
                    break;
                case DisplayModes.Tech:
                    displayModeId = Rhino.Display.DisplayModeDescription.TechId;
                    break;
                case DisplayModes.Xray:
                    displayModeId = Rhino.Display.DisplayModeDescription.XRayId;
                    break;
            }

            vport.Viewport.DisplayMode = Rhino.Display.DisplayModeDescription.GetDisplayMode(displayModeId);

            RefreshView();
        }

        public void SetProjectionMode()
        {

            switch (projectionMode)
            {
                case ProjectionModes.Parallel:
                    vport.Viewport.ChangeToParallelProjection(true);
                    break;
                case ProjectionModes.Perpsective:
                    vport.Viewport.ChangeToPerspectiveProjection(false, 50);
                    break;
                case ProjectionModes.TwoPointPerspective:
                    vport.Viewport.ChangeToTwoPointPerspectiveProjection(50);
                    break;
            }

            RefreshView();
        }

        public void SetConstructionAxes()
        {
            vport.Viewport.DisplayMode.DisplayAttributes.ViewSpecificAttributes.DrawGridAxes = hasConstructionAxes;

            RefreshView();
        }

        public void SetConstructionGrid()
        {
            vport.Viewport.DisplayMode.DisplayAttributes.ViewSpecificAttributes.DrawGrid = hasGrid;

            RefreshView();
        }

        public void SetWorldAxes()
        {
            vport.Viewport.DisplayMode.DisplayAttributes.ViewSpecificAttributes.DrawWorldAxes = hasWorldAxes;

            RefreshView();
        }

        public override List<object> GetValues()
        {
            return new List<object> { viewport };
        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {

            this.ElementType = ElementTypes.Border;

            this.AllowTransparency = false;

            SetViewport();
            SetDisplayMode();
            SetProjectionMode();

            Swi.WindowsFormsHost vHost = (Swi.WindowsFormsHost)vport.ControlObject;
            vHost.MinWidth = 300;
            vHost.MinHeight = 300;

            vHost.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            vHost.VerticalAlignment = Sw.VerticalAlignment.Stretch;

            Rhino.Commands.Command.EndCommand -= (o, e) => { RefreshView(); };
            Rhino.Commands.Command.EndCommand += (o, e) => { RefreshView(); };

            Grasshopper.Instances.ActiveCanvas.Document.SolutionEnd -= (o, e) => { RefreshView(); };
            Grasshopper.Instances.ActiveCanvas.Document.SolutionEnd += (o, e) => { RefreshView(); };


            this.border.Child = vHost;

            border.VerticalAlignment = Sw.VerticalAlignment.Stretch;

            SetConstructionAxes();
            SetConstructionGrid();
            SetWorldAxes();

            RefreshView();
            base.SetInputs(Alignment.Stretch);
        }

        public override string ToString()
        {
            return "Ui Rhino Viewer | " + this.Name;
        }

        #endregion

    }
}