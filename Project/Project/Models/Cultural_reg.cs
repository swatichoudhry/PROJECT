//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cultural_reg
    {
        public int cultural_id { get; set; }
        public string event_name { get; set; }
        public Nullable<int> eventid { get; set; }
        public Nullable<int> userid { get; set; }
    
        public virtual Cultural_eventlist Cultural_eventlist { get; set; }
        public virtual User User { get; set; }
    }
}