using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;
using UiPlus.Elements;
using Grasshopper.Kernel.Parameters;

namespace UiPlus.Components.GH_Controls.GH_Static
{
    public class GH_Gradient : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Gradient class.
        /// </summary>
        public GH_Gradient()
          : base("UI Gradient", "Gradient",
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
            pManager.AddColourParameter("Colors", "C", "The controls gradient colors", GH_ParamAccess.list);
            pManager.AddNumberParameter("Parameters", "P", "The controls gradient values", GH_ParamAccess.list);
            pManager[1].Optional = true;
            pManager.AddTextParameter("Low Value", "L", "The min value label for the gradient", GH_ParamAccess.item, "Low");
            pManager[2].Optional = true;
            pManager.AddTextParameter("High Value", "H", "The max value label for the gradient", GH_ParamAccess.item, "High");
            pManager[3].Optional = true;
            pManager.AddBooleanParameter("Horizontal Alignment", "A", "Is legend orientation horizontal", GH_ParamAccess.item, false);
            pManager[4].Optional = true;
            pManager.AddColourParameter("Text Color", "T", "The control's text color", GH_ParamAccess.item, Sd.Color.Black);
            pManager[5].Optional = true;
            //pManager.AddIntegerParameter("Location", "L", "The legend icon", GH_ParamAccess.item, 0);
            //pManager[4].Optional = true;

            //Param_Integer param = (Param_Integer)pManager[4];
            //foreach (UiGradient.TextAlignments value in Enum.GetValues(typeof(UiGradient.TextAlignments)))
            //{
            //    param.AddNamedValue(value.ToString(), (int)value);
            //}

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Ui Element | Gradient Display", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            List<Sd.Color> colors = new List<Sd.Color>();
            DA.GetDataList(0, colors);

            List<double> parameters = new List<double>();
            DA.GetDataList(1, parameters);

            string min = "Low";
            DA.GetData(2, ref min);

            string max = "High";
            DA.GetData(3, ref max);

            bool isHorizontal = false;
            DA.GetData(4, ref isHorizontal);

            Sd.Color textColor = Sd.Color.Black;
            DA.GetData(5, ref textColor);

            if (colors.Count < 1)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    double t = (double)i / (double)(parameters.Count - 1);
                    if (t < 0.5) colors.Add(Sd.Color.FromArgb(233, 30, 99).Tween(Sd.Color.FromArgb(33, 150, 243), t * 2.0));
                    if (t == 0.5) colors.Add(Sd.Color.FromArgb(33, 150, 243));
                    if (t > 0.5) colors.Add(Sd.Color.FromArgb(33, 150, 243).Tween(Sd.Color.FromArgb(255, 193, 7), (t - 0.5) * 2.0));
                }
            }
            else
            {
                if (parameters.Count != colors.Count)
                {
                    this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "If colors are provided the number of colors and values must match");
                    return;
                }
            }

            Dictionary<double, Sd.Color> stops = new Dictionary<double, Sd.Color>();
            for (int i = 0; i < parameters.Count; i++)
            {
                stops.Add(parameters[i], colors[i]);
            }

            UiGradient control = new UiGradient();
            control.Stops = stops;
            control.MinValue = min;
            control.MaxValue = max;
            control.IsHorizontal = isHorizontal;
            control.TextColor = textColor;

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
                return Properties.Resources.UiPlus_Elements_Gradients_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e19cb2d1-88eb-4d7d-8569-b2ebdb459f66"); }
        }
    }
}