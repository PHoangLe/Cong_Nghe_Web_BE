using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Enum
{
  public enum EnumReservationStatus
  {
    [Description("PENDING")]
    PENDING,
    [Description("CANCLED")]
    CANCLED,
    [Description("COMPLETED")]
    COMPLETED,
  }
}
