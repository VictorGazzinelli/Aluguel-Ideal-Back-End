using System;

namespace AluguelIdeal.Domain.Entities
{
    public class Flat : Residence
    {
        public double Condominium { get; set; }
        public int Floor { get; set; }

        public override double GetFinalPrice()
        {
            return base.GetFinalPrice() + Condominium;
        }

        public override object AsTableRow() =>
            new { id = Id, condominium = Condominium, floor = Floor };
    }
}
