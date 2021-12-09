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
    public class UiCheckList : UiElement
    {

        #region Members

        Xcd.CheckListBox ctrl = new Xcd.CheckListBox();

        #endregion

        #region Constructors

        public UiCheckList() : base()
        {
            SetInputs();
        }

        public UiCheckList(UiCheckList uiControl) : base(uiControl)
        {
            this.border = uiControl.border;
        }

        #endregion

        #region Properties

        public virtual List<string> Items
        {
            set { ctrl.ItemsSource = value; }
        }
        public virtual List<bool> States
        {
            get
            {
                Dictionary<object, bool> results = new Dictionary<object, bool>();
                foreach(object item in ctrl.Items)
                {
                    results.Add(item, false);
                }

                foreach (object item in ctrl.SelectedItems)
                {
                    if (results.ContainsKey(item)) results[item] = true;
                }
                return results.Values.ToList(); 
            }
            set 
            {
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i]) { ctrl.SelectedItems.Add(ctrl.Items[i]); }
                }
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ElementType = ElementTypes.Border;

            this.control = this.ctrl;
            this.border.Child = this.control;
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.ItemSelectionChanged -= (o, e) => { component.ExpireSolution(true); };
            ctrl.ItemSelectionChanged += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            List<object> outputs = new List<object>();

            foreach(bool state in this.States)
            {
                outputs.Add(state);
            }

            return outputs;
        }

        public override string ToString()
        {
            return "Ui Check List | " + this.Name;
        }

        #endregion

    }
}