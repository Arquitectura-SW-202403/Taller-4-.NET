using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Grpc;
using Presentation.Proto;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MyApp.Namespace
{
    public class SpacesModel : PageModel
    {
        private readonly ILogger<SpacesModel> _logger;
        private readonly GrpcBroker _broker;

        // Constructor con inyección de dependencias para el logger y el broker de gRPC
        public SpacesModel(ILogger<SpacesModel> logger)
        {
            _logger = logger;
            _broker = new GrpcBroker("https://localhost:7125");
        }

        
        public SpaceList Spaces { get; set; }

        // Método que carga la lista de espacios 
        public async Task OnGetAsync()
        {
            try
            {
                Spaces = await _broker.GetSpaceList(); // Obtener la lista de espacios
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los espacios.");
            }
        }

        // Método para eliminar un espacio
        public async Task<IActionResult> OnPostDeleteSpaceAsync(long id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("El ID del espacio es inválido.");
                return Page();
            }

            try
            {
                // Llamar al método DeleteSpace del broker de gRPC
                await _broker.DeleteSpace(new SpaceId { Id = (int)id });

                _logger.LogInformation("Espacio eliminado con éxito.");
                return RedirectToPage(); // Redirigir a la misma página para refrescar la lista
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el espacio.");
                return Page(); // Regresar a la página actual en caso de error
            }
        }

        // Método para crear un nuevo espacio
        public async Task<IActionResult> OnPostCreateSpaceAsync(string spaceName)
        {
            if (string.IsNullOrWhiteSpace(spaceName))
            {
                _logger.LogWarning("El nombre del espacio no puede estar vacío.");
                return Page();
            }

            try
            {
                var newSpace = new FormSpace { Name = spaceName };

                // Llamar al método CreateSpace del broker de gRPC
                await _broker.CreateSpace(newSpace);

                _logger.LogInformation("Espacio creado con éxito.");
                return RedirectToPage(); // Redirigir a la misma página para refrescar la lista de espacios
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el espacio.");
                return Page(); // Regresar a la página actual en caso de error
            }
        }
    }
}
