﻿using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls.GH_Color
{
    public class GH_ColorPaletteDropdown : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_ColorPaletteDropodown class.
        /// </summary>
        public GH_ColorPaletteDropdown()
          : base("Ui Color Palette Dropdown", "Color Palette Dropdown",
              "Select a color from a dropdown palette of swatches",
              "Ui", "Control")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            base.RegisterInputParams(pManager);
            pManager.AddColourParameter("Color", "C", "The control's color.", GH_ParamAccess.item, Constants.MaterialColor());
            pManager[1].Optional = true;
            pManager.AddColourParameter("Palette", "P", "The control's optional color set.", GH_ParamAccess.list);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Color Palette Dropdown Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiColorPalettePicker control = new UiColorPalettePicker();
            if (DA.GetData(0, ref control)) Message = "Update";

            Sd.Color color = Constants.MaterialColor();
            DA.GetData(1, ref color);

            List<Sd.Color> palette = new List<Sd.Color>();
            bool hasPalette = DA.GetDataList(2, palette);

            control.Color = color;
            if (hasPalette) control.Palette = palette;
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
                return Properties.Resources.UiPlus_Elements_ColorPickerDrop_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("2c9557c2-4cef-4c27-9287-1740d8ca17c6"); }
        }
    }
}