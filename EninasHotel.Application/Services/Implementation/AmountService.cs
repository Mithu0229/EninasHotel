using EninasHotel.Application.Common.Interfaces;
using EninasHotel.Application.Common.Utility;
using EninasHotel.Application.Services.Interface;
using EninasHotel.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EninasHotel.Application.Services.Implementation
{
    public class AmountService : IAmountService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AmountService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public void CreateAmount(Amount amount)
        {
            

            _unitOfWork.Amount.Add(amount);
            _unitOfWork.Save();
        }

        public bool DeleteAmount(Guid id)
        {
            try
            {
                Amount? objFromDb = _unitOfWork.Amount.Get(u => u.Id == id);
                if (objFromDb is not null)
                {
                    
                    _unitOfWork.Amount.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Amount> GetAllAmounts()
        {
            return _unitOfWork.Amount.GetAll();
        }

        public Amount GetAmountById(Guid id)
        {
            return _unitOfWork.Amount.Get(u => u.Id == id);
        }

        public void UpdateAmount(Amount amount)
        {
           

            _unitOfWork.Amount.Update(amount);
            _unitOfWork.Save();
        }
    }
}
