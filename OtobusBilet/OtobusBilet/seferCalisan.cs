//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OtobusBiletSatis
{
    using System;
    using System.Collections.Generic;
    
    public partial class seferCalisan
    {
        public int seferCalisanID { get; set; }
        public int seferID { get; set; }
        public int calisanID { get; set; }
    
        public virtual calisan calisan { get; set; }
        public virtual sefer sefer { get; set; }
    }
}
