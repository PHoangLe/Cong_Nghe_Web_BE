using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Enum
{
    public enum EnumFoodOrderStatus
    {
        [Description("PENDING")]
        PENDING,
        [Description("PROCESSING")]
        PROCESSING,
        [Description("SERVED")]
        SERVED
    }
}
