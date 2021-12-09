using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using UiPlus.Elements;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;

namespace UiPlus.Components.GH_Utilities
{
    public class GH_ElementValues : GH_Component
    {
        List<Wpf.Control> controls = new List<Wpf.Control>();
        List<Wpf.Control> tempcontrols = new List<Wpf.Control>();

        /// <summary>
        /// Initializes a new instance of the GH_ElementValues class.
        /// </summary>
        public GH_ElementValues()
          : base("UI Values", "Get Values",
              "Description",
              "Ui", "Get")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.septenary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "A Ui Control Element", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Values", "V", "Control values", GH_ParamAccess.list);
            pManager.AddTextParameter("Debug", "D", "Description", GH_ParamAccess.item);
        }

        protected override void BeforeSolveInstance()
        {
            base.BeforeSolveInstance();
            tempcontrols = new List<Wpf.Control>();
        }

        protected override void AfterSolveInstance()
        {
            base.AfterSolveInstance();
            controls = tempcontrols;
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiElement uiElement = null;
            if (!DA.GetData(0, ref uiElement)) return;

                    tempcontrols.Add(uiElement.Control);

            switch (uiElement.ElementType)
            {
                case UiElement.ElementTypes.Control:
                case UiElement.ElementTypes.Border:
                    if (!controls.Contains(uiElement.Control))
                    {
                        controls.Add(uiElement.Control);
                        uiElement.Update(this);
                    }
                    break;
            }

            DA.SetDataList(0, uiElement.GetValues());
            DA.SetData(1, uiElement.Control);
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
                return Properties.Resources.UiPlus_Utility_Listen_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("023a26ba-3e69-45ea-bee5-44cd867a6004"); }
        }
    }
}