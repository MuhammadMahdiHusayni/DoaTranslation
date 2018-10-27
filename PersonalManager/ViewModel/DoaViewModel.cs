using PersonalManager.Controllers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace PersonalManager.ViewModel
{
    public class DoaViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Heading { get; set; }

        public string Description { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<DoaController, ActionResult>> update =
                    (c => c.UpdateDoa(this));

                Expression<Func<DoaController, ActionResult>> create =
                    (c => c.CreateDoa(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}