using System;
using System.Collections.Generic;

namespace TW.Vault.Model.JSON
{
    public class UserStats
    {
        public float DVsAtHome { get; set; }
        public float BacklineDVsAtHome { get; set; }
        public float DVsTraveling { get; set; }
        public int NukesInPastWeek { get; set; }
        public int FangsInPastWeek { get; set; }
        public int FakesInPastWeek { get; set; }
        public int SnipesInPastWeek { get; set; }
        public Dictionary<String, float> PopPerTribe { get; set; }
    }
}
