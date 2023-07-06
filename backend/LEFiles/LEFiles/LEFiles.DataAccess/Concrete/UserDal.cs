using LEFiles.Core.DataAccess;
using LEFiles.DataAccess.Abstract;
using LEFiles.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.DataAccess.Concrete
{
  public class UserDal:CrudOperations<User,AppDbContext> , IUserDal
  {
    public UserDal()
    {

    }
    public UserDal(DbContext context):base(context)
    {
      
    }
  }
}
