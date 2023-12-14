using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    /// <summary>
    /// An enumeration representing different authorization levels.
    /// </summary>
    public enum AuthorizationLevelEnum
    {
        ClassOneInitial = 1,
        ClassOneRecurrent = 2,
        ClassTwoInitial = 3,
        ClassTwoRecurrent = 4,
        MedicalReport = 5
    }
}
