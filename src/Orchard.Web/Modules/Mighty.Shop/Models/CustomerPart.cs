using Orchard.ContentManagement;
using Orchard.Security;
using Orchard.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mighty.Shop.Models
{
    public class CustomerPart : ContentPart<CustomerRecord>
    {

        [Required]
        public string FirstName
        {
            get { return Record.FirstName; }
            set { Record.FirstName = value; }
        }

        [Required]
        public string LastName
        {
            get { return Record.LastName; }
            set { Record.LastName = value; }
        }

        public string Title
        {
            get { return Record.Title; }
            set { Record.Title = value; }
        }

        public DateTime CreatedAt
        {
            get { return Record.CreatedAt; }
            set { Record.CreatedAt = value; }
        }

        public UserPart User
        {
            get { return this.As<UserPart>(); }
        }
    }
}