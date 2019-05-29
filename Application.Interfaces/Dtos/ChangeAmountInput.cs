using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Dtos
{
    public class ChangeAmountInput
    {
        public Guid Id { get; set; }
        public bool Plus { get; set; }
        public decimal Amount { get; set; }
    }
}
