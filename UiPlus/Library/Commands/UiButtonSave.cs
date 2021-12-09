using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rg = Rhino.Geometry;
using Gk = Grasshopper.Kernel;

using Sw = System.Windows;
using Wm = System.Windows.Media;
using Sd = System.Drawing;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;


namespace UiPlus.Elements
{
    public class UiButtonSave : UiElement
    {

        #region Members

        Wpf.Button button = new Wpf.Button();

        Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();

        protected Dictionary<string, string> extensions = new Dictionary<string, string>();
        protected string folderPath = null;

        #endregion

        #region Constructors

        public UiButtonSave() : base()
        {
            SetInputs();
        }

        public UiButtonSave(UiButtonSave uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual string Label
        {
            get { return button.Content.ToString(); }
            set { button.Content = value; }
        }

        public virtual Dictionary<string, string> Extensions
        {
            get { return extensions; }
            set { extensions = value; }
        }

        public virtual string FolderPath
        {
            get { return folderPath; }
            set { folderPath = value; }
        }

        #endregion

        #region Methods

        public void SaveDialog()
        {

            if (folderPath != null)
            {
                if (System.IO.Directory.Exists(folderPath)) dialog.InitialDirectory = folderPath;
            }

            if (extensions.Count > 0)
            {
                dialog.DefaultExt = extensions.Values.ToList()[0]; // Default file extension

                List<string> filters = new List<string>();
                foreach (KeyValuePair<string, string> pair in extensions)
                {
                    filters.Add(pair.Key + "|*" + pair.Value);
                }
                string filter = String.Join("|", filters);
                dialog.Filter = filter; // Filter files by extension
            }

            dialog.ShowDialog();
        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {
            Mat.ButtonAssist.SetCornerRadius(button, new Sw.CornerRadius(Constants.DefaultRadius()));

            button.Click -= (o, e) => { SaveDialog(); };
            button.Click += (o, e) => { SaveDialog(); };

            this.control = button;
            base.SetInputs(Alignment.Stretch);
        }

        public override void Update(Gk.GH_Component component)
        {
            dialog.FileOk -= (o, e) => { component.ExpireSolution(true); };
            dialog.FileOk += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            List<object> paths = new List<object>();
            foreach (String file in dialog.FileNames)
            {
                paths.Add(file);
            }

            return paths;
        }

        public override string ToString()
        {
            return "Ui Save Button | " + this.Name;
        }

        #endregion

    }
}