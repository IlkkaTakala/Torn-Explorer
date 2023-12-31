﻿using System;
using System.Windows.Markup;
using Newtonsoft.Json;

namespace Project.Model
{
    public class Networth
    {
        public Int64 Pending { get; set; }
        public Int64 Wallet { get; set; }
        public Int64 Bank { get; set; }
        public Int64 Points { get; set; }
        public Int64 Cayman { get; set; }
        public Int64 Vault { get; set; }
        public Int64 Piggybank { get; set; }
        public Int64 Items { get; set; }
        public Int64 Displaycase { get; set; }
        public Int64 Bazaar { get; set; }
        public Int64 Itemmarket { get; set; }
        public Int64 Properties { get; set; }
        public Int64 Stockmarket { get; set; }
        public Int64 Auctionhouse { get; set; }
        public Int64 Company { get; set; }
        public Int64 Bookie { get; set; }
        public Int64 Enlistedcars { get; set; }
        public Int64 Loan { get; set; }
        public Int64 Unpaidfees { get; set; }
        public long Total { get; set; }
    }

    public class Life
    {
        public int Current { get; set; }
        public int Maximum { get; set; }
    }

    public class Status
    {
        public string Description { get; set; }
        public string Details { get; set; }
        public string State { get; set; }
        public string Color { get; set; }

        [JsonProperty("until"), JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime Until { get; set; }

        public string For
        {
            get
            {
                if (Until > DateTime.Now)
                {
                    TimeSpan span = Until - DateTime.Now;
                    int hours = (int)span.TotalHours;
                    return $"For: {hours}h {span.Minutes}m {span.Seconds}s";
                }
                else return "";
            }
        }
    }

    public class Job
    {
        public string Position { get; set; }
        public string Company_name { get; set; }
    }

    public class Faction
    {
        public string Position { get; set; }
        public int Faction_id { get; set; }
        public int Days_in_faction { get; set; }
        public string Faction_name { get; set; }
    }

    public class States
    {
        [JsonProperty("hospital_timestamp"), JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime Hospital_timestamp { get; set; }

        [JsonProperty("jail_timestamp"), JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime Jail_timestamp { get; set; }
    }

    public class Last_Action
    {
        public string Status { get; set; }

        [JsonProperty("timestamp"), JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime Timestamp { get; set; }

        public string LastAction
        {
            get
            {
                TimeSpan span = DateTime.Now.ToUniversalTime() - Timestamp;
                int hours = (int)span.TotalHours;
                return $"Last action: {hours}h {span.Minutes}m {span.Seconds}s";
               // return "Last action: " + (DateTime.Now.ToUniversalTime() - Timestamp).ToString("h'h 'm'm 's's'");
            }
        }
    }

    public class Competition
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public object Position { get; set; }
    }


    public class Profile
    {
        public string Rank { get; set; }
        public int Level { get; set; }
        public string Gender { get; set; }
        public string Property { get; set; }
        public int Age { get; set; }
        public int Player_id { get; set; }
        public string Name { get; set; }
        public Life Life { get; set; }
        public Status Status { get; set; }
        public Job Job { get; set; }
        public Faction Faction { get; set; }
        public States States { get; set; }
        public Last_Action Last_action { get; set; }
        public Competition Competition { get; set; }
        public Networth Networth { get; set; }

        public string LifeStr { get { return $"{Life.Current}/{Life.Maximum}"; } }
        public string IDStr { get { return $"[{Player_id}]"; } }
        public string FactionStr { get { return $"{Faction.Position} of {Faction.Faction_name} for {Faction.Days_in_faction} days"; } }
        public string JobStr { get { return $"{Job.Position} of {Job.Company_name}"; } }
    }
}
