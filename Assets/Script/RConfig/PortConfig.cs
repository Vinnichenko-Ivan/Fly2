using System;

public class PortConfig
{
    public String button1;
    public int part1;
    public int port1;
    public int part2;
    public int port2;

    public PortConfig(int part1, int port1, int part2, int port2)
    {
        this.part1 = part1;
        this.port1 = port1;
        this.part2 = part2;
        this.port2 = port2;
    }

    public PortConfig(string button1, int part1, int port1, int part2, int port2)
    {
        this.button1 = button1;
        this.part1 = part1;
        this.port1 = port1;
        this.part2 = part2;
        this.port2 = port2;
    }
}