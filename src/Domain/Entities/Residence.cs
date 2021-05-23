﻿using System;

namespace AluguelIdeal.Domain.Entities
{
    public class Residence
    {
        public Guid Id { get; set; }
        public Guid DistrictId { get; set; }
        public string Street { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double Area { get; set; }
        public double Rent { get; set; }
        public double Tax { get; set; }
        public string Description { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual double GetFinalPrice() =>
            Rent + Tax;
    }
}
