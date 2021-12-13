using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Wm = System.Windows.Media;

namespace UiPlus
{
    public static class Constants
    {
        
        public static double DefaultRadius()
        {
            return 3;
        }

        public static double DefaultPadding()
        {
            return 3;
        }

        public static Sd.Color DefaultDarkColor()
        {
            return Sd.Color.FromArgb(57, 61, 71);
        }


        #region Material Default

        public static Sd.Color MaterialColor()
        {
            return Sd.Color.FromArgb(38, 50, 56);
        }

        public static Wm.Color MaterialMediaColor()
        {
            return Wm.Color.FromRgb(38, 50, 56);
        }

        public static Wm.Brush MaterialBrush()
        {
            return new Wm.SolidColorBrush(Wm.Color.FromRgb(38, 50, 56));
        }

        #endregion

        public static Sd.Color TransparentGray()
        {
            return Sd.Color.FromArgb(100,100, 100, 100);
        }

        public static Sd.Color DarkGray()
        {
            return Sd.Color.FromArgb(57, 61, 71);
        }

        public static Sd.Color OffWhite()
        {
            return Sd.Color.FromArgb(250, 249, 246);
        }
        
        public static Sd.Color[] DefaultDrawingColors()
        {
            return new Sd.Color[] { Sd.Color.FromArgb(255,235,238),Sd.Color.FromArgb(255,205,210),Sd.Color.FromArgb(239,154,154),Sd.Color.FromArgb(229,115,115),Sd.Color.FromArgb(239,83,80),Sd.Color.FromArgb(244,67,54),Sd.Color.FromArgb(229,57,53),Sd.Color.FromArgb(211,47,47),Sd.Color.FromArgb(198,40,40),Sd.Color.FromArgb(183,28,28),Sd.Color.FromArgb(252,228,236),Sd.Color.FromArgb(248,187,208),Sd.Color.FromArgb(244,143,177),Sd.Color.FromArgb(240,98,146),Sd.Color.FromArgb(236,64,122),Sd.Color.FromArgb(233,30,99),Sd.Color.FromArgb(216,27,96),Sd.Color.FromArgb(194,24,91),Sd.Color.FromArgb(173,20,87),Sd.Color.FromArgb(136,14,79),Sd.Color.FromArgb(243,229,245),Sd.Color.FromArgb(225,190,231),Sd.Color.FromArgb(206,147,216),Sd.Color.FromArgb(186,104,200),Sd.Color.FromArgb(171,71,188),Sd.Color.FromArgb(156,39,176),Sd.Color.FromArgb(142,36,170),Sd.Color.FromArgb(123,31,162),Sd.Color.FromArgb(106,27,154),Sd.Color.FromArgb(74,20,140),Sd.Color.FromArgb(237,231,246),Sd.Color.FromArgb(209,196,233),Sd.Color.FromArgb(179,157,219),Sd.Color.FromArgb(149,117,205),Sd.Color.FromArgb(126,87,194),Sd.Color.FromArgb(103,58,183),Sd.Color.FromArgb(94,53,177),Sd.Color.FromArgb(81,45,168),Sd.Color.FromArgb(69,39,160),Sd.Color.FromArgb(49,27,146),Sd.Color.FromArgb(232,234,246),Sd.Color.FromArgb(197,202,233),Sd.Color.FromArgb(159,168,218),Sd.Color.FromArgb(121,134,203),Sd.Color.FromArgb(92,107,192),Sd.Color.FromArgb(63,81,181),Sd.Color.FromArgb(57,73,171),Sd.Color.FromArgb(48,63,159),Sd.Color.FromArgb(40,53,147),Sd.Color.FromArgb(26,35,126),Sd.Color.FromArgb(227,242,253),Sd.Color.FromArgb(187,222,251),Sd.Color.FromArgb(144,202,249),Sd.Color.FromArgb(100,181,246),Sd.Color.FromArgb(66,165,245),Sd.Color.FromArgb(33,150,243),Sd.Color.FromArgb(30,136,229),Sd.Color.FromArgb(25,118,210),Sd.Color.FromArgb(21,101,192),Sd.Color.FromArgb(13,71,161),Sd.Color.FromArgb(225,245,254),Sd.Color.FromArgb(179,229,252),Sd.Color.FromArgb(129,212,250),Sd.Color.FromArgb(79,195,247),Sd.Color.FromArgb(41,182,246),Sd.Color.FromArgb(3,169,244),Sd.Color.FromArgb(3,155,229),Sd.Color.FromArgb(2,136,209),Sd.Color.FromArgb(2,119,189),Sd.Color.FromArgb(1,87,155),Sd.Color.FromArgb(224,247,250),Sd.Color.FromArgb(178,235,242),Sd.Color.FromArgb(128,222,234),Sd.Color.FromArgb(77,208,225),Sd.Color.FromArgb(38,198,218),Sd.Color.FromArgb(0,188,212),Sd.Color.FromArgb(0,172,193),Sd.Color.FromArgb(0,151,167),Sd.Color.FromArgb(0,131,143),Sd.Color.FromArgb(0,96,100),Sd.Color.FromArgb(224,242,241),Sd.Color.FromArgb(178,223,219),Sd.Color.FromArgb(128,203,196),Sd.Color.FromArgb(77,182,172),Sd.Color.FromArgb(38,166,154),Sd.Color.FromArgb(0,150,136),Sd.Color.FromArgb(0,137,123),Sd.Color.FromArgb(0,121,107),Sd.Color.FromArgb(0,105,92),Sd.Color.FromArgb(0,77,64),Sd.Color.FromArgb(232,245,233),Sd.Color.FromArgb(200,230,201),Sd.Color.FromArgb(165,214,167),Sd.Color.FromArgb(129,199,132),Sd.Color.FromArgb(102,187,106),Sd.Color.FromArgb(76,175,80),Sd.Color.FromArgb(67,160,71),Sd.Color.FromArgb(56,142,60),Sd.Color.FromArgb(46,125,50),Sd.Color.FromArgb(27,94,32),Sd.Color.FromArgb(241,248,233),Sd.Color.FromArgb(220,237,200),Sd.Color.FromArgb(197,225,165),Sd.Color.FromArgb(174,213,129),Sd.Color.FromArgb(156,204,101),Sd.Color.FromArgb(139,195,74),Sd.Color.FromArgb(124,179,66),Sd.Color.FromArgb(104,159,56),Sd.Color.FromArgb(85,139,47),Sd.Color.FromArgb(51,105,30),Sd.Color.FromArgb(249,251,231),Sd.Color.FromArgb(240,244,195),Sd.Color.FromArgb(230,238,156),Sd.Color.FromArgb(220,231,117),Sd.Color.FromArgb(212,225,87),Sd.Color.FromArgb(205,220,57),Sd.Color.FromArgb(192,202,51),Sd.Color.FromArgb(175,180,43),Sd.Color.FromArgb(158,157,36),Sd.Color.FromArgb(130,119,23),Sd.Color.FromArgb(255,253,231),Sd.Color.FromArgb(255,249,196),Sd.Color.FromArgb(255,245,157),Sd.Color.FromArgb(255,241,118),Sd.Color.FromArgb(255,238,88),Sd.Color.FromArgb(255,235,59),Sd.Color.FromArgb(253,216,53),Sd.Color.FromArgb(251,192,45),Sd.Color.FromArgb(249,168,37),Sd.Color.FromArgb(245,127,23),Sd.Color.FromArgb(255,248,225),Sd.Color.FromArgb(255,236,179),Sd.Color.FromArgb(255,224,130),Sd.Color.FromArgb(255,213,79),Sd.Color.FromArgb(255,202,40),Sd.Color.FromArgb(255,193,7),Sd.Color.FromArgb(255,179,0),Sd.Color.FromArgb(255,160,0),Sd.Color.FromArgb(255,143,0),Sd.Color.FromArgb(255,111,0),Sd.Color.FromArgb(255,243,224),Sd.Color.FromArgb(255,224,178),Sd.Color.FromArgb(255,204,128),Sd.Color.FromArgb(255,183,77),Sd.Color.FromArgb(255,167,38),Sd.Color.FromArgb(255,152,0),Sd.Color.FromArgb(251,140,0),Sd.Color.FromArgb(245,124,0),Sd.Color.FromArgb(239,108,0),Sd.Color.FromArgb(230,81,0),Sd.Color.FromArgb(251,233,231),Sd.Color.FromArgb(255,204,188),Sd.Color.FromArgb(255,171,145),Sd.Color.FromArgb(255,138,101),Sd.Color.FromArgb(255,112,67),Sd.Color.FromArgb(255,87,34),Sd.Color.FromArgb(244,81,30),Sd.Color.FromArgb(230,74,25),Sd.Color.FromArgb(216,67,21),Sd.Color.FromArgb(191,54,12),Sd.Color.FromArgb(239,235,233),Sd.Color.FromArgb(215,204,200),Sd.Color.FromArgb(188,170,164),Sd.Color.FromArgb(161,136,127),Sd.Color.FromArgb(141,110,99),Sd.Color.FromArgb(121,85,72),Sd.Color.FromArgb(109,76,65),Sd.Color.FromArgb(93,64,55),Sd.Color.FromArgb(78,52,46),Sd.Color.FromArgb(62,39,35),Sd.Color.FromArgb(250,250,250),Sd.Color.FromArgb(245,245,245),Sd.Color.FromArgb(238,238,238),Sd.Color.FromArgb(224,224,224),Sd.Color.FromArgb(189,189,189),Sd.Color.FromArgb(158,158,158),Sd.Color.FromArgb(117,117,117),Sd.Color.FromArgb(97,97,97),Sd.Color.FromArgb(66,66,66),Sd.Color.FromArgb(33,33,33),Sd.Color.FromArgb(236,239,241),Sd.Color.FromArgb(207,216,220),Sd.Color.FromArgb(176,190,197),Sd.Color.FromArgb(144,164,174),Sd.Color.FromArgb(120,144,156),Sd.Color.FromArgb(96,125,139),Sd.Color.FromArgb(84,110,122),Sd.Color.FromArgb(69,90,100),Sd.Color.FromArgb(55,71,79),Sd.Color.FromArgb(38,50,56) };
        }

