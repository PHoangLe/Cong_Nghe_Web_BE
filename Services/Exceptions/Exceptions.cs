﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base($"{message}") { }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base($"{message}") { }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base($"{message}") { }
    }
}
