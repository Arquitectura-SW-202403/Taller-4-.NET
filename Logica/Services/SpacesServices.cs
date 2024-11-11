using Grpc.Core;
using Logica;

namespace Logica.Services
{
    public class SpaceServiceImpl : SpaceService.SpaceServiceBase
    {

        //private readonly SpaceService _spaceService;
        //private readonly ReservationService _reservationService;

        public SpaceServiceImpl(SpaceService spaceService, ReservationService reservationService)
        {
            _spaceService = spaceService;
            _reservationService = reservationService;
        }

        // Obtener todos los espacios
        public override async Task<SpaceList> GetSpaces(Empty request, ServerCallContext context)
        {
            var spaces = await _spaceService.GetSpacesAsync();
            var spaceList = new SpaceList();
            foreach (var space in spaces)
            {
                spaceList.Spaces.Add(new Space
                {
                    Id = space.Id,
                    Name = space.name,
                    Location = space.Location,
                    Available = space.Available
                });
            }
            return spaceList;
        }

        // Obtener un espacio por ID
        public override async Task<Space> GetSpace(SpaceId request, ServerCallContext context)
        {
            var space = await _spaceService.GetSpaceByIdAsync(request.Id);
            if (space == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Space not found"));
            }

            return new Space
            {
                Id = space.Id,
                Name = space.Name,
                Location = space.Location,
                Available = space.Available
            };
        }

        // Crear un nuevo espacio
        public override async Task<Space> CreateSpace(Space request, ServerCallContext context)
        {
            var space = new Entidades.Space
            {
                name = request.Name,
                Available = request.Available
            };

            var createdSpace = await _spaceService.CreateSpaceAsync(space);

            return new Space
            {
                Id = createdSpace.Id,
                Name = createdSpace.Name,
                Location = createdSpace.Location,
                Available = createdSpace.Available
            };
        }

        // Actualizar un espacio existente
        public override async Task<Space> UpdateSpace(Space request, ServerCallContext context)
        {
            var space = new Entidades.Space
            {
                Id = request.Id,
                Name = request.Name,
                Location = request.Location,
                Available = request.Available
            };

            var updatedSpace = await _spaceService.UpdateSpaceAsync(request.Id, space);

            if (updatedSpace == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Space not found"));
            }

            return new Space
            {
                Id = updatedSpace.Id,
                Name = updatedSpace.Name,
                Location = updatedSpace.Location,
                Available = updatedSpace.Available
            };
        }

        // Eliminar un espacio
        public override async Task<Empty> DeleteSpace(SpaceId request, ServerCallContext context)
        {
            var success = await _spaceService.DeleteSpaceAsync(request.Id);
            if (!success)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Space not found"));
            }
            return new Empty();
        }

        // Reservar un espacio
        public override async Task<Reservation> ReserveSpace(ReservationRequest request, ServerCallContext context)
        {
            var reservation = new Entidades.Reservation
            {
                SpaceId = request.SpaceId,
                CustomerName = request.CustomerName,
                ReservationTime = request.ReservationTime,
                Status = "confirmed"
            };

            var createdReservation = await _reservationService.CreateReservationAsync(reservation);

            return new Reservation
            {
                Id = createdReservation.Id,
                SpaceId = createdReservation.SpaceId,
                CustomerName = createdReservation.CustomerName,
                ReservationTime = createdReservation.ReservationTime,
                Status = createdReservation.Status
            };
        }

        // Cambiar una reserva
        public override async Task<Reservation> ChangeReservation(ChangeReservationRequest request, ServerCallContext context)
        {
            var updatedReservation = await _reservationService.ChangeReservationAsync(request.ReservationId, request.NewReservationTime);

            if (updatedReservation == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Reservation not found"));
            }

            return new Reservation
            {
                Id = updatedReservation.Id,
                SpaceId = updatedReservation.SpaceId,
                CustomerName = updatedReservation.CustomerName,
                ReservationTime = updatedReservation.ReservationTime,
                Status = updatedReservation.Status
            };
        }

        // Cancelar una reserva
        public override async Task<Empty> CancelReservation(ReservationId request, ServerCallContext context)
        {
            var success = await _reservationService.CancelReservationAsync(request.Id);
            if (!success)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Reservation not found"));
            }
            return new Empty();
        }

        // Obtener reservas de un espacio
        public override async Task<ReservationList> GetReservationsForSpace(SpaceId request, ServerCallContext context)
        {
            var reservations = await _reservationService.GetReservationsForSpaceAsync(request.Id);
            var reservationList = new ReservationList();

            foreach (var reservation in reservations)
            {
                reservationList.Reservations.Add(new Reservation
                {
                    Id = reservation.Id,
                    SpaceId = reservation.SpaceId,
                    CustomerName = reservation.CustomerName,
                    ReservationTime = reservation.ReservationTime,
                    Status = reservation.Status
                });
            }

            return reservationList;
        }
    }
}
