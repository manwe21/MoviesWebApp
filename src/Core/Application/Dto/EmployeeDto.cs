﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
    }
}
