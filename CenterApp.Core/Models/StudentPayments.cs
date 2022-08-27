﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterApp.Core.Models
{
    public  class StudentPayments
    {
        public int Id { get; set; }
        public int Student_Id { get; set; }
        public Student Student { get; set; }
        public int Group_Id { get; set; }
        public Group Group { get; set; }
        public double Price { get; set; }
        public bool IsPaid { get; set; }
        public DateTime Date { get; set; }
        
    }
}
