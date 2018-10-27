using System.ComponentModel.DataAnnotations;

namespace PersonalManager.Models
{
    public class DoaText
    {
        public int Id { get; set; }

        public string Arabic { get; set; }

        public string MalayTranslation { get; set; }

        [Required]
        public int DoaId { get; set; }

        public Doa Doa { get; set; }
    }
}