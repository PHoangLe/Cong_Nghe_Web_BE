using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.ReservationDTO;
using Microsoft.Extensions.Logging;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Exceptions;

namespace Services
{
    public class ReservationService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ReservationService> _logger;
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IMapper mapper, ILogger<ReservationService> logger, IReservationRepository reservationRepository)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._reservationRepository = reservationRepository;
        }

        public async Task createReservation(CreateReservationDto dataInvo)
        {
            try
            {
                Reservation reservationCreate = new Reservation
                {
                    CustomerName = dataInvo.CustomerName,
                    CustomerPhone = dataInvo.CustomerPhone,
                    Note = dataInvo.Note,
                    ReservationTime = DateTime.Parse(dataInvo.ReservationTime.ToString()),
                    NumberOfCustomer = dataInvo.NumberOfCustomer ?? 1,
                    CreatedBy = dataInvo.CreatedBy,
                    Status = dataInvo.Status
                };
                // insert Reservations
                await this._reservationRepository.InsertReservation(reservationCreate);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Reservation>> getAllReservation()
        {
            try
            {
                List<Reservation> reservations = await this._reservationRepository.FindAllReservation();
                return reservations;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<Reservation> getAnReservation(int reservationId)
        {
            try
            {
                Reservation reservationResult = await this._reservationRepository.FindReservationById(reservationId);
                return reservationResult;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<Reservation> updateReservation(int reservationId, UpdateReservationDto reservationData)
        {
            try
            {
                Reservation dataExist = await this._reservationRepository.FindReservationById(reservationId);
                if (dataExist == null)
                {
                    throw new NotFoundException($"Not found Reservation with Id = {reservationId}");
                }

                dataExist.CustomerName = reservationData?.CustomerName ?? dataExist.CustomerName;
                dataExist.CustomerPhone = reservationData?.CustomerPhone ?? dataExist.CustomerPhone;
                dataExist.NumberOfCustomer = reservationData?.NumberOfCustomer ?? (int)dataExist.NumberOfCustomer;
                dataExist.Note = reservationData?.Note ?? dataExist.Note;
                dataExist.Status = reservationData?.Status ?? dataExist.Status;
                dataExist.UpdatedAt = DateTime.Now;
                if(reservationData?.ReservationTime != null)
                {
                    dataExist.ReservationTime = DateTime.Parse(reservationData.ReservationTime.ToString());
                }

                await this._reservationRepository.UpdateReservationById(reservationId, dataExist);
                return dataExist;
            }

            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch
            (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteReservation(int reservationId)
        {
            try
            {
                Reservation dataExist = await this._reservationRepository.FindReservationById(reservationId);
                if (dataExist == null)
                {
                    throw new NotFoundException($"Not found Reservation with Id = {reservationId}");
                }

                await this._reservationRepository.DeleteReservationById(dataExist);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch
            (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
