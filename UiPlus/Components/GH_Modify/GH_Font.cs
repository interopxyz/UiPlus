using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;
using Grasshopper.Kernel.Parameters;

namespace UiPlus.Components.GH_Modify
{
    public class GH_Font : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Font class.
        /// </summary>
        public GH_Font()
          : base("Ui Set Font", "Font",
              "Change the font of an element",
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
            pManager.AddTextParameter("Family", "F", "The font family", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Size", "S", "Text size", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Justification", "J", "The text justification", GH_ParamAccess.item);
            pManager[3].Optional = true;
            pManager.AddBooleanParameter("Bold", "B", "Specifies if the font is bold or regular", GH_ParamAccess.item);
            pManager[4].Optional = true;
            pManager.AddBooleanParameter("Italic", "I", "Specifies if the font is italic or regular", GH_ParamAccess.item);
            pManager[5].Optional = true;
            //pManager.AddBooleanParameter("Underline", "U", "Specifies if the font is underlined or regular", GH_ParamAccess.item, false);
            //pManager[6].Optional = true;



            Param_Integer paramA = (Param_Integer)pManager[3];
            foreach (UiElement.Justifications value in Enum.GetValues(typeof(UiElement.Justifications)))
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

            string family = "Arial";
            bool hasFamily = DA.GetData(1, ref family);

            double size = 8;
            bool hasSize = DA.GetData(2, ref size);

            int justify = 0;
            bool hasJustify = DA.GetData(3, ref justify);

            bool isBold = false;
            bool hasBold = DA.GetData(4, ref isBold);

            bool isItalic = false;
            bool hasItalic = DA.GetData(5, ref isItalic);

            if (hasFamily) control.FontFamily = family;
            if (hasSize) control.FontSize = size;
            if (hasJustify) control.TextJustification = (UiElement.Justifications)justify;
            if (hasBold) control.IsBold = isBold;
            if (hasItalic) control.IsItalic = isItalic;


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
                return Properties.Resources.UiPlus_Modify_Font_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("209d8b6c-2f03-46ad-b0ed-f71a4141ea37"); }
        }
    }
}