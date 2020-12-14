using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Currency
    {
        public string Pair { get; set; }
        public decimal Value { get; set; }
    }
}
