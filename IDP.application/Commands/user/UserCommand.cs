using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.application.Commands.user
{
    public class UserCommand:IRequest<bool>
    {
        [Required(ErrorMessage ="نام الزامی است ")]
        [MinLength(4)]
        public required string FullName { get; set; }
        public required string NumberCode { get; set; }
    }
}
