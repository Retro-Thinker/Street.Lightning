﻿using Street.Lightning.Domain.Common;

namespace Street.Lightning.Domain;

public class Country : BaseEntity
{
    public string CountryName { get; set; } = string.Empty;
    public IEnumerable<City> Cities { get; set; }
}