//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CountdownDataBaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProgressSettings
    {
        public int Id { get; set; }
        public int Interval { get; set; }
        public int Duration { get; set; }
        public System.DateTime Start { get; set; }
    
        public virtual Reminder Reminder { get; set; }
    }
}
