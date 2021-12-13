using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace UiPlus.Components
{
    public abstract class GH_EditBase : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_EditBase class.
        /// </summary>
        public GH_EditBase()
          : base("GH_EditBase", "Nickname",
              "Description",
              "Category", "Subcategory")
        {
        }

        public GH_EditBase(string Name, string NickName, string Description, string Category, string Subcategory) : base(Name, NickName, Description, Category, Subcategory)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "*E", "An optional input for an Element. If an Element is provided its properties will be updates, if empty a new Element will be created." + Environment.NewLine+"NOTE: The input control must be of the same type as the component", GH_ParamAccess.item);
            pManager[0].Optional = true;
        }

        protected override void BeforeSolveInstance()
        {
            base.BeforeSolveInstance();
            Message = "New";
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "Ui Element | ", GH_ParamAccess.item);
            //pManager.AddGenericParameter("Values", "V", "The controls returned values", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
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
            get { return new Guid("4b59dd2b-651c-4bd7-98d3-71bb975fe1b5"); }
        }
    }
}