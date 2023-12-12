using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle5
{
    internal static class PartA
    {
        public static void Run()
        {
            Console.WriteLine("part a");
            var seedFactory = new SeedFactory("seeds.txt");
            var seedSoilMapper = new Mapper("seedsoil.txt");
            var soilFertilizerMapper = new Mapper("soilfertilizer.txt");
            var fertilizerWaterMapper = new Mapper("fertilizerwater.txt");
            var waterLightMapper = new Mapper("waterlight.txt");
            var lightTemperatureMapper = new Mapper("lighttemperature.txt");
            var temperatureHumidityMapper = new Mapper("temperaturehumidity.txt");
            var humidityLocation = new Mapper("humiditylocation.txt");


            foreach (var seed in seedFactory.Seeds)
            {
                seed.Soil = seedSoilMapper.GetDestination(seed.Seed);
                seed.Fertilizer = soilFertilizerMapper.GetDestination(seed.Soil);
                seed.Water = fertilizerWaterMapper.GetDestination(seed.Fertilizer);
                seed.Light = waterLightMapper.GetDestination(seed.Water);
                seed.Temperature = lightTemperatureMapper.GetDestination(seed.Light);
                seed.Humidity = temperatureHumidityMapper.GetDestination(seed.Temperature);
                seed.Location = humidityLocation.GetDestination(seed.Humidity);

                Console.WriteLine(
                    $"Seed {seed.Seed}, soil {seed.Soil}, fertilizer {seed.Fertilizer}, water {seed.Water}, light {seed.Light}, temperature {seed.Temperature}, humidity {seed.Humidity}, location {seed.Location}.");
            }

            Console.WriteLine($"Lowest Location : {seedFactory.Seeds.Min(x => x.Location)}");

        }
    }
}
