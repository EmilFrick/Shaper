using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shaper.Models.Entities;


namespace Shaper.Models.ViewModels.ShaperUser
{
    public class ShaperUserDetails
    {
        public string IdentityId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }


        public ShaperUserDetails()
        {

        }
        public Entities.ShaperUser GetEntity()
        {
            return new()
            {
                IdentityId = this.IdentityId,
                FullName = this.FullName,
                Address = this.Address,
                PostalCode = this.PostalCode,
                City = this.City
            };
        }
    }
}
