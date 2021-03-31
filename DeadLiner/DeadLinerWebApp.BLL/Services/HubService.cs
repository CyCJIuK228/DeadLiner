using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.DAL.Interfaces;
using DeadLinerWebApp.DAL.Models;
using DeadLinerWebApp.PL.Models;

namespace DeadLinerWebApp.BLL.Services
{
    public class HubService : IHubService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HubService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public HubsViewModel GetHubs()
        {
            var hubs = _unitOfWork.Hubs.GetAll();
            var hubsList = _mapper.Map<List<HubModel>>(hubs.ToList());
            return new HubsViewModel { Hubs = hubsList };
        }
    }
}