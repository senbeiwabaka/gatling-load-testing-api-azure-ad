using System.ComponentModel.DataAnnotations;

namespace load_testing_api.Repository.Entities
{
    public sealed class Person
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string GivenName { get; set; } = null!;

        [MaxLength(200)]
        public string FamilyName { get; set; } = null!;
    }
}