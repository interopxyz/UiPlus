using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;
using UiPlus.Elements;
using Grasshopper.Kernel.Parameters;

namespace UiPlus.Components.GH_Controls.GH_Static
{
    public class GH_Legend : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Legend class.
        /// </summary>
        public GH_Legend()
          : base("UI Legend", "Legend",
              "Description",
              "Ui", "Display")
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
            pManager.AddTextParameter("Values", "V", "The controls legend values", GH_ParamAccess.list);
            pManager.AddColourParameter("Colors", "C", "The controls legend colors", GH_ParamAccess.list);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Horizontal", "H", "Is legend orientation horizontal", GH_ParamAccess.item, false);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Icon", "I", "The legend icon", GH_ParamAccess.item, 1);
            pManager[3].Optional = true;
            //pManager.AddIntegerParameter("Size", "S", "The legend item size", GH_ParamAccess.item, 0);
            //pManager[4].Optional = true;

            Param_Integer param = (Param_Integer)pManager[3];
            foreach (UiLegend.IconModes value in Enum.GetValues(typeof(UiLegend.IconModes)))
            {
                param.AddNamedValue(value.ToString(), (int)value);
            }

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Ui Element | Legend Display", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<string> values = new List<string>();
            DA.GetDataList(0, values);

            List<Sd.Color> colors = new List<Sd.Color>();
            DA.GetDataList(1, colors);

            bool isHorizontal = false;
            DA.GetData(2, ref isHorizontal);

            int icon = 1;
            DA.GetData(3, ref icon);

            //int size = 0;
            //DA.GetData(4, ref size);

            if (colors.Count < 1)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    double t = (double)i / (double)(values.Count - 1);
                    if (t < 0.5) colors.Add(Sd.Color.FromArgb(233, 30, 99).Tween(Sd.Color.FromArgb(33, 150, 243), t*2.0));
                            if (t == 0.5) colors.Add(Sd.Color.FromArgb(33, 150, 243));
                    if (t > 0.5) colors.Add(Sd.Color.FromArgb(33, 150, 243).Tween(Sd.Color.FromArgb(255, 193, 7), (t-0.5)*2.0));
                }
            }
            else
            {
                if (values.Count != colors.Count)
                {
                    this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "If colors are provided the number of colors and values must match");
                    return;
                }
            }

            Dictionary<string, Sd.Color> entries = new Dictionary<string, Sd.Color>();
            for(int i = 0; i < values.Count; i++)
            {
                entries.Add(values[i], colors[i]);
            }

            UiLegend control = new UiLegend();
            control.Entries = entries;
            control.IconType = (UiLegend.IconModes)icon;
            control.IsHorizontal = isHorizontal;
            //control.Spacing = size;

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
                return Properties.Resources.UiPlus_Elements_Legend_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("928f2900-0c3f-4dc4-995c-2fdd9c54c38e"); }
        }
    }
}