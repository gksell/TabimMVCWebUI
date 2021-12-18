using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TabimMVCWebUI.Entity
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            List<UserOperation> user = new List<UserOperation>()
            {
                
            };
            foreach (var item in user)
            {
                context.UserOperation.Add(item);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}