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

using Lch = LiveCharts.Wpf;

namespace UiPlus
{
    public class UiDataSet
    {
        #region Members

        public enum DataTypes { Text=0, Integer=1, Number=2, Point=3, Domain=4 };
        public enum Markers { None, Circle, Square, Diamond };

        protected string name = string.Empty;
        protected DataTypes type = DataTypes.Text;

        protected List<string> textItems = new List<string>();
        protected List<double> numberItems = new List<double>();
        protected List<int> integerItems = new List<int>();
        protected List<Rg.Point3d> pointItems = new List<Rg.Point3d>();
        protected List<Rg.Interval> domainItems = new List<Rg.Interval>();

        protected List<double> strokeWeights = new List<double>();
        public bool HasStrokeWeights = false;

        protected List<Sd.Color> strokeColors = new List<Sd.Color>();
        public bool HasStrokeColors = false;

        protected List<Sd.Color> fillColors = new List<Sd.Color>();
        public bool HasFillColors = false;

        protected List<Sd.Color> textColors = new List<Sd.Color>();
        public bool HasTextColors = false;

        protected List<string> labels = new List<string>();

        public bool HasTooltips = false;
        protected List<string> tooltips = new List<string>();

        public Sd.Color PrimaryColor = Sd.Color.Black;
        public bool HasPrimaryColor = false;

        public Sd.Color SecondaryColor = Sd.Color.Transparent;
        public bool HasSecondaryColor = false;

        public double Weight = 0;
        public bool HasWeight = false;

        public bool HasLabel = false;

        public Sd.Color LabelColor = Sd.Color.Black;
        public bool HasLabelColor = false;

        public double LabelAngle = 0;
        public bool HasLabelAngle = false;

        public string LabelPrefix = "";
        public string LabelSuffix = "";

        public bool HasPointGraphics = false;

        protected Markers marker = Markers.None;

        #endregion

        #region Constructors

        public UiDataSet()
        {
        }

        public UiDataSet(UiDataSet dataSet)
        {
            this.name = dataSet.name;
            this.type = dataSet.type;

            this.textItems = dataSet.textItems;
            this.numberItems = dataSet.numberItems;
            this.integerItems = dataSet.integerItems;
            this.pointItems = dataSet.pointItems;
            this.domainItems = dataSet.domainItems;

            this.strokeWeights = dataSet.strokeWeights;
            this.HasStrokeWeights = dataSet.HasStrokeWeights;

            this.strokeColors = dataSet.strokeColors;
            this.HasStrokeColors = dataSet.HasStrokeColors;

            this.fillColors = dataSet.fillColors;
            this.HasFillColors = dataSet.HasFillColors;

            this.textColors = dataSet.textColors;
            this.HasTextColors = dataSet.HasTextColors;

            this.labels = dataSet.labels;
            this.tooltips = dataSet.tooltips;

            this.marker = dataSet.marker;

            this.PrimaryColor = dataSet.PrimaryColor;
            this.HasPrimaryColor = dataSet.HasPrimaryColor;

            this.SecondaryColor = dataSet.SecondaryColor;
            this.HasSecondaryColor = dataSet.HasSecondaryColor;

            this.Weight = dataSet.Weight;
            this.HasWeight = dataSet.HasWeight;

            this.HasLabel = dataSet.HasLabel;

            this.LabelColor = dataSet.LabelColor;
            this.HasLabelColor = dataSet.HasLabelColor;

            this.LabelAngle = dataSet.LabelAngle;
            this.HasLabelAngle = dataSet.HasLabelAngle;

            this.LabelPrefix = dataSet.LabelPrefix;
            this.LabelSuffix = dataSet.LabelSuffix;

            this.HasPointGraphics = dataSet.HasPointGraphics;
        }

        public UiDataSet(List<string> data, string name)
        {
            this.name = name;
            this.TextItems = data;
        }

        public UiDataSet(List<int> data, string name)
        {
            this.name = name;
            this.IntegerItems = data;
        }

        public UiDataSet(List<double> data, string name)
        {
            this.name = name;
            this.NumberItems = data;
        }

        public UiDataSet(List<Rg.Point3d> data, string name)
        {
            this.name = name;
            this.PointItems = data;
        }

        public UiDataSet(List<Rg.Interval> data, string name)
        {
            this.name = name;
            this.DomainItems = data;
        }

        #endregion

        #region Properties

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual DataTypes Type
        {
            get { return type; }
        }

        public virtual Markers Marker
        {
            get { return marker; }
            set { marker = value; }
        }

        public virtual bool HasMarker
        {
            get { return (marker != Markers.None); }
        }

        public virtual bool HasCustomLabels
        {
            get { return (labels.Count > 0); }
        }
        public virtual List<string> Labels
        {
            get { if(labels.Count == textItems.Count) { return labels ; } else { return labels; } }
            set { labels = value; }
        }

