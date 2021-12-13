using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Modify
{
    public class GH_Graphics : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Graphics class.
        /// </summary>
        public GH_Graphics()
          : base("Ui Set Graphics", "Graphics",
              "Change the basic graphics of an element",
              "Ui", "Modify")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "The element to update.", GH_ParamAccess.item);
            pManager.AddColourParameter("Primary Color", "P", "The primary color", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddColourParameter("Accent Color", "A", "The accent color", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddColourParameter("Stroke Color", "S", "The stroke color", GH_ParamAccess.item);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Stroke Weight", "W", "The stroke weight", GH_ParamAccess.item);
            pManager[4].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "The updated element.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiElement control = new UiElement();
            if(!DA.GetData(0, ref control)) return;

            Sd.Color primary = Constants.DarkGray();
            bool hasPrimary = DA.GetData(1, ref primary);

            Sd.Color accent = Constants.OffWhite();
            bool hasAccent = DA.GetData(2, ref accent);

            Sd.Color strokeColor = Constants.OffWhite();
            bool hasStroke = DA.GetData(3, ref strokeColor);

            double strokeWeight = 1;
            bool hasWeight = DA.GetData(4, ref strokeWeight);

            if (hasPrimary) control.SetPrimaryColors(primary);
            if (hasAccent) control.SetAccentColors(accent);
            if (hasStroke) control.SetStrokeColor(strokeColor);
            if (hasWeight) control.SetStrokeWidth(strokeWeight);


            DA.SetData(0, control);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Properties.Resources.UiPlus_Modify_Graphics_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("cab0a251-95f7-4d2a-bce6-315a0298548b"); }
        }
    }
}