using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using Presentation.Grpc;
using Presentation.Proto;

namespace MyApp.Namespace
{
    public class ReservationsModel : PageModel
    {

        private readonly ILogger<SpacesModel> _logger;
        private readonly GrpcBroker _broker;
        // Propiedades para el modelo de la página
        public ZonesList Zones { get; set; }

        public int SelectedZoneId { get; set; }

        [BindProperty]
        public int SelectedSpaceId { get; set; }
        
        [BindProperty]
        public string SelectedTimeSlot { get; set; }

        [BindProperty]
        public string CustomerName { get; set; }

        [BindProperty]
        public DateTime ReservationDate { get; set; }

        public Dictionary<long, List<Presentation.Proto.Space>> spaces;

        public List<Presentation.Proto.Space> selectedSpaces {get; set;}

        public List<Occupance> occupancyList {get; set;} = new List<Occupance>();

        public ReservationsModel()
        {
            
            _broker = new GrpcBroker("https://localhost:7125");
            spaces = new Dictionary<long, List<Presentation.Proto.Space>>();
            selectedSpaces = new List<Presentation.Proto.Space>();
            // Cargar zonas ficticias
        }

        public async Task OnGet() 
        {
            Zones = await _broker.GetZonesList();
            var spacesList = (await _broker.GetSpaceList()).Results;
            foreach (var s in spacesList)
            {
                if (!spaces.ContainsKey(s.ZoneId)) spaces.Add(s.ZoneId, new List<Presentation.Proto.Space>());
                spaces[s.ZoneId].Add(s);
            }
            if (!spaces.ContainsKey(SelectedZoneId)) spaces.Add(SelectedZoneId, new List<Presentation.Proto.Space>());
            selectedSpaces = spaces[SelectedZoneId];
        }

        public object ChangeSelectedSpaces(long zoneId) {
            selectedSpaces = spaces[zoneId];
            return new {};
        }

        public async Task<string> GetOccupancy(long space_id) 
        {
            Console.WriteLine(SelectedSpaceId);
            var t = (await _broker.GetOccupancyList(
                new OccupancyQuery {
                    SpaceId=space_id,
                    Start=6,
                    End=20
                }
            )).Result;

            occupancyList = [..t];

            //occupancyList = occupancyResult.Result; 

            var availableTimeSlots = GetAvailableTimeSlots(occupancyList);

            Console.WriteLine(availableTimeSlots.ToString());

            return "";
        }

        /*

        

        */

        // Método OnPost que se llama cuando el formulario se envía
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Si hay errores de validación, se vuelve a mostrar la página con los datos ingresados
                return Page();
            }

            // Aquí procesarías la reserva (guardarla en base de datos, enviar confirmación, etc.)
            // Puedes usar los valores de SelectedZoneId, SelectedSpaceId, SelectedTimeSlot, CustomerName, ReservationDate

            //  Mostrar mensaje de Reserva de Confirmación
            return RedirectToPage("Confirmation", new { name = CustomerName, zoneId = SelectedZoneId, spaceId = SelectedSpaceId });
        }

        private List<TimeSlot> GetAvailableTimeSlots(List<Occupance> occupancyList)
        {
            var availableTimeSlots = new List<TimeSlot>();

            foreach (var occ in occupancyList)
            {
                if (occ.Status == "available") //sI esta disponible en u horaras o franjas dispónibles, mostrar y establecer
                {
                    availableTimeSlots.Add(new TimeSlot 
                    {
                        
                        start = occ.StartTime,
                        end= occ.EndTime

                    });
                }
            }

            return availableTimeSlots;
        }
    }

    // Modelo de Zona (Simulación)
    public class Zone
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // Modelo de Espacio Deportivo (Simulación)
    public class Space
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TimeSlot{
        public int Id {get; set;}
        public int start{get;set;}
        public int end{get;set;}


    }
}
