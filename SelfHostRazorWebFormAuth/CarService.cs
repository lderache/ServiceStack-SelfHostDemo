using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostRazorWebFormAuth
{
    // Basic car class
    public class Car
    {
        public string Plate { get; set; }
    }

    // Request DTO
    [Route("/Cars", "GET")]
    public class CarRequest
    {
    }

    // Response DTO
    public class CarResponse
    {
        public List<Car> Cars { get; set; }
    }

    [Authenticate]
    [DefaultView("Cars")]
    public class CarService : Service
    {
        List<Car> CarsResult = new List<Car>
        {
            new Car { Plate = "FG98745" },
            new Car { Plate = "VN236PL" }
        };

        public object Get(CarRequest request)
        {
            return new CarResponse { Cars = CarsResult };
        }
    }
}
