﻿namespace Domain.Commission.Models.Commission;

public class Commission
{
    public Guid Id { get; set; }
    public Company.Models.Company Company { get; set; }
    public Client.Models.Client Client { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set;}
}