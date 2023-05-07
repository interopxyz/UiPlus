using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;

namespace UiPlus.Components.GH_Modify
{
    public class GH_BackgroundImage : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_BackgroundImage class.
        /// </summary>
        public GH_BackgroundImage()
          : base("Ui Set Background Image", "Background",
              "Add a background image to an element",
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
            pManager.AddGenericParameter("Bitmap", "B", "A bitmap or image filepath to display.", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Fitting Mode", "M", "The Image fitting mode", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[2];
            foreach (UiElement.Fitting value in Enum.GetValues(typeof(UiElement.Fitting)))
            {
                paramA.AddNamedValue(value.ToString(), (int)value);
            }
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
            if (!DA.GetData(0, ref control)) return;

            int fitting = 0;
            DA.GetData(2, ref fitting);

            IGH_Goo goo = null;
            bool hasBitmap = DA.GetData(1, ref goo);
            Sd.Bitmap bitmap = null;
            string message = string.Empty;
            if (hasBitmap)
            {
                if (!goo.TryGetBitmap(out bitmap, out message))
                {
                    this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, message);
                    return;
                }
                control.SetBackgroundImage(bitmap, (UiElement.Fitting) fitting);
            }



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
                return Properties.Resources.UiPlus_Modify_Image_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("cb80794b-a809-4ac6-9e82-9324aed1fc86"); }
        }
    }
}