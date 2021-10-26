using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Common;

public abstract class AppEntity
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
}