        public virtual List<string> Tooltips
        {
            get { if (tooltips.Count == textItems.Count) { return tooltips; } else { return tooltips; } }
            set { tooltips = value; }
        }

        public virtual List<double> StrokeWeights
        {
            get
            {
                if (strokeWeights.Count < 1) strokeWeights.Add(1);

                int count = strokeWeights.Count;
                for (int i = count; i < this.Total; i++)
                {
                    strokeWeights.Add(strokeWeights[count - 1]);
                }

                return strokeWeights;
            }
            set
            {
                strokeWeights = value;
                if (strokeWeights.Count < 1) strokeWeights.Add(1);

                int count = strokeWeights.Count;
                for (int i = count; i < this.Total; i++)
                {
                    strokeWeights.Add(strokeWeights[count - 1]);
                }
            }

        }

        public virtual List<Sd.Color> TextColors
        {
            get
            {
                if (textColors.Count < 1) textColors.Add(Constants.DarkGray());

                int count = textColors.Count;
                for (int i = count; i < this.Total; i++)
                {
                    textColors.Add(textColors[count - 1]);
                }

                return textColors;
            }
            set
            {
                textColors = value;
                if (textColors.Count < 1) textColors.Add(Constants.DarkGray());

                int count = textColors.Count;
                for (int i = count; i < this.Total; i++)
                {
                    textColors.Add(textColors[count - 1]);
                }
            }

        }

        public virtual List<Sd.Color> StrokeColors
        {
            get
            {
                if (strokeColors.Count < 1) strokeColors.Add(Constants.DarkGray());

                int count = strokeColors.Count;
                for (int i = count; i < this.Total; i++)
                {
                    strokeColors.Add(strokeColors[count - 1]);
                }

                return strokeColors;
            }
            set
            {
                strokeColors = value;
                if (strokeColors.Count < 1) strokeColors.Add(Constants.DarkGray());

                int count = strokeColors.Count;
                for (int i = count; i < this.Total; i++)
                {
                    strokeColors.Add(strokeColors[count - 1]);
                }
            }

        }

        public virtual List<Sd.Color> FillColors
        {
            get
            {
                if (fillColors.Count < 1) fillColors.Add(Constants.DarkGray());

                int count = fillColors.Count;
                for (int i = count; i < this.Total; i++)
                {
                    fillColors.Add(fillColors[count - 1]);
                }

                return fillColors; 
            }
            set
            {
                fillColors = value;
                if (fillColors.Count<1) fillColors.Add(Constants.DarkGray());

                int count = fillColors.Count;
                for (int i = count; i < this.Total; i++)
                {
                    fillColors.Add(fillColors[count - 1]);
                }
            }

        }

        public virtual int Total
        {
            get 
            {
                int total = 0;

                switch (type)
                {
                    case DataTypes.Text:
                        total = textItems.Count;
                        break;
                    case DataTypes.Number:
                        total = numberItems.Count;
                        break;
                    case DataTypes.Integer:
                        total = integerItems.Count;
                        break;
                    case DataTypes.Point:
                        total = pointItems.Count;
                        break;
                    case DataTypes.Domain:
                        total = domainItems.Count;
                        break;
                }

                return total;
            }
        }

        public virtual List<string> TextItems
        {
            get { return textItems; }
            set
            {
                textItems = value;
                this.type = DataTypes.Text;
            }
        }

        public virtual List<double> NumberItems
        {
            get { return numberItems; }
            set
            {
                numberItems = value;
                foreach (double val in value) textItems.Add(val.ToString());
                this.type = DataTypes.Number;
            }
        }

        public virtual List<int> IntegerItems
        {
            get { return integerItems; }
            set
            {
                integerItems = value;
                foreach (int val in value) textItems.Add(val.ToString());
                this.type = DataTypes.Integer;
            }
        }

        public virtual List<Rg.Point3d> PointItems
        {
            get { return pointItems; }
            set
            {
                pointItems = value;
                foreach (Rg.Point3d val in value) textItems.Add(val.ToString());
                this.type = DataTypes.Point;
            }
        }

        public virtual List<Rg.Interval> DomainItems
        {
            get { return domainItems; }
            set
            {
                domainItems = value;
                foreach (Rg.Interval val in value) textItems.Add(val.ToString());
                this.type = DataTypes.Domain;
            }
        }

        #endregion

        #region Methods

        public Wm.Geometry GetMarkerGeometry()
        {
                switch (marker)
                {
                    default:
                        return Lch.DefaultGeometries.None;
                case Markers.Circle:
                    return Lch.DefaultGeometries.Circle;
                case Markers.Square:
                    return Lch.DefaultGeometries.Square;
                    case Markers.Diamond:
                    return Lch.DefaultGeometries.Diamond;
                }
        }


        #endregion

        #region Overloads

        public override string ToString()
        {
            return this.type.ToString()+" Data | " + Total;
        }

        #endregion

    }
}
