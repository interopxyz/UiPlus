using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UiPlus.Elements;

namespace UiPlus.Components.GH_Utilities
{
    public class GH_Capture : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Capture class.
        /// </summary>
        public GH_Capture()
          : base("Ui Capture Image", "Capture",
              "Create a bitmap of the current state of a window with an option to save a file",
              "Ui", "Window")
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
            pManager.AddGenericParameter("Window", "W", "A Ui Window element.", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager.AddTextParameter("Folder Path", "F", "The folder path to save the file", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddTextParameter("File Name", "N", "The file name for the image", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Resolution", "R", "The PPI (Pixels Per Inch) resolution for the image which must be greater than or equal to 72.", GH_ParamAccess.item, 96);
            pManager[3].Optional = true;
            pManager.AddBooleanParameter("Save", "S", "If true, save image file", GH_ParamAccess.item, false);
            pManager[4].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "Bitmap", GH_ParamAccess.item);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiWindow window = new UiWindow();
            if (!DA.GetData(0, ref window)) return;

            string path = "C:\\Users\\Public\\Documents\\";
            bool hasPath = DA.GetData(1, ref path);

            string name = DateTime.UtcNow.ToString("yyyy-dd-M_HH-mm-ss");
            bool hasName = DA.GetData(2, ref name);

            int extension = 0;
            DA.GetData(3, ref extension);
            if (extension < 0) extension = 0;
            if (extension > 3) extension = 3;
            int ppi = 96;
            DA.GetData(3, ref ppi);
            if (ppi < 72) ppi = 72;

            if(window.Stack != null)
            { 
            int XD = 800;
            int YD = 600;

            if (window.Container.ActualWidth > 0) { XD = (int)window.Container.ActualWidth; }
            if (window.Container.ActualHeight > 0) { YD = (int)window.Container.ActualHeight; }

            RenderTargetBitmap B = new RenderTargetBitmap((int)(XD * (ppi / 96.0)), (int)(YD * (ppi / 96.0)), ppi, ppi, PixelFormats.Pbgra32);
            B.Render(window.Container);

            MemoryStream stream = new MemoryStream();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(B));
            encoder.Save(stream);

            Bitmap bitmap = new Bitmap(stream);
            bitmap.MakeTransparent();

            bool save = false;
            DA.GetData(4, ref save);

            if (!hasPath)
            {
                if (this.OnPingDocument().FilePath != null)
                {
                    path = Path.GetDirectoryName(this.OnPingDocument().FilePath) + "\\";
                }
            }

            if (path.Last() != '\\') path += "\\";

            if (!Directory.Exists(path))
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "The provided folder path does not exist. Please verify this is a valid path.");
                return;
            }


            string ext = ".png";

            System.Drawing.Imaging.ImageFormat encoding = System.Drawing.Imaging.ImageFormat.Png;

            Bitmap bmp = (Bitmap)bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            string filepath = path + name + ext;

            if (save)
            {
                bmp.Save(filepath, encoding);
                bmp.Dispose();

                DA.SetData(0, filepath);
            }

            DA.SetData(0, bitmap);
            }
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
                return Properties.Resources.UiPlus_Export_Image_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("c00cc8f7-99a7-4562-a5ff-b0853d385c5a"); }
        }
    }
}