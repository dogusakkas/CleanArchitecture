﻿using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IMailService
    {
        Task SendMailAsync(SendMailDto sendMailDto);
    }
}
