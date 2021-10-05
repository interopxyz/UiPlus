using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using UiPlus.Elements;

using Mc = MahApps.Metro.Controls;
using Wpf = System.Windows.Controls;

namespace UiPlus.Components.GH_Utilities
{
    public class GH_ElementValues : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_ElementValues class.
        /// </summary>
        public GH_ElementValues()
          : base("UI Values", "Get Values",
              "Description",
              "Ui", "Elements")
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
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiElement uiElement = null;
            if (!DA.GetData(0, ref uiElement)) return;

            List<object> values = new List<object>();

            switch (uiElement.GetElementType())
            {
                case ("Button"):
                    Wpf.Button C01 = (Wpf.Button)uiElement.Control;
                    C01.MouseDown -= (o, e) => { ExpireSolution(true); };
                    C01.MouseDown += (o, e) => { ExpireSolution(true); };

                    C01.MouseUp -= (o, e) => { ExpireSolution(true); };
                    C01.MouseUp += (o, e) => { ExpireSolution(true); };
                    break;
                case ("ToggleSwitch"):
                    Mc.ToggleSwitch C02 = (Mc.ToggleSwitch)uiElement.Control;
                    C02.Toggled -= (o, e) => { ExpireSolution(true); };
                    C02.Toggled += (o, e) => { ExpireSolution(true); };
                    break;
            }

            DA.SetDataList(0, uiElement.GetValues());
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
                return null;
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