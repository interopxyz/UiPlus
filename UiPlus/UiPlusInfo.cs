using System;
using System.Drawing;

using Grasshopper;
using Grasshopper.Kernel;

namespace UiPlus
{
    public class UiPlusInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "UiPlus";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return Properties.Resources.UiPlus_Logo_24;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "Extend Grasshopper's UI with WPF Dashboards";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("a59fd30a-29d8-4a87-856c-5c8351a4fadc");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "David Mans";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "interopxyz@gmail.com";
            }
        }

        public override string AssemblyVersion
        {
            get
            {
                return "1.9.4.0";
            }
        }
    }

    public class UiPlusCategoryIcon : GH_AssemblyPriority
    {
        public object Properties { get; private set; }

        public override GH_LoadingInstruction PriorityLoad()
        {
            Instances.ComponentServer.AddCategoryIcon("Ui", UiPlus.Properties.Resources.UiPlus_Logo_16alt);
            Instances.ComponentServer.AddCategorySymbolName("Ui", 'U');
            return GH_LoadingInstruction.Proceed;
        }
    }
}
