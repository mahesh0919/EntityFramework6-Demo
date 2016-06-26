using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtabaseFirstApproach
{
    public class HospetalDBFirstEntitiesOverride : HospetalDBFirstEntities
    {
        public HospetalDBFirstEntitiesOverride()
        {
            this.Configuration.LazyLoadingEnabled = true;
            
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry,
                                    System.Collections.Generic.IDictionary<object, object> items)
        {
            if (entityEntry.Entity is Patient)
            {
                if (string.IsNullOrEmpty(entityEntry.CurrentValues.GetValue<string>("FirstName")))
                {
                    var list = new List<System.Data.Entity.Validation.DbValidationError>();

                    list.Add(new System.Data.Entity.Validation
                       .DbValidationError("FirstName", "FirstName is required"));

                    return new System.Data.Entity.Validation
                       .DbEntityValidationResult(entityEntry, list);
                }
            }
            
            return base.ValidateEntity(entityEntry, items);
        }

    }
}
