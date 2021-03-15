﻿using System;

namespace AluguelIdeal.Api.Entities
{
    public class Advertisement : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
