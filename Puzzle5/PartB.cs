using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle5
{
    internal static class PartB
    {
        public static void Run()
        {
            Console.WriteLine("part b");
            var seedFactory = new SeedFactory2("seeds.txt");
            var seedSoilMapper = new Mapper("seedsoil.txt");
            var soilFertilizerMapper = new Mapper("soilfertilizer.txt");
            var fertilizerWaterMapper = new Mapper("fertilizerwater.txt");
            var waterLightMapper = new Mapper("waterlight.txt");
            var lightTemperatureMapper = new Mapper("lighttemperature.txt");
            var temperatureHumidityMapper = new Mapper("temperaturehumidity.txt");
            var humidityLocation = new Mapper("humiditylocation.txt");

            long lowest = 0;

            bool hasLocation = false;

            while (hasLocation == false)
            {
                var l = new LocationInfo(lowest);
                l.Humidity = humidityLocation.GetSource(l.Location);
                l.Temperature = temperatureHumidityMapper.GetSource(l.Humidity);
                l.Light = lightTemperatureMapper.GetSource(l.Temperature);
                l.Water = waterLightMapper.GetSource(l.Light);
                l.Fertilizer = fertilizerWaterMapper.GetSource(l.Water);
                l.Soil = soilFertilizerMapper.GetSource(l.Fertilizer);
                l.Seed = seedSoilMapper.GetSource(l.Soil);

                //Console.WriteLine(
                //    $"Seed {l.Seed}, soil {l.Soil}, fertilizer {l.Fertilizer}, water {l.Water}, light {l.Light}, temperature {l.Temperature}, humidity {l.Humidity}, location {l.Location}.");


                if (seedFactory.HasSeed(l.Seed)) hasLocation = true;
                else
                {
                    lowest++;
                }

            }

            Console.WriteLine($"Lowest Location : {lowest}");

        }
    }
}
