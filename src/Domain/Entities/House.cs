using System;

namespace AluguelIdeal.Domain.Entities
{
    public class House : Residence
    {
        public double YardArea { get; set; }
        public override object AsTableRow() =>
            new { id = Id, yard_area = YardArea };
    }
}
