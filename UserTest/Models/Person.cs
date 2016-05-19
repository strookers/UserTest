using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UserTest.Models
{
    [DataContract(IsReference = true)]
    public abstract class Person
    {
        [Key, Required, DataMember]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Fornavn er krævet"), DataMember]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternavn er krævet"), DataMember]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Telefonnummer er krævet"), DataMember]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email er krævet"), DataMember,]
        [Index(IsUnique = true)]
        [RegularExpression(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password er krævet"), DataMember]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataMember]
        public string Salt { get; set; }
    }
}