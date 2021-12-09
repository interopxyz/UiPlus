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
    public class UiButtonCommand : UiElement
    {

        #region Members

        Wpf.Button button = new Wpf.Button();

        List<string> commands = new List<string>();

        #endregion

        #region Constructors

        public UiButtonCommand() : base()
        {
            SetInputs();
        }

        public UiButtonCommand(UiButtonCommand uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual string Label
        {
            get { return button.Content.ToString(); }
            set
            { button.Content = value; }
        }

        public virtual List<string> Commands
        {
            get { return commands; }
            set { commands = value; }
        }

        #endregion

        #region Methods

        public void RunCommands()
        {
            foreach(string command in commands)
            {
                Rhino.RhinoApp.RunScript(command,false);
            }
        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {

            Mat.ButtonAssist.SetCornerRadius(button, new Sw.CornerRadius(Constants.DefaultRadius()));
            button.Click -= (o, e) => { RunCommands(); };
            button.Click += (o, e) => { RunCommands(); };

            this.control = button;
            base.SetInputs(Alignment.Stretch);
        }

        public override List<object> GetValues()
        {
            List<object> output = new List<object>();
            foreach (string command in commands) output.Add(command);
            return output;
        }

        public override string ToString()
        {
            return "Ui Rhino Command | " + this.Name;
        }

        #endregion

    }
}