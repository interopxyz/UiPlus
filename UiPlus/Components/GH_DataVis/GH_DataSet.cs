using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

namespace UiPlus.Components.GH_DataVis
{
    public class GH_DataSet : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_DataSet class.
        /// </summary>
        public GH_DataSet()
          : base("UI Data Series", "Data Series",
              "Compile a list of data, similar to a column in a spreadsheet as a single item.",
              "Ui", "Chart")
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
            pManager.AddTextParameter("Label", "L", "The collection label. (Try to make this value unique)", GH_ParamAccess.item);
            pManager.AddGenericParameter("Data", "D", "The list of data associated with each element", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Type", "T", "The datatype of the data", GH_ParamAccess.item,0);
            pManager[2].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[2];
            foreach (UiDataSet.DataTypes value in Enum.GetValues(typeof(UiDataSet.DataTypes)))
            {
                paramA.AddNamedValue(value.ToString(), (int)value);
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Data Series", "Ds", "A compiled data series object.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = string.Empty;
            if(!DA.GetData(0,ref name)) return;

            List<IGH_Goo> data = new List<IGH_Goo>();
            DA.GetDataList(1, data);

            int type = 0;
            DA.GetData(2,ref type);
            UiDataSet.DataTypes DataType = (UiDataSet.DataTypes)type;


            UiDataSet uiDataSet = new UiDataSet();

            switch (DataType)
            {
                case UiDataSet.DataTypes.Text:
                    List<string> text = new List<string>();
                    foreach (IGH_Goo obj in data)
                    {
                        string txt = string.Empty;
                        int txtInt = 0;
                        double txtDbl = 0;
                        Point3d txtPnt = Point3d.Unset;
                        Interval txtDom = Interval.Unset;
                        if (obj.CastTo<string>(out txt)) { }
                        else if (obj.CastTo<int>(out txtInt)) { txt = Convert.ToString(txtInt); }
                        else if (obj.CastTo<double>(out txtDbl)) { txt = Convert.ToString(txtDbl); }
                        else if (obj.CastTo<Point3d>(out txtPnt)) { txt = txtPnt.ToString(); }
                        else if (obj.CastTo<Interval>(out txtDom)) { txt = txtDom.ToString(); }

                        text.Add(txt);
                    }
                    uiDataSet = new UiDataSet(text, name);
                    break;
                case UiDataSet.DataTypes.Number:
                    List<double> numbers = new List<double>();
                    foreach (IGH_Goo obj in data)
                    {
                        double dbl = double.NaN;
                        int intDbl = 0;
                        if (obj.CastTo<double>(out dbl)) numbers.Add(dbl);
                        if (obj.CastTo<int>(out intDbl)) numbers.Add((double)intDbl);
                    }
                    uiDataSet = new UiDataSet(numbers, name);
                    break;
                case UiDataSet.DataTypes.Integer:
                    List<int> integers = new List<int>();
                    foreach (IGH_Goo obj in data)
                    {
                        int integer = 0;
                        if (obj.CastTo<int>(out integer)) integers.Add(integer);
                    }
                    uiDataSet = new UiDataSet(integers, name);
                    break;
                case UiDataSet.DataTypes.Point:
                    List<Point3d> points = new List<Point3d>();
                    foreach (IGH_Goo obj in data)
                    {
                        Point3d pt = Point3d.Unset;
                        Vector3d vec = Vector3d.Unset;
                        if (obj.CastTo<Point3d>(out pt)) 
                        { 
                            points.Add(pt); 
                        }
                        else
                        {
                        if (obj.CastTo<Vector3d>(out vec)) points.Add(new Point3d(vec.X,vec.Y,vec.Z));
                        }
                    }
                    uiDataSet = new UiDataSet(points, name);
                    break;
                case UiDataSet.DataTypes.Domain:
                    List<Interval> domains = new List<Interval>();
                    foreach (IGH_Goo obj in data)
                    {
                        Interval domain = Interval.Unset;
                        if (obj.CastTo<Interval>(out domain)) domains.Add(domain);
                    }
                    uiDataSet = new UiDataSet(domains, name);
                    break;
            }


            DA.SetData(0, uiDataSet);
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
                return Properties.Resources.UiPlus_Data_DataSet_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("bd70cee1-6f7c-4a20-b86e-f62853fe0d2e"); }
        }
    }
}