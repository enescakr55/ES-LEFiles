using LEFiles.Core.DataAccess;
using LEFiles.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.DataAccess.Abstract
{
  public interface IClientDal:ICrudOperations<Client>
  {
  }
}
