using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace MyApp.Namespace
{
    public class ReservationsModel : PageModel
    {
        // Propiedades para el modelo de la página
        public List<Space> Spaces { get; set; }

        [BindProperty]
        public int SelectedSpaceId { get; set; }
        
        [BindProperty]
        public string SelectedTimeSlot { get; set; }

        [BindProperty]
        public string CustomerName { get; set; }

        [BindProperty]
        public DateTime ReservationDate { get; set; }

        // Constructor para inicializar los datos (simulado)
        public ReservationsModel()
        {
            // Cargar datos ficticios de espacios deportivos
            Spaces = new List<Space>
            {
                new Space { Id = 1, Name = "Cancha de Fútbol" },
                new Space { Id = 2, Name = "Cancha de Tenis" },
                new Space { Id = 3, Name = "Cancha de Baloncesto" }
            };
        }

        // Método para obtener las franjas horarias en función del espacio seleccionado (solicitud AJAX)
        public IActionResult OnGetGetTimeSlots(int spaceId)
        {
            List<string> timeSlots = new List<string>();

            // Lógica para devolver las franjas horarias según el espacio seleccionado
            if (spaceId == 1) // Cancha de Fútbol
            {
                timeSlots = new List<string> { "8:00 AM - 10:00 AM", "10:00 AM - 12:00 PM", "12:00 PM - 2:00 PM" };
            }
            else if (spaceId == 2) // Cancha de Tenis
            {
                timeSlots = new List<string> { "9:00 AM - 11:00 AM", "11:00 AM - 1:00 PM", "1:00 PM - 3:00 PM" };
            }
            else if (spaceId == 3) // Cancha de Baloncesto
            {
                timeSlots = new List<string> { "8:00 AM - 10:00 AM", "10:00 AM - 12:00 PM", "12:00 PM - 2:00 PM" };
            }

            // Retornar las franjas horarias como JSON
            return new JsonResult(timeSlots);
        }
    }

    // Modelo de Espacio Deportivo (Simulación)
    public class Space
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
