﻿namespace Puzzle5;

public class LocationInfo(long l)
{
    public long Seed { get; set; } 
    public long Soil { get; set; }
    public long Fertilizer { get; set; }
    public long Water { get; set; }
    public long  Light { get; set; }
    public long Temperature  { get; set; }
    public long Humidity { get; set; }
    public long Location { get; set; } = l;
}