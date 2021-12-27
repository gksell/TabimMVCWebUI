using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TabimMVCWebUI.Entity
{
    public class DataContext:DbContext
    {
        
        public DataContext():base("dataConnection")
        {
            
        }
        public DbSet<UserOperation> UserOperation { get; set; }    
    }
}