﻿namespace Definition.Contract.Dto;

[Serializable]
public class CountryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Iso3 { get; set; }
    public string Iso2 { get; set; }
    public string PhoneCode { get; set; }
    public string Capital { get; set; }
    public string Currency { get; set; }
    public string Native { get; set; }
}