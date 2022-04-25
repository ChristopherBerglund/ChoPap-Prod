using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Features.Selenium
{
    public class Coordinates
    {
        public string Name { get; set; }
        public int xpos { get; set; }
        public int ypos { get; set; }
        //public static void WriteCoordsToJson()
        //{
        //    List<Coordinates> CoordsList = new List<Coordinates>();
        //    var Coords = new Coordinates[]
        //    {
        //        new Coordinates { Name = "cord1", xpos = 1840, ypos = 130 },
        //        new Coordinates { Name = "cord2", xpos = 1840, ypos = 210 },
        //        new Coordinates { Name = "cord3", xpos = 1840, ypos = 320 },
        //        new Coordinates { Name = "cord4", xpos = 1840, ypos = 400 },
        //        new Coordinates { Name = "cord5", xpos = 1840, ypos = 300 },
        //        new Coordinates { Name = "cord6", xpos = 1400, ypos = 980 },
        //        new Coordinates { Name = "cord7", xpos = 1000, ypos = 300 },
        //        new Coordinates { Name = "cord8", xpos = 1100, ypos = 700 },
        //    };
        //    CoordsList.AddRange(Coords);
        //    ExportJson(CoordsList);
        //}
        //public static void ExportJson(List<Coordinates> coord)
        //{
        //    string fullpath = @"C:\myCode\ChoPap-Prod\ChoPap\Features\Selenium\Coords.json";
        //    string json = JsonConvert.SerializeObject(coord);
        //    File.WriteAllText(fullpath, json);
        //}

        //public static List<Coordinates> ImportJson()
        //{
        //    string fullpath = @"C:\myCode\ChoPap-Prod\ChoPap\Features\Selenium\Coords.json";
        //    string json = File.ReadAllText(fullpath);
        //    var Coords = JsonConvert.DeserializeObject<List<Coordinates>>(json);
        //    return Coords;
        //}
    }
}

