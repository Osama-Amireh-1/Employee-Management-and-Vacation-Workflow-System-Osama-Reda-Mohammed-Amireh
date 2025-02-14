using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Entities
{
    public class RequestStates
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}
