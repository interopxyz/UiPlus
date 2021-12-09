using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls.GH_Commands
{
    public class GH_FileSave : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_FileSave class.
        /// </summary>
        public GH_FileSave()
          : base("UI Save Button", "Save Btn",
              "Description",
              "Ui", "Command")
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
            base.RegisterInputParams(pManager);
            pManager.AddTextParameter("Label", "L", "The control label.", GH_ParamAccess.item, "Save");
            pManager[1].Optional = true;
            pManager.AddTextParameter("Extensions", "E", "A list of standard file extension formats (Name | Extension)", GH_ParamAccess.list);
            pManager[2].Optional = true;
            pManager.AddTextParameter("Folder Path", "P", "A default folder path.", GH_ParamAccess.item);
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Save File Button";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiButtonSave control = new UiButtonSave();
            if (DA.GetData(0, ref control)) Message = "Update";

            string label = "Save";
            bool hasLabel = DA.GetData(1, ref label);

            List<string> extensions = new List<string>();
            bool hasCommands = DA.GetDataList(2, extensions);

            string filePath = "";
            bool hasPath = DA.GetData(3, ref filePath);


            if (hasLabel) control.Label = label;
            if (hasCommands)
            {
                Dictionary<string, string> extensionSets = new Dictionary<string, string>();
                foreach (string extension in extensions)
                {
                    if (extension != null)
                    {
                        string[] names = extension.Split('|');
                        if (names.Count() > 1)
                        {
                            string name = names[0].Trim();
                            string val = names[1].Trim();
                            if (!extensionSets.ContainsKey(name)) extensionSets.Add(name, val);
                        }
                    }
                }
                control.Extensions = extensionSets;
            }

            if (hasPath) control.FolderPath = filePath;

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
                return Properties.Resources.UiPlus_Elements_CommandSave_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("6474b518-0f56-4791-a96a-9626254c310e"); }
        }
    }
}