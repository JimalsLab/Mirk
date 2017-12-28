using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mirk.ViewModels
{
    public class FeedbackViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}