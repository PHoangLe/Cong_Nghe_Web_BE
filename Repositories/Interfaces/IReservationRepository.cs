using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.ReservationDTO;

namespace Repositories.Interfaces
{
    public interface IReservationRepository
    {
        public Task InsertReservation(Reservation reservation);
        public Task<List<Reservation>> FindAllReservation();
        public Task<Reservation> FindReservationById(int reservationId);
        public Task UpdateReservationById(int reservationId, Reservation ReservationUpdate);
        public Task DeleteReservationById(Reservation reservationDelete);
    }
}
