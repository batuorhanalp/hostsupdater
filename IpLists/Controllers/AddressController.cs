using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IpLists.Models;

namespace IpLists.Controllers
{
    public class AddressController : ApiController
    {
        // GET api/address
        public List<HostAddress> Get()
        {
            var db = new IpAddressesEntities();
            var addresses = db.addresses.ToList();
            var liAddresses = new List<HostAddress>();
            foreach (var address in addresses)
            {
                liAddresses.Add(new HostAddress{ Ip = address.ip, Url = address.siteurl });
            }
            return liAddresses;
        }
    }
}
