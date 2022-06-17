﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Exceptions
{
    public class ValidationConflictException : Exception
    {
        public ValidationConflictException(string message) : base(message)
        {
        }
    }
}
