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
    
    public partial class Monthes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountdownSettingsId { get; set; }
        public int Number { get; set; }
    
        public virtual CountdownSettings CountdownSetting { get; set; }
    }
}
