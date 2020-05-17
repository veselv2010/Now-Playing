﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NowPlaying.Core.InputSender
{
    public sealed class KeyFormatterWindows : IKeyFormatter
    {
        /// <summary>
        /// VirtualKey ushort to source engine key
        /// </summary>
        public string GetSourceKey(ushort key)
        {
            if (VirtualKeysToSourceKeys.TryGetValue(key, out string sourceKey))
                return sourceKey;

            return "key not found";
        }

        private readonly IDictionary<ushort, string> VirtualKeysToSourceKeys = new Dictionary<ushort, string>()
        {
            {0, "none" },
            {32,"space"}, 
            //{"capslock"},
            {27,"escape"},
            {112,"f1"},
            {113,"f2"},
            {114,"f3"},
            {115,"f4"},
            {116,"f5"},
            {117,"f6"},
            {118,"f7"},
            {119,"f8"},
            {120,"f9"},
            {121,"f10"},
            {122,"f11"},
            {123,"f12"},
            {19,"pause"},
            {192,"`"},
            {189,"-"},
            //{"="},
            //{"backspace"},
            {9,"tab"},
            {221,"]"},
            {219,"["},
            //{"/"},
            //{"semicolon"},
            {222,"'"},
            {226,"\\"},
            {16,"shift"},
            {13,"enter"},
            //{","},
            {17, "ctrl"},
            {18, "alt"},
            {49,"1"},
            {50,"2"},
            {51,"3"},
            {52,"4"},
            {53,"5"},
            {54,"6"},
            {55,"7"},
            {56,"8"},
            {57,"9"},
            {48,"0"},
            {65,"a"},
            {66,"b"},
            {67,"c"},
            {68,"d"},
            {69,"e"},
            {70,"f"},
            {71,"g"},
            {72,"h"},
            {73,"i"},
            {74,"j"},
            {75,"k"},
            {76,"l"},
            {77,"m"},
            {78,"n"},
            {79,"o"},
            {80,"p"},
            {81,"q"},
            {82,"r"},
            {83,"s"},
            {84,"t"},
            {85,"u"},
            {86,"v"},
            {87,"w"},
            {88,"x"},
            {89,"y"},
            {90,"z"},
            {38,"uparrow"},
            {40,"downarrow"},
            {39,"rightarrow"},
            {37,"leftarrow"},
            {45,"ins"},
            {36,"home"},
            {33,"pgup"},
            {34,"pgdn"},
            {46,"del"},
            {35,"end"},
            //{"mouse1"},
            //{2,"mouse2"},
            //{"mouse3"},
            {5,"mouse4"},
            {6,"mouse5"},
            //{"mwheelup"},
            //{"mwheeldown"},
            {97,"kp_end"},
            {98,"kp_downarrow"},
            {99,"kp_pgdn"},
            {100,"kp_leftarrow"},
            {101,"kp_5"},
            {102,"kp_rightarrow"},
            {103,"kp_home"},
            {104,"kp_uparrow"},
            {105,"kp_pgup"},
            {96, "kp_ins"},
            {187,"kp_plus"},
            //{"kp_minus"},
            //{"kp_slash"},
            //{"kp_del"},
            //{"*"},
            //{"kp_enter"}
        };
    }
}
