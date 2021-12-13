using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UiPlus.Elements;

namespace UiPlus.Components.GH_Utilities
{
    public class GH_Print : GH_Component
    {
        public bool toggle { get; set; }

        /// <summary>
        /// Initializes a new instance of the GH_Print class.
        /// </summary>
        public GH_Print()
          : base("Ui Print", "Print",
              "Launch a pdf print dialog for a window",
              "Ui", "Window")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        public override void CreateAttributes()
        {
            m_attributes = new Attributes_Custom(this);
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "Elements", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiWindow window = new UiWindow();
            if (!DA.GetData(0, ref window)) return;

            if (toggle)
            {
                UiPrint PrintElement = new UiPrint(window.Container);
                toggle = false;
            }
        }



        public class Attributes_Custom : GH_ComponentAttributes
        {
            public Attributes_Custom(GH_Component owner) : base(owner) { }
            private Rectangle ButtonBounds { get; set; }

            protected override void Layout()
            {
                base.Layout();
                int len = 22;

                Rectangle rec0 = GH_Convert.ToRectangle(Bounds);
                rec0.Height += len;

                Rectangle rec1 = rec0;
                rec1.Y = rec1.Bottom - len;
                rec1.Height = len;
                rec1.Inflate(-2, -2);

                Bounds = rec0;
                ButtonBounds = rec1;
            }

            protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
            {
                base.Render(canvas, graphics, channel);
                GH_Print comp = Owner as GH_Print;

                if (channel == GH_CanvasChannel.Objects)
                {
                    GH_Capsule button = GH_Capsule.CreateTextCapsule(ButtonBounds, ButtonBounds, comp.toggle ? GH_Palette.Grey : GH_Palette.Black, "Print", 2, 0);
                    button.Render(graphics, Selected, Owner.Locked, false);
                    button.Dispose();
                }
            }

            public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
            {
                GH_Print comp = Owner as GH_Print;
                if (e.Button == MouseButtons.Left)
                {
                    RectangleF rec = ButtonBounds;
                    if (rec.Contains(e.CanvasLocation))
                    {
                        comp.RecordUndoEvent("Toggled True");
                        comp.toggle = true;

                        comp.ExpireSolution(true);
                        return GH_ObjectResponse.Handled;
                    }
                }
                return base.RespondToMouseDown(sender, e);
            }

            public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
            {
                GH_Print comp = Owner as GH_Print;
                if (e.Button == MouseButtons.Left)
                {
                    RectangleF rec = ButtonBounds;
                    if (rec.Contains(e.CanvasLocation))
                    {
                        comp.RecordUndoEvent("Toggled False");
                        comp.toggle = false;


                        comp.ExpireSolution(true);
                        return GH_ObjectResponse.Handled;
                    }
                }
                return base.RespondToMouseUp(sender, e);
            }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.UiPlus_Export_Print_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("35d13ec2-c2e0-48c5-bcb4-3c4715caa49a"); }
        }
    }
}