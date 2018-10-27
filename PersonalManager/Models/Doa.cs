using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalManager.Models
{
    public class Doa
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Description { get; set; }

        public bool Publish { get; set; }

        public ICollection<DoaText> DoaTexts { get; set; }

    }
}