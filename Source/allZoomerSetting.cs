using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace NowanoAllZoomer
{
    public class allZoomerSetting : ModSettings
    {
        public static void DoSettingsWindowContents(Rect rect)
        {
            Listing_Standard row = new Listing_Standard(GameFont.Small);
            row.ColumnWidth = rect.width;
            row.Begin(rect);


            row.Label("Minimum zoom" + Math.Round(minimumZoom,2).ToString().ToString(), -1f, null);
            minimumZoom = row.Slider(allZoomerSetting.minimumZoom, 0.5f, 10);
            
           

            row.End();
        }


        public override void ExposeData()
        {
            base.ExposeData();

			Scribe_Values.Look<float>(ref allZoomerSetting.minimumZoom, "minimumZoom", 5f, false);

        }

        public static float minimumZoom = 5f;

    }
}
