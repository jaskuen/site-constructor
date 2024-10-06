using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities.LocalUser;

namespace Domain.Models.Entities.SiteName;

public class SiteName : Entity
{
    public string Name { get; set; }
    public int UserId { get; set; }
}
