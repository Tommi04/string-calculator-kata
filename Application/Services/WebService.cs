using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using string_calculator_kata.Application.Interfaces;

namespace string_calculator_kata.Application.Services
{
    public class WebService : IWebservice
    {
        public void NotifyLoggingFailure(string message) { }
    }
}