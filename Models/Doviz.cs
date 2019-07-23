using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DovizApp.Models
{
    public class Doviz
    {
        public int Id { get; set; }
        public float Buy { get; set; }
        public float Sell { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}
