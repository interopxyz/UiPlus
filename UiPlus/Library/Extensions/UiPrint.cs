using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Mat = MaterialDesignThemes.Wpf;

namespace UiPlus
{
    public class UiPrint
    {
        public PrintDialog printDialog = new PrintDialog();
        public UiPrint(Mat.ColorZone Container)
        {
            printDialog.PrintVisual(Container, "Print");
        }
    }
}
