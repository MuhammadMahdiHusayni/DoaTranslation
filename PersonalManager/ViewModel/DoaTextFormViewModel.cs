using System.Collections.Generic;

namespace PersonalManager.ViewModel
{
    public class DoaTextFormViewModel
    {
        public int DoaId { get; set; }

        public string DoaTitle { get; set; }

        public List<DoaTextViewModel> DoaTexts { get; set; }

    }
}