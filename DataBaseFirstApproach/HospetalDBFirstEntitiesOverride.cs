using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtabaseFirstApproach
{
    public class HospetalDBFirstEntitiesOverride : HospetalDBFirstEntities
    {
        public HospetalDBFirstEntitiesOverride()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    }
}
