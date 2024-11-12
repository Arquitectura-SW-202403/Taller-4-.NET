using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
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

        // Método OnPost que se llama cuando el formulario se envía
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Si hay errores de validación, se vuelve a mostrar la página con los datos ingresados
                return Page();
            }

            // Aquí procesarías la reserva (guardarla en base de datos, enviar confirmación, etc.)
            // Puedes usar los valores de SelectedSpaceId, SelectedTimeSlot, CustomerName, ReservationDate

            // Redirigir a una página de confirmación o mostrar mensaje
            return RedirectToPage("Confirmation", new { name = CustomerName, spaceId = SelectedSpaceId });
        }
    }

    // Modelo de Espacio Deportivo (Simulación)
    public class Space
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
