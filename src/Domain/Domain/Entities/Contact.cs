﻿using System;

namespace AluguelIdeal.Domain.Entities
{
    public class Contact 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DeletedAt { get; set; }

        public override string ToString() =>
            $"Id: {Id}, Name: \"{Name}\", Email: \"{Email}\", Phone: \"{Phone}\", DeletedAt: {DeletedAt?.ToString() ?? "null"}";

        public override bool Equals(object obj) =>
            obj is Contact dto &&
            Id == dto.Id &&
            Equals(Name, dto.Name) &&
            Equals(Email, dto.Email) &&
            Equals(Phone, dto.Phone);

        public bool EqualsIgnoreId(object obj) =>
            obj is Contact dto &&
            Equals(Name, dto.Name) &&
            Equals(Email, dto.Email) &&
            Equals(Phone, dto.Phone);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name, Email, Phone, DeletedAt);
    }
}