        public static Wm.Color[] DefaultMediaColors()
        {
            return new Wm.Color[] { Wm.Color.FromArgb(255,255,235,238),Wm.Color.FromArgb(255,255,205,210),Wm.Color.FromArgb(255,239,154,154),Wm.Color.FromArgb(255,229,115,115),Wm.Color.FromArgb(255,239,83,80),Wm.Color.FromArgb(255,244,67,54),Wm.Color.FromArgb(255,229,57,53),Wm.Color.FromArgb(255,211,47,47),Wm.Color.FromArgb(255,198,40,40),Wm.Color.FromArgb(255,183,28,28),Wm.Color.FromArgb(255,252,228,236),Wm.Color.FromArgb(255,248,187,208),Wm.Color.FromArgb(255,244,143,177),Wm.Color.FromArgb(255,240,98,146),Wm.Color.FromArgb(255,236,64,122),Wm.Color.FromArgb(255,233,30,99),Wm.Color.FromArgb(255,216,27,96),Wm.Color.FromArgb(255,194,24,91),Wm.Color.FromArgb(255,173,20,87),Wm.Color.FromArgb(255,136,14,79),Wm.Color.FromArgb(255,243,229,245),Wm.Color.FromArgb(255,225,190,231),Wm.Color.FromArgb(255,206,147,216),Wm.Color.FromArgb(255,186,104,200),Wm.Color.FromArgb(255,171,71,188),Wm.Color.FromArgb(255,156,39,176),Wm.Color.FromArgb(255,142,36,170),Wm.Color.FromArgb(255,123,31,162),Wm.Color.FromArgb(255,106,27,154),Wm.Color.FromArgb(255,74,20,140),Wm.Color.FromArgb(255,237,231,246),Wm.Color.FromArgb(255,209,196,233),Wm.Color.FromArgb(255,179,157,219),Wm.Color.FromArgb(255,149,117,205),Wm.Color.FromArgb(255,126,87,194),Wm.Color.FromArgb(255,103,58,183),Wm.Color.FromArgb(255,94,53,177),Wm.Color.FromArgb(255,81,45,168),Wm.Color.FromArgb(255,69,39,160),Wm.Color.FromArgb(255,49,27,146),Wm.Color.FromArgb(255,232,234,246),Wm.Color.FromArgb(255,197,202,233),Wm.Color.FromArgb(255,159,168,218),Wm.Color.FromArgb(255,121,134,203),Wm.Color.FromArgb(255,92,107,192),Wm.Color.FromArgb(255,63,81,181),Wm.Color.FromArgb(255,57,73,171),Wm.Color.FromArgb(255,48,63,159),Wm.Color.FromArgb(255,40,53,147),Wm.Color.FromArgb(255,26,35,126),Wm.Color.FromArgb(255,227,242,253),Wm.Color.FromArgb(255,187,222,251),Wm.Color.FromArgb(255,144,202,249),Wm.Color.FromArgb(255,100,181,246),Wm.Color.FromArgb(255,66,165,245),Wm.Color.FromArgb(255,33,150,243),Wm.Color.FromArgb(255,30,136,229),Wm.Color.FromArgb(255,25,118,210),Wm.Color.FromArgb(255,21,101,192),Wm.Color.FromArgb(255,13,71,161),Wm.Color.FromArgb(255,225,245,254),Wm.Color.FromArgb(255,179,229,252),Wm.Color.FromArgb(255,129,212,250),Wm.Color.FromArgb(255,79,195,247),Wm.Color.FromArgb(255,41,182,246),Wm.Color.FromArgb(255,3,169,244),Wm.Color.FromArgb(255,3,155,229),Wm.Color.FromArgb(255,2,136,209),Wm.Color.FromArgb(255,2,119,189),Wm.Color.FromArgb(255,1,87,155),Wm.Color.FromArgb(255,224,247,250),Wm.Color.FromArgb(255,178,235,242),Wm.Color.FromArgb(255,128,222,234),Wm.Color.FromArgb(255,77,208,225),Wm.Color.FromArgb(255,38,198,218),Wm.Color.FromArgb(255,0,188,212),Wm.Color.FromArgb(255,0,172,193),Wm.Color.FromArgb(255,0,151,167),Wm.Color.FromArgb(255,0,131,143),Wm.Color.FromArgb(255,0,96,100),Wm.Color.FromArgb(255,224,242,241),Wm.Color.FromArgb(255,178,223,219),Wm.Color.FromArgb(255,128,203,196),Wm.Color.FromArgb(255,77,182,172),Wm.Color.FromArgb(255,38,166,154),Wm.Color.FromArgb(255,0,150,136),Wm.Color.FromArgb(255,0,137,123),Wm.Color.FromArgb(255,0,121,107),Wm.Color.FromArgb(255,0,105,92),Wm.Color.FromArgb(255,0,77,64),Wm.Color.FromArgb(255,232,245,233),Wm.Color.FromArgb(255,200,230,201),Wm.Color.FromArgb(255,165,214,167),Wm.Color.FromArgb(255,129,199,132),Wm.Color.FromArgb(255,102,187,106),Wm.Color.FromArgb(255,76,175,80),Wm.Color.FromArgb(255,67,160,71),Wm.Color.FromArgb(255,56,142,60),Wm.Color.FromArgb(255,46,125,50),Wm.Color.FromArgb(255,27,94,32),Wm.Color.FromArgb(255,241,248,233),Wm.Color.FromArgb(255,220,237,200),Wm.Color.FromArgb(255,197,225,165),Wm.Color.FromArgb(255,174,213,129),Wm.Color.FromArgb(255,156,204,101),Wm.Color.FromArgb(255,139,195,74),Wm.Color.FromArgb(255,124,179,66),Wm.Color.FromArgb(255,104,159,56),Wm.Color.FromArgb(255,85,139,47),Wm.Color.FromArgb(255,51,105,30),Wm.Color.FromArgb(255,249,251,231),Wm.Color.FromArgb(255,240,244,195),Wm.Color.FromArgb(255,230,238,156),Wm.Color.FromArgb(255,220,231,117),Wm.Color.FromArgb(255,212,225,87),Wm.Color.FromArgb(255,205,220,57),Wm.Color.FromArgb(255,192,202,51),Wm.Color.FromArgb(255,175,180,43),Wm.Color.FromArgb(255,158,157,36),Wm.Color.FromArgb(255,130,119,23),Wm.Color.FromArgb(255,255,253,231),Wm.Color.FromArgb(255,255,249,196),Wm.Color.FromArgb(255,255,245,157),Wm.Color.FromArgb(255,255,241,118),Wm.Color.FromArgb(255,255,238,88),Wm.Color.FromArgb(255,255,235,59),Wm.Color.FromArgb(255,253,216,53),Wm.Color.FromArgb(255,251,192,45),Wm.Color.FromArgb(255,249,168,37),Wm.Color.FromArgb(255,245,127,23),Wm.Color.FromArgb(255,255,248,225),Wm.Color.FromArgb(255,255,236,179),Wm.Color.FromArgb(255,255,224,130),Wm.Color.FromArgb(255,255,213,79),Wm.Color.FromArgb(255,255,202,40),Wm.Color.FromArgb(255,255,193,7),Wm.Color.FromArgb(255,255,179,0),Wm.Color.FromArgb(255,255,160,0),Wm.Color.FromArgb(255,255,143,0),Wm.Color.FromArgb(255,255,111,0),Wm.Color.FromArgb(255,255,243,224),Wm.Color.FromArgb(255,255,224,178),Wm.Color.FromArgb(255,255,204,128),Wm.Color.FromArgb(255,255,183,77),Wm.Color.FromArgb(255,255,167,38),Wm.Color.FromArgb(255,255,152,0),Wm.Color.FromArgb(255,251,140,0),Wm.Color.FromArgb(255,245,124,0),Wm.Color.FromArgb(255,239,108,0),Wm.Color.FromArgb(255,230,81,0),Wm.Color.FromArgb(255,251,233,231),Wm.Color.FromArgb(255,255,204,188),Wm.Color.FromArgb(255,255,171,145),Wm.Color.FromArgb(255,255,138,101),Wm.Color.FromArgb(255,255,112,67),Wm.Color.FromArgb(255,255,87,34),Wm.Color.FromArgb(255,244,81,30),Wm.Color.FromArgb(255,230,74,25),Wm.Color.FromArgb(255,216,67,21),Wm.Color.FromArgb(255,191,54,12),Wm.Color.FromArgb(255,239,235,233),Wm.Color.FromArgb(255,215,204,200),Wm.Color.FromArgb(255,188,170,164),Wm.Color.FromArgb(255,161,136,127),Wm.Color.FromArgb(255,141,110,99),Wm.Color.FromArgb(255,121,85,72),Wm.Color.FromArgb(255,109,76,65),Wm.Color.FromArgb(255,93,64,55),Wm.Color.FromArgb(255,78,52,46),Wm.Color.FromArgb(255,62,39,35),Wm.Color.FromArgb(255,250,250,250),Wm.Color.FromArgb(255,245,245,245),Wm.Color.FromArgb(255,238,238,238),Wm.Color.FromArgb(255,224,224,224),Wm.Color.FromArgb(255,189,189,189),Wm.Color.FromArgb(255,158,158,158),Wm.Color.FromArgb(255,117,117,117),Wm.Color.FromArgb(255,97,97,97),Wm.Color.FromArgb(255,66,66,66),Wm.Color.FromArgb(255,33,33,33),Wm.Color.FromArgb(255,236,239,241),Wm.Color.FromArgb(255,207,216,220),Wm.Color.FromArgb(255,176,190,197),Wm.Color.FromArgb(255,144,164,174),Wm.Color.FromArgb(255,120,144,156),Wm.Color.FromArgb(255,96,125,139),Wm.Color.FromArgb(255,84,110,122),Wm.Color.FromArgb(255,69,90,100),Wm.Color.FromArgb(255,55,71,79),Wm.Color.FromArgb(255,38,50,56) };
        }

    }
}
