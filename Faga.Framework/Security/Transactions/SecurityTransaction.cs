using System.Security.Principal;
using Faga.Framework.Configuration;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security.Model;

namespace Faga.Framework.Security.Transactions
{
  public class SecurityTransaction : BaseTransaction
  {
    public SecurityTransaction(User _usuario)
      : base(_usuario, TransactionType.Simple,
        ApplicationConfiguration.SecurityProvider.ConnectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public SecurityTransaction(IPrincipal _user)
      : base(new User(_user.Identity.Name), TransactionType.Simple,
        ApplicationConfiguration.SecurityProvider.ConnectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }
  }
}