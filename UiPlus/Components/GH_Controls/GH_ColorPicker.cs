using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls
{
    public class GH_ColorPicker : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_ColorPicker class.
        /// </summary>
        public GH_ColorPicker()
          : base("UI Color Picker", "Color Picker",
              "Description",
              "Ui", "Elements")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddColourParameter("Color", "C", "The control's color.", GH_ParamAccess.item, Sd.Color.Teal);
            pManager[0].Optional = true;
            pManager.AddColourParameter("Palette", "P", "The control's optional color set.", GH_ParamAccess.list);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Color Picker", "C", "Ui Color Picker", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Sd.Color color = Sd.Color.Teal;
            DA.GetData(0, ref color);

            List<Sd.Color> palette = new List<Sd.Color>();
            bool hasPalette = DA.GetDataList(1, palette);

            UiColorPicker control = new UiColorPicker();
            control.Color = color;
            if(hasPalette)control.Palette = palette;

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
                return Properties.Resources.UiPlus_Elements_ColorPicker_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("3e6d5bea-dbb7-45b7-84ae-62d0e251f459"); }
        }
    }
}