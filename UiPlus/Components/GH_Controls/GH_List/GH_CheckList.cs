using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls
{
    public class GH_CheckList : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_CheckList class.
        /// </summary>S
        public GH_CheckList()
          : base("Ui Check List", "Check List",
              "Description",
              "Ui", "Control")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.hidden; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            base.RegisterInputParams(pManager);
            pManager.AddTextParameter("Values", "V", "The control's values", GH_ParamAccess.list);
            pManager.AddBooleanParameter("States", "S", "Optional starting states for each value.", GH_ParamAccess.list);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Check List Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiCheckList control = new UiCheckList();
            if (DA.GetData(0, ref control)) Message = "Update";

            List<string> items = new List<string>();
            if (!DA.GetDataList(1, items)) return;

            List<bool> states = new List<bool>();
            bool hasStates = DA.GetDataList(2, states);

            control.Items = items;
            if (hasStates) {
                int countA = states.Count;
                int countB = items.Count;

                for (int i = countA; i < countB; i++)
                {
                    states.Add(states[countA - 1]);
                }

                control.States = states;
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
                return Properties.Resources.UiPlus_Elements_Checklist_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("4acb06e4-6a9e-4c9c-90c7-71bcbc793952"); }
        }
    }
}