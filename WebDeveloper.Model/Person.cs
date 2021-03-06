namespace WebDeveloper.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Person.Person")]
    public partial class Person
    {
        public Person()
        {
            BusinessEntityContact = new HashSet<BusinessEntityContact>();
            EmailAddress = new HashSet<EmailAddress>();
            PersonPhone = new HashSet<PersonPhone>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BusinessEntityID { get; set; }

        [Required(ErrorMessage ="This Field is Required.")]
        [StringLength(2,ErrorMessage ="This Field is Required 2 characters.")]
        [Display(Name = "Type")]
        public string PersonType { get; set; }

        public bool NameStyle { get; set; }

        [StringLength(8,ErrorMessage ="This Field is Required 8 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This Field is Required.")]
        [StringLength(50, ErrorMessage = "This Field is Required 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "This Field is Required 50 characters.")]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "This Field is Required.")]
        [StringLength(50, ErrorMessage = "This Field is Required 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(10, ErrorMessage = "This Field is Required 10 characters.")]
        public string Suffix { get; set; }

        public int EmailPromotion { get; set; }

        [Column(TypeName = "xml")]
        public string AdditionalContactInfo { get; set; }

        [Column(TypeName = "xml")]
        public string Demographics { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual BusinessEntity BusinessEntity { get; set; }

        public virtual ICollection<BusinessEntityContact> BusinessEntityContact { get; set; }

        public virtual ICollection<EmailAddress> EmailAddress { get; set; }

        public virtual Password Password { get; set; }

        public virtual ICollection<PersonPhone> PersonPhone { get; set; }
    }
}
