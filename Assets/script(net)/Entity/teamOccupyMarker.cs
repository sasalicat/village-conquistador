namespace KBEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class teamOccupyMarker : Entity
    {
        public void updateTeam1(sbyte dom,sbyte num)
        {
            Dictionary<string, object> point = new Dictionary<string, object>();
            point["no"] = MarkersCenter.TEAM1_TEXT;
            point["denominator"] = (int)dom;
            point["numerator"] = (int)num;
            MarkersCenter.AddMarkerEvent(point);
        }
        public void updateTeam2(sbyte dom, sbyte num)
        {
            Dictionary<string, object> point = new Dictionary<string, object>();
            point["no"] = MarkersCenter.TEAM2_TEXT;
            point["denominator"] = (int)dom;
            point["numerator"] = (int)num;
            MarkersCenter.AddMarkerEvent(point);
        }
    }
}
